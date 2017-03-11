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

    public void HandleEvent(AIEvent aiEvent, Enemy self)
    {
        switch (aiEvent)
        {
            case AIEvent.TriggerAttack:
                self.currState = Enemy.attackState;
                break;
            case AIEvent.TriggerIdle:
                self.currState = Enemy.idleState;
                break;
        }
    }

    public override void HandleEvent(AIEvent aiEvent, Enemy self, Collider other)
    {
        if(other.tag == "Waypoint")
        {
            self.speed *= -1;//reverse directions
        }
    }


    // Update is called once per frame
    public override void AiUpdate(Enemy self)
    {
        float new_x = self.speed * Time.deltaTime + self.accel * Time.deltaTime * Time.deltaTime;
        Vector3 offset = new Vector3(new_x, 0, 0);

        self.transform.position += offset;
    }
}
