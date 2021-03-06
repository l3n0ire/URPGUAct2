using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    // can only set this value from within the class
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat armor;
    public Stat rangedDamage;

    public event System.Action<int, int> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }
    public void TakeDamage (int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage");

        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }
}
