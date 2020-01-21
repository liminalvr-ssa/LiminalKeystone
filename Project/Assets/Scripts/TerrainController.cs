using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {

    float angle = .02f;

    void Start() {
        
    }

    void Update() {
        transform.Rotate(GameGraphics.POI[0].transform.localPosition, angle);
    }
}
