using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    public Transform player;

    public EquipmentManager equipmentManager { 
        get { 
            return instance.player.GetComponent<EquipmentManager>(); 
        } 
    }

    [SerializeField] StatsEventChannel statsEventChannel;
    [SerializeField] InventorySystemEventChannel inventorySystemEventChannel;

    private void OnEnable()
    {
        statsEventChannel.OnEnemyDeath += OnEnemyDeath;
    }

    private void OnDisable()
    {
        statsEventChannel.OnEnemyDeath -= OnEnemyDeath;
    }

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnEnemyDeath(CharacterConfig enemyConfig)
    {
        if (enemyConfig != null)
        {
            player.GetComponent<PlayerStats>().UpdateXP(enemyConfig.xp);
            foreach (Item i in enemyConfig.loot)
            {
                inventorySystemEventChannel?.RaiseLootItemEvent(i);
            }
        }

    }
}
