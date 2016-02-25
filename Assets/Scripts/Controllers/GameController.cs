using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

    public LevelController LevelController;
    public PlayerController PlayerController;
    public static GameController Instance;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Use this for initialization
    void Start () {

        GameDataManager.Instance.LevelManager.CreateTestLevel();
        PlayerController.CreateTestShip();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
