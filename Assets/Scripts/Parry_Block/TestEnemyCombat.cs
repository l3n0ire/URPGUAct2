using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyCombat : CharacterCombat
{
    public float lookRadius = 3f;
    // fov in degrees
    public float fov = 45f;
    public Attack attack;
    Transform target;
    private float timeElapsed = 0f;
    private bool isAttacking = false;

    public Material attackMaterial;
    public Material parryMaterial;
    public Material defaultMaterial;

    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        
        target = PlayerManager.instance.player.transform;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // change color to know when to parry
        if (isAttacking)
        {
            
            timeElapsed = timeElapsed +Time.deltaTime;
            if (timeElapsed< attack.parryTime.GetValue())
            {
                rend.material = attackMaterial;
            }
            
            
            else if(timeElapsed >= attack.parryTime.GetValue() && timeElapsed<= attack.timeToHit.GetValue())
            {
                rend.material = parryMaterial;
            }
            else if(timeElapsed >= attack.timeToHit.GetValue())
            {
                rend.material = defaultMaterial;
                timeElapsed = 0f;
                isAttacking = false;
            }
        }

        // click h to tell enemy to attack
        if (Input.GetKeyDown(KeyCode.H))
        {
            
            float distance = Vector3.Distance(target.position, transform.position);

            // check if player is in range
            if (distance <= lookRadius)
            {

                
                Vector3 lookDir = target.position - transform.position;

                Vector3 enemyDir = transform.forward;

                // angle roataion need to face each other
                float enemyAngle = Vector3.Angle(enemyDir, lookDir);
                Debug.Log(enemyAngle);
                // check if player is within the fov of the enemy
                if (enemyAngle <= fov)
                {
                    // attack the player
                    PlayerStats targetStats = target.GetComponent<PlayerStats>();
                    if (targetStats != null)
                    {
                        targetStats.TakeDamageFromAttack(attack, this.transform);
                        isAttacking = true;
                    }
                }


            }
        }

    }
    
    // visualize the look radius in the scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
