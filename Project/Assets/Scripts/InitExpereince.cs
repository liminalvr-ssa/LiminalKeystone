using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.Core.Fader;

public class InitExpereince : MonoBehaviour {

    public static Vector3 CurrentAvatarTarget = GameGraphics.AVATAR_ZERO;
    public static Quaternion CurrentAvatarRotation = new Quaternion();
    public static bool Y_FLOATING = false;

    Vector3 yAxisOffset = Vector3.zero;
    static float yOffsetRangeUp = .1f;
    static float yOffsetRangeDown = -.1f;
    public float yOffsetTime;
    float timeOffsetModifier;
    static float timeOffsetModifierUp = .01f;
    static float timeOffsetModifierDown = .007f;

    public static float TIME = 0;
    float speed = .00001f;
    float acceleration = .001f;
    float accelerationMultiplier = 0;

    public FontController welcome;
    public FontController select;
    public FontController min3;
    public FontController min5;
    public FontController min10;
    //public FontController hint;
    public FontController now;
    public FontController relax;

    float timeModifier;

    int fadeInterval = 2;

    int currentPoint = 0;
    float travelInterval;

    void Start() {
        GameGraphics.loadResources();

        GameObject.Find("Star").GetComponent<SkinnedMeshRenderer>().sharedMaterial = GameGraphics.STAR_MAT;
        GameObject.Find("AquaLegs").GetComponent<SkinnedMeshRenderer>().sharedMaterial = GameGraphics.AQUA_MAT;
        GameObject.Find("Traveler").GetComponent<SkinnedMeshRenderer>().sharedMaterial = GameGraphics.TRAVELER_MAT;
        GameObject.Find("poi1plant").GetComponent<MeshRenderer>().sharedMaterials[1] = GameGraphics.PLANT_MAT;

        POI.POICounter = 1;
        GameGraphics.POIC[0].togglePOILight = 0;
        GameGraphics.POIC[0].isAllPOIEnabled = false;

        //hint.enabled = select.enabled = min3.enabled = min5.enabled = min10.enabled = false;
        now.enabled = relax.enabled = select.enabled = min3.enabled = min5.enabled = min10.enabled = false;

        Invoke("showSelect", fadeInterval);
        Invoke("showMin3", fadeInterval*1.5f);
        Invoke("showMin5", fadeInterval*2f);
        Invoke("showMin10", fadeInterval*2.5f);
    }

    private void Update() {
        timeModifier = Mathf.Clamp(Mathf.Sin(TIME), .15f, .25f) * accelerationMultiplier;

        if (Y_FLOATING) {
            timeOffsetModifier = yAxisOffset.y > 0 ? timeOffsetModifierUp : timeOffsetModifierDown;

            yAxisOffset.Set(0, Mathf.Clamp(Mathf.Sin(yOffsetTime) * 2, yOffsetRangeDown, yOffsetRangeUp), 0);

            yOffsetTime += timeOffsetModifier;
        }

        if (accelerationMultiplier < 1) {
            accelerationMultiplier += acceleration;
        }

        //Debug.Log("Offset: " + yAxisOffset.y + " timeMod: " + timeModifier);

        GameGraphics.AVATAR.transform.position = Vector3.Lerp(GameGraphics.AVATAR.transform.position, CurrentAvatarTarget + yAxisOffset, timeModifier * Time.deltaTime);
        GameGraphics.AVATAR.transform.rotation = Quaternion.Lerp(GameGraphics.AVATAR.transform.rotation, CurrentAvatarRotation, timeModifier * Time.deltaTime * .8f);

        TIME += speed;
    }

    void switchPoint() {
        
        if (currentPoint == GameGraphics.WAYPOINTS.Length) {
            CancelInvoke("switchPoint");
            ScreenFader.Instance.FadeTo(Color.black, 4);
            return;
        }
        
        CurrentAvatarTarget = GameGraphics.WAYPOINTS[currentPoint].transform.localPosition - GameGraphics.HEAD_ZERO;
        CurrentAvatarRotation = GameGraphics.WAYPOINTS[currentPoint].transform.rotation;
        Y_FLOATING = true;
        accelerationMultiplier = 0;

        Invoke("disableFloating", 5);

        if (GameGraphics.WAYPOINTS[currentPoint].isNewPoint) {
            GameGraphics.POIC[0].togglePOILight = POI.POICounter;
            GameGraphics.POIC[POI.POICounter].ps.Play();
            POI.POICounter++;
        }

        POI.TRAVEL.Play();

        currentPoint++;
    }

    void disableFloating () {
        Y_FLOATING = false;
    }

    public void startJourney(int dur) {
        min3.GetComponent<BoxCollider>().enabled = min5.GetComponent<BoxCollider>().enabled = min10.GetComponent<BoxCollider>().enabled = false;

        travelInterval = dur / GameGraphics.WAYPOINTS.Length;

        min10.speed *= 10;
        min5.speed = min3.speed = select.speed = welcome.speed = min10.speed;

        //Invoke("showHint", 3);
        Invoke("showNow", 3);
        Invoke("showRelax", 4);

        Invoke("_hideWelcome", fadeInterval * .7f);
        Invoke("hideSelect", fadeInterval * .6f);
        Invoke("hideMin3", fadeInterval * .5f);
        Invoke("hideMin5", fadeInterval * .4f);
        Invoke("hideMin10", fadeInterval * .3f);

        InvokeRepeating("switchPoint", 7, travelInterval);
        GameGraphics.POIC[0].ps.Play();
    }

    /*void showHint() {
        hint.enabled = true;
        GameGraphics.POIC[0].togglePOI = 0;

        Invoke("hideHint", 5);
    }

    void hideHint() {
        hint.direction = 0;
        Destroy(hint.gameObject, 3);
    }*/

    void showNow() {
        now.enabled = true;
        Invoke("hideNow", 9);
    }

    void hideNow() {
        now.direction = 0;
        Destroy(now.gameObject, 3);
    }

    void showRelax() {
        relax.enabled = true;
        Invoke("hideRelax", 9);
    }

    void hideRelax() {
        relax.direction = 0;
        Destroy(relax.gameObject, 3);
    }

    void showSelect () {
        select.enabled = true;
    }

    void showMin3() {
        min3.enabled = true;
        min3.GetComponent<BoxCollider>().enabled = true;
    }

    void showMin5() {
        min5.enabled = true;
        min5.GetComponent<BoxCollider>().enabled = true;
    }

    void showMin10() {
        min10.enabled = true;
        min10.GetComponent<BoxCollider>().enabled = true;
    }

    void _hideWelcome() {
        welcome.direction = 0;
        Destroy(welcome.gameObject, 3);
    }

    void hideSelect() {
        select.direction = 0;
        Destroy(select.gameObject, 3);
    }

    void hideMin3() {
        min3.direction = 0;
        Destroy(min3.gameObject, 3);
    }

    void hideMin5() {
        min5.direction = 0;
        Destroy(min5.gameObject, 3);
    }

    void hideMin10() {
        min10.direction = 0;
        Destroy(min10.gameObject, 3);
    }

}
