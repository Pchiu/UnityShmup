using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public class Hardpoint : MonoBehaviour {

        public List<SubsystemType> Types;
        public Vector2 Position;
        public Subsystem Subsystem;

        public Hardpoint(List<SubsystemType> Types, Vector2 Position)
        {
            this.Types = Types;
            this.Position = Position;
            this.Subsystem = null;
        }
    
        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }
    }
}
