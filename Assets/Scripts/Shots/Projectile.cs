using UnityEngine;
using System.Collections;

public class Projectile : Shot {

    public MovementPattern MovementPattern;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Ship ship = collider.gameObject.GetComponent<Ship>();
        if (ship != null)
        {
            if (this.tag == "FriendlyShot" && ship.tag == "Hostile" ||
                this.tag == "HostileShot" && ship.tag == "Friendly")
            {
                ship.TakeDamage(Damage);
                Destroy(gameObject);
                Instantiate(HitAnimation, transform.position, transform.rotation);
            }
        }
    }
}