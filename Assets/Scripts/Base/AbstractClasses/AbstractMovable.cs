using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractMovable : AbstractDrawable, IMovable
{
    
    public Queue<MovementAction> MovementQueue
    {
        get
        {
            return movementQueue;
        }
        set
        {
            movementQueue = value;
        }
    }

    public float TurnRate
    {
        get
        {
            return turnRate;
        }
        set
        {
            turnRate = value;
        }
    }

    public float turnRate;
    public Queue<MovementAction> movementQueue;
    protected MovementAction currentAction;
    protected List<Vector2> currentWaypoints;
    protected Vector3 currentDirection;
    protected float elapsedMovementTime;
    protected float elapsedMovementTimeFraction;
    public bool faceTarget;
    public Transform target;
    public Vector3 movementVector;

    public AbstractMovable(string ID) : base(ID) { }

    public virtual void Awake()
    {
        elapsedMovementTime = 0f;
        currentWaypoints = new List<Vector2>();
        currentAction = null;
        faceTarget = false;
        target = null;
        movementVector = new Vector3(0, 0, 0);
    }

    public virtual void Update()
    {
        if (faceTarget && target != null)
        {
            Vector3 targetDirection = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, TurnRate * Time.deltaTime); ;
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

    public IEnumerator MoveCoroutine()
    {
        if (MovementQueue != null)
        {
            SetCurrentMovementAction();
        }
        while (currentAction != null)
        {
            if (elapsedMovementTime > currentAction.Time)
            {
                if (currentAction.GetType().ToString() == "WaypointMovementAction")
                {
                    Vector2 newPosition = Utilities.CalculateCurvePosition(currentWaypoints, 1);
                    movementVector = new Vector3(newPosition.x, newPosition.y) - transform.position;
                    transform.position = newPosition;
                }
                if (MovementQueue.Count == 0)
                {
                    currentAction = null;
                    movementVector = Vector3.zero;
                    continue;
                }
                else
                {
                    SetCurrentMovementAction();
                }
            }
            if (currentAction.GetType().ToString() == "WaypointMovementAction")
            {
                var WaypointAction = (WaypointMovementAction)currentAction;
                elapsedMovementTimeFraction = elapsedMovementTime / WaypointAction.Time;
                Vector2 newPosition = Utilities.CalculateCurvePosition(currentWaypoints, elapsedMovementTimeFraction);
                movementVector = new Vector3(newPosition.x, newPosition.y) - transform.position;
                transform.position = newPosition;
            }
            else
            {
                var VectorAction = (VectorMovementAction)currentAction;
                if (VectorAction.ReferenceFrame == "Local")
                {
                    currentDirection = transform.TransformDirection(new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed);
                }
                transform.position += currentDirection * Time.deltaTime;
                movementVector = currentDirection;
            }

            elapsedMovementTime += Time.deltaTime;
            yield return null;
        }
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

    public void SetCurrentMovementAction()
    {
        currentAction = MovementQueue.Dequeue();
        if (currentAction.GetType().ToString() == "WaypointMovementAction")
        {
            var WaypointAction = (WaypointMovementAction)currentAction;
            currentWaypoints.Clear();
            currentWaypoints.Add(transform.position);
            if (WaypointAction.ReferenceFrame == "Local")
            {
                Vector3 direction = Vector2.zero;
                foreach (Vector2 controlPoint in WaypointAction.ControlPoints)
                {
                    direction = transform.TransformDirection(controlPoint);
                    currentWaypoints.Add(transform.position + direction);
                }

                direction = transform.TransformDirection(WaypointAction.Origin);
                currentWaypoints.Add(transform.position + direction);
            }
            else
            {
                currentWaypoints.AddRange(WaypointAction.ControlPoints);
                currentWaypoints.Add(WaypointAction.Origin);
            }

        }
        else
        {
            var VectorAction = (VectorMovementAction)currentAction;
            if (VectorAction.ReferenceFrame == "Local")
            {
                currentDirection = transform.TransformDirection(new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed);
            }
            else
            {
                currentDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed;
            }
        }
        elapsedMovementTime = 0;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void ToggleRotateTowardsTarget(bool value)
    {
        faceTarget = value;
    }
}