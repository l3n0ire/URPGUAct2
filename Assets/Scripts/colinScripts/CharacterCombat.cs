using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    public float attackDelay = 1f;


    private float attackCooldown = 0f;
    protected CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    public virtual void MeleeAttack(CharacterStats targetStats)
    {
       
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay, myStats.damage));

            attackCooldown = 1f / attackSpeed;
        }
    }
    public virtual void RangedAttack(CharacterStats targetStats)
    {

        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay,myStats.rangedDamage));

            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage (CharacterStats stats, float delay, Stat damage)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(damage.GetValue());
    }
}
