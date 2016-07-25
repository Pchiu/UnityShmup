using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface IMovable : IDrawable {

    Queue<MovementAction> MovementQueue { get; set; }
}
