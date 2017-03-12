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
        Debug.Log("trigger body deer");
        Destroy(gameObject);
    }

    public void OnTriggerEnterBody(Collider other)
    {
        Debug.Log("trigger body deer");

        if (other.tag == "PlayerArrow")
            Destroy(other.gameObject);

        currState.HandleEvent(AIEvent.Collision, this, other);
    }

    public void OnTriggerEnterAttackRange(Collider other)
    {
        Debug.Log("trigger body deer");
        currState.HandleEvent(AIEvent.EnterRange, this, other);
    }

    public void OnTriggerExitAttackRange(Collider other)
    {
        Debug.Log("trigger body deer");
        currState.HandleEvent(AIEvent.ExitRange, this, other);
    }

    // Update is called once per frame
    public void Update()
    {
        if (currState == idleState)
            Debug.Log("Deer idle");
        else if (currState == patrolState)
            Debug.Log("Deer patrol");
        else if (currState == deerAlert)
            Debug.Log("Deer alert");

        EnemyUpdate();
    }

}
