using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class particleAttractorLinearSC : MonoBehaviour {

    ParticleSystem ps;
    ParticleSystem.Particle[] m_Particles;
    public int NumPoints=3;
    public float PointDistance ;
    private Vector3[] target;
    int numParticlesAlive;


    public float soundLevel;
    public float drag = 0.1f;

    public AudioListener auList;  //Assign in Editor or through Code

    private float volume;

    public int qSamples = 4096;
    private float[] samples;
    private Vector3 pos;

    // Use this for initialization
    void Start()
    {
        target = new Vector3[NumPoints];
        target[0] = (transform.position *transform.lossyScale.x) + new Vector3(Random.Range(PointDistance,-PointDistance), Random.Range(PointDistance, -PointDistance), Random.Range(PointDistance, -PointDistance));
        for ( int p=1; p < target.Length; p++)
        {

            target[p] = target[p - 1] + new Vector3(Random.Range(PointDistance, -PointDistance), Random.Range(PointDistance, -PointDistance), Random.Range(PointDistance, -PointDistance)); ; 

        }
        samples = new float[qSamples];

        ps = GetComponent<ParticleSystem>();
        if (!GetComponent<Transform>()) {
            GetComponent<Transform>();
        }
    }
    void Update() {


        float rawVolume = GetRMS(0) + GetRMS(1);
        rawVolume *= soundLevel;
        rawVolume += 0.02f;
         


        m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
        numParticlesAlive = ps.GetParticles(m_Particles);

        for (int i = 0; i < numParticlesAlive; i++) {
            float life = m_Particles[i].remainingLifetime / m_Particles[i].startLifetime;
            int point = Mathf.FloorToInt( Mathf.Lerp( 0, target.Length-1, 1-life));
            m_Particles[i].velocity += (target[point] - m_Particles[i].position * rawVolume);
            m_Particles[i].velocity *= drag;
        }
        ps.SetParticles(m_Particles, numParticlesAlive);

        //for (int p = 1; p < target.Length; p++)
        //{
        //    Debug.DrawLine(target[p - 1], target[p]);
        //}
    }

    float GetRMS(int channel)
    {

        //Replaced the AudioListener with the public AudioSource auSrc from above
        AudioListener.GetOutputData(samples, channel);

        float sum = 0;
        foreach (float f in samples)
        {
            sum += f * f;
        }
        return sum / qSamples;

    }
}