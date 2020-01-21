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

    public static InitExpereince SCRIPT;

    public static GameObject AVATAR;
    public static GameObject UI;
    public static GameObject WAYPOINT_UI;

    static string STAR_MAT_NAME = "starBody";
    static string AQUA_MAT_NAME = "aquaLegs";
    static string TRAVELER_MAT_NAME = "travelerBody";
    static string PLANT_MAT_NAME = "plantStroke";

    public static Material STAR_MAT;
    public static Material AQUA_MAT;
    public static Material TRAVELER_MAT;
    public static Material PLANT_MAT;

    public static Material[] MATS;

    public static GameObject STAR_POI;
    public static GameObject AQUA_POI;
    public static GameObject TRAVELER_POI;
    public static GameObject[] POI;
    public static POI[] POIC;
    public static POI CURRENT_POI;

    public static CameraWaypoints[] WAYPOINTS;

    public static AudioClip[] NodeClips;

    public static void loadResources() {
        Transform w = GameObject.Find("CameraWaypoints").transform;
        int waypointCount = w.childCount;

        AVATAR = GameObject.Find("VRAvatar");
        UI = GameObject.Find("Canvas");

        STAR_POI = GameObject.Find("Star POI");
        AQUA_POI = GameObject.Find("Aqua POI");
        TRAVELER_POI = GameObject.Find("Traveler POI");
        POI = new GameObject[] { STAR_POI, AQUA_POI, TRAVELER_POI };
        POIC = new POI[3];
        POIC[0] = POI[0].GetComponent<POI>();
        POIC[1] = POI[1].GetComponent<POI>();
        POIC[2] = POI[2].GetComponent<POI>();

        SCRIPT = GameObject.Find("Scripts").GetComponent<InitExpereince>();

        WAYPOINT_UI = Resources.Load<GameObject>("UI/waypointUI");

        STAR_MAT = Resources.Load<Material>("Maps/" + STAR_MAT_NAME);
        AQUA_MAT = Resources.Load<Material>("Maps/" + AQUA_MAT_NAME);
        TRAVELER_MAT = Resources.Load<Material>("Maps/" + TRAVELER_MAT_NAME);
        PLANT_MAT = Resources.Load<Material>("Maps/" + PLANT_MAT_NAME);
        MATS = new Material[] {STAR_MAT, AQUA_MAT, TRAVELER_MAT, PLANT_MAT};

        WAYPOINTS = new CameraWaypoints[waypointCount];
        for (int i = 0; i < waypointCount; i++) {
            WAYPOINTS[i] = w.GetChild(i).GetComponent<CameraWaypoints>();
        }

        NodeClips = new AudioClip[3];
        NodeClips[0] = Resources.Load<AudioClip>("Sounds/Node_1_(C)");
        NodeClips[1] = Resources.Load<AudioClip>("Sounds/Node_2_(Eb)");
        NodeClips[2] = Resources.Load<AudioClip>("Sounds/Node_3_(Bb)");
        //Debug.Log(STAR_MAT);
    }
}
