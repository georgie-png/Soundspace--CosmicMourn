using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
//[RequireComponent(typeof(AudioEchoFilter))]

public class particleSoundmove : MonoBehaviour
{
 
    public float soundLevel;
    public float MoveMaxSize=1, MoveMinSize=0;
    
    public AudioListener auList;  //Assign in Editor or through Code

    private float volume;

    public int qSamples = 4096;
    private float[] samples;
    private Vector3 pos;
   // public GameObject gObj;

    // Use this for initialization
    void Start()
    {
        samples = new float[qSamples];
        pos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float rawVolume = GetRMS(0) + GetRMS(1);

        //float dist = Vector3.Distance(transform.position,  Camera.main.transform.position) ;
        // float distMult = Mathf.Lerp(2,1, Mathf.Clamp( dist,0,auSrc.transform.position) / auSrc.maxDistance) ;
        volume = rawVolume;// * distMult;
       // Debug.Log(volume);
        float Move =  Mathf.Lerp(MoveMinSize, MoveMaxSize, volume);
        Vector3 newPos = new Vector3(pos.x * Move, pos.y * Move, pos.z * Move);
        transform.localPosition = newPos;

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
        return Mathf.Sqrt(sum / qSamples);
       
    }
  


}
