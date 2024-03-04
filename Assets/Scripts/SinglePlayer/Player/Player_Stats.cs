using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] private Player_MobilityStats base_Stats;

    [Header("Horizontal movement")]
    public Statistic maxSpeed;
    public Statistic maxAcceleration;
    public Statistic maxAirAcceleration;

    [Header("Vertical movement")]

    public Statistic jumpHeight;
    public Statistic maxAirJumps;
    public Statistic fallingGravity;
    public Statistic jumpingGravity;
    private void Awake()
    {
        maxSpeed.ChangeBaseValue(base_Stats.maxSpeed);
        maxAcceleration.ChangeBaseValue(base_Stats.maxAcceleration);
        maxAirAcceleration.ChangeBaseValue(base_Stats.maxAirAcceleration);
        maxAirJumps.ChangeBaseValue(base_Stats.maxAirJumps);
        fallingGravity.ChangeBaseValue(base_Stats.fallingGravity);
        jumpingGravity.ChangeBaseValue(base_Stats.jumpingGravity);
        jumpHeight.ChangeBaseValue(base_Stats.jumpHeight);

    }
    
    
}
