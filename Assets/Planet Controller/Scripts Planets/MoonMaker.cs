using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMaker : MonoBehaviour {

    // script to controll the moons/ controllers and sound effects
    [SerializeField]
    private GameObject echoObj, pitchObj, chorusObj;
    [SerializeField]
    private float Chance;
    private int numSlct;

   
    void Start()
    {
        //a counter to check the an effect is added
        numSlct = 0;

            // echo rand
            float erand = Random.Range(0, 10);
            // if greater or = to chance
            if (erand >= Chance)
            {
                //set echo active and enable the audio control scripts in the parent
                echoObj.SetActive(true);
                GetComponentInParent<AudioEchoFilter>().enabled = true;
                GetComponentInParent<EchoController>().enabled = true;
                //add 1 to selected
                numSlct++;
        }
            else 
            {
                // else deactivate controller and audio control scripst
                echoObj.SetActive(false);
                GetComponentInParent<AudioEchoFilter>().enabled = false;
                GetComponentInParent<EchoController>().enabled = false;
        }

        // pitch rande 
        float prand = Random.Range(0, 10);
        // if greater than chance
        if (prand >= Chance)
        {
            //enable controller and audio controll scripts
            pitchObj.SetActive(true);
            GetComponentInParent<PitchController>().enabled = true;
            // add 1 to num selected
            numSlct++;

        }
        else
        {
            //else deactivate object + scripst
            pitchObj.SetActive(false);
            GetComponentInParent<PitchController>().enabled = false;
        }

        //Chorus rand
        float crand = Random.Range(0, 10);
       
        // if greater than chance
        if (crand >= Chance)
        {
            //enable controller and audio controll scripts
            chorusObj.SetActive(true);
            GetComponentInParent<ChorusController>().enabled = true;
            GetComponentInParent<AudioChorusFilter>().enabled = true;
            // add one to num selected
            numSlct++;

        }
        else
        {
            //else deactivate object + scripst
            chorusObj.SetActive(false);
            GetComponentInParent<ChorusController>().enabled = false;
            GetComponentInParent<AudioChorusFilter>().enabled = false;
        }

        // if none where selected 
        if (numSlct == 0)
        {
            //restart
            Start();

        }

    }
}



