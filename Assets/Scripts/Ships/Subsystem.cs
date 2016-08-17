using Assets.Scripts.Base.AbstractClasses;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Ships
{
    public abstract class Subsystem : AbstractDrawable {

        public SubsystemType Type;
        public bool Active;
        // Use this for initialization

        public Subsystem(string ID) : base(ID) { }

        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public virtual void Action()
        {

        }

        public virtual void ToggleAction(bool toggle)
        {

        }
    }
}
