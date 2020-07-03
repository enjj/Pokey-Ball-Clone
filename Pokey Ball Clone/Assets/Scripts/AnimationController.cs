using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    [SerializeField]
    private Transform _gfx;
    [SerializeField]
    private float speed = 20.0f; //how fast it shakes
    private float _amount = 0; //how much it shakes
    [SerializeField]
    private LeanTweenType _easeType;
    private LTDescr _ltdesc;

    private void Start() {
        BallController.onStick += DecreaseShakeAmount;
    }

    private void DecreaseShakeAmount() {
        _amount = 2f;
         _ltdesc = LeanTween.value(_amount, 0, 2f).setEase(_easeType).setOnUpdate((float value) => {
            _amount = value;
        });
        
       // LeanTween.cancel(ltdescr.id);
    }

    private void Update() {
        ShakeTheBall();
    }
    private void ShakeTheBall() {
        Vector3 pos = transform.position;
        pos.y += (Mathf.Sin(Time.time * speed) * _amount);
        _gfx.position = pos;
    }

}
