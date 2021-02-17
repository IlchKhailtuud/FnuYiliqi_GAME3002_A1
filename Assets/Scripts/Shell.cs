using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shell : MonoBehaviour
{
    //set Shell lifespan
    private float lifeSpan;
    
    // Start is called before the first frame update
    void Start()
    {
        lifeSpan = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //deduct shell lifespan by delta seconds
        lifeSpan -= Time.deltaTime;

        //destroy the shell itself once its lifespan reaches 0
        if (lifeSpan <= 0) 
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }

}
