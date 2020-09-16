using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class Orbiting : MonoBehaviour
{
    // A script to setup and controll orbits. I found the easiest way to deliver this was to plot a 2D circle and rotate it in 3D space.
    // Using the points from the circle plotting as points to lerp the moon/controllers through, creating their orbit.
    [SerializeField]
    private Transform ObjToOrbit;

    [SerializeField]
    private int Divisions = 10;

    [SerializeField]
    private float minDist, maxDist;

    [SerializeField]
    private int seed;

    private LineRenderer orbitCircle;
    private float distance;
    private Vector3[] orbitPoints;
    private bool touchBool = false;
    private float touchDelay;



    void Start()
    {
        //set random seed as I had multiple controllers using the random on different scripts at the same time.
        //As it is auto seeded of the time they landed in the same positions so adding this allowed for variance.
        Random.InitState((int)(Time.time * 5) + seed);

       // geting LineRender Component
        orbitCircle = GetComponent<LineRenderer>();
        
        // rotating the 3D space so they all land in different rotations
        transform.rotation = Random.rotation;
        
        // setting a random distance
        distance = Random.Range(minDist,maxDist);
        
        // creating an orbitLine and get orbitPoints
        orbitPoints = OrbitLine(distance, Divisions);

        // set orbitCircle (renderline) positions from orbitPoints
        orbitCircle.SetPositions(orbitPoints);
    }

    
    void Update()
    {

        
        // if its not touched
        if (touchBool == false)
        {
            // Orbit the controller/moon
            OrbitObject(orbitPoints);

        }
        

    }

    // public function for reseting line orbit when touched
    public void Touched()
    {
        //if first calc on touch record time for touchDelay
        if (touchBool == false) { touchDelay = Time.time - touchDelay; }
        
        // set distance from controller to parent
        distance = Vector3.Distance(transform.localPosition, ObjToOrbit.transform.localPosition);

        distance *= (1 / transform.localScale.x);

        // reset orbitline and orbitPoints
        orbitPoints = OrbitLine(distance, Divisions);
        
        // set orbitCircle (renderline) positions from orbitPoints
        orbitCircle.SetPositions(orbitPoints);
        
        //Set touchBool to touched
        touchBool = true;
    }

    // public function to reset it to normal orbiting after touch
    public void Untouched()
    {
        // if first calc after touch set touchDelay  
        if (touchBool == true) { touchDelay  = Time.time - touchDelay; }

        // set touchBool to untouched
        touchBool = false;


    }
    // function to set render Line and return a vec3[] of the points
    private Vector3 []  OrbitLine(float distance, int divisions)
    {
        // get position of objtoOrbit
        Vector3 pos = ObjToOrbit.position;

       // set orbitCircle LineRenderer to have the num of divisions
        orbitCircle.positionCount = divisions;

        // create a list of vec3 the size of divisions to hold the points
        Vector3[] Points = new Vector3[divisions];

        // set velocity to 0
        Vector3 velocity = new Vector3(0, 0, 0);
        
        
        // for loop num divisions to create the circle
        for (int i = 0; i < divisions; i++)
        {
            
            // normalize the for loops postion over the number of loops and turn it into degrees by * 360
            float division = (float)i / (float)divisions * 360f;

            //convert that to radians to work cos and sin
            division *= Mathf.Deg2Rad;

            //find x and z position in the circle from cos and sin
            float x = distance * Mathf.Cos(division);
            float z = distance * Mathf.Sin(division);

            // creating a vec3 with them
            pos = new Vector3(x, 0, z);

            // converting that into a world position
            pos = transform.localToWorldMatrix * pos;

            // adding these values to Points list 
            Points.SetValue( pos,i);
        }
        // return Points a vec3 list
        return Points;
    }

    // function to orbit or lerp between the points in the list
    private void OrbitObject(Vector3[] orbitPoints)
    {
        //find place by using modulo of divisions on time
        float place = (Time.time - touchDelay) % (float)Divisions;

        // rounding this up and down to find the point before and after the place
        int pointFrom = Mathf.FloorToInt(place);
        int pointTo = Mathf.CeilToInt(place);

        // if not the last division
        if (pointFrom < Divisions-1)
        {
            // get position by lerping from between point before and after by the decimal remainder between 1 and 0 of place - pointFrom (place rounded down)
            ObjToOrbit.localPosition = Vector3.Lerp(orbitPoints[pointFrom], orbitPoints[pointTo], place - pointFrom);
        }
        else
        {
            // if last division
            // get position by lerping from between point before and first division by the decimal remainder between 1 and 0 of place - pointFrom (place rounded down)
            ObjToOrbit.localPosition = Vector3.Lerp(orbitPoints[pointFrom], orbitPoints[0], place - pointFrom);
        }


    }
}
