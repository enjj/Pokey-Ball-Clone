using System;
using UnityEngine;

public class BallController : MonoBehaviour {

    #region Variables
    private Rigidbody _rb;
    private InputManager _inputManager;

    [SerializeField]
    private float _gravityMultiplier = 0;
    [SerializeField]
    private float _rayDistance = 0f;
    private bool _isFlying = false;
    #endregion


    #region MonoBehaviour
    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();
        DragHandler.onEndDrag += ApplyJumpForce;
        InputManager.omClickToScreen += StickToTower;
        _rb.isKinematic = true;
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
        _rb.isKinematic = false;
        _rb.AddForce(Vector3.up * Mathf.Abs(_inputManager.SwipeDistance / 10), ForceMode.Impulse);
        _isFlying = true;
    }

    private void AdjustPosition() {
        transform.position = new Vector3(transform.position.x, transform.position.y + (_inputManager.Delta / 100), transform.position.z);
    }

    private void StickToTower() {
        if (_isFlying) {
            if (GetHitInfo() == "Stickable") {
                _rb.isKinematic = true;
                _isFlying = false;
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

