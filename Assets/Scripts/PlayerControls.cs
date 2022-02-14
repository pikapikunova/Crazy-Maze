using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public float Speed;
    public static bool isPaused = false;
    private Rigidbody componentRigidbody;
    public GameObject Game_menu;
    public GameObject IndicatesForUser;
    private float h;
    private float v;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody>();
        IndicatesForUser.SetActive(true);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }

    }

    public void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0f, v);
        componentRigidbody.AddForce(movement*Speed);
    }

    public void Pause()
    {
        
        Game_menu.SetActive(true);
        IndicatesForUser.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {

        Game_menu.SetActive(false);
        IndicatesForUser.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "newLev")
        {
            MazeSpawner.n += 3;
            MazeSpawner.m += 3;
            Timer.timeLeft = 60F;
            SceneManager.LoadScene(1);
        }
        
        if (collision.gameObject.tag == "dangerous")
        {
            Health.health--;
            Timer.timeLeft = 60F;
            transform.position = new Vector3(0, 1.1F, 0);
        }

        
    }

}