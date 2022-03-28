using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat armor;
    public Stat damage;
    public event System.Action<int, int> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, damage);
        

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int hp)
    {
        hp = Mathf.Clamp(hp, 0, hp);
        currentHealth += hp;

        Debug.Log(transform.name + " heal " + hp + " hp");

        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(maxHealth, currentHealth);
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }
}
