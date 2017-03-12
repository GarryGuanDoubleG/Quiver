using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerEnemy : Enemy
{
    public static DeerIdle idleState;
    public static DeerPatrol patrolState;
    public static DeerAggro alertState;

	// Use this for initialization
	void Start () {
        currState = idleState;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
