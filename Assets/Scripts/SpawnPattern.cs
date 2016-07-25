using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern {

    public List<IDrawable> Entities;
    public List<int> TimeOffsets;
	// Use this for initialization
	void Start () {
        Entities = new List<IDrawable>();
        TimeOffsets = new List<int>();
	}
}
