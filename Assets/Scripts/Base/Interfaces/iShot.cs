using Assets.Scripts.Shots;
using UnityEngine;

namespace Assets.Scripts.Base.Interfaces
{
    public interface IShot : IDrawable {

        int Damage { get; set; }
        SpawnPattern FirePattern { get; set; }
        GameObject HitAnimation { get; set; }
        Color Color { get; set; }
    }
}
