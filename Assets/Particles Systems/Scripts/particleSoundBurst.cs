using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
//[RequireComponent(typeof(AudioEchoFilter))]

public class particleSoundBurst : MonoBehaviour
{
   // public soundController sound;
    public ParticleSystem particles;
    public float emissionLevel;
    public int burstSize;

    public AudioListener auList;  //Assign in Editor or through Code


    public int qSamples = 4096;
    private float[] samples;

   // public GameObject gObj;

    // Use this for initialization
    void Start()
    {
        samples = new float[qSamples];
    }

    // Update is called once per frame
    void Update()
    {
        float rawVolume = GetRMS(0) + GetRMS(1);
        rawVolume *= emissionLevel;
        int emission = Mathf.FloorToInt( Mathf.Lerp(0, burstSize, rawVolume));
        particles.Emit(emission);
        //Debug.Log(emission + "em");
        //Debug.Log(rawVolume + "raw");

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
