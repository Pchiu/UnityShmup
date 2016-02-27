using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void QuitToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}
