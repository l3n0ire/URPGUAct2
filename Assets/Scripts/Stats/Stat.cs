using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// show up in menu
[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    private List<int> modifiers = new List<int>();

    // returns the sum of all modifiers of a stat
    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
   
}
