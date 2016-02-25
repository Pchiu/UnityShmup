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
    public List<Effect> Effects;
    private Vector3 LastPosition;
    private Vector3 MovementVector;

	// Use this for initialization
	void Start () {
        Subsystems = new List<Subsystem>();
        Hardpoints = new List<Hardpoint>();
        LastPosition = transform.position;
        MovementVector = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	public virtual void Update () {
        MovementVector = transform.position - LastPosition;
        LastPosition = transform.position;
        //Debug.Log(MovementVector);
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
                        //Debug.Log(movementRotation);
                        float angle = Quaternion.Angle(movementRotation, effect.transform.rotation);
                        //float angle = Mathf.Abs(movementRotation.eulerAngles.z - effect.transform.rotation.eulerAngles.z);
                        Debug.Log(angle);
                        // Change this to use 
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
                    //Debug.Log(coefficient);
                    
                }
            }
        }
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
