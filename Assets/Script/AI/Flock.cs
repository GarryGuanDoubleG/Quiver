using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public static FlockManager manager;

    public float maxSpeed = 25f;
    public float speed = .05f;
    public float rotationSpeed = 1.5f;

    bool flockToCenter = false;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindWithTag("test").GetComponent<FlockManager>();
        speed = 0.05f;
    }
	
	// Update is called once per frame
	void Update () {

        if (Random.Range(1, 6) == 1)
            ApplyFlock();


        if (speed > maxSpeed)
            speed = maxSpeed;
        transform.Translate(0, 0, Time.deltaTime * speed);
	}

    void ApplyFlock()
    {
        GameObject[] swarm = manager.swarmies;

        Vector3 center = Vector3.zero;
        Vector3 avoid = Vector3.zero; //avoid neighbors

        float neighborHood = 5.0f;
        float neighborDist = 1.0f;

        float groupSpeed = speed;
        float groupSize = 0;

        foreach (GameObject flockie in swarm)
        {
            if(flockie != this)
            {
                float dist = Vector3.Distance(flockie.transform.position, transform.position);

                if (dist <= neighborHood)
                {

                    groupSize++;
                    center += flockie.transform.position;
                    
                    if(dist < neighborDist)
                    {
                        avoid = avoid + (this.transform.position - flockie.transform.position);
                    }

                    float flockspeed = flockie.GetComponent<Flock>().speed;
                    groupSpeed += flockspeed;
                }                
            }
        }


        if(groupSize > 0)
        {
            center = center / groupSize + manager.targetPos.position - transform.position;

            speed = groupSpeed / groupSize;

            Debug.Log("Speed is " + speed);

            Vector3 direction = (center + avoid) - transform.position;

            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);
            }

        }


    }
}
