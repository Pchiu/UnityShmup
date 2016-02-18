using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPattern {

    public List<Entity> Entities;
    public List<int> TimeOffsets;
	// Use this for initialization
	void Start () {
        Entities = new List<Entity>();
        TimeOffsets = new List<int>();
	}
}
