using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract class AbstractMovable : AbstractDrawable, IMovable
{
    public Queue<MovementAction> MovementQueue
    {
        get
        {
            return MovementQueue;
        }
        set
        {
            MovementQueue = value;
        }
    }

    public float TurnRate
    {
        get
        {
            return TurnRate;
        }
        set
        {
            TurnRate = value;
        }
    }


    public AbstractMovable(string ID) : base(ID) { }
}