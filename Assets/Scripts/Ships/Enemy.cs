using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Ship {

    public MovementPattern MovementPattern;
    private float ElapsedTimeFraction;
    private MovementAction CurrentAction;
    private Vector2 StartPosition, EndPosition;
    private List<Vector2> CurrentWaypoints;
    private Vector3 CurrentDirection;
    private float ElapsedMovementTime;


	// Use this for initialization
	void Start () {
        StartPosition = transform.position;
        ElapsedMovementTime = 0f;
        CurrentAction = null;
        CurrentWaypoints = new List<Vector2>();
        StartCoroutine("Move");
	}
	
	// Update is called once per frame
	void Update () {
	}

    public IEnumerator Move()
    {
        if (MovementPattern != null)
        {
            SetCurrentAction();
        }
        while (CurrentAction != null)
        {
            if (ElapsedMovementTime > CurrentAction.Time)
            {
                if (MovementPattern.MovementQueue.Count == 0)
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
                transform.position = CalculateCurvePosition(CurrentWaypoints, ElapsedTimeFraction);
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
        CurrentAction = MovementPattern.MovementQueue.Dequeue();
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
                    direction = transform.InverseTransformDirection(new Vector3(controlPoint.x, controlPoint.y) - transform.position);
                    CurrentWaypoints.Add(transform.position + direction);
                }

                direction = transform.InverseTransformDirection(new Vector3(WaypointAction.Origin.x, WaypointAction.Origin.y) - transform.position);
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
            CurrentDirection = transform.InverseTransformDirection(new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed);
        }
        ElapsedMovementTime = 0;
    }

    public Vector2 CalculateCurvePosition(List<Vector2> points, float fraction)
    {
        if (points.Count == 2)
        {
            return Vector2.Lerp(points[0], points[1], fraction);
        }
        List<Vector2> newPoints = new List<Vector2>();
        for (int i = 0; i < points.Count - 1; i++)
        {
            newPoints.Add(Vector2.Lerp(points[i], points[i + 1], fraction));
        }
        return CalculateCurvePosition(newPoints, fraction);

    }
}
