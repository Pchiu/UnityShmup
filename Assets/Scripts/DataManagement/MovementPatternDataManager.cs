using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementPatternDataManager : MonoBehaviour {

    public Dictionary<string, List<MovementAction>> MovementPatterns;
	// Use this for initialization
	void Start () {
        MovementPatterns = new Dictionary<string, List<MovementAction>>();
    }
	
    public void CreateTestMovementPattern()
    {
        List<MovementAction> movementPattern = new List<MovementAction>();

        WaypointMovementAction waypointAction1 = new WaypointMovementAction(new Vector2(0, 5), new List<Vector2>() { new Vector2(-4, 1.5f), new Vector2(4, 3.5f) }, 3, "Local");
        WaypointMovementAction waypointAction2 = new WaypointMovementAction(new Vector2(10, 0), new List<Vector2>() { new Vector2(2.5f, 5), new Vector2(7.5f, -5)}, 3, "Local");
        VectorMovementAction vectorAction1 = new VectorMovementAction(0, 2, 0, 2, "Local");
        VectorMovementAction vectorAction2 = new VectorMovementAction(135, 2, 0, 2, "World");

        movementPattern.Add(waypointAction1);
        //pattern.MovementQueue.Enqueue(waypointAction2);
        //pattern.MovementQueue.Enqueue(vectorAction1);
        //pattern.MovementQueue.Enqueue(vectorAction2);
        MovementPatterns.Add("TestPattern", movementPattern);
    }

    public void CreateTestShotPattern()
    {
        //MovementPattern pattern = new MovementPattern();
        List<MovementAction> movementPattern = new List<MovementAction>();
        WaypointMovementAction waypointAction1 = new WaypointMovementAction(new Vector2(0, 15), new List<Vector2>() { new Vector2(-2, 1.5f), new Vector2(2, 3.5f) }, 1, "Local");
        WaypointMovementAction waypointAction2 = new WaypointMovementAction(new Vector2(0, 5), new List<Vector2>() , 1, "Local");

        movementPattern.Add(waypointAction1);
        movementPattern.Add(waypointAction2);
        MovementPatterns.Add("TestShotPattern", movementPattern);
    }
}
