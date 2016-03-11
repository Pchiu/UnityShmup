using UnityEngine;
using System.Collections;

public class ParrySubsystem : Subsystem {

    private GameObject ParryShield;
    public float Duration;
    public float Cooldown;

    void Awake()
    {
        Ready = true;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Action()
    {
        if (!Ready)
        {
            return;
        }
        Ready = false;
        StartCoroutine(ActivateParryShield());
    }

    public IEnumerator ActivateParryShield()
    {
        float elapsedTime = 0f;
        float totalTime = Duration + Cooldown;

        Sprite parentSprite = ParentShip.GetComponent<SpriteRenderer>().sprite;
        PolygonCollider2D parentCollider = ParentShip.GetComponent<PolygonCollider2D>();

        ParryShield = new GameObject();
        SpriteRenderer shieldSpriteRenderer = ParryShield.AddComponent<SpriteRenderer>();
        Rigidbody2D shieldRigidbody = ParryShield.AddComponent<Rigidbody2D>();
        PolygonCollider2D shieldCollider = ParryShield.AddComponent<PolygonCollider2D>();
        shieldCollider = parentCollider;

        shieldSpriteRenderer.sprite = parentSprite;
        shieldSpriteRenderer.color = Color.cyan;
        shieldRigidbody.gravityScale = 0;
        shieldRigidbody.isKinematic = true;

        ParryShield.transform.position = transform.position;
        ParryShield.transform.rotation = transform.rotation;
        ParryShield.transform.parent = transform;
         
        while (elapsedTime < Duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(ParryShield);

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Ready = true;
    }
}
