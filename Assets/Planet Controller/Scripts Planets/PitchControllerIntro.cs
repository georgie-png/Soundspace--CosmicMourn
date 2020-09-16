using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PitchControllerIntro : MonoBehaviour {

    
    public GameObject PitchOBJ;
    public float PMax = 1f;
    public float OMin = 0, OMax = 10;

    private Vector3 OriginslPos;
    void Start()
    {
        OriginslPos = PitchOBJ.transform.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Clamp(Vector3.Distance(PitchOBJ.transform.localPosition, OriginslPos), 0, PMax);

        float mod = dist / PMax;
        float pitch = Mathf.Lerp(OMin, OMax, mod);

        GetComponent<AudioSource>().pitch = pitch ;


        
    }
}
