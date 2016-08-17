using Assets.Scripts.Base.Interfaces;

namespace Assets.Scripts.Base.AbstractClasses
{
    public abstract class AbstractMovableCollidable : AbstractMovable, ICollidable
    {
        public AbstractMovableCollidable(string ID) : base(ID) { }

        public virtual void Collide()
        {

        }
    }
}