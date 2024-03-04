using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthComponent attachedHealth;

    [SerializeField]
    private Image healthBarImg;

    [SerializeField]
    private Color healthLeftColor, unusedHealth;

    [SerializeField]
    private bool disableOnDeath = true;

    private void OnEnable()
    {
        if (attachedHealth == null)
            attachedHealth = GetComponentInParent<HealthComponent>();

        attachedHealth.OnHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDisable()
    {
        attachedHealth.OnHealthChanged.RemoveListener(UpdateHealthBar);
    }

    private void UpdateHealthBar(float prevHealth, float newHealth)
    {
        float percent = newHealth / attachedHealth.GetMaxHealth();
        healthBarImg.fillAmount = percent;
        if (disableOnDeath && percent <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);

            }
        }
    }
}
