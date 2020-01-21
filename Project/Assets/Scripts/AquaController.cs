using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaController : MonoBehaviour {
    static int SWIM_MORPH = 0;

    static string waveStrength = "_waveStrength";
    static string waveTime = "_waveTime";
    static string waveDetail = "_waveDetail";
    static string waveStrengthPresence = "_waveStrengthPresence";
    static string time = "_time";

    public SkinnedMeshRenderer legsRenderer;
    public SkinnedMeshRenderer hair1Renderer;
    public SkinnedMeshRenderer hair2Renderer;
    public SkinnedMeshRenderer hair3Renderer;

    ValueAnimator liftAnimator;
    ValueAnimator timeAnimator;
    ValueAnimator waveAnimator;
    ValueAnimator presenceAnimator;
    ValueAnimator strengthAnimator;

    Vector3 yPos;

    public float speed = 0.2f;
    float t;
    int t1;

    int state = 0;
    float interval;
    int minStateInteraval = 15;
    int maxStateInteraval = 20;

    float a; //yPos
    float b = 0; // waveTime false
    float c = 1; // waveDetail false
    float d = 0; // waveStrengthPresence false
    float e = .01f; // waveStrengthPresence false

    float a1 = -2.1f; //yPos
    float b1 = 1; // waveTime true
    float c1 = 12.7f; // waveDetail true
    float d1 = 1; // waveStrengthPresence true
    float e1 = .2f; // waveStrengthPresence true

    float hairPlayhead;

    AudioSource Node;

    float liftAnimation {
        get {
            return liftAnimator.scale;
        }
        set {
            liftAnimator.scale = value;
            yPos.y = liftAnimator.animation;
            transform.localPosition = yPos;

            legsRenderer.SetBlendShapeWeight(SWIM_MORPH, Mathf.Clamp(liftAnimator.scale * 100, 0, 90));
        }
    }

    float timeAnimation {
        get {
            return timeAnimator.scale;
        }
        set {
            timeAnimator.scale = value;
            GameGraphics.AQUA_MAT.SetFloat(waveTime, timeAnimator.animation);
        }
    }

    float strengthAnimation {
        get {
            return strengthAnimator.scale;
        }
        set {
            strengthAnimator.scale = value;
            GameGraphics.AQUA_MAT.SetFloat(waveStrength, strengthAnimator.animation);
        }
    }

    float waveAnimation {
        get {
            return waveAnimator.scale;
        }
        set {
            waveAnimator.scale = value;
            GameGraphics.AQUA_MAT.SetFloat(waveDetail, waveAnimator.animation);
        }
    }

    float wavePresenceAnimation {
        get {
            return presenceAnimator.scale;
        }
        set {
            presenceAnimator.scale = value;
            GameGraphics.AQUA_MAT.SetFloat(waveStrengthPresence, presenceAnimator.animation);
        }
    }

    float hairAnimation {
        get {
            return hairPlayhead;
        }
        set {
            hairPlayhead = Mathf.Clamp (value, 0, 100);

            hair1Renderer.SetBlendShapeWeight(0, hairPlayhead);
            hair2Renderer.SetBlendShapeWeight(0, hairPlayhead);
            hair3Renderer.SetBlendShapeWeight(0, hairPlayhead);
        }
    }

    void Start() {
        yPos = transform.localPosition;
        a = yPos.y;

        liftAnimator = new ValueAnimator(a, a1);
        timeAnimator = new ValueAnimator(b, b1);
        waveAnimator = new ValueAnimator(c, c1);
        presenceAnimator = new ValueAnimator(d, d1);
        strengthAnimator = new ValueAnimator(e, e1);

        timeAnimation = 1;
        waveAnimation = 1;
        wavePresenceAnimation = 1;
        strengthAnimation = 1;
        liftAnimation = 0;
        hairAnimation = 50;

        Node = GetComponent<AudioSource>();

        //Debug.Log(HairBlend1.currentBlend);

        interval = Random.Range(minStateInteraval, maxStateInteraval);
        Invoke("changeState", 0);
    }

    void Update() {
        hairAnimation += Mathf.Sin(t) * .1f;

        switch (state) {
            case 1:
                strengthAnimation -= Time.deltaTime;
                waveAnimation -= Time.deltaTime;
                break;
            case 2:
                wavePresenceAnimation -= .5f * Time.deltaTime;
                break;
            case 3:
                liftAnimation = MyMath.easyIn(500, 1, t1);
                t1++;
                hairAnimation -= .1f;
                break;
            case 4:
                liftAnimation -= speed * .1f;
                hairAnimation += .1f;
                break;
            case 5:
                wavePresenceAnimation += .5f * Time.deltaTime;
                break;
            case 6:
                strengthAnimation += Time.deltaTime;
                waveAnimation += Time.deltaTime;
                break;
        }

        //t += speed;
        t += .05f;
    }

    void changeState() {
        state++;
        
        switch (state) {
            case 1:
                interval = 1f;
                break;
            case 2:
                interval = 2f;
                break;
            case 3:
                Node.clip = GameGraphics.NodeClips[Random.Range(0, 2)];
                Node.Play();
                interval = 7;
                break;
            case 4:
                Node.clip = GameGraphics.NodeClips[Random.Range(0, 2)];
                Node.Play();
                interval = 14;
                break;
            case 5:
                interval = 2f;
                break;
            case 6:
                interval = 2f;
                break;
            case 7:
                yPos = transform.localPosition;
                a = yPos.y;
                interval = Random.Range(minStateInteraval, maxStateInteraval);

                timeAnimation = 1;
                waveAnimation = 1;
                wavePresenceAnimation = 1;
                strengthAnimation = 1;
                //liftAnimation = 0;
                t1 = 0;
                
                state = 0;

                break;
        }

        Invoke("changeState", interval);
    }
}
