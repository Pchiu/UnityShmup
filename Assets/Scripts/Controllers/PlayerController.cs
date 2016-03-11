using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class PlayerController : MonoBehaviour {

    public PlayerShip PlayerShip;
    public bool InputEnabled;
    public bool AllRange;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    // Use this for initialization
    void Start () {
        SetBounds();
        InputEnabled = true;
        AllRange = true;
        //CreateTestShip();
    }

    public void CreateTestShip()
    {
        GameObject shipObject = Instantiate(Resources.Load("Prefabs/Fighter")) as GameObject;
        PlayerShip ship = shipObject.GetComponent<PlayerShip>();
        ship.Subsystems = new List<Subsystem>();
        ship.Effects = new List<Effect>();

        GameObject shipCoreObject = Instantiate(Resources.Load("Prefabs/ShipCore")) as GameObject;
        ShipCore shipCore = shipCoreObject.GetComponent<ShipCore>();
        shipCoreObject.transform.parent = ship.transform;
        shipCore.Ship = ship;
        shipCore.tag = "Friendly";
        ship.Core = shipCore;

        ship.Hardpoints.Add(new Hardpoint(SusbsystemTypes.Weapon, new Vector2(-0.1f, 0)));
        ship.Hardpoints.Add(new Hardpoint(SusbsystemTypes.Weapon, new Vector2(0.1f, 0)));

        /*
        GameObject shotObject = Resources.Load("Prefabs/Beam1") as GameObject;
        Shot shot = shotObject.GetComponent<Shot>();
        shotObject.tag = "FriendlyShot";
        */
        
        GameObject shotObject = Resources.Load("Prefabs/BulletAnimated") as GameObject;
        Projectile shot = shotObject.GetComponent<Projectile>();
        shotObject.tag = "FriendlyShot";
        shot.MovementPattern = GameDataManager.Instance.MovementPatternManager.MovementPatterns["TestShotPattern"];

        GameObject gun1Object = Instantiate(Resources.Load("Prefabs/Gun")) as GameObject;

        Weapon gun1 = gun1Object.GetComponent<Weapon>();
        gun1.transform.parent = ship.transform;
        gun1.transform.position = ship.Hardpoints[0].Position;
        gun1.ParentShip = PlayerShip;

        GameObject engineFlareObject = Instantiate(Resources.Load("Prefabs/EngineFlare1"), new Vector3(0, -.4f, 0), ship.transform.rotation) as GameObject;
        Effect engineFlare = engineFlareObject.GetComponent<Effect>();
        engineFlare.transform.parent = ship.transform;
        ship.Effects.Add(engineFlare);

        GameObject engineFlareObject2 = Instantiate(Resources.Load("Prefabs/EngineFlare1"), new Vector3(-0.1f, .2f, 0), Quaternion.LookRotation(Vector3.forward, ship.transform.right)) as GameObject;
        Effect engineFlare2 = engineFlareObject2.GetComponent<Effect>();
        engineFlare2.transform.parent = ship.transform;
        ship.Effects.Add(engineFlare2);

        FirePattern pattern1 = new FirePattern();
        pattern1.TimeOffsets = new List<int>();
        pattern1.Entities = new List<Entity>();
        pattern1.Entities.Add(shot);
        pattern1.TimeOffsets.Add(0);
        pattern1.TimeOffsets.Add(100);
        pattern1.TimeOffsets.Add(200);
        pattern1.TotalTime = 500;

        gun1.FirePattern = pattern1;
        gun1.FireMode = FireModes.Single;
        ship.Subsystems.Add(gun1);

        GameObject parryShieldObject = new GameObject();
        parryShieldObject.transform.parent = ship.transform;
        ParrySubsystem parryShield = parryShieldObject.AddComponent<ParrySubsystem>();
        parryShield.ParentShip = ship;
        parryShield.Type = Enums.SusbsystemTypes.Ability;
        parryShield.Cooldown = 1.0f;
        parryShield.Duration = 1.0f;

        ship.Subsystems.Add(parryShield);
        

        PlayerShip = ship;
    }

    // Update is called once per frame
    void Update () {
        if (InputEnabled)
        {
            if (AllRange)
            {
                RotateShip();
            }
            MoveShip();
            if (Input.GetButtonDown("Fire1"))
            {
                PlayerShip.ToggleWeapons(true);
            }
            if (Input.GetButtonUp("Fire1"))
            {
                PlayerShip.ToggleWeapons(false);
            }
            if (Input.GetButtonDown("Fire2"))
            {
                PlayerShip.ActivateParry();
            }
        }
    }

    public void SetBounds()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = left.x;
        xMax = right.x;
        Vector3 top = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        yMin = bottom.y;
        yMax = top.y;
    }

    private void MoveShip()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        PlayerShip.transform.position += move * PlayerShip.Speed * Time.deltaTime;
        var clampedX = Mathf.Clamp(PlayerShip.transform.position.x, xMin, xMax);
        var clampedY = Mathf.Clamp(PlayerShip.transform.position.y, yMin, yMax);
        PlayerShip.transform.position = new Vector3(clampedX, clampedY, PlayerShip.transform.position.z);
    }

    private void RotateShip()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, mousePos - PlayerShip.transform.position);
        PlayerShip.transform.rotation = Quaternion.RotateTowards(PlayerShip.transform.rotation, targetRotation, PlayerShip.TurnRate * Time.deltaTime);
    }

    public bool IsPlayerAlive()
    {
        return (PlayerShip != null);
    }
}
