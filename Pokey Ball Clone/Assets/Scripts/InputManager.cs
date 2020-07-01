using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {
    #region Variables

    private float _swipeStartPos;
    private float _swipeEndPos;
    private float _delta;
    private float _swipeDistance;
    private bool _isSwiping = false;
    #endregion

    #region Props

    public float SwipeDistance {
        get { return _swipeDistance; }
    }
    public float Delta {
        get { return _delta; }
    }

    public bool IsSwiping {
        get { return _isSwiping; }
    }

    #endregion

    #region MonoBehaviour

    private void Start() {
        DragHandler.onBeginDrag += SetStartPos;
        DragHandler.onDragging += CalculateDistance;
        DragHandler.onEndDrag += StopSwiping;
    }

    #endregion

    #region Helper Methods

    private void SetStartPos(PointerEventData data) {
        _swipeStartPos = data.position.y;
        _isSwiping = true;
    }

    private void CalculateDistance(PointerEventData data) {
        _swipeDistance = (data.position.y - _swipeStartPos);
        _delta = data.delta.y;
    }

    private void StopSwiping() {
        _isSwiping = false;
        _delta = 0f;
    }
    #endregion

}

