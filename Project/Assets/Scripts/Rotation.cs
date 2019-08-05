using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    public float speed = 1;
    public Direction dir;

    Vector3 d;

    void Start() {
        switch (dir) {
            case Direction.Up:
                d = Vector3.up;
                break;
            case Direction.Left:
                d = Vector3.left;
                break;
            case Direction.Forward:
                d = Vector3.forward;
                break;
        }
    }

    void Update() {
        gameObject.transform.Rotate(d*speed);
    }
}

public enum Direction {
    Up,
    Left,
    Forward
};
