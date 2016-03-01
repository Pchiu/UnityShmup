using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointMovementAction : MovementAction {

    public readonly Vector2 Origin;
    public readonly List<Vector2> ControlPoints;
    public readonly string ReferenceFrame;

    public WaypointMovementAction (Vector2 origin, List<Vector2> controlPoints, float time, string referenceFrame)
    {
        this.Origin = origin;
        this.ControlPoints = controlPoints;
        this.Time = time;
        this.ReferenceFrame = referenceFrame;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
