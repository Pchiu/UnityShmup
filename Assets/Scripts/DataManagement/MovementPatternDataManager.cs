using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementPatternDataManager : MonoBehaviour {

    public Dictionary<string, MovementPattern> MovementPatterns;
	// Use this for initialization
	void Start () {
	}
	
    public void CreateTestMovementPattern()
    {
        MovementPatterns = new Dictionary<string, MovementPattern>();
        MovementPattern pattern = new MovementPattern();
        pattern.MovementQueue = new Queue<MovementAction>();

        WaypointMovementAction waypointAction1 = new WaypointMovementAction(new Vector2(5, 0), new List<Vector2>() { new Vector2(-2, 5), new Vector2(2, -5)}, 3);
        WaypointMovementAction waypointAction2 = new WaypointMovementAction(new Vector2(4, 0), new List<Vector2>(), 1);
        VectorMovementAction vectorAction1 = new VectorMovementAction(270, 2, 0, 3);
        VectorMovementAction vectorAction2 = new VectorMovementAction(225, 2, 0, 3);

        pattern.MovementQueue.Enqueue(waypointAction1);
        //pattern.MovementQueue.Enqueue(waypointAction2);
        //pattern.MovementQueue.Enqueue(vectorAction1);
        //pattern.MovementQueue.Enqueue(vectorAction2);
        MovementPatterns.Add("TestPattern", pattern);
    }
}
