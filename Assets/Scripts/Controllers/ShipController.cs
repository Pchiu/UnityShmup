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

    public Ship SpawnShip(string name, Vector2 position)
    {
        GameObject ShipObject = Instantiate(Resources.Load("Prefabs/" + name), position, Quaternion.identity) as GameObject;
        return ShipObject.GetComponent<Ship>();   
    }

    public Enemy SpawnEnemy(string name, Vector2 position)
    {
        GameObject EnemyObject = Instantiate(Resources.Load("Prefabs/" + name), position, Quaternion.identity) as GameObject;
        return EnemyObject.GetComponent<Enemy>();
    }
}
