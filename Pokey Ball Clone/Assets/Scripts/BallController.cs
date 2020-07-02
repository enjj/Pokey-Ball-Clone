using System;
using System.Data;
using UnityEngine;

public class BallController : MonoBehaviour {

    #region Variables
    private Rigidbody _rb;
    private InputManager _inputManager;

    [SerializeField]
    private float _gravityMultiplier = 0;
    [SerializeField]
    private float _rayDistance = 0f;
    [SerializeField]
    private float _deadZone = 0f;

    private bool _isFlying = false;
    private Vector3 _stickPos = Vector3.zero;
    #endregion


    #region MonoBehaviour
    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();
        DragHandler.onEndDrag += CorrectPosition;
        DragHandler.onEndDrag += ApplyJumpForce;
        InputManager.omClickToScreen += StickToTower;
        _rb.isKinematic = true;
        _stickPos = transform.position;
    }

    private void Update() {
        ApplyConstantFallForce();
        if (_inputManager.IsSwiping) {
            AdjustPosition();
        }
        GetHitInfo();
    }
    #endregion


    #region Helper Methods

    private void ApplyConstantFallForce() {
        _rb.AddForceAtPosition(Vector3.down * _gravityMultiplier, transform.position);
    }

    private void ApplyJumpForce() {
        if (_inputManager.SwipeDistance >= 0 || _inputManager.SwipeDistance / 100 > _deadZone) {
            return;
        }
        LeanTween.cancelAll();
        _rb.isKinematic = false;
        _rb.AddForce(Vector3.up * Mathf.Abs(_inputManager.SwipeDistance / 10), ForceMode.Impulse);
        _isFlying = true;
    }

    /// <summary>
    /// Adjust the gameobject position based on swipe distance to simulate strech effect
    /// </summary>
    private void AdjustPosition() {
        transform.position = new Vector3(transform.position.x, _stickPos.y + (_inputManager.SwipeDistance / 100), transform.position.z);
    }

    /// <summary>
    ///  Player swiped but if the amount of swipe is not bigger than deadzone which means we didn't add force to gameobject
    ///  than we set its position to where it stick to wall
    /// </summary>
    private void CorrectPosition() {
        if (!_isFlying) {
            if (Vector3.Distance(transform.position,_stickPos) != 0) {
                LeanTween.move(gameObject, _stickPos, 0.2f).setEase(LeanTweenType.easeOutCubic);
            }
        }
    }

    private void StickToTower() {
        if (_isFlying) {
            if (GetHitInfo() == "Stickable") {
                _rb.isKinematic = true;
                _isFlying = false;
                _stickPos = transform.position;
            }
        }
    }

    private string GetHitInfo() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, (Vector3.forward), out hit, _rayDistance)) {
            Debug.DrawRay(transform.position, (Vector3.forward) * hit.distance, Color.yellow);
            return hit.collider.tag;
        } else {
            return null;
        }
    }
    #endregion

}

