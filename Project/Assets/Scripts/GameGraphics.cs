using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGraphics {
    public static Vector3 AVATAR_ZERO = new Vector3(0, .5f, 0);
    public static Vector3 HEAD_ZERO = new Vector3(0, 1.7f, 0);

    public static string PURPLE = "Purple";
    public static string GREEN = "Green";
    public static Dictionary<string, Color> COLORS = new Dictionary<string, Color>() {
        {PURPLE, new Color(1,0,1) },
        {GREEN, new Color(0,1,0) },
    };

    public static GameObject AVATAR;
    public static GameObject UI;
    public static GameObject WAYPOINT_UI;

    static string STAR_MAT_NAME = "starBody";
    static string AQUA_MAT_NAME = "aquaLegs";
    static string TRAVELER_MAT_NAME = "travelerBody";

    public static Material STAR_MAT;
    public static Material AQUA_MAT;
    public static Material TRAVELER_MAT;

    public static Material[] MATS;

    public static GameObject STAR_POI;
    public static GameObject AQUA_POI;
    public static GameObject TRAVELER_POI;
    public static GameObject[] POI;

    public static void loadResources() {
        AVATAR = GameObject.Find("VRAvatar");
        UI = GameObject.Find("Canvas");

        STAR_POI = GameObject.Find("Star POI");
        AQUA_POI = GameObject.Find("Aqua POI");
        TRAVELER_POI = GameObject.Find("Traveler POI");
        POI = new GameObject[] { STAR_POI, AQUA_POI, TRAVELER_POI };

        WAYPOINT_UI = Resources.Load<GameObject>("UI/waypointUI");

        STAR_MAT = Resources.Load<Material>("Maps/" + STAR_MAT_NAME);
        AQUA_MAT = Resources.Load<Material>("Maps/" + AQUA_MAT_NAME);
        TRAVELER_MAT = Resources.Load<Material>("Maps/" + TRAVELER_MAT_NAME);
        MATS = new Material[] {STAR_MAT, AQUA_MAT, TRAVELER_MAT};
        //Debug.Log(STAR_MAT);
    }
}
