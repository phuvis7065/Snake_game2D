using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 direction = Vector2.right;
    List<Transform> tails = new List<Transform>();
    public Transform tailprefab;
    Rigidbody2D rb;
    float movetime, sec_to_move;
    public bool _can_move = true;
    public int speed = 1;
    bool temp = true;
    void Start()
    {
        sec_to_move = .4f;
        movetime = sec_to_move;
        tails.Add(transform);
    }
    // Update is called once per frame
    void Update()
    {
        Snake_movement_Input(_can_move);
        if (Input.GetKeyDown(KeyCode.Escape) && !Gamemanager.Instance.check_gameover)
        {
            temp = !temp;
            if (!temp)
            {
                Gamemanager.Instance.Pause();
            }
            else if (temp)
            {
                Gamemanager.Instance.Resume();
            }       
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Snake_reset();
        }

    }
    private void FixedUpdate()
    {
        if (Gamemanager.Instance.play_status)
        {
            Move();
            Snake_head_rotate(direction);
        }
        else
        {        
        }
    }
    void Snake_head_rotate(Vector2 direction)
    {
        float temp = Mathf.Atan2((direction.y), (direction.x)) * Mathf.Rad2Deg;
        if (temp < 0)
        {
            temp += 360;
        }
        transform.eulerAngles = new Vector3(0, 0, temp);
    }
    void Move()
    {
        movetime += Time.deltaTime;
        if(movetime >= sec_to_move)
        {
            Snake_tail_move_follow();
            Snake_move();
            movetime -= sec_to_move;
            _can_move = true;
        }    
    }
    void Snake_move()
    {
        transform.position = new Vector3(Mathf.Round((transform.position.x)) + direction.x,
                                        Mathf.Round((transform.position.y)) + direction.y,
                                        0);
    }
    void Snake_tail_move_follow()
    {
        for (int i = tails.Count - 1; i > 0; i--)
        {
            tails[i].position = tails[i - 1].position;
        }
    }
    string dir_arrow;
    void Snake_movement_Input(bool can_move)
    {
        if (can_move && Gamemanager.Instance.play_status)
        {        
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                dir_arrow = "right";          
                direction = Change_direction(direction, dir_arrow);
                _can_move = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                dir_arrow = "left";                
                direction = Change_direction(direction, dir_arrow);
                _can_move = false;     
            }
        }      
    }
    public Vector2 Change_direction(Vector2 direction,string dir_arrow)
    {
        Vector2 dir = Vector2.right;
        if(dir_arrow == "right")
        {
            if (direction == Vector2.right)
            {
                dir = Vector2.down;
            }
            else if (direction == Vector2.left)
            {
                dir = Vector2.up;
            }
            else if (direction == Vector2.up)
            {
                dir = Vector2.right;
            }
            else if (direction == Vector2.down)
            {
                dir = Vector2.left;
            }
        }
        if(dir_arrow == "left")
        {
            if (direction == Vector2.right)
            {
                dir = Vector2.up;
            }
            else if (direction == Vector2.left)
            {
                dir = Vector2.down;
            }
            else if (direction == Vector2.up)
            {
                dir = Vector2.left;
            }
            else if (direction == Vector2.down)
            {
                dir = Vector2.right;
            }
        }
        return dir;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "tail" || collision.gameObject.tag == "Obstacle")
        {
            Gamemanager.Instance.play_status = false;
            Gamemanager.Instance.Show_GameoverUI();
        }
        if (collision.gameObject.tag == "apple" || collision.gameObject.tag == "grape" || collision.gameObject.tag == "durian")
        {            
            Eat(collision.gameObject.tag);
        }        
    }
    private void Eat(string fruit)
    {
        switch (fruit)
        {
            case "apple":
                Snake_grow();
                break;
            case "grape":
                Snake_speed_change(1);
                break;
            case "durian":
                Snake_speed_change(-1);
                break;
        }
    }
    void Snake_grow()
    {
        tails.Add(Instantiate(tailprefab, transform.position, Quaternion.identity).transform);
    }
    void Snake_speed_change(int spd)
    {
        if (speed >= 1 || speed <= 4)
        {
            speed += spd;
            if (speed < 1)
            {
                speed = 1;
            }
            if (speed > 4)
            {
                speed = 4;
            }
        }
        switch (speed)
        {
            case 1:
                sec_to_move = .4f;
                break;
            case 2:
                sec_to_move = .2f;
                break;
            case 3:
                sec_to_move = .1f;
                break;
            case 4:
                sec_to_move = .065f;
                break;
        }
        Gamemanager.Instance.speed_status = speed;
    }
   public void Snake_reset()
    {
        for (int i = 1; i < tails.Count; i++)
        {
            Destroy(tails[i].gameObject);
        }
        tails.Clear();
        tails.Add(this.transform);
        speed = 1;
        sec_to_move = .4f;
        direction = Vector2.right;
        this.transform.position = new Vector3(0,0,0);
    }
}
