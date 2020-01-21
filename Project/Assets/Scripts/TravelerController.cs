using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerController : MonoBehaviour {
    static int ATTACK = 2;

    static int WAVE_MORPH = 5;
    static int TAIL_WAVE_MORPH = 10;

    static int LEFT_WING_HIGH = 3;
    static int RIGHT_WING_HIGH = 4;

    static int LEFT_WING_LOW = 9;
    static int RIGHT_WING_LOW = 8;

    static int UP_BEND_HIGH = 6;
    static int UP_BEND_LOW = 11;

    public SkinnedMeshRenderer bodyRenderer;
    public float speed = 0.2f;
    float t;

    Vector3 yPos;
    float yOrigin;

    int interval;
    int minStateInteraval = 20;
    int maxStateInteraval = 50;

    bool isRotating = false;

    AudioSource Node;

    void Start() {
        //yPos = transform.position;
        //yOrigin = yPos.y;

        Node = GetComponent<AudioSource>();

        interval = Random.Range(minStateInteraval, maxStateInteraval);
        Invoke("nodesSound", 0);
        //Invoke("changeState", interval);
    }

    void Update() {

        //yPos.y = yOrigin + Mathf.PingPong(t*.001f, .01f);
        //bodyRenderer.transform.position = yPos;

        bodyRenderer.SetBlendShapeWeight(WAVE_MORPH, Mathf.PingPong(t * 20, 100));
        bodyRenderer.SetBlendShapeWeight(TAIL_WAVE_MORPH, Mathf.PingPong(t * 10, 100));
        bodyRenderer.SetBlendShapeWeight(UP_BEND_LOW, Mathf.PingPong(t, 5));

        bodyRenderer.SetBlendShapeWeight(ATTACK, Mathf.PingPong(t * 3, 50));
        bodyRenderer.SetBlendShapeWeight(LEFT_WING_HIGH, Mathf.PingPong(t * 2, 20));
        bodyRenderer.SetBlendShapeWeight(RIGHT_WING_HIGH, Mathf.PingPong(t * 2, 20));

        //animate(LEFT_WING_HIGH, 50);
        //animate(RIGHT_WING_LOW, 50);

        /*if (isRotating) {
            bodyRenderer.gameObject.transform.Rotate(0,0,speed*7);
            //Debug.Log("Q: " + bodyRenderer.gameObject.transform.localRotation.z + "Euler: " + bodyRenderer.gameObject.transform.localRotation.eulerAngles.z);
            if (bodyRenderer.gameObject.transform.localRotation.eulerAngles.z >= 359) {
                bodyRenderer.gameObject.transform.localRotation = Quaternion.identity;
                changeState();
            }
        }*/

        t += speed;
    }

    void animate (int node, float range) {
        bodyRenderer.SetBlendShapeWeight(node,
            Mathf.Abs(
                Mathf.Clamp(bodyRenderer.gameObject.transform.localRotation.z * range, 0, 100)
            )
        );
    }

    void changeState() {
        isRotating = !isRotating;
        interval = Random.Range(minStateInteraval, maxStateInteraval);
        Invoke("changeState", interval);
    }

    void nodesSound () {
        interval = Random.Range(minStateInteraval, maxStateInteraval);

        Node.clip = GameGraphics.NodeClips[Random.Range(0, 2)];
        Node.Play();

        Invoke("nodesSound", interval);
        Invoke("secondaryNodeSound", 1);
    }

    void secondaryNodeSound() {
        Node.clip = GameGraphics.NodeClips[Random.Range(0, 2)];
        Node.Play();
    }
}
