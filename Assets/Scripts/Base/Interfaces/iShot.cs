using UnityEngine;
using System.Collections;

interface iShot : IDrawable {

    int Damage { get; set; }
    SpawnPattern FirePattern { get; set; }
    GameObject HitAnimation { get; set; }
    Color Color { get; set; }

    void Fire();
}
