using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioEchoFilter))]

public class EchoControllerIntro : MonoBehaviour {

    // script for intro chorus controller, just on x and y

    [SerializeField]
    private GameObject echoOBj;
    [SerializeField]
    private float  PMax=1f;
    [SerializeField]
    private float OMin=0, OMax=10;

    private Vector3 OriginalPos;
    void Start()
    {
        OriginalPos = echoOBj.transform.localPosition;
    }

  
    // Update is called once per frame
    void Update()
    {
        // distance from start pos to current pos
        float dist = Mathf.Clamp(Vector3.Distance(echoOBj.transform.localPosition, OriginalPos), 0, PMax);
        // normalize it 
        float mod = dist / PMax;
        //lerp it
        float delay = Mathf.Lerp(OMin, OMax, mod);
        // set the echo delay  with value
        GetComponent<AudioEchoFilter>().delay = delay;

        
    }
}
