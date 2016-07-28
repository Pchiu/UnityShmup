using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Levels
{
    public class Area {

        public AreaTypes AreaType;
        public int Time;
        public float Speed;
        public string AreaBackgroundImage;
        public List<Doodad> Doodads;

        void Start()
        {
        
        }
    }
}