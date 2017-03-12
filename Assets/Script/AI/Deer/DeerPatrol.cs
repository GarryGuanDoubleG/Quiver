using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerPatrol : PatrolState
{

    // Use this for initialization
    public virtual void HandleSwitch(AIState newState, Enemy self)
    {
        self.nextState = newState;

        self.moveDelay = Enemy.StateSwitchDelay;
        self.animDelay = Enemy.StateAnimDelay;

    }

    public override void HandleEvent(AIEvent aiEvent, Enemy self)
    {
        if (self.nextState != null)
            return;

        switch (aiEvent)
        {
            case AIEvent.TriggerIdle:
                self.currState = self.idleState;
                break;
            case AIEvent.TriggerAttack:
                self.currState = self.attackState;
                self.currState.HandleSwitch(self);
                break;
        }
    }

    public override void HandleEvent(AIEvent aiEvent, Enemy self, Collider other)
    {

        switch (aiEvent)
        {
            case AIEvent.EnterRange:
                {
                    if (other.tag == "test")
                    {
                        HandleEvent(AIEvent.TriggerAttack, self);
                    }
                    break;
                }
            case AIEvent.Collision:
                {
                    if (self.patrolDelay <= 0)
                    {
                        if (other.tag == "LeftWP")
                        {
                            self.speed = self.base_speed;
                        }
                        else if (other.tag == "RightWP")
                        {
                            self.speed = -self.base_speed;
                        }

                        self.patrolDelay = Enemy.StatePatrolDelay;
                    }
                    break;
                }
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


        if (self.speed == 0)
            self.speed = self.base_speed;

        self.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
        float new_x = self.speed * Time.deltaTime + self.speed * self.accel * Time.deltaTime * Time.deltaTime;
        Vector3 offset = new Vector3(new_x, 0, 0);

        self.transform.position += offset;
    }
}
