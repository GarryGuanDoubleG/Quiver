using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : AIState {

    public PatrolState()
    {

    }
    // Use this for initialization
    void Start()
    {

    }

    public virtual void HandleSwitch(AIState newState, Enemy self) {
        self.currState = newState;

        self.moveDelay = Enemy.StateSwitchDelay;
        self.animDelay = Enemy.StateAnimDelay;
    }

    public override void HandleEvent(AIEvent aiEvent, Enemy self)
    {
        if (self.moveDelay >= 0)
            return; 

        switch (aiEvent)
        {
            case AIEvent.TriggerIdle:
                HandleSwitch(self.idleState, self);
                break;
            case AIEvent.TriggerAttack:
                HandleSwitch(self.attackState, self);
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
                    if(self.patrolDelay <= 0)
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
        if (self.speed == 0)
            self.speed = self.base_speed;

        self.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
        float new_x = self.speed * Time.deltaTime + self.speed * self.accel * Time.deltaTime * Time.deltaTime;
        Vector3 offset = new Vector3(new_x, 0, 0);

        self.transform.position += offset;
    }
}
