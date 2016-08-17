using System.Collections.Generic;
using Assets.Scripts.Base.Interfaces;
using Assets.Scripts.Enums;
using Assets.Scripts.Shots;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public class Weapon : Subsystem {

        public string WeaponType;
        public List<Transform> ShotOrigins;
        public IShot Shot;
        public FirePattern FirePattern;
        public FireMode FireMode;
        public bool isFiring;
        private bool isFiringCycleActive;
        private float TimeElapsed;
        private int Index;
        private int ShotOriginIndex;
        private System.Random RandomGenerator;

        public Weapon(string ID) : base(ID) { }

        // Use this for initialization
        void Start () {
            isFiring = false;
            RandomGenerator = new System.Random();
            ShotOriginIndex = 0;
        }
	
        void Update () {
            if (isFiring || isFiringCycleActive)
            {
                if (!isFiringCycleActive)
                {
                    isFiringCycleActive = true;
                    TimeElapsed = 0f;
                    Index = 0;
                }
                if (isFiringCycleActive)
                {
                    if (Index >= FirePattern.TimeOffsets.Count)
                    {
                        if (TimeElapsed * 1000 > FirePattern.TotalTime)
                        {
                            isFiringCycleActive = false;
                        }
                        TimeElapsed += Time.deltaTime;
                        return;
                    }
                    else
                    {
                        while (Index < FirePattern.TimeOffsets.Count && TimeElapsed * 1000 >= FirePattern.TimeOffsets[Index])
                        {
                            foreach (IShot shot in FirePattern.Entities)
                            {
                                switch (FireMode)
                                {
                                    case FireMode.Loop:
                                        SpawnShot(shot, ShotOrigins[ShotOriginIndex].position, ShotOrigins[ShotOriginIndex].rotation);
                                        ShotOriginIndex++;
                                        if (ShotOriginIndex >= ShotOrigins.Count)
                                        {
                                            ShotOriginIndex = 0;
                                        }

                                        break;
                                    case FireMode.Random:
                                        ShotOriginIndex = RandomGenerator.Next(0, ShotOrigins.Count);
                                        SpawnShot(shot, ShotOrigins[ShotOriginIndex].position, ShotOrigins[ShotOriginIndex].rotation);
                                        break;
                                    case FireMode.Single:
                                    default:
                                        SpawnShot(shot, ShotOrigins[ShotOriginIndex].position, ShotOrigins[ShotOriginIndex].rotation);
                                        break;
                                }
                                Index++;
                            }
                        }
                    }
                    TimeElapsed += Time.deltaTime;
                }
            }
        }

        public void SpawnShot(IShot shot, Vector3 position, Quaternion rotation)
        {
            if (shot.GetType().Name == "Projectile")
            {
                var projectile = (Projectile)shot;
                var newShot = Instantiate((Projectile)shot, position, rotation);
                var newProjectile = (Projectile)newShot;
                Projectile projectileComponent = projectile.GetComponent<Projectile>();
                newProjectile.MovementPattern = projectileComponent.MovementPattern;
            }
            /*
        var newShot = Instantiate(shot, position, rotation);
        if (newShot.GetType().ToString() == "Projectile")
        {
            var newProjectile = (Projectile)newShot;
            Projectile projectile = shot.GetComponent<Projectile>();
            newProjectile.MovementPattern = projectile.MovementPattern;
        }
        */
        }

        public override void Action()
        {
            if (!isFiring)
            {
                isFiring = true;
            }
        }    

        public override void ToggleAction(bool isFiring)
        {
            this.isFiring = isFiring;
        }
    }
}
