using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilenceEmitter : MonoBehaviour
{
    // public soundController sound;
    private ParticleSystem particles;
    public float emissionLevel;
    public int burstSize;

    public AudioListener auList;  //Assign in Editor or through Code

    private float volume;

    public int qSamples = 4096;
    private float[] samples;

    

    // public GameObject gObj;

    // Use this for initialization
    void Start()
    {
        samples = new float[qSamples];
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float rawVolume = GetRMS(0) + GetRMS(1);

       // rawVolume += emissionLevel;
       // rawVolume = Mathf.Clamp(rawVolume, 0, 1);

        if (rawVolume < emissionLevel)
        {


            particles.Emit(burstSize);
            //Debug.Log(emission + "em");
            //Debug.Log(rawVolume + "raw");
        }
    }

    float GetRMS(int channel)
    {

        AudioListener.GetOutputData(samples, channel);

        float sum = 0;
        foreach (float f in samples)
        {
            sum += f * f;
        }
        return Mathf.Sqrt(sum / qSamples);

    }



}
