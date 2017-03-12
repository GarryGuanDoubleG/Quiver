using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static GameObject player;    
    public AIState currState;
    public AIState nextState;

    public static IdleState enemyIdleState;
    public static PatrolState enemyPatrolState;
    public static AttackState enemyAttackState;

    public static float StateSwitchDelay = 0.5f;
    public static float StateAnimDelay = 0.5f;
    public static float StatePatrolDelay = 0.5f; // delay between wall collisions

    public IdleState idleState;    
    public PatrolState patrolState;
    public AttackState attackState;
    
    public float base_speed = 2.5f;
    public float speed = 2.5f;
    public float accel = 0.25f; //multiply this by speed

    public float stateChangeDelay = 0;
    public float moveDelay;
    public float animDelay;
    public float patrolDelay;    

    void Awake()
    {
        enemyIdleState = new IdleState();
        enemyPatrolState = new PatrolState();
        enemyAttackState = new AttackState();
    }
    
    public virtual void EnemyInit()
    {
        if (!player)
            player = GameObject.FindWithTag("test");

        speed = base_speed;
        currState = enemyPatrolState;
    }

    // Use this for initialization
    void Start ()
    {
        EnemyInit();

        idleState = Enemy.enemyIdleState;
        patrolState = Enemy.enemyPatrolState;
        attackState = Enemy.enemyAttackState;
       
    }

    public void OnTriggerEnterWeakspot(Collider other)
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnterBody(Collider other)
    {
        if (other.tag == "PlayerArrow")
            Destroy(other.gameObject);
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

    public void EnemyUpdate()
    {
        stateChangeDelay -= Time.deltaTime;
        moveDelay -= Time.deltaTime;
        animDelay -= Time.deltaTime;
        patrolDelay -= Time.deltaTime;

        currState.AiUpdate(this);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyUpdate();
    }


}
