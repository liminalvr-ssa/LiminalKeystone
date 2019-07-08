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

    void Start() {
        yPos = transform.position;
        yOrigin = yPos.y;
    }

    void Update() {
        hairRenderer.SetBlendShapeWeight(HAIR_MORPH, Mathf.PingPong(Time.time*10, 30));
        legsRenderer.SetBlendShapeWeight(RIPPLE_MORPH, Mathf.PingPong(Time.time * 10, 100));
        legsRenderer.SetBlendShapeWeight(SWIM_MORPH, Mathf.PingPong(Time.time * 2, 5));

        yPos.y = yOrigin + Mathf.PingPong(Time.time*.005f, .01f);
        transform.position = yPos;
    }
}
