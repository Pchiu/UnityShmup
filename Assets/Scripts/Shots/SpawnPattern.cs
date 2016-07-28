using System.Collections.Generic;
using Assets.Scripts.Base.Interfaces;

namespace Assets.Scripts.Shots
{
    public class SpawnPattern {

        public List<IDrawable> Entities;
        public List<int> TimeOffsets;
        // Use this for initialization
        void Start () {
            Entities = new List<IDrawable>();
            TimeOffsets = new List<int>();
        }
    }
}
