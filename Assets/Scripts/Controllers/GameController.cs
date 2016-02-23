using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

    public LevelController LevelController;
    // Use this for initialization
    void Start () {
        //LevelManager = gameObject.AddComponent<LevelManager>();
        GameDataManager.Instance.LevelManager.CreateTestLevel();
        LevelController.SetActiveLevel("Level1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
