using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontController : MonoBehaviour {

    public static string opacity = "_opacity";

    public int startDirection;
    public Vector3 offsetPosition;
    public float speed;
    public ValueAnimator positionAnimator;

    Material m;

    public int direction;
    public float t;

    void Start() {
        direction = startDirection;

        m = gameObject.GetComponent<Renderer>().sharedMaterial;
        positionAnimator = new ValueAnimator(0, 1);

        m.SetFloat(opacity, startDirection == 1 ? 0 : 1);
    }

    void Update() {
        positionAnimator.scale = Mathf.Lerp(positionAnimator.scale, direction, t * Time.deltaTime);
        m.SetFloat(opacity, positionAnimator.scale);

        t += speed;
    }
}
