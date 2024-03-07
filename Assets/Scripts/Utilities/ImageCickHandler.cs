using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageCickHandler
    : MonoBehaviour,
        IPointerUpHandler,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler
{
    public event Action<ImageCickHandler> OnElementEnter;
    public event Action<ImageCickHandler> OnElementExit;
    public event Action<ImageCickHandler> OnElementDown;
    public event Action<ImageCickHandler> OnElementUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnElementDown?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnElementEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnElementExit?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnElementUp?.Invoke(this);
    }
}
