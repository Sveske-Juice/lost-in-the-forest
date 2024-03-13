using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI title, description, lvl, cost;

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    public void set(InventoryItem item)
    {
        title.text = item.data.DisplayName;
        description.text = item.data.Description;
        lvl.text = "lvl: " + item.data.Level.ToString();

        if (cost != null)
        {
            Color textColor = CreditManager.Instance.Coins >= item.data.Cost ? Color.green : Color.red;
            cost.text = item.data.Cost.ToString();
            cost.color = textColor;
        }
    }
}
