using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinalIdle : IdleState {


    public override void HandleEvent(AIEvent aiEvent, Enemy self)
    {
        switch (aiEvent)
        {
            case AIEvent.TriggerAttack:
                self.currState = self.attackState;
                break;
            case AIEvent.TriggerPatrol:
                self.currState = self.patrolState;
                self.stateChangeDelay = Sentinal.StatePatrolDelay;
                break;
        }
    }

    public override void HandleEvent(AIEvent aiEvent, Enemy self, Collider other)
    {
        if(other.tag == "test")
        {
            HandleEvent(AIEvent.TriggerAttack, self);
        }
    }
    // Update is called once per frame
    public override void AiUpdate (Enemy self)
    {
        self.GetComponent<Renderer>().material.color = new Color(0, 0, 0);

        if(self.stateChangeDelay <= 0)
        {
            HandleEvent(AIEvent.TriggerPatrol, self);
        }

	}
}
