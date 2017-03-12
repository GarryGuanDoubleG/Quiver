using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

    public GameObject[] swarmies;//game objs in swarm
    public Transform targetPos;//center to swarm around

    public float flockRadius = 2.5f;
	// Use this for initialization
	void Start () {
        targetPos = GameObject.FindWithTag("Enemy").transform;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
