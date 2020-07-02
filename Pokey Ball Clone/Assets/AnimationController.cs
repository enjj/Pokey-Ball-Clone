using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    [SerializeField]
    private Transform _gfx;
    [SerializeField]
    private float speed = 20.0f; //how fast it shakes
    [SerializeField]
    private float amount = 1.0f; //how much it shakes
    private void Start() {
        LeanTween.linear(amount, 0, 1);
    }
    private void Update() {
        ShakeTheBall();
    }
    private void ShakeTheBall() {
        Vector3 pos = transform.position;
        pos.y += (Mathf.Sin(Time.time * speed) * amount);
        _gfx.position = pos;
    }

}
