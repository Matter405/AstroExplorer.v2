using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }

    void ReloadLevel()
    {
        int activeSceneIndex =
            SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}
