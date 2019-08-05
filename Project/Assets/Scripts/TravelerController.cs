using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerController : MonoBehaviour {
    static int ATTACK = 2;

    static int WAVE_MORPH = 5;
    static int TAIL_WAVE_MORPH = 10;

    static int LEFT_WING_HIGH = 3;
    static int RIGHT_WING_HIGH = 4;

    static int UP_BEND_HIGH = 6;
    static int UP_BEND_LOW = 11;

    public SkinnedMeshRenderer bodyRenderer;
    public float speed = 0.2f;
    float t;

    Vector3 yPos;
    float yOrigin;

    void Start() {
        yPos = transform.position;
        yOrigin = yPos.y;
    }

    void Update() {
        bodyRenderer.SetBlendShapeWeight(ATTACK, Mathf.PingPong(t * 3, 50));

        bodyRenderer.SetBlendShapeWeight(WAVE_MORPH, Mathf.PingPong(t*5, 100));
        bodyRenderer.SetBlendShapeWeight(TAIL_WAVE_MORPH, Mathf.PingPong(t*5, 100));

        bodyRenderer.SetBlendShapeWeight(LEFT_WING_HIGH, Mathf.PingPong(t * 2, 20));
        bodyRenderer.SetBlendShapeWeight(RIGHT_WING_HIGH, Mathf.PingPong(t * 2, 20));

        bodyRenderer.SetBlendShapeWeight(UP_BEND_LOW, Mathf.PingPong(t * 2, 20));

        yPos.y = yOrigin + Mathf.PingPong(t*.001f, .01f);
        transform.position = yPos;

        t += speed;
    }
}
