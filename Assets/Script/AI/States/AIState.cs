using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIEvent
{
    TriggerAttack,
    TriggerIdle,
    TriggerPatrol,

    Collision
}

public class AIState
{
    public static GameObject player;

    public virtual void OnValidate()
    {
        if(!player)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    public virtual  void HandleEvent(AIEvent aiEvent, Enemy self)
    {

    }

    public virtual void HandleEvent(AIEvent aiEvent, Enemy self, Collider other)
    {

    }

    // Update is called once per frame
    public virtual void AiUpdate (Enemy self) {		

	}
}
