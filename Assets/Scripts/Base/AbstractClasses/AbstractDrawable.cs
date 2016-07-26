using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbstractDrawable : AbstractIdentifiable, IDrawable
{
    public List<Effect> effects;
    public float turnRate;

    public float TurnRate
    {
        get { return turnRate; }
        set { turnRate = value; }
    }

    public List<Effect> Effects
    {
        get { return effects; }
        set { effects = value; }
    }

    

    public AbstractDrawable(string ID) : base(ID) { }
}