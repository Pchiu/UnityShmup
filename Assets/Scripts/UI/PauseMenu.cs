using UnityEngine;
using System.Collections;

public class PauseMenu : GameMenu {
    
    public void Resume()
    {
        GameController gameController = GameController.Instance;
        gameController.TogglePause();
    }
}
