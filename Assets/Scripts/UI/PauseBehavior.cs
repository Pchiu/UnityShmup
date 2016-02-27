using UnityEngine;
using System.Collections;

public class PauseBehavior : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(ButtonNames.Cancel))
        {
            GameController.Instance.TogglePause();
        }
    }
}
