using System.Collections.Generic;
using Assets.Scripts.Base.Interfaces;

namespace Assets.Scripts.Base.AbstractClasses
{
    public class AbstractDrawable : AbstractIdentifiable, IDrawable
    {
        public List<Effect> effects;
        public float turnRate;
        public List<Behavior.Behavior> BehaviorList;

        public float TurnRate
        {
            get { return turnRate; }
            set { turnRate = value; }
        }

        public List<Effect> Effects
        {
            get { return effects; }
            set { effects = value; }
        }

        public virtual void Awake()
        {
            Effects = new List<Effect>();
            BehaviorList = new List<Behavior.Behavior>();
        }

        public AbstractDrawable(string ID) : base(ID)
        {
            
        }
    }
}