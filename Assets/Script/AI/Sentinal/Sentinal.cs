using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentinal : Enemy {

    public static SentinalPatrol sentinalPatrol = new SentinalPatrol();
    public static SentinalIdle sentinalIdle = new SentinalIdle();
    public static float SentinalTurnDelay = 2.0f;

    // Use this for initialization
    void Start()
    {      
        EnemyInit();

        idleState = sentinalIdle;
        patrolState = sentinalPatrol;
        attackState = Enemy.enemyAttackState;

        currState = patrolState;
    }
    // Update is called once per frame
    void Update () {
        EnemyUpdate();
	}
}
