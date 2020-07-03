using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    #region Singleton
    public static LevelManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    public static Action onLevelChanged;

    [SerializeField]
    private GameObject[] _levels;
    private int _currentlevel = 1;

    public int CurrentLevel {
        get { return _currentlevel; }
        private set { _currentlevel = value; }
    }

    public void NextLevel() {
        if (_levels[_currentlevel] != null) {
            _levels[_currentlevel - 1].SetActive(false);
            _levels[_currentlevel].SetActive(true);
            _currentlevel++;
            onLevelChanged?.Invoke();
        }
    }
}
