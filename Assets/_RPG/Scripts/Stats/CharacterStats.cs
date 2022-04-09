using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; protected set; }

    public Stat armor;
    public Stat damage;

    [SerializeField] protected StatsEventChannel statsEventChannel;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, damage);
        

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage. Health: "+currentHealth);
        statsEventChannel?.RaiseHealthChanged(gameObject.GetInstanceID(), maxHealth, currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int hp)
    {
        hp = Mathf.Clamp(hp, 0, hp);
        currentHealth += hp;
        statsEventChannel?.RaiseHealthChanged(gameObject.GetInstanceID(), maxHealth, currentHealth);

        Debug.Log(transform.name + " heal " + hp + " hp");
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }
}
