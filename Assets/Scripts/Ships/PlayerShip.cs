using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class PlayerShip : Ship {

    public List<string> Crew;
    public float Speed;
    public ShipCore Core;
	// Use this for initialization
    void Awake()
    {
        this.GetComponent<Collider2D>().enabled = false;
    }
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        MovementVector = transform.position - LastPosition;
        LastPosition = transform.position;
        base.Update();    
	}

    public void ActivateParry()
    {
        foreach (Subsystem subsystem in Subsystems)
        {
            if (subsystem.GetType().ToString() == "ParrySubsystem")
            {
                subsystem.Action();
            }
        }
    }
}
