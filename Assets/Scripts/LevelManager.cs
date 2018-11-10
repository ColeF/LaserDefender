using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
        EnemyController.fireRate = 0.25f;
        EnemyController.shotSpeed = 3;
    }

    public void QuitRequest()
    {
        Debug.Log("I want to quit.");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel + 1);
        EnemyController.fireRate = 0.25f;
        EnemyController.shotSpeed = 3;
    }
}
