using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWaypoints : MonoBehaviour {
    public GameObject targetPoint;

    GameObject avatar;

    void Start() {
        avatar = GameObject.Find("VRAvatar");
    }

    void Update() {
        
    }
}
