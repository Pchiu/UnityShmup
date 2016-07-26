using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface IMovable : IDrawable {

    List<MovementAction> MovementPattern { get; set; }
}
