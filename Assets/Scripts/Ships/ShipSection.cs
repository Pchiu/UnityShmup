using System.Collections.Generic;
using Assets.Scripts.Base.AbstractClasses;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public class ShipSection : AbstractMovableCollidable {

        public int Hull;
        public List<Hardpoint> Hardpoints;
        public ShipSectionType Type;
        public Ship Ship;


        public ShipSection(string ID) : base(ID) { }

        void Awake()
        {
            Hardpoints = new List<Hardpoint>();
        }
        /*
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
        */
        public virtual void TakeDamage(int damage)
        {
            Hull -= damage;
            if (Hull <= 0)
            {
                Destroy(this.gameObject);
                Ship.CheckCriticalSections();
            }
        }

        public virtual void Update()
        {
            foreach (Effect effect in Effects)
            {
                if (effect.EffectType == EffectTypes.ThrusterAnimation)
                {
                    Animator animator = effect.GetComponent<Animator>();
                    if (animator != null)
                    {
                        if (Ship.MovementVector != Vector3.zero)
                        {
                            Quaternion movementRotation = Quaternion.LookRotation(Vector3.forward, Ship.MovementVector);
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
        }

        public override void Collide()
        {
        
        }

        public void Collide(int damage)
        {

        }
    }
}
