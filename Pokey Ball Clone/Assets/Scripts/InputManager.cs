using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    #region Variables

    private Vector2 _swipeStartPos;
    private Vector2 _swipeEndPos;

    private float _swipeDistance;
    private bool _isStartToSwipe;
    #endregion

    #region Props

    public float SwipeDistance {
        get { return _swipeDistance; }
    }

    #endregion

    #region MonoBehaviour
    private void Update() {
        //foreach (Touch touch in Input.touches) {
        //    if (touch.phase == TouchPhase.Began) {
        //        _swipeStartPos = touch.position;
        //        _swipeEndPos = touch.position;
        //    }
        //}
        if (Input.GetMouseButtonDown(0)) {
            _swipeStartPos = Input.mousePosition;
           // Debug.Log("touch down START: " + _swipeStartPos);
        }

        //still down
        if (Input.GetMouseButton(0)) {
            _swipeDistance = (Input.mousePosition.y - _swipeStartPos.y) * .75f;
           // Debug.Log("touch still down - swipe dist: " + _swipeDistance);
        }

        //up
        if (Input.GetMouseButtonUp(0)) {
            //Debug.Log("touch up - swipe: " + _swipeDistance);
        }
    }

    #endregion



    #region Helper Methods

    #endregion

}

