using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public static Action<EquipableItemStats, EquipableItemStats> OnInventoryEquipmentChange;
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

    [Header("Statistics")]
    public Statistic armour;
    private void Awake()
    {
        maxSpeed.ChangeBaseValue(base_Stats.maxSpeed);
        maxAcceleration.ChangeBaseValue(base_Stats.maxAcceleration);
        maxAirAcceleration.ChangeBaseValue(base_Stats.maxAirAcceleration);
        maxAirJumps.ChangeBaseValue(base_Stats.maxAirJumps);
        fallingGravity.ChangeBaseValue(base_Stats.fallingGravity);
        jumpingGravity.ChangeBaseValue(base_Stats.jumpingGravity);
        jumpHeight.ChangeBaseValue(base_Stats.jumpHeight);
        OnInventoryEquipmentChange += RefreshStats;

    }

    private void RefreshStats(EquipableItemStats stats1, EquipableItemStats stats2)
    {
        if (stats1 != null)
        {
            armour.RemoveModifier(stats1.armourBonus);
            armour.RemoveMultiplier(stats1.armourMultiplier);
        }
        armour.AddModifier(stats2.armourBonus);
        armour.AddMultiplier(stats2.armourMultiplier);
        armour.OnStatValueChanged?.Invoke();
    }

}
