using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAggro : AttackState {


    // Use this for initialization
    public virtual void HandleSwitch(AIState newState, Enemy self)
    {
        self.nextState = newState;

        self.moveDelay = DeerEnemy.StateSwitchDelay;
        self.animDelay = Enemy.StateAnimDelay;

    }

    public override void HandleEvent(AIEvent aiEvent, Enemy self)
    {

        switch (aiEvent)
        {
            case AIEvent.TriggerIdle:
                HandleSwitch(self.idleState, self);
                break;
            case AIEvent.TriggerPatrol:
                HandleSwitch(self.patrolState, self);
                break;
        }

    }

    public override void HandleEvent(AIEvent aiEvent, Enemy self, Collider other)
    {

        Debug.Log("Deer event " + aiEvent.ToString());
        switch (aiEvent)
        {
            case AIEvent.ExitRange:
                {
                    if (other.tag == "test") // player leaves range
                    {
                        HandleEvent(AIEvent.TriggerPatrol, self);
                    }
                }
                break;
            case AIEvent.Collision:
                {
                    if(other.tag == "LeftWP" || other.tag == "RightWP")                    
                        self.speed = 0;
                }
                break;
        }
    }

    // Update is called once per frame
    public override void AiUpdate(Enemy self)
    {
        if (self.nextState != null && self.moveDelay <= 0)
        {
            self.currState = self.nextState;
            self.nextState = null;
        }

        self.GetComponent<Renderer>().material.color = new Color(0.3f, 0.2f, 0.2f);
        //colliding
        if (self.speed == 0)
            return;

        if(Enemy.player.transform.position.x >= self.transform.position.x)
        {
            self.speed = -1.0f * self.base_speed * 1.33f;
        }
        else
        {
            self.speed = self.base_speed * 1.33f;
        }


        float xoffset = self.speed * Time.deltaTime + self.speed * self.accel * Time.deltaTime * Time.deltaTime;
        

        self.transform.position += new Vector3(xoffset, 0, 0);

    }
}
