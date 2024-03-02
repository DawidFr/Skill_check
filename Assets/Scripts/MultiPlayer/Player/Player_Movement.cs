using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class Player_Movement : NetworkBehaviour
{
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 25f;
    [SerializeField, Range(0f, 100f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float downwardMovement = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovement = 1.7f;


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
        Awake_Synchronization();
    }
    private void Update()
    {
        desiredJump |= Input.GetKeyDown(KeyCode.W);
        Update_Moving();
        //timer.Update(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if (!IsOwner) return;
        FixedUpdate_Moving();
        /*while (timer.ShouldTick())
        {
            HandleClientTick();   
            HandleServerTick();
        }*/
    }

    /*private void HandleServerTick()
    {
        var bufferIndex = -1;
        while(serverInputQueue.Count > 0){
            InputPayload inputPayload = serverInputQueue.Dequeue();
            
            bufferIndex = inputPayload.tick % BUFFER_SIZE;

            StatePayload statePayload = SimulateMovement(inputPayload);
            serverStateBuffer.Add(statePayload, bufferIndex);
        }
        if(bufferIndex == -1) return;
        SendToClientRpc(serverStateBuffer.Get(bufferIndex));
    }
    [ClientRpc]
    private void SendToClientRpc(StatePayload statePayload)
    {
        if(!IsOwner) return;
        lastServerState = statePayload;
    }

    private StatePayload SimulateMovement(InputPayload inputPayload)
    {
        Physics2D.simulationMode = SimulationMode2D.Script;

        Move(inputPayload.inputVector, inputPayload.hasToJump);

        Physics2D.Simulate(Time.fixedDeltaTime);
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;

        return new StatePayload
        {
            tick = inputPayload.tick,
            position = transform.position,
            velocity = rgb.velocity,
        };
    }

    private void HandleClientTick()
    {
        if (!IsClient) return;
        var currentTick = timer.CurrentTick;
        var bufferIndex = currentTick % BUFFER_SIZE;

        InputPayload inputPayload = new InputPayload()
        {
            tick = currentTick,
            inputVector = direction.x
    };
        clientInputBuffer.Add(inputPayload, bufferIndex);
        SendToServerRpc(inputPayload);

        StatePayload statePayload = ProcessMovement(inputPayload);
        clientStateBuffer.Add(statePayload, bufferIndex);

        // HandleServerReconcilation;
    }

    [ServerRpc]
    private void SendToServerRpc(InputPayload inputPayload)
    {
        serverInputQueue.Enqueue(inputPayload);
    }

    StatePayload ProcessMovement(InputPayload input){
        Move(input.inputVector, input.hasToJump);
        return new StatePayload
        {
            tick = input.tick,
            position = transform.position,
            velocity = rgb.velocity,
        };

    }*/

    void Move(float inputVector, bool canJump){
        onGround = ground.GetOnGround();
        velocity = rgb.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        if(onGround){
            jumpPhase = 0;
        }
        if(desiredJump){
            desiredJump = false;
            JumpAction();
        }
        if(rgb.velocity.y > 0) rgb.gravityScale = upwardMovement;
        else if(rgb.velocity.y < 0) rgb.gravityScale = downwardMovement;
        else rgb.gravityScale = defaultGravityScale;
        rgb.velocity = velocity;
    }

    private void Update_Moving(){

        direction.x = Input.GetAxisRaw("Horizontal");
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max((maxSpeed - ground.GetFriction()), 0f);
    }
    private void FixedUpdate_Moving(){
        Move(direction.x, desiredJump);
    }
    private void JumpAction(){
        if(onGround || jumpPhase < maxAirJumps){
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if(velocity.y > 0){
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
    }
    private void Awake_GetComponent(){
        rgb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }
    #region Synchronization

    //Netcode general
    NetworkTimer timer;
    const float SERVER_TICK_RATE = 60f;
    const int BUFFER_SIZE = 1024;

    //Client
    CircularBufffer<StatePayload> clientStateBuffer;
    CircularBufffer<InputPayload> clientInputBuffer;
    StatePayload lastServerState;
    StatePayload lastProcessedState;

    //Server
    CircularBufffer<StatePayload> serverStateBuffer;
    Queue<InputPayload> serverInputQueue;

    public struct InputPayload : INetworkSerializable
    {
        public int tick;
        public float inputVector;
        public bool hasToJump;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref tick);
            serializer.SerializeValue(ref inputVector);
            serializer.SerializeValue(ref hasToJump);
        }
    }

    public struct StatePayload : INetworkSerializable
    {
        public int tick;
        public Vector3 position;
        public Vector3 velocity;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref tick);
            serializer.SerializeValue(ref position);
            serializer.SerializeValue(ref velocity);
        }
    }

    private void Awake_Synchronization(){
        timer = new(SERVER_TICK_RATE);
        clientStateBuffer = new(BUFFER_SIZE);
        clientInputBuffer = new(BUFFER_SIZE);
        serverStateBuffer = new(BUFFER_SIZE);
        serverInputQueue = new();
    }
    #endregion
}
