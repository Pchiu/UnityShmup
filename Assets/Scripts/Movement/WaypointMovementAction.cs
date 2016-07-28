using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class WaypointMovementAction : MovementAction {

        public readonly Vector2 Origin;
        public readonly List<Vector2> ControlPoints;
    

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
}
