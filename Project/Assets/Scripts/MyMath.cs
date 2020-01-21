using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMath {
    public static float easyIn(float d, float S, int t) { //duration, distance, time
        return S * (t / d) * (t / d);
    }
}
