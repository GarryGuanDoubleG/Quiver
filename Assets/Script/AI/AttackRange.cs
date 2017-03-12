using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {

    public Enemy parent;
    public static float alertRate = .1f;

    public float delay = 0;

	// Use this for initialization
	void Start () {       
        parent = GetComponentInParent<Enemy>();        
	}

    private void Update()
    {
        delay -= Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        parent.OnTriggerEnterAttackRange(other);
    }

    void OnTriggerStay(Collider other)
    {
        if (delay <= 0)
        {
            parent.OnTriggerEnterAttackRange(other);
            delay = alertRate;
        }
    }

    void OnTriggerExit(Collider other)
    {
        parent.OnTriggerExitAttackRange(other);
    }
}
