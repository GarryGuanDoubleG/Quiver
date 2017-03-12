using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : Enemy
{
    public SwarmController controller;

    public static float maxSpeed = 5.5f;
    public static float maxForce = 3.0f;

    public Vector3 currVel;
    public Vector3 currForce;
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
            speed = base_speed * 2;
            Steer();
            //if (Vector3.Distance(transform.position, player.transform.position) > 10.0f)
            //{
            //    speed = base_speed * 2;
            //    Steer();
            //}
            //else
            //{
            //    if (speed >= maxSpeed)
            //        speed = maxSpeed;

            //    ApplyFlock();

            //    transform.Translate(0, 0, Time.deltaTime * speed);
            //}
            
        }
	}

    void Steer()
    {
        Vector3 desiredDir = Vector3.Normalize(player.transform.position - transform.position) * maxSpeed;
        Vector3 steering = desiredDir - currVel;

        currVel = (steering + currVel) * speed;
        if(currVel.magnitude > maxSpeed)
        {
            currVel = currVel.normalized * maxSpeed;
        }
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

    Vector3 Cohesion()
    {
        float neighborDist = 4.0f;
        Vector3 sum = Vector3.zero;
        int count = 0;

        foreach (Swarm swarmie in controller.swarm)
        {
            if (swarmie == this)
                continue;
            float dist = Vector3.Distance(transform.position, swarmie.transform.position);

            if (dist < neighborDist)
            {
                sum += swarmie.transform.position;
                count++;
            }
        }

        if (count > 0)
        {
            sum /= count;
            return (sum - currVel);
        }

        return Vector3.zero;
    }

    void ApplyFlock()
    {
       if(Random.Range(0, 10) <= 1)
        {
            Vector3 vecAligh = Align();
            Vector3 vecCohension = Cohesion();

            Vector3 targetDir = (player.transform.position - transform.position);

            currForce = vecAligh + vecCohension + targetDir;
            currForce = currForce.normalized;
        }

        currForce.z = 0;       
        if(currForce.magnitude > maxForce)
        {
            currForce = currForce.normalized * maxForce;            
        }

        this.GetComponent<Rigidbody>().AddForce(currForce);

        if (this.GetComponent<Rigidbody>().velocity.magnitude > maxSpeed )
        {
            this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity.normalized * maxSpeed;            
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
