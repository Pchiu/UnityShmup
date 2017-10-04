using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.GameData
{
    public class Hardpoint
    {
        public List<SubsystemType> Types { get; set; }
        public string Group { get; set; }
        public Vector2 Position { get; set; }
        public string SubsystemID { get; set; }
    }
}
