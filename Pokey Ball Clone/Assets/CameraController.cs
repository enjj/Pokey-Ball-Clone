﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private InputManager _inputmanager;

    private CinemachineVirtualCamera _cam;
    private float _fov = 0f;
    private void Start() {
        _cam = GetComponent<CinemachineVirtualCamera>();
        _fov = _cam.m_Lens.FieldOfView;
        DragHandler.onEndDrag += ResetFOV;
    }

    private void Update() {
        if (_inputmanager.IsSwiping)
            SetFieldOfView();
        ResetFOV();

    }

    private void SetFieldOfView() {
        _cam.m_Lens.FieldOfView = _fov - _inputmanager.SwipeDistance / 100;
    }
    private void ResetFOV() {
        if (!_inputmanager.IsSwiping) {
            _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _fov, 0.025f);
        }
    }
}
