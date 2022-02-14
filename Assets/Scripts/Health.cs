using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static int health = 3;
    public Text text;

    void Update()
    {
        if (health == 0)
            SceneManager.LoadScene(0);
        text.text = "x" + health.ToString();
    }

}
