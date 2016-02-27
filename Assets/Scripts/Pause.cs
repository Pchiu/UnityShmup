using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    
    GameController game;

	void Start () {
        game = GameController.Instance;
    }
	
	void Update () {
        if (Input.GetButtonDown(ButtonNames.Cancel))
        {
            game.TogglePause();
            Time.timeScale = game.Paused ? 0 : 1;

            if (game.Paused)
            {
                SceneManager.LoadScene(SceneNames.PauseMenu, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadScene(SceneNames.PauseMenu);
            }
        }
    }
}
