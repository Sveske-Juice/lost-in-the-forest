using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private IHealthComponent attachedHealth;

    [SerializeField]
    private Image healthBarImg;

    [SerializeField]
    private Color healthLeftColor, unusedHealth;

    [SerializeField]
    private bool disableOnDeath = true;

    private void OnEnable()
    {
        attachedHealth = GetComponentInParent<IHealthComponent>();
        attachedHealth.OnHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDisable()
    {
        attachedHealth.OnHealthChanged.RemoveListener(UpdateHealthBar);
    }

    private void UpdateHealthBar(float prevHealth, float newHealth)
    {
        float percent = newHealth / attachedHealth.getMaxHealth();
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
