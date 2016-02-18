using UnityEngine;
using System.Collections;
using Enums;

public abstract class Subsystem : Entity {

    public SusbsystemTypes Type;
    public bool Active;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Action()
    {

    }

    public virtual void ToggleAction(bool toggle)
    {

    }
}
