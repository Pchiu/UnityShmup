using UnityEngine;
using System.Collections;

public class Beam : Shot {

    public float BeamLength;
    private float BeamMiddleHeight;
    private float BeamStartHeight;
    public GameObject BeamMiddle;
    public GameObject BeamStart;
    // Use this for initialization
    void Start () {
        BeamLength = 20f;
        BeamMiddleHeight = BeamMiddle.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        BeamStartHeight = BeamStart.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        Destroy(this.gameObject);
    }
}
