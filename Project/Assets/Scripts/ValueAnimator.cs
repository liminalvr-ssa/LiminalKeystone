using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueAnimator {
    public float range;
    public float from;
    public float to;
    float _scale;
    public float animation;

    public ValueAnimator(float _from, float _to) {
        from = _from;
        to = _to;

        range = Mathf.Abs(Mathf.Abs(from) - Mathf.Abs(to));
    }

    public float scale {
        get {
            return _scale;
        }
        set {
            _scale = Mathf.Clamp(value, 0, 1);
            animation = Mathf.Clamp(from + range * _scale, from, to);
        }
    }
}
