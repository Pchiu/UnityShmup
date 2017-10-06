using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Base.Interfaces;
using Assets.Scripts.Movement;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Base.AbstractClasses
{
    public abstract class AbstractMovable : AbstractDrawable, IMovable
    {

        public MovementPattern MovementPattern { get; set; }
        public int RepeatCount;
        protected MovementAction CurrentAction;
        protected int CurrentActionIndex;
        protected List<Vector2> CurrentWaypoints;
        protected Vector3 CurrentDirection;
        protected float ElapsedMovementTime;
        protected float ElapsedMovementTimeFraction;
        public bool FaceTarget;
        public Transform Target;
        public Vector3 MovementVector;

        public AbstractMovable(string ID) : base(ID) { }

        public virtual void Awake()
        {
            ElapsedMovementTime = 0f;
            CurrentWaypoints = new List<Vector2>();
            CurrentAction = null;
            CurrentActionIndex = 0;
            FaceTarget = false;
            Target = null;
            RepeatCount = 0;
            MovementVector = new Vector3(0, 0, 0);
        }

        public virtual void Update()
        {
            if (FaceTarget && Target != null)
            {
                Vector3 targetDirection = Target.position - transform.position;
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
            if (MovementPattern != null)
            {
                SetCurrentMovementAction();
            }
            while (CurrentAction != null)
            {
                if (ElapsedMovementTime > CurrentAction.Time)
                {
                    if (CurrentAction.GetType().Name == "WaypointMovementAction")
                    {
                        Vector2 newPosition = Utilities.Utilities.CalculateCurvePosition(CurrentWaypoints, 1);
                        MovementVector = new Vector3(newPosition.x, newPosition.y) - transform.position;
                        transform.position = newPosition;
                    }
                    if (CurrentActionIndex >= MovementPattern.MovementActions.Count)
                    {
                        if (RepeatCount < MovementPattern.Repeat)
                        {
                            CurrentActionIndex = 0;
                            RepeatCount += 1;
                            continue;
                        }
                        else
                        {
                            CurrentAction = null;
                            MovementVector = Vector3.zero;
                            continue;
                        }
                    }
                    else
                    {
                        SetCurrentMovementAction();
                    }
                }
                if (CurrentAction.GetType().Name == "WaypointMovementAction")
                {
                    var WaypointAction = (WaypointMovementAction)CurrentAction;
                    ElapsedMovementTimeFraction = ElapsedMovementTime / WaypointAction.Time;
                    Vector2 newPosition = Utilities.Utilities.CalculateCurvePosition(CurrentWaypoints, ElapsedMovementTimeFraction);
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
            CurrentAction = MovementPattern.MovementActions[CurrentActionIndex];
            var typeName = CurrentAction.GetType().Name;
            if (CurrentAction.GetType().Name == "WaypointMovementAction")
            {
                var WaypointAction = (WaypointMovementAction)CurrentAction;
                CurrentWaypoints.Clear();
                CurrentWaypoints.Add(transform.position);
                if (WaypointAction.ReferenceFrame == MovementReferenceFrame.Local)
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
                if (VectorAction.ReferenceFrame == MovementReferenceFrame.Local)
                {
                    CurrentDirection = transform.TransformDirection(new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed);
                }
                else
                {
                    CurrentDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * VectorAction.Angle), Mathf.Cos(Mathf.Deg2Rad * VectorAction.Angle)) * VectorAction.Speed;
                }
            }
            ElapsedMovementTime = 0;
            CurrentActionIndex += 1;
        }

        public void SetTarget(Transform target)
        {
            this.Target = target;
        }

        public void ToggleRotateTowardsTarget(bool value)
        {
            FaceTarget = value;
        }
    }
}