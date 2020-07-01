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
        _rb.isKinematic = true;
    }

    private void Update() {
        ApplyConstantFallForce();
        _rb.AddForce(Vector3.up * Mathf.Abs(_inputManager.SwipeDistance), ForceMode.Impulse);
    }

    #endregion



    #region Helper Methods

    private void ApplyConstantFallForce() {
        _rb.AddForceAtPosition(Vector3.down * _gravityMultiplier, transform.position);
    }

    #endregion

}

