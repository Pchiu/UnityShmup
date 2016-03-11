using UnityEngine;
using System.Collections;

public class ShortBeam : Shot {

    public float BeamLength;
    public GameObject BeamStart;
    public GameObject BeamMiddle;    
    private float BeamMiddleHeight;
    private float BeamStartHeight;
	// Use this for initialization
	void Start () {
        BeamStartHeight = BeamStart.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        BeamMiddleHeight = BeamMiddle.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
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
        var beamStartObject = Instantiate(BeamStart, transform.position + transform.TransformDirection(new Vector3(0, BeamStartHeight / 2, 0)), transform.rotation) as GameObject;
        var beamMiddleObject= Instantiate(BeamMiddle, transform.position + transform.TransformDirection(Vector3.up) * (BeamLength / 2) + transform.TransformDirection(new Vector3(0, BeamStartHeight/2, 0)), transform.rotation) as GameObject;
        beamStartObject.GetComponent<SpriteRenderer>().color = this.Color;
        beamMiddleObject.GetComponent<SpriteRenderer>().color = this.Color;
        beamMiddleObject.transform.localScale = new Vector3(1, (BeamLength - BeamStartHeight) * (1 / BeamMiddleHeight) , 1);
    }
}
