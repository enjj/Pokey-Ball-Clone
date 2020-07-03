﻿using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    private Enums.GameStates _state;

    public static Action<Enums.GameStates> onGameStateChange;
    public Enums.GameStates State {
        get { return _state; }
        set { ChangeGameState(value); }
    }

    private void Start() {
        _state = Enums.GameStates.Gameplay;
    }

    public void ChangeGameState(Enums.GameStates state) {
        onGameStateChange?.Invoke(state);
    }
}
