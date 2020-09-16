using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioEchoFilter))]

public class EchoController : MonoBehaviour {

    // script to control parents delay attribute of echo component

    [SerializeField]
    private GameObject echoOBj;
    [SerializeField]
    private float pointMin =.5f, pointMax=1f;
    [SerializeField]
    private float outMin=0, outMax=10;


    private float LerpF;
    void Start()
    {

        //difference of min and max point distances
         LerpF = pointMax - pointMin;
    }

  
    
    void Update()
    {
        // get the distance from echo contollet to parent planet and clamp it 
        float dist = Mathf.Clamp( Vector3.Distance(echoOBj.transform.localPosition, echoOBj.transform.parent.localPosition),pointMin,pointMax);

        // invert and normalize the distance
        float mod = (pointMax - dist) / LerpF;

        // lerp between the out min and max
        float delay = Mathf.Lerp(outMin, outMax, mod);

        // set delay attribute in the AudioEchoFilter component 
        GetComponent<AudioEchoFilter>().delay = delay;


    }
}
