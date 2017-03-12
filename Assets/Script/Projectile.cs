using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 10.0f;
    public float lifetime = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = new Vector3(speed * Time.deltaTime, 0, 0);
        transform.position += offset;

        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void CreateProjectile(Vector3 start, bool goRight)
    {
        Projectile projectile = (Projectile)Instantiate(this, start, Quaternion.identity);
        projectile.speed = goRight ? projectile.speed : (-1.0f * projectile.speed);

    }

}
