using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;

    public int currentHealth { get; protected set; }

    public Stat armor;
    public Stat damage;
    public CharacterConfig Config => config;

    [SerializeField] CharacterConfig config;
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
        statsEventChannel?.RaiseHealthChanged(gameObject.GetInstanceID(), maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //public void UseMana(int mana)
    //{
    //    currentMana -= mana;
    //    currentMana = currentMana >= 0 ? currentMana : 0;
    //}

    //public void RestoreMana(int mana)
    //{
    //    currentMana += mana;
    //    currentMana = Mathf.Clamp(currentMana, 0, maxMana);
    //}

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
