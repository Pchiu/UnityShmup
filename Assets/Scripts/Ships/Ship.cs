using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Ship : Movable {

    public int Hull = 1;
    public List<Subsystem> Subsystems;
    public List<Hardpoint> Hardpoints;

    public bool IsInvulnerable;
    public List<Effect> Effects;
    protected Vector3 LastPosition;
    
    void Awake()
    {
        base.Awake();
        Subsystems = new List<Subsystem>();
        Hardpoints = new List<Hardpoint>();
        LastPosition = transform.position;
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	public virtual void Update () {
	    foreach (Effect effect in Effects)
        {
            if (effect.EffectType == EffectTypes.ThrusterAnimation)
            {
                Animator animator = effect.GetComponent<Animator>();
                if (animator != null)
                {
                    if (MovementVector != Vector3.zero)
                    {
                        Quaternion movementRotation = Quaternion.LookRotation(Vector3.forward, MovementVector);
                        float angle = Quaternion.Angle(movementRotation, effect.transform.rotation);
                        if (angle <= 45)
                        {
                            animator.SetFloat("ThrustCoefficient", 1);
                        }
                        else
                        {
                            animator.SetFloat("ThrustCoefficient", 0);
                        }
                    }
                    else
                    {
                        animator.SetFloat("ThrustCoefficient", 0);
                    }
                }
            }
        }
        base.Update();
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
