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
        public List<SubsystemType> Classes { get; set; }
        public string Group { get; set; }
        public Vector2 Origin { get; set; }
        public string SubsystemID { get; set; }
    }
}
