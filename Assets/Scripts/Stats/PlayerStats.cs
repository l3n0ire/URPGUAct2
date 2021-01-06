using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public float fov = 30f;

    private bool shouldCheckInput = false;
    private bool isBlocking = false;
    private float timeElapsed = 0f;
    private Attack attack;
    private KeyCode blockButton;
    private float offset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // subscribe to callback
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        // block button is also parry button
        blockButton = KeyCode.P;
    }

    private void Update()
    {
        checkForParryBlock();
        
    }
    void checkForParryBlock()
    {
        if (attack != null && shouldCheckInput)
        {
            // time elapsed since enemy attacked
            timeElapsed += Time.deltaTime;

            if (attack.isParryable && attack.isBlockable)
            {
                // failed parry/ block
                if (timeElapsed > attack.timeToHit.GetValue()+offset)
                {
                    shouldCheckInput = false;
                    timeElapsed = 0;
                    TakeDamage(attack.damage.GetValue());
                }

                // player presses block button
                if (Input.GetKeyDown(blockButton))
                {
                    isBlocking = true;

                    // player parrys
                    if (timeElapsed >= attack.parryTime.GetValue() && timeElapsed < attack.timeToHit.GetValue())
                    {
                        isBlocking = false;
                        shouldCheckInput = false;
                        timeElapsed = 0;
                        Debug.Log("parried");

                    }

                }

                // player stops blocking
                if (Input.GetKeyUp(blockButton))
                {
                    isBlocking = false;
                }

                // player successfully blocks
                if (timeElapsed <= attack.timeToHit.GetValue()+offset && isBlocking)
                {
                    shouldCheckInput = false;
                    isBlocking = false;
                    timeElapsed = 0;
                    Debug.Log("blocked");

                }

            }

        }

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

    public void TakeDamageFromAttack(Attack attack, Transform attacker)
    {
        if (FacingEachOther(attacker))
        {
            Debug.Log(isBlocking);
            shouldCheckInput = true;
            this.attack = attack;
        }
        else
        {
            Debug.Log("notfacing");
            // player is not facing attacker so they take damage
            TakeDamage(attack.damage.GetValue());
        }
        
    }
    public bool FacingEachOther(Transform attacker)
    {
        Vector3 lookDir = attacker.position - transform.position;

        Vector3 playerDir = transform.forward;
        Vector3 attackerDir = attacker.forward;

        // angle roataion need to face each other
        float playerAngle = Vector3.Angle(playerDir, lookDir);
        float attackerAngle = Vector3.Angle(attackerDir, -lookDir);

        if (attackerAngle < fov && playerAngle < fov)
        {
            return true;
        }

        return false;

    }
    public override void Die()
    {
        base.Die();
        // kill the player
        PlayerManager.instance.KillPlayer();
    }


}
