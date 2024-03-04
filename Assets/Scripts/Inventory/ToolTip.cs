using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title, description;

    public void set(InventoryItem item)
    {
        title.text = item.data.DisplayName;
        description.text = item.data.Description;
    }


}
