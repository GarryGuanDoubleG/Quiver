using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerEnemy : Enemy
{
    public static DeerAggro deerAlert;
    public static float StateSwitchDelay = 2.5f;    

    private void Awake()
    {
        deerAlert = new DeerAggro();
    }
   
    // Update is called once per frame
    void Start()
    {
        EnemyInit();

        idleState = Enemy.enemyIdleState;
        patrolState = Enemy.enemyPatrolState;
        attackState = DeerEnemy.deerAlert;

        currState = patrolState;
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

    // Update is called once per frame
    public void Update()
    {

        EnemyUpdate();
    }

}
