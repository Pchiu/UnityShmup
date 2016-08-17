namespace Assets.Scripts.Ships
{
    public class Turret : Subsystem {

        public Weapon Weapon;
        public float InitialAngle;
        public float MaxDeviation;
        public float TurnRate;

        public Turret(string ID) : base(ID) { }
        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }
    }
}
