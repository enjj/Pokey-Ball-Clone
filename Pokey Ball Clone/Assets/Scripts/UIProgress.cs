using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIProgress : MonoBehaviour {

    [Header("Dynamic Assignment")]
    [SerializeField]
    private Transform _meTransform = null;
    [SerializeField]
    private Transform _startTransform = null;
    [SerializeField]
    private Transform _endTransform = null;

    [Header("Initializations")]
    [SerializeField]
    private TextMeshProUGUI _txtCurrentLevel = null;
    [SerializeField]
    private RectTransform _meRect = null;

    [SerializeField]
    private float _rectEndX = 350;
    [SerializeField]
    private float _rectStartX = -295;

    private int _currentSceneIndex = 0;

    private void Awake() {
        _meRect.anchoredPosition = new Vector2(_rectStartX, _meRect.anchoredPosition.y);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode) {
        _currentSceneIndex = scene.buildIndex;

        if (_currentSceneIndex == 0) {
            SceneManager.LoadScene(1);
            return;
        }

        _txtCurrentLevel.text = scene.buildIndex.ToString();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) {
            _meTransform = playerObject.transform;
        }

        _startTransform = GameObject.FindGameObjectWithTag("START").transform;
        _endTransform = GameObject.FindGameObjectWithTag("FINISH").transform;
    }

    private void Update() {
        if (_currentSceneIndex == 0) {
            return;
        }

        float mePosZ = 0f;
        if (_meTransform) {
            mePosZ = _meTransform.position.z;
        }

        float meMappedValue = ExtensionMethods.Map(mePosZ, _startTransform.position.z, _endTransform.position.z, _rectStartX, _rectEndX);
        _meRect.anchoredPosition = new Vector2(meMappedValue, _meRect.anchoredPosition.y);
    }

}
