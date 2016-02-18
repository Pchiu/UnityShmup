using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShip : Ship {

    public List<string> Crew;
    public float Speed;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile projectile = collider.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            if (projectile.tag == "HostileShot")
            {
                Hull -= projectile.Damage;
                // Call Projectile's death function here
            }
        }
    }
}
