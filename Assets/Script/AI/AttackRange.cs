using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {

    public Enemy parent;
	// Use this for initialization
	void Start () {       
        parent = GetComponentInParent<Enemy>();        
	}

    void OnTriggerEnter(Collider other)
    {
        parent.OnTriggerEnterAttackRange(other);
    }

    void OnTriggerStay(Collider other)
    {
        parent.OnTriggerEnterAttackRange(other);
    }

    void OnTriggerExit(Collider other)
    {
        parent.OnTriggerExitAttackRange(other);
    }
}
