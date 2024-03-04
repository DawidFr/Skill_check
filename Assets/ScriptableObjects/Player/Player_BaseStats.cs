using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Player_MobilityStats", menuName = "Player/Player_MobilityStats")]
public class Player_MobilityStats : ScriptableObject {

    [Header("Horizontal movement")]
    public float maxSpeed;
    public float maxAcceleration;
    public float maxAirAcceleration;

    [Header("Vertical movement")]
    public float jumpHeight;
    public int maxAirJumps;
    public float fallingGravity;
    public float jumpingGravity;
    

}
