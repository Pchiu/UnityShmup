using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern {

    public Entity Entity;
    public List<Transform> PositionOffsets;
    public List<int> TimeOffsets;
    public List<int> RotationOffsets;
    public float Difficulty;
    public float TimeElapsed;

    public int Index;
	// Use this for initialization
	void Start () {
        TimeOffsets = new List<int>();
        PositionOffsets = new List<Transform>();
        RotationOffsets = new List<int>();
        TimeElapsed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
