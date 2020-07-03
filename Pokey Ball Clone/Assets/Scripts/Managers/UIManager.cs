using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    #region Singleton
    public static UIManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField]
    private Transform _levelFinishPanel;


    private void Start() {
        GameManager.onGameStateChange += OpenLevelFinishPanel;    
    }

    private void OpenLevelFinishPanel(Enums.GameStates state) {
        if (state == Enums.GameStates.LevelFinish) {
            _levelFinishPanel.gameObject.SetActive(true);
        } else {
            _levelFinishPanel.gameObject.SetActive(false);
        }
    }
}
