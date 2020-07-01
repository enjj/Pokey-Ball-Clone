using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public static Action<PointerEventData> onBeginDrag;
    public static Action<PointerEventData> onDragging;
    public static Action onEndDrag;

    private bool _isBlocked = false;

    public void OnBeginDrag(PointerEventData eventData) {
        if (_isBlocked) {
            return;
        }
        onBeginDrag?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData) {
        if (_isBlocked) {
            return;
        }
        onDragging?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (_isBlocked) {
            return;
        }
        onEndDrag?.Invoke();
    }
}
