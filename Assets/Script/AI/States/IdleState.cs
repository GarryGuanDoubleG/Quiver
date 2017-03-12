using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState {

	public IdleState()
    {

    }
	
    public override void HandleEvent(AIEvent aiEvent, Enemy self)
    {
        switch (aiEvent)
        {
            case AIEvent.TriggerAttack:
                self.currState = self.attackState;
                break;
            case AIEvent.TriggerPatrol:
                self.currState = self.patrolState;
                break;
        }
    }

	// Update is called once per frame
	void AiUpdate(Enemy self) {
		//idle probably stands still
	}
}
