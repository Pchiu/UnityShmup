using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public class ShipPhase
    {
        public ShipPhase(Ship ship)
        {
            this.Ship = ship;
            this.ShipSections = new List<ShipSection>();
            this.MovementPattern = null;
            this.HardpointGroups = new Dictionary<string, HardpointGroup>();
        }

        public List<ShipSection> ShipSections { get; set; }
        public MovementPattern MovementPattern { get; set; }
        public Dictionary<string, HardpointGroup> HardpointGroups { get; set; }
        public Ship Ship { get; set; }
    }
}
