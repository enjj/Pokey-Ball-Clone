using Deform;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendTween : MonoBehaviour {

    private BendDeformer _bendDeformer;
    private LTDescr _ltdesc;

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

    private void Start() {
        BallController.onRelase += ShakeTheBar;
        BallController.onStick += StopShaking;
    }

   

    private void Update() {

        float mappedAngle = 0;
        if (_parentTransform.localRotation.x < 0) {
            mappedAngle = ExtensionMethods.Map(_parentTransform.localRotation.eulerAngles.x - 360, _minParentRotZ, _maxParentRotZ, _minBendAngle, _maxBendAngle);
        } else {
            mappedAngle = ExtensionMethods.Map(_parentTransform.localRotation.eulerAngles.x, _minParentRotZ, _maxParentRotZ, _minBendAngle, _maxBendAngle);
        }

        _bendDeformer.Angle = mappedAngle;
    }

    private void ShakeTheBar() {

        _ltdesc = LeanTween.value(_minBendAngle, _maxBendAngle, 2f).setEase(LeanTweenType.easeInOutBounce).setOnUpdate((float value) => {
            _minBendAngle = value;
        });
    }

    private void StopShaking() {
        if (_ltdesc != null) {
            LeanTween.cancel(_ltdesc.id);
        }
    }
}
