using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    float visibleTime = 5;

    float lastMadeVisibleTime;
    Transform ui;
    Image healthSlider;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;

        foreach(Canvas c in FindObjectsOfType<Canvas>())
        {
            if(c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        // subscribe to onHealthChanged
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }
    private void LateUpdate()
    {
        ui.position = target.position;
        ui.forward = cam.forward;

        // hide heath bar after visibleTime seconds
        if(Time.time - lastMadeVisibleTime > visibleTime)
        {
            ui.gameObject.SetActive(false);
        }
    }

    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;

            float healthPercent = currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
