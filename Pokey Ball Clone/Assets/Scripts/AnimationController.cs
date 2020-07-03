using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationController : MonoBehaviour {

    [SerializeField]
    private Transform _gfx;
    [SerializeField]
    private float speed = 20.0f; //how fast it shakes
    private float _amount = 0; //how much it shakes
    [SerializeField]
    private LeanTweenType _easeType;
    private LTDescr _ltdesc;
    [SerializeField]
    private Transform _test;
    private void Start() {
        BallController.onStick += DecreaseShakeAmount;
        GameManager.onGameStateChange += PlayLevelFinishAnimation;
        DragHandler.onBeginDrag += CancelShakeAnimation;
    }

    private void PlayLevelFinishAnimation(Enums.GameStates state) {
        if (state == Enums.GameStates.LevelFinish) {
            Vector3 pos = transform.position;
            LeanTween.move(gameObject, _test.position, 1f).setEase(LeanTweenType.easeInCubic).setOnComplete(() => {
                //TODO : Open LevelFinish UI;
            });
        }
    }

    private void DecreaseShakeAmount() {
        _amount = 2f;
        _ltdesc = LeanTween.value(_amount, 0, 2f).setEase(_easeType).setOnUpdate((float value) => {
            _amount = value;
        });
    }

    private void CancelShakeAnimation(PointerEventData data) {
        if (_ltdesc != null) {
            LeanTween.cancel(_ltdesc.id);
            _amount = 0f;
        }
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
