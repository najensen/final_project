using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioMovement : MonoBehaviour
{
    private Rigidbody2D marioRB;
    private Camera cam;

    private float input;
    private Vector2 velo;

    public float movementSpeed = 8.0f;
    public float jumpHeight = 5.0f;
    public float jumpLen = 1.0f;
    public float jump => (2f * jumpHeight) / (jumpLen / 2f);
    public float grav => (-2f * jumpHeight) / Mathf.Pow(jumpLen / 2f, 2f);
    public int level = 1;

    public bool onGround { get; private set; }
    public bool inAir { get; private set; }

    private void Awake()
    {
        marioRB = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        Running();

        onGround = marioRB.Raycast(Vector2.down);

        if(onGround)
        {
            Jumping();
        }

        ApplyGravity();

        if(marioRB.position.y < -20.0f)
        {
            SceneManager.LoadScene("1");
        }
        if (marioRB.position.x > 200.0f)
        {
            if(level == 1)
            {
                SceneManager.LoadScene("2");
                level = 2;
            }
            else if (level == 2)
            {
                SceneManager.LoadScene("3");
                level = 3;
            }

        }
    }

    private void Running()
    {
        input = Input.GetAxis("Horizontal");
        velo.x = Mathf.MoveTowards(velo.x, input * movementSpeed, movementSpeed * Time.deltaTime);

        if (marioRB.Raycast(Vector2.right * velo.x))
        {
            velo.x = 0f;
        }
        if (velo.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velo.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void Jumping()
    {
        velo.y = Mathf.Max(velo.y, 0f);
        inAir = velo.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velo.y = jump;
            inAir = true;
        }

    }

    private void ApplyGravity()
    {
        bool falling = velo.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        velo.y += grav * multiplier * Time.deltaTime;
        velo.y = Mathf.Max(velo.y, grav / 2f);
    }

    private void FixedUpdate()
    {
        Vector2 position = marioRB.position;
        position += velo * Time.fixedDeltaTime;

        Vector2 leftSide = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightSide = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftSide.x, rightSide.x);

        marioRB.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (transform.DotTest(coll.transform, Vector2.up))
        {
            velo.y = 0f;
        }
        
    }
}

