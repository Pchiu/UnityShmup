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
        
        GameObject shipObject = Instantiate(Resources.Load("Prefabs/TestPlayerShip")) as GameObject;
        PlayerShip ship = shipObject.GetComponent<PlayerShip>();
        ShipSection mainSection = ship.ShipSections[0].GetComponent<ShipSection>();
        

        mainSection.Subsystems = new List<Subsystem>();
        mainSection.Effects = new List<Effect>();
        mainSection.Ship = ship;
        mainSection.Hardpoints.Add(new Hardpoint(SusbsystemTypes.Weapon, new Vector2(-0.1f, 0)));
        mainSection.Hardpoints.Add(new Hardpoint(SusbsystemTypes.Weapon, new Vector2(0.1f, 0)));

        /*
        GameObject shotObject = Resources.Load("Prefabs/Beam1") as GameObject;
        ShortBeam shot = shotObject.GetComponent<ShortBeam>();
        shotObject.tag = "FriendlyShot";
        */
        
        GameObject shotObject = Resources.Load("Prefabs/BulletAnimated") as GameObject;
        Projectile shot = shotObject.GetComponent<Projectile>();
        shotObject.tag = "FriendlyShot";
        shot.MovementPattern = GameDataManager.Instance.MovementPatternManager.MovementPatterns["TestShotPattern"];

        GameObject gun1Object = Instantiate(Resources.Load("Prefabs/Gun")) as GameObject;

        Weapon gun1 = gun1Object.GetComponent<Weapon>();
        gun1.transform.parent = mainSection.transform;
        gun1.transform.position = mainSection.Hardpoints[0].Position;

        GameObject engineFlareObject = Instantiate(Resources.Load("Prefabs/EngineFlare1"), new Vector3(0, -.4f, 0), mainSection.transform.rotation) as GameObject;
        Effect engineFlare = engineFlareObject.GetComponent<Effect>();
        engineFlare.transform.parent = mainSection.transform;
        mainSection.Effects.Add(engineFlare);

        GameObject engineFlareObject2 = Instantiate(Resources.Load("Prefabs/EngineFlare1"), new Vector3(-0.1f, .2f, 0), Quaternion.LookRotation(Vector3.forward, mainSection.transform.right)) as GameObject;
        Effect engineFlare2 = engineFlareObject2.GetComponent<Effect>();
        engineFlare2.transform.parent = mainSection.transform;
        mainSection.Effects.Add(engineFlare2);

        FirePattern pattern1 = new FirePattern();
        pattern1.TimeOffsets = new List<int>();
        pattern1.Entities = new List<IDrawable>();
        pattern1.Entities.Add(shot);
        pattern1.TimeOffsets.Add(0);
        //pattern1.TimeOffsets.Add(150);
        //pattern1.TimeOffsets.Add(300);
        pattern1.TotalTime = 100;

        gun1.FirePattern = pattern1;
        gun1.FireMode = FireModes.Single;
        mainSection.Subsystems.Add(gun1);

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
