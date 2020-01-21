using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {

    public float length;
    public float speed;

    Vector3 v;
    Vector3 originalPosition;
    float t;

    private void Start() {
        originalPosition = transform.position;
        v = Vector3.zero;
    }

    void Update() {
        v.y += Mathf.Sin(t) * length;
        transform.position = v;
        t += speed;
    }
}
