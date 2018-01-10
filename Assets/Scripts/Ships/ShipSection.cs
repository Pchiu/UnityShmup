using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Actions;
using Assets.Scripts.Base.AbstractClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Animations;
using Assets.Scripts.Behaviors;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public class ShipSection : AbstractMovableCollidable {

        public int Hull;
        public List<Hardpoint> Hardpoints;
        public ShipSectionType Type;
        public Ship Ship;

        public List<GameObject> DeathEffects;
        public ShipSection DestroyedShipSection;
        public DeathBehavior DeathBehavior;


        public ShipSection(string ID) : base(ID) { }

        void Awake()
        {
            Hardpoints = new List<Hardpoint>();
            DeathEffects = new List<GameObject>();
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
            if (Type == ShipSectionType.Indestructible)
            {
                return;
            }
            Hull -= damage;
            if (Hull <= 0)
            {
                /*
                if (DisableCollisionOnDestruction)
                {
                    var collider = GetComponent<Collider2D>();
                    collider.enabled = false;
                }
                if (DetachOnDestruction)
                {
                    this.transform.parent = null;
                    // Add movement at background velocity
                }
                */
                Ship.ShipSections.Remove(this);
                Die();

                // Move to some controller
                /*
                GameObject animationSpawner = new GameObject();
                var spawner = animationSpawner.AddComponent<AnimationSpawner>();
                spawner.Duration = 3f;
                spawner.Radius = 3;
                spawner.transform.parent = this.transform;
                spawner.transform.position = this.transform.position;
                spawner.Items.Add(new AnimationSpawnerItem { Sprite = (GameObject)Resources.Load("Prefabs/BulletDeathAnimated_0"), Interval = 0.3f });
                spawner.Parent = this.gameObject;
                spawner.Initialize();
                */ 

                //StartCoroutine(Die());
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

        public void Die()
        {
            if (DeathBehavior != null)
            {
                switch (DeathBehavior.Type)
                {
                    case DeathBehaviorType.Destroy:
                        Destroy(this.gameObject);
                        break;
                    case DeathBehaviorType.Replace:
                        var destroyedSection = Instantiate(this.DestroyedShipSection, this.transform.position, this.transform.rotation);
                        destroyedSection.Type = DeathBehavior.SectionType;
                        destroyedSection.transform.SetParent(this.Ship.transform);
                        this.Ship.ShipSections.Add(destroyedSection);
                        destroyedSection.tag = Ship.tag;
                        if (DeathBehavior.DisableCollider)
                        {
                            destroyedSection.GetComponent<Collider2D>().enabled = false;
                        }
                        Destroy(this.gameObject);

                        break;
                    case DeathBehaviorType.Detach:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
