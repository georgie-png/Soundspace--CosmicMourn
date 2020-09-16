using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioChorusFilter))]

public class ChorusController : MonoBehaviour {

    // script to control parents pitch attribute of audio source component

    [SerializeField]
    private GameObject chorusObj;
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

        // get the distance from chorus contoller to parent planet and clamp it 
        float dist = Mathf.Clamp(Vector3.Distance(chorusObj.transform.position, transform.position), pointMin, pointMax);

        // invert and normalize the distance
        float mod = (pointMax - dist) / LerpF;

        // lerp between the out min and max
        float chorus = Mathf.Lerp(outMin, outMax, mod);

        // set depth attribute in the AudioChorusFilter component 
        GetComponent<AudioChorusFilter>().depth = chorus;

        
    }
}
