using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmController : MonoBehaviour {

    public static GameObject player;
    public Swarm [] swarm;

    public bool activated = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("test");
        swarm = GetComponentsInChildren<Swarm>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateSwarm()
    {
        activated = true;
    }
}
