using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TravelClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public Transform travelPoint;
    public Transform texturedObject;

    static string fresnelColor = "_fresnelColor";
    static string fadeIn = "_fadeIn";

    Material m;
    MeshRenderer hoverRender;
    Light lightSource;
    BoxCollider boxCollider;

    float fade = 0f;
    float fadeSpeed = 0.01f;
    float fadeLimit = 0.25f;

    bool lightOn = false;

    public bool isLightEnabled {
        get {
            return lightOn;
        }
        set {
            lightSource.enabled = value;
            transform.GetChild(1).GetComponent<Light>().enabled = value;
            transform.GetChild(2).GetComponent<Light>().enabled = value;
            transform.GetChild(3).GetComponent<Light>().enabled = value;
        }
    }

    private void Start() {
        lightSource = transform.GetChild(0).GetComponent<Light>();
        boxCollider = GetComponent<BoxCollider>();
        hoverRender = transform.GetChild(4).GetComponent<MeshRenderer>();

        hoverRender.enabled = false;
        hoverRender.material.SetFloat(fadeIn, 0f);

        isLightEnabled = false;
    }

    private void Update() {
        if (hoverRender.enabled && fade <= fadeLimit) {
            hoverRender.material.SetFloat(fadeIn, fade);
            fade += fadeSpeed;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        m.SetColor(fresnelColor, Color.black);

        lightSource.color = GameGraphics.COLORS[GameGraphics.PURPLE];
        //boxCollider.enabled = false;

        InitExpereince.CurrentAvatarTarget = travelPoint.localPosition - GameGraphics.HEAD_ZERO;

        //Debug.Log(m + " Clicked");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        m = texturedObject.GetComponent<SkinnedMeshRenderer>().material;
        m.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.GREEN]);

        lightSource.color = GameGraphics.COLORS[GameGraphics.GREEN];

        hoverRender.enabled = true;

        //for (int i = 0; i < GameGraphics.POI.Length; i++) {
        //gameObject.GetComponent<TravelClick>().isLightEnabled = true; //(gameObject == GameGraphics.STAR_POI);
        //}

        //Debug.Log(transform.name + " Enter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        m.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.PURPLE]);

        lightSource.color = GameGraphics.COLORS[GameGraphics.PURPLE];

        hoverRender.enabled = false;
        fade = 0;

        //for (int i = 0; i < GameGraphics.POI.Length; i++) {
        //gameObject.GetComponent<TravelClick>().isLightEnabled = false; //(gameObject == GameGraphics.STAR_POI);
        //}

        

        //Debug.Log(transform.name + " Exit");
    }

}
