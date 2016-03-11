using UnityEngine;
using System.Collections;

public class ShipCore : Ship {

    public Ship Ship;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public override void TakeDamage(int damage)
    {
        Ship.TakeDamage(damage);
    }
}
