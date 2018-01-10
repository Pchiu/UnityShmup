using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Enums;
using Action = Assets.Scripts.Actions.Action;

namespace Assets.Scripts.Behaviors
{
    public class DeathBehavior
    {
        public DeathBehaviorType Type { get; set; }
        public ShipSectionType SectionType { get; set; }
        public List<Action> ActionQueue { get; set; }
        public bool DisableCollider { get; set; }
    }
}
