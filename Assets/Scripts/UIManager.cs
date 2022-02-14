using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public void New_game()
    {
        Health.health = 3;
        Timer.timeLeft = 60F;
        MazeSpawner.n = 4;
        MazeSpawner.m = 4;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }


    public void Exit()
    {
        Application.Quit();
    }
}
