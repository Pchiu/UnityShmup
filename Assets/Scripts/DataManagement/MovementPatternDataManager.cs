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

        WaypointMovementAction waypointAction1 = new WaypointMovementAction(new Vector2(5, 0), new List<Vector2>() { new Vector2(-2, 5), new Vector2(2, -5)}, 3, "Local");
        WaypointMovementAction waypointAction2 = new WaypointMovementAction(new Vector2(0, 1), new List<Vector2>(), 1, "World");
        VectorMovementAction vectorAction1 = new VectorMovementAction(0, 2, 0, 2);
        VectorMovementAction vectorAction2 = new VectorMovementAction(135, 2, 0, 2);

        pattern.MovementQueue.Enqueue(waypointAction1);
        pattern.MovementQueue.Enqueue(waypointAction2);
        //pattern.MovementQueue.Enqueue(vectorAction1);
        //pattern.MovementQueue.Enqueue(vectorAction2);
        MovementPatterns.Add("TestPattern", pattern);
    }
}
