using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_script : MonoBehaviour
{
    public GameObject gameoverUI,pauseUI,temp_snake;
    public Button restart_button,restart_button_pause, resume_button;
    public Snake snake_script;
    private void Awake()
    {
        gameoverUI.SetActive(false);
        pauseUI.SetActive(false);
    }
    void Start()
    {
       
        resume_button.onClick.AddListener(Resume);
        restart_button.onClick.AddListener(Restart);
        restart_button_pause.onClick.AddListener(Restart);
    }

    public void Resume()
    {
        Gamemanager.Instance.play_status = true;
    }
    public void Restart()
    {
        Gamemanager.Instance.Stage_reset();
        snake_script.Snake_reset();
    }
}
