using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        // subscribe to callback
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        // when an item is equipped
        // add modifier of new items
        if(newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            rangedDamage.AddModifier(newItem.rangedDamageModifier);
        }
        // remove modifier of old items
        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            rangedDamage.RemoveModifier(newItem.rangedDamageModifier);
        }

    }

    public override void Die()
    {
        base.Die();
        // kill the player
        PlayerManager.instance.KillPlayer();
    }


}
