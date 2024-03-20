using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PassiveSlot : MonoBehaviour
{
    [SerializeField] private RawImage m_icon;
    public InventoryItem Item { get; private set; }

    public void Set(InventoryItem item)
    {
        this.Item = item;
        m_icon.texture = item.data.Icon;
    }



}
