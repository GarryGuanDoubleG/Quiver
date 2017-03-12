using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    private static float speed = 2.0f;    

    public static float maxSpeed = 50.0f;
    public static float maxForce = 100.0f;

    public Vector3 currVel;

	// Use this for initialization
	void Start () {
        currVel = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("Mouse pos " + mousePos);

        }

	}
}
