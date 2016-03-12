using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour {

    public List<Ship> Ships;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Ship SpawnShip(string name, Vector2 position, float rotation)
    {
        GameObject ShipObject = Instantiate(Resources.Load("Prefabs/" + name), position, Quaternion.AngleAxis(rotation, Vector3.forward)) as GameObject;
        return ShipObject.GetComponent<Ship>();   
    }
}
