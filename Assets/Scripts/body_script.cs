using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_script : MonoBehaviour
{
    BoxCollider2D col;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        StartCoroutine(Delay_collider());
    }
    IEnumerator Delay_collider()
    {
        yield return new WaitForSeconds(1f);
        col.enabled = true;
    }
}
