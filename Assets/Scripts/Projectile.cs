using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //get shell's rigidbody
    public Rigidbody shellPrefabs;
    
    //set cannon's shoot point
    public Transform shootPoint;
    
    //set the red dot sprite as cursor indicator
    public GameObject cursor;
    
    //set the layernmask for raycasting
    public LayerMask layer;

    private Camera cam;
    //starts counting once the player launches a shell
    private float fireTime;

    //set coll down time for launching shells
    private float coolDownTime;

    //boolean flag that it set to false once the player launches a shell
    private bool hasLaunched;

    // Start is called before the first frame update
    void Start()
    {
        //grab the camera with "Main Camera" tag as cam
        cam = Camera.main;
        fireTime = 1.0f;
        coolDownTime = 1.0f;
        hasLaunched = false;
    }

    // Update is called once per frame
    void Update()
    {
        AddTime();
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        //cast a ray from mouse position
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //if the ray gets a positive hit result
        if (Physics.Raycast(mouseRay, out hit, 1000f, layer))
        {
            //show the cursor indicator sprite
            cursor.SetActive(true);
            
            //Bring up cursor indicator sprite a little on z axis in order to make it visible
            cursor.transform.position = hit.point + Vector3.up * 0.2f;
            
            //calculate the initial velocity at launch & uses raycast hit position as destination 
            Vector3 launchVel = CalculateVelocity(hit.point, shootPoint.position, 1.0f);

            transform.rotation = Quaternion.LookRotation(launchVel);

            //if player clicks left mouse button & the launch interval is longer than cool down time
            if (Input.GetMouseButtonDown(0) && (fireTime >= coolDownTime))
            {
                //Instantiate a shell prefab at shoot point
                Rigidbody shelRb = Instantiate(shellPrefabs, shootPoint.position, Quaternion.identity);
                
                //set shell prefab's velocity to launch velocity 
                shelRb.velocity = launchVel;
                
                //reset fire time
                fireTime = 0.0f;

                //set haslaunched to true
                hasLaunched = true;
            }
        }
        else
        {
            //if the ray gets a negative hit result set the cursor indicator sprite as invisible
            cursor.SetActive(false);
        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //Get the lateral distance between origin and target
        Vector3 distance = target - origin;
        //Get the distance between origin and target on x-z plain
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0.0f;
        
        //Get the max height of the target projectile
        float height = distance.y;

        //Get the max range of the target projectile
        float range = distanceXZ.magnitude;

        //initial speed on x axis = range / time
        float flatVel = range / time;

        //intial speed on y axis = height / t + 0.5 * gravity  * time
        float verticalVel = height / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 launchVel = distanceXZ.normalized;
        launchVel *= flatVel;
        launchVel.y = verticalVel;

        return launchVel;
    }

    void AddTime()
    {
        //Starts counting by delta seconds once the player launches a shell
        if (hasLaunched)
        {
            fireTime += Time.deltaTime;
        }
    }
}
