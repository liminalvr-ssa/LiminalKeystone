using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerController : MonoBehaviour {
    static int WAVE_MORPH = 5;
    static int TAIL_WAVE_MORPH = 10;

    public SkinnedMeshRenderer bodyRenderer;

    Vector3 yPos;
    float yOrigin;

    void Start() {
        yPos = transform.position;
        yOrigin = yPos.y;
    }

    void Update() {
        bodyRenderer.SetBlendShapeWeight(WAVE_MORPH, Mathf.PingPong(Time.time*5, 100));
        bodyRenderer.SetBlendShapeWeight(TAIL_WAVE_MORPH, Mathf.PingPong(Time.time*5, 100));

        yPos.y = yOrigin + Mathf.PingPong(Time.time*.005f, .01f);
        transform.position = yPos;
    }
}
