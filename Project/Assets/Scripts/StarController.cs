using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour {
    static int BODY_MORPH = 0;
    static int EYES_IN_MORPH = 0;
    static int EYES_OUT_MORPH = 1;

    public SkinnedMeshRenderer bodyRenderer;
    public SkinnedMeshRenderer eyesRenderer;

    Vector3 yPos;
    float yOrigin;

    public float speed = 0.2f;
    float t;

    void Start() {
        yPos = transform.position;
        yOrigin = yPos.y;
    }

    void Update() {
        eyesRenderer.SetBlendShapeWeight(EYES_OUT_MORPH, Mathf.PingPong(t*10, 30));
        bodyRenderer.SetBlendShapeWeight(BODY_MORPH, Mathf.PingPong(t*2, 5));

        yPos.y = yOrigin + Mathf.PingPong(t*.005f, .01f);
        transform.position = yPos;

        t += speed;
    }
}
