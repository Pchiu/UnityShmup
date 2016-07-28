using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Base.AbstractClasses;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Ships
{
    public class Ship : AbstractMovable {

        public List<ShipSection> ShipSections;
        public Dictionary<string, HardpointGroup> HardpointGroups;

        public Ship(string ID) : base(ID) { }

        public void ToggleWeapons(bool toggle)
        {
            var group = HardpointGroups["Weapons"];
            foreach (Hardpoint hardpoint in group.Hardpoints)
            {
                if (hardpoint.Subsystem != null)
                {
                    hardpoint.Subsystem.ToggleAction(toggle);
                }
            }
        }

        public void CheckCriticalSections()
        {
            if (ShipSections.Any(s => s.Type == ShipSectionTypes.Critical))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
