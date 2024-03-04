using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Statistic
{
    public System.Action OnStatValueChanged;
    [SerializeField] float baseValue;
    private readonly List<float> modifiers = new();
    private readonly List<float> multipliers = new();

    public float GetValue()
    {

        float finalValue;
        finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        multipliers.ForEach(y => finalValue *= y);
        return finalValue;

    }
    public float GetModifiers()
    {
        float value = 0;
        modifiers.ForEach(x => value += x);
        return value;
    }
    public float GetBaseValue()
    {
        return baseValue;
    }
    public float GetMultipliers()
    {
        float value = 1;
        multipliers.ForEach(x => value *= x);
        return value;
    }
    public void ChangeBaseValue(float newValue)
    {
        baseValue = newValue;
    }
    public void AddModifier(float modifier)
    {
        if (modifier != 0) modifiers.Add(modifier);
        InvokeOnChangeAction();
    }
    public void RemoveModifier(float modifier)
    {
        if (modifier != 0) modifiers.Remove(modifier);
        InvokeOnChangeAction();
    }
    public void AddMultipliers(float multiplier)
    {
        if (multiplier != 0) multipliers.Add(multiplier);
        InvokeOnChangeAction();
    }
    public void RemoveMultipliers(float multiplier)
    {
        if (multiplier != 0) multipliers.Remove(multiplier);
        InvokeOnChangeAction();
    }
    public void ClearAllModifiers()
    {
        modifiers.Clear();
        InvokeOnChangeAction();
    }
    public void ClearAllMultipliers()
    {
        multipliers.Clear();
        InvokeOnChangeAction();
    }
    public void ClearAll()
    {
        modifiers.Clear();
        multipliers.Clear();
        InvokeOnChangeAction();
    }
    public void InvokeOnChangeAction()
    {
        OnStatValueChanged?.Invoke();
    }


}