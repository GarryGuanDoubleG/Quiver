using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AIState currState;

    public static IdleState idleState;
    public static AttackState attackState;
    public static PatrolState patrolState;

    public float speed = 2.5f;
    public float accel = 0.5f;

    public Transform[] wayPoints;

    private void Awake()
    {
        AIState.player = GameObject.FindWithTag("test");

        idleState = new IdleState();
        attackState = new AttackState();
        patrolState = new PatrolState();
    }
    // Use this for initialization
    void Start () {
        currState = patrolState;

        GameObject platform;
	}

    void OnTriggerEnter(Collider other)
    {
        currState.HandleEvent(AIEvent.Collision, this, other);
    }


    // Update is called once per frame
    void Update()
    {
        currState.AiUpdate(this);
    }


}
