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
        MovementVector = transform.position - LastPosition;
        LastPosition = transform.position;
        base.Update();    
	}

}
