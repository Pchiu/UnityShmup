using UnityEngine;
using System.Collections;

public class ShortBeam : Shot {

    public float BeamLength;
    public GameObject BeamStartObject;
    public GameObject BeamMiddleObject;    
    private float BeamMiddleHeight;
    private float BeamStartHeight;
    private GameObject BeamMiddle;
    private GameObject BeamStart;
	// Use this for initialization
	void Start () {
        BeamLength = 20f;
        BeamMiddleHeight = BeamMiddleObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        BeamStartHeight = BeamStartObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        Fire();
        Destroy(this.gameObject);
    }

    public void Fire()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, BeamLength);
        if (hit.collider != null)
        {
            BeamLength = Vector2.Distance(hit.point, transform.position);
            var ship = hit.collider.gameObject.GetComponent<Ship>();
            if (ship)
            {
                if (this.gameObject.tag == "FriendlyShot" && ship.tag == "Hostile" ||
                    this.gameObject.tag == "HostileShot" && ship.tag == "Friendly")
                {
                    ship.TakeDamage(Damage);
                }
            }
        }
        BeamStart = Instantiate(BeamStartObject, transform.position + transform.TransformDirection(new Vector3(0, BeamStartHeight / 2, 0)), transform.rotation) as GameObject;
        BeamMiddle = Instantiate(BeamMiddleObject, transform.position + transform.TransformDirection(Vector3.up) * (BeamLength / 2) + transform.TransformDirection(new Vector3(0, BeamStartHeight/2, 0)), transform.rotation) as GameObject;
        BeamStart.GetComponent<SpriteRenderer>().color = this.Color;
        BeamMiddle.GetComponent<SpriteRenderer>().color = this.Color;
        BeamMiddle.transform.localScale = new Vector3(1, (BeamLength - BeamStartHeight) * (1 / BeamMiddleHeight) , 1);
    }

    
}
