using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGraphics {
    public static Vector3 AVATAR_ZERO = new Vector3(0, .5f, 0);

    public static string PURPLE = "Purple";
    public static string GREEN = "Green";
    public static Dictionary<string, Color> COLORS = new Dictionary<string, Color>() {
        {PURPLE, new Color(1,0,1) },
        {GREEN, new Color(0,1,0) },
    };

    public static GameObject AVATAR;
    public static GameObject UI;
    public static GameObject WAYPOINT_UI;

    public static void loadResources() {
        AVATAR = GameObject.Find("VRAvatar");
        UI = GameObject.Find("Canvas");
        WAYPOINT_UI = Resources.Load<GameObject>("UI/waypointUI");
    }
}
