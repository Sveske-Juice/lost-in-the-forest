using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RawImage m_icon;
    [SerializeField] private TextMeshProUGUI m_label;
    [SerializeField] private GameObject m_stackObj;
    [SerializeField] private TextMeshProUGUI m_stackLabel;

    public float elevation = 100;

    private GameObject tooltipPrefab, tooltip;

    public InventoryItem Item { get; private set; }

    public void Set(InventoryItem item)
    {
        this.Item = item;

        m_icon.texture = item.data.Icon;
        m_label.text = item.data.DisplayName;
        if (item.stackSize <= 1)
        {
            m_stackObj?.SetActive(false);
            return;
        }

        m_stackLabel.text = item.stackSize.ToString();
    }

    private void OnDestroy()
    {
        if (tooltip != null)
            Destroy(tooltip);
    }


    public void SetTooltip(GameObject _tooltipPrefab)
    {
        this.tooltipPrefab = _tooltipPrefab;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null)
            Destroy(tooltip);

        tooltip = Instantiate(tooltipPrefab, GameObject.FindObjectOfType<Canvas>().transform);
        tooltip.transform.position = transform.position + new Vector3(0, elevation, 0);
        tooltip.GetComponent<ToolTip>().set(Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(tooltip);
    }
}
