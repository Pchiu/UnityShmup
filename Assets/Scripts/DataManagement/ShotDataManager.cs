using System.Collections.Generic;
using Assets.Scripts.Base.Interfaces;
using UnityEngine;

namespace Assets.Scripts.DataManagement
{
    public class ShotDataManager : MonoBehaviour {

        public Dictionary<string, IShot> Shots;

        public bool AddShot()
        {
            return true;
        }
    }
}
