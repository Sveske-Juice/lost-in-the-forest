using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using System.Collections;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private bool onlyCallZeroHealthOnce = true;
    [SerializeField] float maxHealth = 100;
    public float CurrentHealth {  get; private set; }

    public static event Action<Vector3, float> OnHealthChangeAtPos;

    public UnityEvent OnHealthZero;
    public UnityEvent<float, float> OnHealthChanged;

    bool alreadyReachedZero = false;

    SpriteRenderer sr;
    private Color defaultColor;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        CurrentHealth = maxHealth;
        defaultColor = sr.color; 
        OnHealthChanged.AddListener((prev, nex) => showRedTint = nex<prev);
        StartCoroutine(RedTintLoop());
    }

    bool showRedTint = false;
    private IEnumerator RedTintLoop()
    {
        while (true)
        {
            if (!showRedTint)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            sr.color = Color.red;
            yield return new WaitForSecondsRealtime(0.1f);
            sr.color = defaultColor;
            yield return new WaitForSecondsRealtime(0.1f);
            showRedTint = false;
        }
    }
    public float GetMaxHealth() {
        return maxHealth;
    }

    public void SetMaxHealth(float amount, bool heal = false) {
        maxHealth = amount;

        if (heal)
        {
            CurrentHealth = amount;
            alreadyReachedZero = false;
        }
    }

    public void Modify(float damage)
    {
        if (damage == 0f) return;

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, GetMaxHealth());
        OnHealthChanged?.Invoke(CurrentHealth + damage, CurrentHealth);
        OnHealthChangeAtPos?.Invoke(transform.position, damage);

        if (CurrentHealth <= 0) {
            if (alreadyReachedZero && onlyCallZeroHealthOnce) return;

            alreadyReachedZero = true;
            OnHealthZero?.Invoke();
        }
    }

    public void SimpleKill()
    {
        Destroy(gameObject);
    }
}
