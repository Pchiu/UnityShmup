using UnityEngine;
using System.Collections;
using Assets.Scripts.Enums;

public class Effect : MonoBehaviour {

    public EffectTypes EffectType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
