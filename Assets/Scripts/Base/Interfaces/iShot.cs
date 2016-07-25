using UnityEngine;
using System.Collections;

public interface IShot : IDrawable {

    int Damage { get; set; }
    SpawnPattern FirePattern { get; set; }
    GameObject HitAnimation { get; set; }
    Color Color { get; set; }
}
