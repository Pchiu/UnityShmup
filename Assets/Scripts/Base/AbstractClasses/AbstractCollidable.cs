using UnityEngine;
using System.Collections;

abstract class AbstractCollidable : AbstractDrawable, ICollidable
{
    public AbstractCollidable(string ID) : base(ID) { }

    public void Collide()
    {

    }
}