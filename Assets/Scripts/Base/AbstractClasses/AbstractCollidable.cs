using Assets.Scripts.Base.Interfaces;

namespace Assets.Scripts.Base.AbstractClasses
{
    public abstract class AbstractCollidable : AbstractDrawable, ICollidable
    {
        public AbstractCollidable(string ID) : base(ID) { }

        public virtual void Collide()
        {

        }
    }
}