using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private InputManager _inputmanager;
    [SerializeField]
    private CinemachineVirtualCamera _finishCam;
    private CinemachineVirtualCamera _cam;
    private float _fov = 0f;

    private void Start() {
        _cam = GetComponent<CinemachineVirtualCamera>();
        _fov = _cam.m_Lens.FieldOfView;
        DragHandler.onEndDrag += ResetFOV;
        GameManager.onGameStateChange += ChangeCamera;
    }

    private void Update() {
        SetFOV();
        ResetFOV();
    }

    private void SetFOV() {
        if (_inputmanager.IsSwiping)
            _cam.m_Lens.FieldOfView = _fov - _inputmanager.SwipeDistance / 40;
    }
    private void ResetFOV() {
        if (!_inputmanager.IsSwiping)
            _cam.m_Lens.FieldOfView = Mathf.Lerp(_cam.m_Lens.FieldOfView, _fov, 0.025f);
    }

    private void ChangeCamera(Enums.GameStates state) {
        if (state == Enums.GameStates.LevelFinish) {
            _finishCam.m_Priority = 15;
        } else if (state == Enums.GameStates.Gameplay) {
            _finishCam.m_Priority = 5;
        }
    }
}
