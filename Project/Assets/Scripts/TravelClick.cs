using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TravelClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public Transform travelPoint;

    Material m;
    Light light;
    string fresnelColor = "_fresnelColor";

    private void Start() {
        light = GameObject.Find("StarLight").GetComponent<Light>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        m.SetColor(fresnelColor, Color.black);

        light.color = GameGraphics.COLORS[GameGraphics.PURPLE];

        InitExpereince.CurrentAvatarTarget = travelPoint.position;

        Debug.Log(m + " Clicked");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        m = transform.GetComponent<SkinnedMeshRenderer>().material;
        m.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.GREEN]);

        light.color = GameGraphics.COLORS[GameGraphics.GREEN];

        Debug.Log(transform.name + " Enter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        m.SetColor(fresnelColor, GameGraphics.COLORS[GameGraphics.PURPLE]);

        light.color = GameGraphics.COLORS[GameGraphics.PURPLE];

        Debug.Log(transform.name + " Exit");
    }

}
