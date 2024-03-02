using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_Single : MonoBehaviour
{
[SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 25f;
    [SerializeField, Range(0f, 100f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 15f)] private float downwardMovement = 3f;
    [SerializeField, Range(0f, 15f)] private float upwardMovement = 1.7f;


    private int jumpPhase;
    private float defaultGravityScale;
    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D rgb;
    private Ground ground;

    private bool desiredJump;
    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;


    private void Awake()
    {
        defaultGravityScale = 1f;
        Awake_GetComponent();
    }
    private void Update()
    {
        desiredJump |= Input.GetKeyDown(KeyCode.W);
        Update_Moving();
    }
    private void FixedUpdate()
    {
        FixedUpdate_Moving();

    }

    void Move(float inputVector, bool canJump)
    {
        onGround = ground.GetOnGround();
        velocity = rgb.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        if (onGround)
        {
            jumpPhase = 0;
        }
        if (desiredJump)
        {
            desiredJump = false;
            JumpAction();
        }
        if (rgb.velocity.y > 0) rgb.gravityScale = upwardMovement;
        else if (rgb.velocity.y < 0) rgb.gravityScale = downwardMovement;
        else rgb.gravityScale = defaultGravityScale;
        rgb.velocity = velocity;
    }

    private void Update_Moving()
    {

        direction.x = Input.GetAxisRaw("Horizontal");
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max((maxSpeed - ground.GetFriction()), 0f);
    }
    private void FixedUpdate_Moving()
    {
        Move(direction.x, desiredJump);
    }
    private void JumpAction()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if (velocity.y > 0)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
    }
    private void Awake_GetComponent()
    {
        rgb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

}
