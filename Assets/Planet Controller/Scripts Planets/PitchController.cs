using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PitchController : MonoBehaviour {

    // script to control parents pitch attribute of audio source component

    [SerializeField]
    private GameObject PitchOBJ;
    [SerializeField]
    private float pointMin = .5f, pointMax = 1f;
    [SerializeField]
    private float outMin = 0, outMax = 10;
    private float LerpF;
    void Start()
    {
        //difference of min and max point distances
        LerpF = pointMax - pointMin;
    }


    void Update()
    {

        // get the distance from pitch contoller to parent planet and clamp it 
        float dist = Mathf.Clamp(Vector3.Distance(PitchOBJ.transform.position, transform.position), pointMin, pointMax);

        // invert and normalize the distance
        float mod = (pointMax - dist) / LerpF;

        // lerp between the out min and max
        float pitch = Mathf.Lerp(outMin, outMax, mod);

        // set pitch attribute in the audio source component 
        GetComponent<AudioSource>().pitch = pitch ;

        
    }
}
