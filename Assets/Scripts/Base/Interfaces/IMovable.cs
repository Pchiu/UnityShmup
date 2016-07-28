using Assets.Scripts.Movement;

namespace Assets.Scripts.Base.Interfaces
{
    interface IMovable : IDrawable {

        MovementPattern MovementPattern { get; set; }
    }
}
