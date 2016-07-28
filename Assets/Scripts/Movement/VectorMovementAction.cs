namespace Assets.Scripts.Movement
{
    public class VectorMovementAction : MovementAction {

        public float Angle;
        public float Speed;
        public float TurnTime;

        public VectorMovementAction(float angle, float speed, float turnTime, float time, string referenceFrame)
        {
            this.Angle = angle;
            this.Speed = speed;
            this.TurnTime = turnTime;
            this.Time = time;
            this.ReferenceFrame = referenceFrame;
        }
        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }
    }
}
