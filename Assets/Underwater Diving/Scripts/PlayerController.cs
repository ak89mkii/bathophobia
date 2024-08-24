using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool rushing = false;
    private float speedMod = 0;

    float timeLeft = 2f;

    private Rigidbody2D myRigidBody;
    private Animator myAnim;

    public GameObject bubbles;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        ResetBoostTime();
        ControllerManager();

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
    }

    void ControllerManager()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            MovePlayer(horizontalInput, verticalInput);
        }

        if (Input.GetButtonDown("Jump") && !rushing)
        {
            StartBoost();
        }
    }

    void MovePlayer(float horizontalInput, float verticalInput)
    {
        float currentSpeed = rushing ? moveSpeed + speedMod : moveSpeed;
        Vector2 newVelocity = new Vector2(horizontalInput * currentSpeed, verticalInput * currentSpeed);
        myRigidBody.velocity = newVelocity;
    }

    void StartBoost()
    {
        rushing = true;
        speedMod = moveSpeed; // Boost doubles the move speed
        Instantiate(bubbles, transform.position, transform.rotation);
    }

    void ResetBoostTime()
    {
        if (rushing)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                rushing = false;
                speedMod = 0;
                timeLeft = 2f; // Reset the boost timer for next boost
            }
        }
    }

    public void Hurt()
    {
        if (!rushing)
        {
            myAnim.Play("PlayerHurt");
        }
    }
}
