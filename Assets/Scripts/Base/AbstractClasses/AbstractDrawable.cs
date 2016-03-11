using UnityEngine;
using System.Collections;

public class AbstractDrawable : AbstractIdentifiable, IDrawable
{
    public AbstractDrawable(string ID) : base(ID) { }
}