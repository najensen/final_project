using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public Vector2 direction = Vector2.left;

    private Rigidbody2D enemyRB;
    private Vector2 velo;
    
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnEnable()
    {
        enemyRB.WakeUp();
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnDisable()
    {
        enemyRB.velocity = Vector2.zero;
        enemyRB.Sleep();
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void FixedUpdate()
    {
        velo.x = direction.x * speed;
        velo.y += Time.fixedDeltaTime * Physics2D.gravity.y;

        enemyRB.MovePosition(enemyRB.position + Time.fixedDeltaTime*velo);

        if (enemyRB.Raycast(direction))
        {
            direction = -direction;
        }

        if (enemyRB.Raycast(Vector2.down))
        {
            velo.y = Mathf.Max(velo.y, 0f);
        }

        if (direction.x > 0f)
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (direction.x < 0f)
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
