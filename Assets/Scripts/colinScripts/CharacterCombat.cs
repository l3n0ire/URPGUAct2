using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    public float attackDelay = 1f;

    // short way of creating delegate (pub sub)
    public event System.Action OnAttack;

    private float attackCooldown = 0f;
    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    public void MeleeAttack(CharacterStats targetStats)
    {
       
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay, myStats.damage));

            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
        }
    }
    public void RangedAttack(CharacterStats targetStats)
    {

        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay,myStats.rangedDamage));

            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage (CharacterStats stats, float delay, Stat damage)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(damage.GetValue());
    }
}
