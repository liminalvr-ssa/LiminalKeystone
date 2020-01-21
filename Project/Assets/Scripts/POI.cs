using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class POI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public static int POICounter = 0;
    public static AudioSource HOVER;
    public static AudioSource SELECTION;
    public static AudioSource TRAVEL;

    AudioSource[] FXs;

    bool isPOIEnbaled;
    public int ID;

    public SoundAnalyzer relatedAnalyzer;

    public Transform travelPoint;

    static string fresnelColor = "_fresnelColor";
    static string fadeIn = "_fadeIn";

    public GameObject POITraget;

    Material m;
    MeshRenderer hoverRender;
    Light lightSource;
    Light light1;
    Light light2;
    Light light3;
    public Light[] Lights;
    BoxCollider boxCollider;

    public ParticleSystem ps;
    ParticleSystem.MainModule psMain;

    float lightSourceMax;
    float light1Max;
    float light2Max;
    float light3Max;
    float _lightScale;

    ValueAnimator l1;
    ValueAnimator l2;
    ValueAnimator l3;
    ValueAnimator l4;

    float fade = 0f;
    float fadeTime = 0f;
    float lightFadeTime = 0f;
    float fadeSpeed = 0.001f;
    float fadeLimit = 0.25f;

    int i = 0;
    int i_pos;

    bool lightOn = false;
    public bool lightOnPublic = false;

    public float lightScale {
        get {
            return _lightScale;
        }
        set {
            _lightScale = l1.scale = l2.scale = l3.scale = l4.scale = value;
            lightSource.intensity = l1.animation;
            light1.intensity = l2.animation;
            light2.intensity = l3.animation;
            light3.intensity = l4.animation;
        }
    }

    public bool isLightEnabled {
        get {
            return lightOn;
        }
        set {
            lightOn = lightSource.enabled = light1.enabled = light2.enabled = light3.enabled = value;
        }
    }

    public int togglePOI {
        set {
            if (POICounter >= GameGraphics.POI.Length) {
                isAllPOIEnabled = true;
                return;
            }

            for (i = 0; i < GameGraphics.POI.Length; i++) {
                 GameGraphics.POI[i].GetComponent<BoxCollider>().enabled = (value == i);
            }

            GameGraphics.POIC[value].ps.Play();
        }
    }

    public int togglePOILight {
        set {
            POI p;
            bool match;

            lightFadeTime = 0;

            for (i = 0; i < GameGraphics.POIC.Length; i++) {
                p = GameGraphics.POIC[i];
                match = (value == i);
                if (p.Lights.Length > 0) {
                    p.lightOn = match;
                }
                p.relatedAnalyzer.isReading = match;
            }

            if (SELECTION != null) SELECTION.Play();
        }
    }

    public bool isAllPOIEnabled {
        set {
            isPOIEnbaled = value;
            for (i = 0; i < GameGraphics.POI.Length; i++) {
                GameGraphics.POI[i].GetComponent<BoxCollider>().enabled = value;
            }
        }
        get {
            return isPOIEnbaled;
        }
    }

    private void Start() {
        FXs = GameObject.Find("EnvironmentAudio").GetComponents<AudioSource>();
        HOVER = FXs[1];
        SELECTION = FXs[2];
        TRAVEL = FXs[3];

        lightSource = transform.GetChild(0).GetComponent<Light>();
        light1 = transform.GetChild(1).GetComponent<Light>();
        light2 = transform.GetChild(2).GetComponent<Light>();
        light3 = transform.GetChild(3).GetComponent<Light>();
        Lights = new Light[] { lightSource, light1, light2, light3 };

        lightSourceMax = lightSource.intensity;
        light1Max = light1.intensity;
        light2Max = light2.intensity;
        light3Max = light3.intensity;

        l1 = new ValueAnimator(0, lightSourceMax);
        l2 = new ValueAnimator(0, light1Max);
        l3 = new ValueAnimator(0, light2Max);
        l4 = new ValueAnimator(0, light3Max);

        boxCollider = GetComponent<BoxCollider>();
        hoverRender = transform.GetChild(4).GetComponent<MeshRenderer>();

        hoverRender.material.SetFloat(fadeIn, 0f);
        hoverRender.enabled = false;

        psMain = ps.main;

        //isLightEnabled = lightOnPublic;
        lightOn = lightOnPublic;
        lightScale = lightOnPublic ? 1 : 0;
    }

    private void Update() {
        if (hoverRender.enabled) {
            fade = hoverRender.material.GetFloat(fadeIn);
            hoverRender.material.SetFloat(fadeIn, Mathf.Lerp(fade, fadeLimit, fadeTime * Time.deltaTime));
            fadeTime += fadeSpeed;
        }

        if (lightOn && lightScale < 1) {
            lightScale = Mathf.Lerp(lightScale, 1, lightFadeTime * Time.deltaTime * .5f);
        }
        if (!lightOn && lightScale > 0) {
            lightScale = Mathf.Lerp(lightScale, 0, lightFadeTime * Time.deltaTime * 2);
        }
        lightFadeTime += fadeSpeed;

        //Debug.Log(ps.isPlaying);
    }

    public void OnPointerClick(PointerEventData eventData) {
        //GameGraphics.STAR_MAT.SetColor(fresnelColor, Color.black);

        //lightSource.color = GameGraphics.COLORS[GameGraphics.PURPLE];

        InitExpereince.CurrentAvatarTarget = travelPoint.localPosition - GameGraphics.HEAD_ZERO;
        InitExpereince.Y_FLOATING = true;

        togglePOILight = ID;

        ps.Stop();

        if (!isAllPOIEnabled) {
            POICounter++;
            togglePOI = ID + 1;
            boxCollider.enabled = false;
        }

        TRAVEL.Play();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //GameGraphics.STAR_MAT.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.GREEN]);
        //lightSource.color = GameGraphics.COLORS[GameGraphics.GREEN];

        hoverRender.enabled = true;
        psMain.simulationSpeed = 3;

        fadeLimit = 0.25f;
        fadeTime = 0;

        HOVER.Play();
    }

    public void OnPointerExit(PointerEventData eventData) {
        //GameGraphics.STAR_MAT.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.PURPLE]);
        //lightSource.color = GameGraphics.COLORS[GameGraphics.PURPLE];

        psMain.simulationSpeed = 1;

        fadeLimit = 0;
        fadeTime = 0;
        //InitExpereince.TIME = 0;
    }

}
