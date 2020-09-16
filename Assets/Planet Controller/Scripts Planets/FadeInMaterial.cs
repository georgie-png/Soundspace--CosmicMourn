using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInMaterial : MonoBehaviour
{
    // a script to fade in an object when its Instantiated by controlling paramitters in a shader graph

    [SerializeField] 
    private Renderer fadeRender;

    //public Material fadeMat;
    [SerializeField] 
    private float fadeTime;

    private float age;
   
    void Start()
    {
        // set age to start time
        age = Time.time; 
    }


    void Update()
    {
        // divide the time over  fadeTime
        float fade = (Time.time - age) / fadeTime;
        
        // set the Planet_Alpha attribute with fade
        fadeRender.material.SetFloat("Planet_Alpha", fade);
        
        //if fade is greater that 1.1 deactivate this script
        if (fade > 1.1)
        {
            this.enabled = false;
        }
    }
}
