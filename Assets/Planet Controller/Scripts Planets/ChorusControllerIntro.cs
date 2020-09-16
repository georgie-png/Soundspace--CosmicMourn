using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioChorusFilter))]

public class ChorusControllerIntro : MonoBehaviour {

    // script for intro chorus controller, just on x and y

    [SerializeField]
    private GameObject ChorusObj;
    [SerializeField]
    private float PMax=1f;
    [SerializeField]
    private float OMin=0, OMax=1;
    private Vector3 startPos;

    void Start()
    {

        // record start position
        startPos = ChorusObj.transform.localPosition;
         
    }

  
    void Update()
    {
        // distance from start pos to current pos
        float dist = Mathf.Clamp( Vector3.Distance(ChorusObj.transform.localPosition, startPos),0,PMax);
        // normalize it 
        float mod = dist /  PMax;
        //lerp it
        float chorus = Mathf.Lerp(OMin, OMax, mod);
        // set the chorus depth with value
        GetComponent<AudioChorusFilter>().depth = chorus;

        
    }
}
