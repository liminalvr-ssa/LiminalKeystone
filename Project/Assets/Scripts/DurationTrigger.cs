using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Liminal.SDK.VR.Avatars;

public class DurationTrigger : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {

    public int duration;

    void Start() {
        GetComponent<BoxCollider>().enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData) {
        GameGraphics.SCRIPT.startJourney(duration);
        VRAvatar.Active.PrimaryHand.DeviceComponent.Pointer.Deactivate();
        POI.SELECTION.Play();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        GetComponent<FontController>().t = 0;
        GetComponent<FontController>().positionAnimator.scale = .1f;
        POI.HOVER.Play();
    }
}
