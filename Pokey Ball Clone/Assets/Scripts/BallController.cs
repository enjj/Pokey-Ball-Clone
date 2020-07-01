using UnityEngine;

public class BallController : MonoBehaviour{

    #region Variables
    private Rigidbody _rb;

    [SerializeField]
    private float _forcePower = 0;
    [SerializeField]
    private float _gravityMultiplier= 0;
    #endregion



    #region MonoBehaviour
    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        ApplyConstantFallForce();
        if (Input.GetKeyDown(KeyCode.Space)) {
            _rb.AddForce(Vector3.up * _forcePower,ForceMode.Impulse);
        }
    }

    #endregion



    #region Helper Methods

    private void ApplyConstantFallForce() {
        _rb.AddForceAtPosition(Vector3.down * _gravityMultiplier, transform.position);
    }

    #endregion

}

