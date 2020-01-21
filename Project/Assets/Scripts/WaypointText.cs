using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointText : MonoBehaviour {
    GameObject waypointUI;
    Button button;
    Vector3 offset = new Vector3(0,50,0);

    void Start() {
        //Debug.Log("UI:" + GameGraphics.UI);
        waypointUI = Instantiate(GameGraphics.WAYPOINT_UI, GameGraphics.UI.transform);
        button = waypointUI.GetComponent<Button>();
        //Debug.Log(button);
        button.onClick.AddListener(travel);
    }

    void Update() {
        waypointUI.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position) + offset;
    }

    void travel () {
        Debug.Log("Hello");
        InitExpereince.CurrentAvatarTarget = gameObject.transform.position;
    }
}
