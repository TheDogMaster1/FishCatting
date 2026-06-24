using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent onClick;
    public void OnPointerDown(PointerEventData eventData)
    {
        onClick.Invoke();
    }

}
