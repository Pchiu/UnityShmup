using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface IMovable : IDrawable {

    MovementPattern MovementPattern { get; set; }
}
