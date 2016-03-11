using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movable : Entity {

    public MovementPattern MovementPattern;
    public float TurnRate;
    protected Queue<MovementAction> MovementQueue;
    protected float ElapsedTimeFraction;
    protected MovementAction CurrentAction;
    protected List<Vector2> CurrentWaypoints;
    protected Vector3 CurrentDirection;
    protected float ElapsedMovementTime;
    protected Vector3 PreviousLocation;
    public bool FaceTarget;
    public Transform Target;
    public Vector3 MovementVector;
    // Use this for initialization
    public virtual void Start () {
    }

    public virtual void Awake()
    {
        ElapsedMovementTime = 0f;
        CurrentWaypoints = new List<Vector2>();
        CurrentAction = null;
        FaceTarget = false;
        Target = null;
        MovementVector = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    public virtual void Update () {
	    if (FaceTarget && Target != null)
        {
            Vector3 targetDirection = Target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, TurnRate * Time.deltaTime);
        }
	}

    public void Move()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void Rotate(float angle, float time)
    {
        StartCoroutine(RotateCoroutine(angle, time));
    }

    public IEnumerator RotateCoroutine(float angle, float time)
    {
        var elapsedTime = 0f;
        var clampedTurnAmount = angle > TurnRate ? TurnRate / time : angle / time;
        var targetDirection = transform.TransformDirection(new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle)));
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
        while (elapsedTime < time)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, clampedTurnAmount * Time.deltaTime);
            yield return null;
        }

        // Clamp ending rotation;
        transform.rotation = targetRotation;       
    }

    public IEnumerator MoveCoroutine()
    {
        if (MovementPattern != null)
        {
            MovementQueue = new Queue<MovementAction>(MovementPattern.MovementQueue);
            SetCurrentMovementAction();
        }
        while (CurrentAction != null)
        {
            if (ElapsedMovementTime > CurrentAction.Time)
            {
                if (CurrentAction.GetType().ToString() == "WaypointMovementAction")
                {
                    Vector2 newPosition = Utilities.CalculateCurvePosition(CurrentWaypoints, 1);
                    MovementVector = new Vector3(newPosition.x, newPosition.y) - transform.position;
                    transform.position = newPosition;
                }
                if (MovementQueue.Count == 0)
                {
                    CurrentAction = null;
                    MovementVector = Vector3.zero;
                    continue;
                }
                else
                {
                    SetCurrentMovementAction();
                }
            }
            if (CurrentAction.GetType().ToString() == "WaypointMovementAction")
            {
                var WaypointAction = (WaypointMovementAction)CurrentAction;
                ElapsedTimeFraction = ElapsedMovementTime / WaypointAction.Time;
                Vector2 newPosition = Utilities.CalculateCurvePosition(CurrentWaypoints, ElapsedTimeFraction);
                MovementVector = new Vector3(newPosition.x, newPosition.y) - transform.position;
                transform.position = newPosition;
            }
            else
            {
                var VectorAction = (VectorMovementAction)CurrentAction;
                if (VectorAction.ReferenceFrame == "Local")
                {
                    CurrentDirection = transform.TransformDirection(new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed);
                }
                transform.position += CurrentDirection * Time.deltaTime;
                MovementVector = CurrentDirection;
            }
            
            ElapsedMovementTime += Time.deltaTime;
            yield return null;
        }
    }

    public void SetCurrentMovementAction()
    {
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
                CurrentDirection = transform.TransformDirection(new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed);
            }
            else
            {
                CurrentDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed;
            }
        }
        ElapsedMovementTime = 0;
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void ToggleRotateTowardsTarget(bool value)
    {
        FaceTarget = value;
    }
}
