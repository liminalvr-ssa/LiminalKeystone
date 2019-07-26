using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class POI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public static int POICounter = 0;
    bool isPOIEnbaled;
    public bool isPOICurrent;

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
            lightSource.enabled = value;
            light1.enabled = light2.enabled = light3.enabled = value;
        }
    }

    int positiveInt {
        set {
            i_pos = value < 0 ? 0 : value;
        }
        get {
            return i_pos;
        }
    }

    public int togglePOI {
        set {
            if (POICounter == GameGraphics.POI.Length) {
                isAllPOIEnabled = true;
                return;
            }

            positiveInt = POICounter - 1;

            for (i = 0; i < GameGraphics.POI.Length; i++) {
                GameGraphics.POI[i].GetComponent<BoxCollider>().enabled = (value == i);    
                GameGraphics.POI[i].GetComponent<POI>().isPOICurrent = (i == positiveInt);
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

        boxCollider = GetComponent<BoxCollider>();
        hoverRender = transform.GetChild(4).GetComponent<MeshRenderer>();

        hoverRender.material.SetFloat(fadeIn, 0f);
        hoverRender.enabled = false;

        isLightEnabled = lightOnPublic;
        togglePOI = POICounter;
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
        if (!isAllPOIEnabled) {
            POICounter++;
            togglePOI = POICounter;
            //boxCollider.enabled = false;
        }

        //Debug.Log(m + " Clicked");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //GameGraphics.STAR_MAT.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.GREEN]);

        //lightSource.color = GameGraphics.COLORS[GameGraphics.GREEN];

        hoverRender.enabled = true;
        isLightEnabled = true;

        fadeLimit = 0.25f;
        fadeTime = 0;

        //for (int i = 0; i < GameGraphics.POI.Length; i++) {
        //gameObject.GetComponent<TravelClick>().isLightEnabled = true; //(gameObject == GameGraphics.STAR_POI);
        //}

        //Debug.Log(transform.name + " Enter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        POI poi;
        //GameGraphics.STAR_MAT.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.PURPLE]);

        //lightSource.color = GameGraphics.COLORS[GameGraphics.PURPLE];

        //hoverRender.enabled = false;
        fadeLimit = 0;
        fadeTime = 0;
        InitExpereince.TIME = 0;

        for (i = 0; i < GameGraphics.POI.Length; i++) {
            poi = GameGraphics.POI[i].GetComponent<POI>();
            poi.isLightEnabled = poi.isPOICurrent;
        }

        //for (int i = 0; i < GameGraphics.POI.Length; i++) {
        //gameObject.GetComponent<TravelClick>().isLightEnabled = false; //(gameObject == GameGraphics.STAR_POI);
        //}



        //Debug.Log(transform.name + " Exit");
    }

}
