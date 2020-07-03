using Deform; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendTween : MonoBehaviour {

    private BendDeformer _bendDeformer;

    [SerializeField]
    private Transform _parentTransform = null;
    [SerializeField]
    private float _minParentRotZ = 5;
    [SerializeField]
    private float _maxParentRotZ = 5;
    [SerializeField]
    private float _minBendAngle = 5;
    [SerializeField]
    private float _maxBendAngle = 5;

    private void Awake() {
        _bendDeformer = GetComponent<BendDeformer>();
    }

    private void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    float currentRotZ = _parentTransform.rotation.z;
        //    LeanTween.value(currentRotZ, _maxParentRotZ, 0.5f).setOnUpdate((float value) => {
        //        _parentTransform.rotation = Quaternion.Euler(_parentTransform.rotation.x, _parentTransform.rotation.y, value);
        //    });
        //}

        float mappedAngle = 0;
        if (_parentTransform.localRotation.z < 0) {
            mappedAngle = ExtensionMethods.Map(_parentTransform.localRotation.eulerAngles.z - 360, _minParentRotZ, _maxParentRotZ, _minBendAngle, _maxBendAngle);
        } else {
            mappedAngle = ExtensionMethods.Map(_parentTransform.localRotation.eulerAngles.z, _minParentRotZ, _maxParentRotZ, _minBendAngle, _maxBendAngle);
        }

        _bendDeformer.Angle = mappedAngle;
    }

}
