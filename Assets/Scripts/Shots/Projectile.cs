using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : AbstractMovableCollidable, IShot {

    public int damage;
    public SpawnPattern firePattern;
    public GameObject hitAnimation;
    public Color color;
    public float MaxDuration;
    public MovementPattern MovementPattern;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public SpawnPattern FirePattern
    {
        get { return firePattern; }
        set { firePattern = value; }
    }

    public GameObject HitAnimation
    {
        get { return hitAnimation; }
        set { hitAnimation = value; }
    }

    public Color Color
    {
        get { return color; }
        set { color = value; }
    }
    public Projectile(string ID) : base(ID) { }

    // Use this for initialization
    void Awake()
    {
        CurrentWaypoints = new List<Vector2>();
    }

    void Start () {
        Destroy(gameObject, MaxDuration);
        Move();
    }
	    
    void OnTriggerEnter2D(Collider2D collider)
    {
        ShipSection section = collider.gameObject.GetComponent<ShipSection>();
        if (section != null)
        {
            if (this.tag == "FriendlyShot" && section.tag == "Hostile" ||
                this.tag == "HostileShot" && section.tag == "Friendly")
            {
                section.TakeDamage(Damage);
                Instantiate(HitAnimation, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}