using System;
using UnityEngine;

public class BallController : MonoBehaviour {

    #region Variables
    private Rigidbody _rb;
    private InputManager _inputManager;

    [SerializeField]
    private float _gravityMultiplier = 0;
    #endregion


    #region MonoBehaviour
    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();
        DragHandler.onEndDrag+= ApplyJumpForce;
        _rb.isKinematic = true;
    }

    private void Update() {
        ApplyConstantFallForce();
        if (_inputManager.IsSwiping) {
            AdjustPosition();
        }
    }
    #endregion


    #region Helper Methods

    private void ApplyConstantFallForce() {
        _rb.AddForceAtPosition(Vector3.down * _gravityMultiplier, transform.position);
    }

    private void ApplyJumpForce() {
        _rb.isKinematic = false;
        _rb.AddForce(Vector3.up * Mathf.Abs(_inputManager.SwipeDistance / 10), ForceMode.Impulse);
    }

    private void AdjustPosition() {
        transform.position = new Vector3(transform.position.x, transform.position.y + (_inputManager.Delta/ 100), transform.position.z);
    }

    #endregion

}

