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

    int state = 0;
    int interval;
    int minStateInteraval = 15;
    int maxStateInteraval = 20;
    float lift = 0;

    AudioSource Node;

    void Start() {
        yPos = transform.position;
        yOrigin = yPos.y;

        Node = GetComponent<AudioSource>();

        interval = Random.Range(minStateInteraval, maxStateInteraval);
        InvokeRepeating("changeState", 11, interval);
    }

    void Update() {
        eyesRenderer.SetBlendShapeWeight(EYES_OUT_MORPH, Mathf.PingPong(t*10, 30));

        switch (state) {
            //case 0:
                //rest();
                //break;
            case 1:
                lifting(.2f, 1);
                break;
            case 2:
                lifting(.2f, -1);
                break;
        }

        t += speed;
    }

    void rest () {
        bodyRenderer.SetBlendShapeWeight(BODY_MORPH, lift);
        lift = Mathf.PingPong(t * 2, 7);

        yPos.y = yOrigin + Mathf.PingPong(t * .005f, .02f);
        transform.position = yPos;
    }

    void lifting(float liftSpeed, int direction) {
        bodyRenderer.SetBlendShapeWeight( BODY_MORPH, Mathf.Clamp(lift, 0, 100));
        lift += liftSpeed * direction;

        yPos.y = yOrigin + Mathf.Clamp(lift/380, 0, 0.26f);
        transform.position = yPos;
    }

    void changeState() {
        state++;

        switch (state) {
            case 1:
                Node.clip = GameGraphics.NodeClips[Random.Range(0, 2)];
                Node.Play();
                break;
            case 2:
                Node.clip = GameGraphics.NodeClips[Random.Range(0, 2)];
                Node.Play();
                break;
            case 3:
                state = 0;
                break;
        }
        //t = 0;
        lift = bodyRenderer.GetBlendShapeWeight(BODY_MORPH);
        interval = Random.Range(minStateInteraval, maxStateInteraval);
    }
}
