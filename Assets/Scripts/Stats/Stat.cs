using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// show up in menu
[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue;

    private List<float> modifiers = new List<float>();

    // returns the sum of all modifiers of a stat
    public float GetValue()
    {
        float finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(float modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
   
}
