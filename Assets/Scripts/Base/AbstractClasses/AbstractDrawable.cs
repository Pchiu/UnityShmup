using UnityEngine;
using System.Collections;

public class AbstractDrawable : AbstractIdentifiable, IDrawable
{
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

    public AbstractDrawable(string ID) : base(ID) { }
}