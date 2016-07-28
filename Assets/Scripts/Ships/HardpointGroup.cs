using System.Collections.Generic;

namespace Assets.Scripts.Ships
{
    public class HardpointGroup
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public List<Hardpoint> Hardpoints { get; set; }

        public HardpointGroup()
        {
            Name = "";
            Enabled = true;
            Hardpoints = new List<Hardpoint>();
        }
    }
}