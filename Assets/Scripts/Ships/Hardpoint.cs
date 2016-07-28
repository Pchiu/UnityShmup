using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public class Hardpoint : MonoBehaviour {

        public SusbsystemTypes Type;
        public Vector2 Position;

        public Hardpoint(SusbsystemTypes Type, Vector2 Position)
        {
            this.Type = Type;
            this.Position = Position;
        }
    
        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }
    }
}
