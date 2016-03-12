using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class ShipSection : AbstractCollidable {

    public int Hull;
    public List<Subsystem> Subsystems;
    public List<Effect> Effects;
    public ShipSectionTypes Type;
    public Ship Ship;


    public ShipSection(string ID) : base(ID) { }

    void Awake()
    {
        Subsystems = new List<Subsystem>();
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
            Ship.CheckCriticalSections();
        }
    }

    public override void Collide()
    {
        
    }

    public void Collide(int damage)
    {

    }
}
