using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] AnimationCurve damageToSize;
    [SerializeField] TextMeshPro damageText;

    [SerializeField]
    private Color normalColor, critColor, superCritColor;

    [SerializeField]
    private float critThreshold, superCritThreshold;

    public void Init(float damage)
    {
        transform.localScale = Vector3.one * damageToSize.Evaluate(damage);

        damageText.color = normalColor;
        if (damage > critThreshold)
            damageText.color = critColor;
        if (damage > superCritThreshold)
            damageText.color = superCritColor;

        damageText.text = ((int)damage).ToString();
    }
}
