using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SampleSelect : MonoBehaviour {

    
    [SerializeField]
    private AudioClip[] clipz;
    private AudioSource sauce;
    
    void Start () {
        //get audio sauce
        sauce = GetComponent<AudioSource>();

        // if there are clips play a random one
        if (clipz.Length > 1) {
            int rand = Random.Range(0, clipz.Length);
            sauce.clip = clipz[rand];
        }
        // play the sauce
        sauce.Play();

        

	}
	
	
}
