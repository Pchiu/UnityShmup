using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;

public class LevelManager : MonoBehaviour {

    public List<Level> Levels;
    public List<AreaTile> Tiles;
    public static LevelManager instance = null;
	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }
	}

    // Update is called once per frame
    void Update()
    {
    }
}
