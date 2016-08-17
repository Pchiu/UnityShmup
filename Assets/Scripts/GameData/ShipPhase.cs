using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Movement;

namespace Assets.Scripts.GameData
{
    public class ShipPhase
    {
        public MovementPattern MovementPattern { get; set; }
        public List<GameData.ShipSection> ShipSections { get; set; }
    }
}
