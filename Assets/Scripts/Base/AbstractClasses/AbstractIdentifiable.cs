using UnityEngine;

namespace Assets.Scripts.Base.AbstractClasses
{
    public class AbstractIdentifiable : MonoBehaviour {

        public string ID
        {
            get { return ID; }
        }

        public AbstractIdentifiable(string ID)
        {

        }
    }
}
