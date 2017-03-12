using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour {

    public Enemy parent;
    // Use this for initialization
    void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerArrow")
            parent.OnTriggerEnterWeakspot(other);
    }
}
