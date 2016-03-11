using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

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

    public Enemy SpawnEnemy(string name, Vector2 position, float rotation)
    {
        GameObject EnemyObject = Instantiate(Resources.Load("Prefabs/" + name), position, Quaternion.AngleAxis(rotation, Vector3.forward)) as GameObject;
        return EnemyObject.GetComponent<Enemy>();
    }

    public Ship SpawnTestEnemy(string name, Vector2 position, float rotation)
    {
        GameObject EnemyObject = Instantiate(Resources.Load("Prefabs/" + name), position, Quaternion.AngleAxis(rotation, Vector3.forward)) as GameObject;
        Ship ship = EnemyObject.GetComponent<Ship>();

        GameObject shotObject = Resources.Load("Prefabs/BulletAnimated") as GameObject;
        Projectile shot = shotObject.GetComponent<Projectile>();
        shotObject.tag = "HostileShot";
        shot.MovementPattern = GameDataManager.Instance.MovementPatternManager.MovementPatterns["StraightShotTestPattern"];

        GameObject gun1Object = Instantiate(Resources.Load("Prefabs/Gun")) as GameObject;

        Weapon gun1 = gun1Object.GetComponent<Weapon>();
        gun1.transform.parent = ship.transform;
        gun1.transform.position = ship.transform.position;
        gun1.transform.rotation = ship.transform.rotation;
        gun1.ParentShip = ship;

        FirePattern pattern1 = new FirePattern();
        pattern1.TimeOffsets = new List<int>();
        pattern1.Entities = new List<Entity>();
        pattern1.Entities.Add(shot);
        pattern1.TimeOffsets.Add(0);
        pattern1.TotalTime = 100;

        gun1.FirePattern = pattern1;
        gun1.FireMode = FireModes.Single;
        ship.Subsystems.Add(gun1);

        return ship;
    }
}
