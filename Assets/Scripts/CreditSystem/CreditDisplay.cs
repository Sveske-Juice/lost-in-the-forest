using UnityEngine;
using TMPro;

public class CreditDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinTextField;

    private void OnEnable()
    {
        CreditManager.OnCreditChange += UpdateTextField;
    }

    private void OnDisable()
    {
        CreditManager.OnCreditChange -= UpdateTextField;
    }

    private void UpdateTextField(int credit)
    {
        coinTextField.text = $"{credit}";
    }
}
