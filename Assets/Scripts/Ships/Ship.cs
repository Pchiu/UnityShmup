using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Base.AbstractClasses;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public class Ship : AbstractMovable {

        public List<ShipSection> ShipSections;
        public Dictionary<string, HardpointGroup> HardpointGroups;
        public List<ShipPhase> ShipPhases;
        public int CurrentPhaseIndex;

        void Start()
        {
        }

        public Ship(string ID) : base(ID) { }

        public void Initialize()
        {
            SetShipPhase(CurrentPhaseIndex);
        }

        public void SetShipPhase(int index)
        {
            if (index > ShipPhases.Count)
            {
                return;
            }

            foreach (var section in ShipSections)
            {
                Destroy(section);
            }
            //GameObject shipPhaseObject = Instantiate(Resources.Load("Prefabs/" + name), this.transform.position, this.transform.rotation) as GameObject;
            var shipPhase = ShipPhases[CurrentPhaseIndex];
            ShipSections = shipPhase.ShipSections;
            foreach (var section in ShipSections)
            {
                section.tag = this.tag;
                section.transform.parent = this.transform;
                section.transform.position = this.transform.position + this.transform.rotation * (section.transform.position);
                section.transform.rotation = this.transform.rotation;
                section.GetComponent<Renderer>().enabled = true;
                section.GetComponent<Collider2D>().enabled = true;
            }
            MovementPattern = shipPhase.MovementPattern;
            HardpointGroups = shipPhase.HardpointGroups;
            shipPhase.Ship = this;
            CurrentPhaseIndex++;
            this.Move();
        }

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
            if (!ShipSections.Any(s => s.Type == ShipSectionType.Critical))
            {
                if (CurrentPhaseIndex >= ShipPhases.Count)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    SetShipPhase(CurrentPhaseIndex);
                }
            }
        }
    }
}
