using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/Attack")]
public class Attack : ScriptableObject
{
    // override exisistin name field
    new public string name = "New Attack";
    public Stat damage;
    public bool isParryable = true;
    public bool isBlockable = true;
    public Stat range;
    public Stat timeToHit;
    public Stat parryTime;

    
}
