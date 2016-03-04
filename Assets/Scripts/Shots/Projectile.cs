using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : Shot {

    public MovementPattern MovementPattern;
    public float MaxDuration;
    private Queue<MovementAction> MovementQueue;
    private float ElapsedTimeFraction;
    private MovementAction CurrentAction;
    private List<Vector2> CurrentWaypoints;
    private Vector3 CurrentDirection;
    private float ElapsedMovementTime;
    private bool FacePlayer;
    // Use this for initialization
    void Start () {
        CurrentWaypoints = new List<Vector2>();
        Destroy(gameObject, MaxDuration);
        StartCoroutine("Move");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Ship ship = collider.gameObject.GetComponent<Ship>();
        if (ship != null)
        {
            if (this.tag == "FriendlyShot" && ship.tag == "Hostile" ||
                this.tag == "HostileShot" && ship.tag == "Friendly")
            {
                ship.TakeDamage(Damage);
                Instantiate(HitAnimation, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
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
                if (CurrentAction.GetType().ToString() == "WaypointMovementAction")
                {
                    transform.position = Utilities.CalculateCurvePosition(CurrentWaypoints, 1);
                }
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
}