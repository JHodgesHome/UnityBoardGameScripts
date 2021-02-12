using System;
using System.Collections.Generic;

public class CharacterStat
{
    public float BaseValue;
    private readonly List<StatModifier> StatModifiers;
    public float Value {
        get {
            if (isDirty) {
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    private bool isDirty = true;
    private float _value;

    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        StatModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        StatModifiers.Add(mod);
        StatModifiers.Sort(CompareModifierOrder);
    }

    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }

    public bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        return StatModifiers.Remove(mod);
    }

    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;

        for(int i = 0; i < StatModifiers.Count; i++ )
        {
            StatModifier mod = StatModifiers[i];

            if(mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            } 
            else if (mod.Type == StatModType.Percent)
            {
                finalValue *= 1 + mod.Value;
            }
            
        }

        return (float)Math.Round(finalValue, 4);
    }
}
