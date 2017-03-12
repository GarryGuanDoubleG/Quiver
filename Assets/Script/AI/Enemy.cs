using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static GameObject player;
    public AIState currState;

    public static IdleState idleState;
    public static AttackState attackState;
    public static PatrolState patrolState;

    public float speed;
    public float base_speed = 2.5f;
    public float accel = 0.5f;

    public Transform[] wayPoints;

    public Coroutine currRoutine;

    private void Awake()
    {
        idleState = new IdleState();
        attackState = new AttackState();
        patrolState = new PatrolState();
    }
    // Use this for initialization
    void Start () {
        if (!player)
            player = GameObject.FindWithTag("test");

        speed = base_speed;
        currState = patrolState;

	}

    public void OnTriggerEnterBody(Collider other)
    {
        currState.HandleEvent(AIEvent.Collision, this, other);
    }

    public void OnTriggerEnterAttackRange(Collider other)
    {
        currState.HandleEvent(AIEvent.EnterRange, this, other);
    }

    public void OnTriggerExitAttackRange(Collider other)
    {
        currState.HandleEvent(AIEvent.ExitRange, this, other);
    }

    // Update is called once per frame
    void Update()
    {
        currState.AiUpdate(this);
    }


}
