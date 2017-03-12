using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : Enemy
{
    public SwarmController controller;

    public static float maxSpeed = 5.5f;
    public static float maxForce = 3.0f;

    public Vector3 currVel;

    public float rotationSpeed = 4.0f;
    public float maxFlockSpeed = 10.0f;

    // Use this for initialization
    void Start ()
    {
        EnemyInit();

        speed = 4.0f;

        controller = GetComponentInParent<SwarmController>();
        player = GameObject.FindWithTag("test");

        currState = patrolState;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(!controller.activated)
        {
            EnemyUpdate();
        }
        else
        {
            if(Vector3.Distance(transform.position, player.transform.position) > 10.0f)
            {
                speed = base_speed * 2;
                Steer();
            }
            else
            {
                if (speed >= maxSpeed)
                    speed = maxSpeed;

                ApplyFlock();

                transform.Translate(0, 0, Time.deltaTime * speed);
            }
            
        }
	}

    void Steer()
    {
        Vector3 desiredDir = Vector3.Normalize(player.transform.position - transform.position) * maxSpeed;
        Vector3 steering = desiredDir - currVel;

        currVel = (steering + currVel) * base_speed;

        currVel.z = 0;

        transform.position += currVel * Time.deltaTime;
    }

    Vector3 Align()
    {
        float neighborDist = 4.0f;
        Vector3 sum = Vector3.zero;
        int count = 0;

        foreach(Swarm swarmie in controller.swarm)
        {
            if (swarmie == this)
                continue;
            float dist = Vector3.Distance(transform.position, swarmie.transform.position);

            if(dist < neighborDist)
            {
                sum += swarmie.transform.position;
                count++;
            }
        }

        if(count > 0)
        {
            sum /= count;
            Vector3 steerVec = sum - currVel;
            return steerVec;
        }

        return Vector3.zero;
    }

    void ApplyFlock()
    {
        Swarm[] swarm = controller.swarm;

        Vector3 center = Vector3.zero;
        Vector3 avoid = Vector3.zero; //avoid neighbors

        float neighborHood = 5.0f;
        float neighborDist = 1.0f;

        float groupSpeed = speed;
        float groupSize = 0;

        foreach (Swarm flockie in swarm)
        {
            if (flockie != this)
            {
                float dist = Vector3.Distance(flockie.transform.position, transform.position);

                if (dist <= neighborHood)
                {

                    groupSize++;
                    center += flockie.transform.position;

                    if (dist < neighborDist)
                    {
                        avoid = avoid + (this.transform.position - flockie.transform.position);
                    }
                    
                    groupSpeed += flockie.speed;
                }
            }
        }


        if (groupSize > 0)
        {
            center = center / groupSize + player.transform.position - transform.position;

            speed = groupSpeed / groupSize;

            Debug.Log("Speed is " + speed);

            Vector3 direction = (center + avoid) - transform.position;

            float z = direction.y;
            direction.y = 1;
            //direction.z = z;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);
            }

        }


    }

    public override void OnTriggerEnterAttackRange(Collider other)
    {
        controller.ActivateSwarm();
    }

    public void AttackPlayer()
    {

        GetComponent<Renderer>().material.color = new Color(1f, .2f, 0.5f);
    }
}
