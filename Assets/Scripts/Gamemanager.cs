using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    private static Gamemanager _instance;
    public static Gamemanager Instance { get { return _instance; } }
    public int score;
    public int speed_status = 1;
    public  bool play_status = true,check_restart = false,check_gameover = false;
    public GameObject gameoverUI, pauseUI;
    public Button restart_button, restart_button_pause, resume_button;
    public Snake snake_script;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        gameoverUI.SetActive(false);
        pauseUI.SetActive(false);
    }
    private void Start()
    {
        resume_button.onClick.AddListener(Resume);
        restart_button.onClick.AddListener(Restart);
        restart_button_pause.onClick.AddListener(Restart);
    }
    public void Resume()
    {      
        pauseUI.SetActive(false);
        gameoverUI.SetActive(false);
        play_status = true;
    }
    public void Pause()
    {
        pauseUI.SetActive(true);
        play_status = false;
    }
    public void Restart()
    {
        check_restart = true;
        check_gameover = false;
        Stage_reset();
        snake_script.Snake_reset();
        Resume();
    }
    public void Stage_reset()
    {
        score = 0;
        speed_status = 1;
        play_status = true;
    }
    int high_score;
    public Text High_Score_text,Score_text;
    public void Show_GameoverUI()
    {
        check_gameover = true;
        high_score = PlayerPrefs.GetInt("Highscore");
        if (score > high_score)
        {
            PlayerPrefs.SetInt("Highscore", score);
            high_score = PlayerPrefs.GetInt("Highscore");
        }
        High_Score_text.text = "HighScore: "+high_score.ToString();
        gameoverUI.SetActive(true);     
    }
    public void Score_update()
    {
        Score_text.text = "Score: "+score.ToString();
    }   
}
public class Fruit : MonoBehaviour
{
    public GameObject fruit;
    public string name;
    public int fruit_score = 0;
    public SpriteRenderer fruit_sprite;
    public int posx_min = -25, posx_max = 25, posy_min = -13, posy_max = 10;
    public Fruit(string name,int fruit_score)
    {
        fruit = GameObject.FindGameObjectWithTag("apple");
        this.name = name;
        this.fruit_score = fruit_score;
        Fruit_setup(name);
    }
    public void Fruit_setup(string name)
    {
        fruit.tag = name;
        fruit_sprite = GameObject.FindGameObjectWithTag("fruit_sprite").GetComponent<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>(name);
        fruit_sprite.sprite = sprite;
    }
    public void Fruit_reposition()
    {
        int posx = Random.Range(this.posx_min + 1, this.posx_max);
        int posy = Random.Range(this.posy_min + 1, this.posy_max);
        fruit.transform.position = new Vector3(posx, posy, 0);      
    }
    public void Fruit_type_set()
    {
        float random_temp = Random.Range(1, 101);
        if (Gamemanager.Instance.speed_status == 1)
        {            
            if (random_temp < 33)
            {
                Fruit_setup("grape");
            }
            else
            {
                Fruit_setup("apple");
            }
        }
        else if (Gamemanager.Instance.speed_status == 4)
        {
            if (random_temp < 33)
            {
                Fruit_setup("durian");
            }
            else
            {
                Fruit_setup("apple");
            }
        }
        else
        {
            if (random_temp < 20)
            {
                Fruit_setup("durian");
            }
            else if(random_temp >80)
            {
                Fruit_setup("grape");
            }
            else if (random_temp >= 20 && random_temp <= 80)
            {
                Fruit_setup("apple");
            }
        }
        Fruit_reposition();
    }
    public void Score_up()
    {
        Gamemanager.Instance.score += fruit_score;
        Gamemanager.Instance.Score_update();
    }
}
