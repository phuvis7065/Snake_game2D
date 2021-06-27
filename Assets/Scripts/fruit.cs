using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit : MonoBehaviour
{
    // Start is called before the first frame update
    Fruit fruit_;
    void Start()
    {
        fruit_ = new Fruit("apple", 1);
    }
    private void Update()
    {
        if (Gamemanager.Instance.check_restart)
        {
            Gamemanager.Instance.check_restart = false;
            transform.position = new Vector3(7, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            StartCoroutine(Delay(.05f));          
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            fruit_.Fruit_reposition();
        }
    }
    IEnumerator Delay(float sec)
    {
        yield return new WaitForSeconds(sec);
        fruit_.Fruit_type_set();
        fruit_.Score_up();
    }

}
