using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StackNumber : MonoBehaviour
{
    [SerializeField] private RawImage m_icon;
    [SerializeField] private TextMeshProUGUI m_label; 
    [SerializeField] private GameObject m_stackObj;
    [SerializeField] private TextMeshProUGUI m_stackLabel;

    public void Set(InventoryItem item)
    {
        m_icon.texture = item.data.Icon;
        m_label.text = item.data.DisplayName;
        if (item.stackSize <= 1)
        {
            m_stackObj?.SetActive(false);
            return;
        }

        m_stackLabel.text = item.stackSize.ToString();

    }








}
