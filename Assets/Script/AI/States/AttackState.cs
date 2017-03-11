using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState {

    // Use this for initialization
    void Start()
    {

    }

    public void HandleEvent(AIEvent aiEvent, Enemy self)
    {
        switch (aiEvent)
        {
            case AIEvent.TriggerIdle:
                self.currState = Enemy.idleState;
                break;
            case AIEvent.TriggerPatrol:
                self.currState = Enemy.patrolState;
                break;
        }
    }

    // Update is called once per frame
    void AiUpdate(Enemy self)
    {
        //idle probably stands still

    }
}
