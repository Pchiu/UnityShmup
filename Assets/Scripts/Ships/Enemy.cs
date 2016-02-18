using UnityEngine;
using System.Collections;

public class Enemy : Ship {

    public MovementPattern MovementPattern;
    public int MovementPatternIndex;
    public MovementAction CurrentMovementAction;

	// Use this for initialization
	void Start () {
	    if (MovementPattern != null)
        {
            CurrentMovementAction = MovementPattern.MovementQueue[MovementPatternIndex];
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

}
