using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Ship : Entity {

    public int Hull = 1;
    public List<Subsystem> Subsystems;
    public List<Hardpoint> Hardpoints;
    public float TurnRate;
    public bool IsInvulnerable;

	// Use this for initialization
	void Start () {
        Subsystems = new List<Subsystem>();
        Hardpoints = new List<Hardpoint>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ToggleWeapons(bool toggle)
    {
        foreach (Subsystem subsystem in Subsystems)
        {
            if (subsystem.Type == SusbsystemTypes.Weapon)
            {
                subsystem.ToggleAction(toggle);
            }
        }
    }

    public virtual void TakeDamage(int damage)
    {
        Hull -= damage;
        if (Hull <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
