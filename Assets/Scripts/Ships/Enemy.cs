using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Ship {

    public MovementPattern MovementPattern;
    private float ElapsedTimeFraction;
    private Queue<MovementAction> MovementQueue;
    private MovementAction CurrentAction;
    private List<Vector2> CurrentWaypoints;
    private Vector3 CurrentDirection;
    private float ElapsedMovementTime;
    private bool FacePlayer;

	// Use this for initialization
	void Start () {
        ElapsedMovementTime = 0f;
        CurrentAction = null;
        CurrentWaypoints = new List<Vector2>();
        StartCoroutine("Move");
        FacePlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (FacePlayer)
        {
            if (GameController.Instance.PlayerController.IsPlayerAlive())
            {
                Vector3 playerDirection = GameController.Instance.PlayerController.PlayerShip.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, playerDirection);
                transform.rotation = targetRotation;
            }
        }
        base.Update();
	}

    public IEnumerator Move()
    {
        if (MovementPattern != null)
        {
            MovementQueue = new Queue<MovementAction>(MovementPattern.MovementQueue);
            SetCurrentAction();
        }
        while (CurrentAction != null)
        {
            if (ElapsedMovementTime > CurrentAction.Time)
            {
                transform.position = Utilities.CalculateCurvePosition(CurrentWaypoints, 1);
                if (MovementQueue.Count == 0)
                {
                    CurrentAction = null;
                    continue;
                }
                else
                {
                    SetCurrentAction();
                }
            }
            if (CurrentAction.GetType().ToString() == "WaypointMovementAction")
            {
                var WaypointAction = (WaypointMovementAction)CurrentAction;
                ElapsedTimeFraction = ElapsedMovementTime / WaypointAction.Time;
                transform.position = Utilities.CalculateCurvePosition(CurrentWaypoints, ElapsedTimeFraction);
            }
            else
            {
                transform.position += CurrentDirection * Time.deltaTime;
            }
            ElapsedMovementTime += Time.deltaTime;
            yield return null;
        }
    }

    public void SetCurrentAction()
    {
        Debug.Log(transform.rotation);
        CurrentAction = MovementQueue.Dequeue();
        if (CurrentAction.GetType().ToString() == "WaypointMovementAction")
        {
            var WaypointAction = (WaypointMovementAction)CurrentAction;
            CurrentWaypoints.Clear();
            CurrentWaypoints.Add(transform.position);
            if (WaypointAction.ReferenceFrame == "Local")
            {
                Vector3 direction = Vector2.zero;
                foreach (Vector2 controlPoint in WaypointAction.ControlPoints)
                {
                    direction = transform.TransformDirection(controlPoint);
                    CurrentWaypoints.Add(transform.position + direction);
                }

                direction = transform.TransformDirection(WaypointAction.Origin);
                CurrentWaypoints.Add(transform.position + direction);
            }
            else
            {
                CurrentWaypoints.AddRange(WaypointAction.ControlPoints);
                CurrentWaypoints.Add(WaypointAction.Origin);
            }
            
        }
        else
        {
            var VectorAction = (VectorMovementAction)CurrentAction;
            if (VectorAction.ReferenceFrame == "Local")
            {
                CurrentDirection = transform.InverseTransformDirection(new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed);
            }
            else
            {
                CurrentDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed;
            }
        }
        ElapsedMovementTime = 0;
    }
}
