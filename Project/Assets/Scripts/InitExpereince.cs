using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitExpereince : MonoBehaviour {

    public static Vector3 CurrentAvatarTarget = GameGraphics.AVATAR_ZERO;
    
    public float moveSpeed;
    public static float TIME = 0;

    void Start() {
        GameGraphics.loadResources();

        GameObject.Find("Star").GetComponent<SkinnedMeshRenderer>().material = GameGraphics.STAR_MAT;
        GameObject.Find("AquaLegs").GetComponent<SkinnedMeshRenderer>().material = GameGraphics.AQUA_MAT;
        GameObject.Find("Traveler").GetComponent<SkinnedMeshRenderer>().material = GameGraphics.TRAVELER_MAT;

        GameGraphics.POI[0].GetComponent<POI>().togglePOI = 0;
        GameGraphics.POI[0].GetComponent<POI>().togglePOILight = 0;
    }

    private void Update() 
    {
        GameGraphics.AVATAR.transform.position = Vector3.Lerp(GameGraphics.AVATAR.transform.position, CurrentAvatarTarget, TIME * Time.deltaTime);
        TIME += moveSpeed;

        //if (time >= 1) {
            //time = 0;
        //}
    }
}
