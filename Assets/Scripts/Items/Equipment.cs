using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Equipmemnt", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        // equip
        EquipmentManager.instance.Equip(this);
        // remove from inventory
        RemoveFromInventory();
    }

}

public enum EquipmentSlot { Head,Chest,Legs,Weapon,Shield, Feet}
