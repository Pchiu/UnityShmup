using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : GameMenu {

    public void StartGame()
    {
        SceneManager.LoadScene(SceneNames.MainScene);
    }
}
