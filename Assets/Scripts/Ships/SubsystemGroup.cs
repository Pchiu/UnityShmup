using System.Collections.Generic;

namespace Assets.Scripts.Ships
{
    public class SubsystemGroup
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public List<Subsystem> Subsystems { get; set; }

        public SubsystemGroup()
        {
            Name = "";
            Enabled = true;
            Subsystems = new List<Subsystem>();
        }
    }
}