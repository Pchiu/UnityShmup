using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Ship {

    public MovementPattern MovementPattern;
    private float ElapsedTimeFraction;
    private MovementAction CurrentAction;
    private Vector2 StartPosition, EndPosition;
    private Vector3 Direction;
    private float ElapsedMovementTime;


	// Use this for initialization
	void Start () {
        StartPosition = transform.position;
        ElapsedMovementTime = 0f;
        CurrentAction = null;
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
        while (MovementPattern.MovementQueue.Count > 0)
        {
            if (CurrentAction == null || ElapsedMovementTime > CurrentAction.Time)
            {
                MovementPattern.MovementQueue.Dequeue();
                if (MovementPattern.MovementQueue.Count == 0)
                {
                    continue;
                }
                SetCurrentAction();
            }

            if (CurrentAction.GetType().ToString() == "WaypointMovementAction")
            {
                var WaypointAction = (WaypointMovementAction)CurrentAction;
                ElapsedTimeFraction = ElapsedMovementTime / WaypointAction.Time;

                List<Vector2> points = new List<Vector2>();
                points.Add(StartPosition);
                points.AddRange(WaypointAction.ControlPoints);
                points.Add(EndPosition);

                transform.position = CalculateCurvePosition(points, ElapsedTimeFraction);
            }
            else
            {
                transform.position += Direction * Time.deltaTime;
            }
            ElapsedMovementTime += Time.deltaTime;
            yield return null;
        }
    }

    public void SetCurrentAction()
    {
        CurrentAction = MovementPattern.MovementQueue.Peek();
        if (CurrentAction.GetType().ToString() == "WaypointMovementAction")
        {
            var WaypointAction = (WaypointMovementAction)CurrentAction;
            StartPosition = transform.position;
            EndPosition = WaypointAction.Origin;
        }
        else
        {
            var VectorAction = (VectorMovementAction)CurrentAction;
            Direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed;
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
