using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Projectile projectile;
    public bool faceRight = true;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            float xoffset;
            if (faceRight)
                xoffset = 1.5f;
            else
                xoffset = -2.0f;
            Vector3 start = new Vector3(transform.position.x + xoffset, transform.position.y + 2.0f, 0);
            projectile.CreateProjectile(start, faceRight);
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            faceRight = false;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            faceRight = true;
        }
	}
}
