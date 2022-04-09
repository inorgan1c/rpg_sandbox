using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject prefab;
    public Transform target;
    [SerializeField] StatsEventChannel statsEventChannel;

    Transform ui;
    Transform cam;
    Image healthSlider;
    float visibleTime = 5f;
    float lastHealthChangedTime;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(prefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }

        statsEventChannel.OnHealthChanged += OnHealthChanged;
    }

    void LateUpdate()
    {
        if (ui)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastHealthChangedTime >= visibleTime)
            {
                ui.gameObject.SetActive(false);
            }
        }
        
    }

    void OnHealthChanged(int id, int maxHealth, int currentHealth)
    {
        if (ui && (gameObject.GetInstanceID() == id))
        {
            float hp = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = hp;
            lastHealthChangedTime = Time.time;
            ui.gameObject.SetActive(true);


            if (hp <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }
}
