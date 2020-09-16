using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]

public class OrbitTouchControll : MonoBehaviour
{
    
    // A script to controll the main touch function in the interaction, allowing you to change orbits of selected objects. 


    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private float Drag = 0.1f;

    [SerializeField]
    private float MinOrbit = 1.0f;

    [SerializeField]
    private float MaxOrbit = 7.0f;

    [SerializeField]
    private float DistanceMult = 0.2f;

    [SerializeField]
    private float clampForce = 0.1f;

    private GameObject SelectedObj;

    private LineRenderer parentLine;

    

    void Start()
    {
        //fetching the line render Component
        parentLine = GetComponent<LineRenderer>();

        //this was a key bit of code so I added this in here which stops the screen from going black
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }


    void Update()
    {
        
        //if touched 
        if(Input.touchCount > 0)
        {
            //getting the touch data
            Touch touch = Input.GetTouch(0);

            //if it's the start of the touch and 
            if(touch.phase == TouchPhase.Began &&  SelectedObj == null)
            {
                // create a ray from touch point and create a list of the hits it makes
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit [] hitObject = Physics.RaycastAll(ray);

                // run through the list of hit objects
                for (int i = 0; i < hitObject.Length; i++)
                {
                    
                        // set the SelectedObj the game object from the Raycast Hit
                        SelectedObj = hitObject[i].collider.gameObject;


                        // if it's not a null act on it
                        if (SelectedObj != null)
                        {

                        

                        //Move object
                        ChangeSelectedObject(SelectedObj, touch);
                           
                        }
                    
                }
            }
            else if ( SelectedObj != null)
            {
                // if not a new touch and selected is filled, i.e. an object has been touched then move object.
                ChangeSelectedObject(SelectedObj, touch);
            }
            
        }
        else
        {
            // if there are no touches 
            
            // if SelectedOBJ is something or isn't null
            if (SelectedObj != null)
            {
                //Get orbititing Component  in parent, if it exists tell it its untouched
                var orbiting = SelectedObj.GetComponentInParent<Orbiting>();
                if (orbiting != null)
                {
                    orbiting.Untouched();
                }

                //set selectedOBJ to null and get rid of the line renderer by setting the num of verts to 0
                SelectedObj = null;
                parentLine.positionCount = 0;
            }

            
        }
    }

    void ChangeSelectedObject(GameObject selected, Touch touch)
    {

        //get the parent transform of the obj thats selected
        Transform ParentTran =  selected.transform.parent;

        //if it doesn't have a parent return
        if(ParentTran == null) { return; }

        //get screen positions of selectedobj and parent transforms
        Vector2 SelectScreenPos = arCamera.WorldToScreenPoint(selected.transform.position);
        Vector2 ParentScrnPos = arCamera.WorldToScreenPoint(ParentTran.position);

        // get orbiting controll component in the parent, if it exists tell it it's being touched.
        var orbiting = SelectedObj.GetComponentInParent<Orbiting>();
        if (orbiting != null)
        {
            orbiting.Touched();
        }

        // set up line renderer for two positions from selected to parent
        parentLine.positionCount = 2;
        parentLine.SetPosition(0, selected.transform.position);
        parentLine.SetPosition(1, ParentTran.position);

        // create a scaler with the distance from touch to selected obj
        float TouchScaler = Vector2.Distance(touch.position, SelectScreenPos);

        // if touch is further away from the parent than the selectedobject the revers the scaler
        if (Vector2.Distance(ParentScrnPos, touch.position)>Vector2.Distance(ParentScrnPos,SelectScreenPos)) { TouchScaler *= -1; }

        //turn touchscaler into a normalised and clamped scaler with drag   
        TouchScaler /= Screen.width * DistanceMult;
        TouchScaler *= Drag;
        TouchScaler  = Mathf.Clamp(TouchScaler, -clampForce, clampForce);

        // movedirection is the direction from the parent to the selected obj
        Vector3 MoveDirection =  ParentTran.position - selected.transform.position;

        //normalise it
        MoveDirection = Vector3.Normalize(MoveDirection);

        // scale the move direction 
        MoveDirection *= TouchScaler;

        //distance from parent to new point
        float distance = Vector3.Distance(ParentTran.position, selected.transform.position + MoveDirection);

        // if there is bit of force in touch scaler and the new place is outside of the MinOrbit but inside the MaxOrbit then apply the force/movment
        if ( Mathf.Abs(TouchScaler) > 0.00000000001 && distance> MinOrbit && distance< MaxOrbit) {
            
            selected.transform.position += MoveDirection;
        }

       
    }
    
    }