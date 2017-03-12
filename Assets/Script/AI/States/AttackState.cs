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
                self.currState = self.idleState;
                break;
            case AIEvent.TriggerPatrol:                
                self.currState = self.patrolState;
                break;
        }
    }

    public override void HandleEvent(AIEvent aiEvent, Enemy self, Collider other)
    {
        switch(aiEvent)
        {
            case AIEvent.ExitRange:
                {
                    if(other.tag == "test") // player leaves range
                    {
                        HandleEvent(AIEvent.TriggerPatrol, self);
                    }                    
                }
                break;
            case AIEvent.Collision:
                {
                    self.speed = 0;
                }
                break;       
        }
    }

    // Update is called once per frame
    public override void AiUpdate(Enemy self)
    {
        self.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 1.0f);

        //check if same platform
        float speed = Mathf.Abs(self.speed);

        Vector3 player_transorm = new Vector3(Enemy.player.transform.position.x, self.transform.position.y, 0);                            
        self.transform.position = Vector3.MoveTowards(self.transform.position, player_transorm, speed * Time.deltaTime);
    }


}
