using UnityEngine;
using System.Collections;

public class GameDataManager : MonoBehaviour {

    public static GameDataManager Instance;
    public LevelDataManager LevelManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        //DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
        LevelManager = new LevelDataManager();
        
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
