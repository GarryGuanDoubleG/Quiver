using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

    public Enemy parent;
    // Use this for initialization
    void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        parent.OnTriggerEnterBody(other);
    }

}
