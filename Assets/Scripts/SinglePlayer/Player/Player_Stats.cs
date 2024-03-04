using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] private Player_BaseStats base_Stats;

    [Header("Horizontal movement")]
    public Statistic maxSpeed;
    public Statistic maxAcceleration;
    public Statistic maxAirAcceleration;

    [Header("Vertical movement")]

    public Statistic jumpHeight;
    public Statistic maxAirJumps;
    public Statistic fallingGravity;
    public Statistic jumpingGravity;
    
    
}
