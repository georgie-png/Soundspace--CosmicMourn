
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ProducePlannet : MonoBehaviour {

    // A Srcipt to produce planets around an AR player using ARRaycastManager to raycast onto trackables

    [SerializeField]
    private GameObject welcomePanel;
    [SerializeField]
    private Camera ArCamera;
    [SerializeField]
    private GameObject[] Gobject;
    [SerializeField]
    private bool stopSpawning = false;
    [SerializeField]
    private float spawnDelay = 15;
    [SerializeField]
    private float scatterRange = 1.5f;
    [SerializeField]
    private float spawnDistance;

    [SerializeField]
    private int minLoops=2, MaxLoops=4;

    private int listPlace = 0;
    private float timer ;

    //private ARPointCloudManager PtCldMngr;
    private ARRaycastManager raycastManager;

    private bool UiClear = true;
	// Use this for initialization
	void Start () {
        
        // pull in ARRaycastManager
        raycastManager = GetComponent<ARRaycastManager>();

        // set timer so it will go off straight away
        timer = spawnDelay;

    }
	
    // function to spawn a game object from specific place on the Gobject list
    public void SpawnObjectList( int place, Vector3 point)
    {
        // set the spawn position by offseting it from the point 
        Vector3 pos = point + new Vector3(Random.Range(-scatterRange, scatterRange), Random.Range(0.5f, 0.5f+scatterRange), Random.Range(-scatterRange, scatterRange));
        
        // set the number of loops
        int loops = Random.Range(minLoops, MaxLoops);

        // find place by moduloing it by the length of Gobject
        place = place % Gobject.Length;

        //instanciat the selected object with the settings and set it to destroy after loops.
        GameObject tmp;
        tmp = Instantiate(Gobject[place], pos, Random.rotation);
        
        Destroy(tmp, spawnDelay * loops);

    }

    // function that finds the shortest distance using a knn and returns its distance as a float.
    public float knn(GameObject[] listObjs, Vector3 point)
    {
        //set distance arbitrarily high
        float distance = 1000;
        //for loop over list of game objects
        foreach (var Objs in listObjs)
        {
            //find distance for object
            float thisDist = Vector3.Distance(Objs.transform.position, point);

            //if lower than current distance
            if ( thisDist < distance)
            {
                // set it as distance
                distance = thisDist;

            }


        }
        // return distance
        return distance;
    }

    void Update()
    {

        // stop here if welcome panel is still active, stoping it from messing with the intro
        if (welcomePanel.activeSelf)
        {
            return;
        }


        
        // if greater than timer
        if (  Time.time + spawnDelay >   timer ) {

            //create a vec2 of center of the screen and then creating a list of ARRaycast hits ready for the raycast mananger
            Vector2 screenCentre =  new Vector2(Screen.width*0.5f, Screen.height*0.5f);
            var hits = new List<ARRaycastHit>();

            // if raycast hits a trackible they are passed into the list 
            if (raycastManager.Raycast(screenCentre , hits, trackableTypes: TrackableType.All)) // trackableTypes:UnityEngine.XR.ARSubsystems.TrackableType.PlaneEstimated))
                    {
                    
                        //Taking the position of the first point on the hits list 
                        Vector3 point = hits[0].pose.position;

                        //Getting a listed of all the spawned game objects by their tag of cosmos
                        GameObject[] sceneobjs =  GameObject.FindGameObjectsWithTag("cosmos");

                        // find the shortest distance the closest one
                        float shortestDist = knn(sceneobjs, point);

                        //if thats greater than specified spawnDistance
                        if (shortestDist > spawnDistance)
                        {
                            //spawn Object
                            SpawnObjectList(listPlace, point);
                            
                            //add a place onto the list
                            listPlace++;
                            
                            // reset the timer 
                            timer = (int)(Time.time + (spawnDelay*2));

                        }
                    
                    }
            
        }


    }

}
