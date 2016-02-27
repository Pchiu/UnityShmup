using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

    public static GameController Instance;
    public LevelController LevelController;
    public PlayerController PlayerController;
    public ShipController ShipController;

    public bool Paused;

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
        GameDataManager.Instance.MovementPatternManager.CreateTestMovementPattern();

        Enemy enemy = ShipController.SpawnEnemy("Enemy", new Vector2(-5, 0));
        enemy.MovementPattern = GameDataManager.Instance.MovementPatternManager.MovementPatterns["TestPattern"];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TogglePause()
    {
        Paused = !Paused;
        PlayerController.InputEnabled = !Paused;
    }
}
