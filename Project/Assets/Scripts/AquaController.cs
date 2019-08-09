using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaController : MonoBehaviour {
    static int HAIR_MORPH = 0;
    static int RIPPLE_MORPH = 0;
    static int SWIM_MORPH = 1;

    public SkinnedMeshRenderer legsRenderer;
    public SkinnedMeshRenderer hairRenderer;

    Vector3 yPos;
    float yOrigin;

    public float speed = 0.2f;
    float t;

    void Start() {
        yPos = transform.position;
        yOrigin = yPos.y;
    }

    void Update() {
        hairRenderer.SetBlendShapeWeight(HAIR_MORPH, Mathf.PingPong(t*10, 30));
        legsRenderer.SetBlendShapeWeight(RIPPLE_MORPH, Mathf.PingPong(t * 60, 100));
        legsRenderer.SetBlendShapeWeight(SWIM_MORPH, Mathf.PingPong(t * 10, 45));

        yPos.y = yOrigin + Mathf.PingPong(t*.005f, .01f);
        transform.position = yPos;

        t += speed;
    }
}
