using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Mario mario = collision.gameObject.GetComponent<Mario>();
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                Destroy(gameObject);
            }
            else
            {
                SceneManager.LoadScene("1");
            }
        }
    }
}
