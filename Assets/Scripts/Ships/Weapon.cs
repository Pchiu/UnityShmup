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
    public FireModes FireMode;
    public bool isFiring;
    private bool isFiringCycleActive;
    private float TimeElapsed;
    private int Index;
    private int ShotOriginIndex;
    private System.Random RandomGenerator;
    
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
                        foreach (Shot shot in FirePattern.Entities)
                        {
                            switch (FireMode)
                            {
                                case FireModes.Loop:
                                    Instantiate(shot, ShotOrigins[ShotOriginIndex].position, ShotOrigins[ShotOriginIndex].rotation);
                                    ShotOriginIndex++;
                                    if (ShotOriginIndex >= ShotOrigins.Count)
                                    {
                                        ShotOriginIndex = 0;
                                    }
                                    break;
                                case FireModes.Random:
                                    int randomIndex = RandomGenerator.Next(0, ShotOrigins.Count);
                                    Instantiate(shot, ShotOrigins[randomIndex].position, ShotOrigins[randomIndex].rotation);
                                    break;
                                case FireModes.Single:
                                default:
                                    Instantiate(shot, ShotOrigins[0].position, ShotOrigins[0].rotation);
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
