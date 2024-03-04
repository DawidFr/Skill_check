using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Player_Movement_Single : MonoBehaviour
{
    #region localStats
    private float local_maxSpeed = 4f;
    private float local_maxAcceleration = 35f;
    private float local_maxAirAcceleration = 25f;
    private float local_jumpHeight = 3f;
    private int local_maxAirJumps = 0;
    private float local_fallingGravity = 3f;
    private float local_jumpingGravity = 1.7f;
    
    #endregion
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
    [SerializeField] private Player_Stats p_Stats;
    private bool lookRight;
    private Player_Animations animmer;
    private void Start(){
        animmer = GetComponent<Player_Animations>();
        p_Stats = GetComponent<Player_Stats>();
        UpdateStats();
        SubscribeToStatsChanges();
        
        defaultGravityScale = 1f;
        Awake_GetComponent();
    }
    
    private void SubscribeToStatsChanges()
    {
        p_Stats.maxSpeed.OnStatValueChanged += UpdateHorizontalValue;
        p_Stats.maxAcceleration.OnStatValueChanged += UpdateHorizontalValue;
        p_Stats.maxAirAcceleration.OnStatValueChanged += UpdateHorizontalValue;
        p_Stats.jumpHeight.OnStatValueChanged += UpdateVerticalValue;
        p_Stats.maxAirJumps.OnStatValueChanged += UpdateVerticalValue;
        p_Stats.fallingGravity.OnStatValueChanged += UpdateVerticalValue;
        p_Stats.jumpingGravity.OnStatValueChanged += UpdateVerticalValue;
    }

    private void UpdateStats()
    {
        UpdateHorizontalValue();
        UpdateVerticalValue();
    }

    private void UpdateVerticalValue()
    {
        local_jumpHeight = p_Stats.jumpHeight.GetValue();
        local_fallingGravity = p_Stats.fallingGravity.GetValue();
        local_jumpingGravity = p_Stats.jumpingGravity.GetValue();
        local_maxAirJumps = (int)MathF.Round(p_Stats.maxAirJumps.GetValue(), 0);

    }

    private void UpdateHorizontalValue()
    {
        local_maxAcceleration = p_Stats.maxAcceleration.GetValue();
        local_maxAirAcceleration = p_Stats.maxAirAcceleration.GetValue();
        local_maxSpeed = p_Stats.maxSpeed.GetValue();
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

    void Move()
    {
        onGround = ground.OnGround;
        velocity = rgb.velocity;

        acceleration = onGround ? local_maxAcceleration : local_maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        if (onGround)
        {
            jumpPhase = 0;
        }
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }
        if (rgb.velocity.y > 0) rgb.gravityScale = local_jumpingGravity;
        else if (rgb.velocity.y < 0) rgb.gravityScale = local_fallingGravity;
        else rgb.gravityScale = defaultGravityScale;
        rgb.velocity = velocity;
    }

    private void Update_Moving()
    {

        direction.x = Input.GetAxisRaw("Horizontal");
        if (direction.x != 0){
            animmer.PlayAnimation(Player_Animations.AnimationState.running);
            if ((direction.x > 0) != lookRight)
            {
                if (lookRight)
                {
                    transform.rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), new Vector3(-1, 0, 0));
                    lookRight = false;
                    //Debug.Log("Left");

                }
                else
                {
                    transform.rotation = Quaternion.identity;
                    lookRight = true;
                    // Debug.Log("Right");

                }
            }
        }
        else{
            animmer.PlayAnimation(Player_Animations.AnimationState.idle);

        }
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max((local_maxSpeed - ground.Friction), 0f);
    }
    private void FixedUpdate_Moving()
    {
        Move();
    }
    private void Jump()
    {
        if (onGround || jumpPhase < local_maxAirJumps)
        {
            animmer.PlayAnimation(Player_Animations.AnimationState.jumping, false);
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * local_jumpHeight);
            if (velocity.y > 0)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            else if(velocity.y < 0){
                jumpSpeed += Mathf.Abs(rgb.velocity.y);
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
