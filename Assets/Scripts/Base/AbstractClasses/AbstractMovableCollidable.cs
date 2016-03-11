using UnityEngine;
using System.Collections;

abstract class AbstractMovableCollidable : AbstractMovable, ICollidable
{
    public AbstractMovableCollidable(string ID) : base(ID) { }

    public void Collide()
    {

    }
}