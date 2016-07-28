using System.Collections.Generic;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.DataManagement
{
    public class MovementPatternDataManager : MonoBehaviour {

        public Dictionary<string, MovementPattern> MovementPatterns;
        // Use this for initialization
        void Start () {
            MovementPatterns = new Dictionary<string, MovementPattern>();
        }
	
        public void CreateTestMovementPattern()
        {
            var movementPattern = new MovementPattern();
            movementPattern.Repeat = 0;
            List<MovementAction> movementActions= new List<MovementAction>();

            WaypointMovementAction waypointAction1 = new WaypointMovementAction(new Vector2(0, 5), new List<Vector2>() { new Vector2(-4, 1.5f), new Vector2(4, 3.5f) }, 3, "Local");
            WaypointMovementAction waypointAction2 = new WaypointMovementAction(new Vector2(10, 0), new List<Vector2>() { new Vector2(2.5f, 5), new Vector2(7.5f, -5)}, 3, "Local");
            VectorMovementAction vectorAction1 = new VectorMovementAction(0, 2, 0, 2, "Local");
            VectorMovementAction vectorAction2 = new VectorMovementAction(135, 2, 0, 2, "World");

            movementActions.Add(waypointAction1);
            movementPattern.MovementActions = movementActions;

            //pattern.MovementQueue.Enqueue(waypointAction2);
            //pattern.MovementQueue.Enqueue(vectorAction1);
            //pattern.MovementQueue.Enqueue(vectorAction2);
            MovementPatterns.Add("TestPattern", movementPattern);
        }

        public void CreateTestShotPattern()
        {
            var movementPattern = new MovementPattern();
            movementPattern.Repeat = 5;
            List<MovementAction> movementActions = new List<MovementAction>();
            WaypointMovementAction waypointAction1 = new WaypointMovementAction(new Vector2(0, 6), new List<Vector2>() { new Vector2(-2, 2f), new Vector2(2, 4f) }, 1, "Local");
            WaypointMovementAction waypointAction2 = new WaypointMovementAction(new Vector2(0, 5), new List<Vector2>() , 1, "Local");

            movementActions.Add(waypointAction1);
            movementPattern.MovementActions = movementActions;
            //movementActions.Add(waypointAction2);
            MovementPatterns.Add("TestShotPattern", movementPattern);
        }
    }
}
