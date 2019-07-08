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

    void Start() {
        yPos = transform.position;
        yOrigin = yPos.y;
    }

    void Update() {
        eyesRenderer.SetBlendShapeWeight(EYES_OUT_MORPH, Mathf.PingPong(Time.time*10, 30));
        bodyRenderer.SetBlendShapeWeight(BODY_MORPH, Mathf.PingPong(Time.time*2, 5));

        yPos.y = yOrigin + Mathf.PingPong(Time.time*.005f, .01f);
        transform.position = yPos;
    }
}
