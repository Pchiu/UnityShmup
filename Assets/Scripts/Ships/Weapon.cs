using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Weapon : Subsystem {

    public string WeaponType;
    public List<Transform> ShotOrigins;
    public Shot Shot;
    public FirePattern FirePattern;
    private ShotTypes ShotType;
    public bool isFiring;
    private bool isFiringCycleActive;
    private float TimeElapsed;
    private int Index;


	// Use this for initialization
	void Start () {
        isFiring = false;
        switch (Shot.GetType().ToString())
        {
            case "Projectile":
                ShotType = ShotTypes.Projectile;
                break;
            case "ShortBeam":
                ShotType = ShotTypes.ShortBeam;
                break;
            default:
                ShotType = ShotTypes.Projectile;
                break;
        }
	}
	
	// Update is called once per frame
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
                    switch (ShotType)
                    {
                        case ShotTypes.Projectile:
                        case ShotTypes.ShortBeam:
                        default:
                            while (Index < FirePattern.TimeOffsets.Count && TimeElapsed * 1000 >= FirePattern.TimeOffsets[Index])
                            {
                                Instantiate(Shot, ShotOrigins[0].position, ShotOrigins[0].rotation);
                                Index++;
                            }
                            break;
                    }
                }
                TimeElapsed += Time.deltaTime;
            }
        }
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
