using UnityEngine;
using System.Collections;

public abstract class AbstractCollidable : AbstractDrawable, ICollidable
{
    public AbstractCollidable(string ID) : base(ID) { }

    public virtual void Collide()
    {

    }
}