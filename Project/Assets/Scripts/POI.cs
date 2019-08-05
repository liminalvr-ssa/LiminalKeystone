using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class POI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public static int POICounter = 0;

    bool isPOIEnbaled;
    public int ID;

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

    float fade = 0f;
    float fadeTime = 0f;
    float fadeSpeed = 0.01f;
    float fadeLimit = 0.25f;

    int i = 0;
    int i_pos;

    bool lightOn = false;
    public bool lightOnPublic = false;

    public bool isLightEnabled {
        get {
            return lightOn;
        }
        set {
            lightSource.enabled = light1.enabled = light2.enabled = light3.enabled = value;
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
        }
    }

    public int togglePOILight {
        set {
            POI p;

            for (i = 0; i < GameGraphics.POI.Length; i++) {
                p = GameGraphics.POI[i].GetComponent<POI>();
                if (p.Lights.Length > 0) {
                    p.isLightEnabled = (value == i);
                }
            }
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
        lightSource = transform.GetChild(0).GetComponent<Light>();
        light1 = transform.GetChild(1).GetComponent<Light>();
        light2 = transform.GetChild(2).GetComponent<Light>();
        light3 = transform.GetChild(3).GetComponent<Light>();
        Lights = new Light[] { lightSource, light1, light2, light3 };

        boxCollider = GetComponent<BoxCollider>();
        hoverRender = transform.GetChild(4).GetComponent<MeshRenderer>();

        hoverRender.material.SetFloat(fadeIn, 0f);
        hoverRender.enabled = false;

        isLightEnabled = lightOnPublic;
    }

    private void Update() {
        if (hoverRender.enabled) {
            fade = hoverRender.material.GetFloat(fadeIn);
            hoverRender.material.SetFloat(fadeIn, Mathf.Lerp(fade, fadeLimit, fadeTime * Time.deltaTime));
            fadeTime += fadeSpeed;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        //GameGraphics.STAR_MAT.SetColor(fresnelColor, Color.black);

        //lightSource.color = GameGraphics.COLORS[GameGraphics.PURPLE];

        InitExpereince.CurrentAvatarTarget = travelPoint.localPosition - GameGraphics.HEAD_ZERO;
        togglePOILight = ID;

        if (!isAllPOIEnabled) {
            POICounter++;
            togglePOI = ID + 1;
            boxCollider.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //GameGraphics.STAR_MAT.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.GREEN]);
        //lightSource.color = GameGraphics.COLORS[GameGraphics.GREEN];

        hoverRender.enabled = true;

        fadeLimit = 0.25f;
        fadeTime = 0;
    }

    public void OnPointerExit(PointerEventData eventData) {
        //GameGraphics.STAR_MAT.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.PURPLE]);
        //lightSource.color = GameGraphics.COLORS[GameGraphics.PURPLE];

        fadeLimit = 0;
        fadeTime = 0;
        InitExpereince.TIME = 0;
    }

}
