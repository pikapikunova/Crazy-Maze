using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timeLeft = 60F;
    public Text text;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && Health.health != 0)
        {
            Health.health--;
            timeLeft = 60F;
            transform.position = new Vector3(0, 1.1F, 0);
        }
        else
            if(timeLeft < 0 && Health.health == 0)
                SceneManager.LoadScene(0);
        text.text = timeLeft.ToString();
    }
}
