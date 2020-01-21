using UnityEngine;
using System.Linq;

public class Blender {

    public BlenderElement[] Blends;
    int period;
    float _playHead;
    int c;

    public float playHead {
        get {
            return _playHead;
        }
        set {
            _playHead =  Mathf.Clamp (value, 0, 99);

            for (c = 0; c < Blends.Length; c++) {
                if (Blends[c].Range.Contains((int)_playHead)) {
                    Blends[c].position = (_playHead - (period * c)) * Blends.Length;
                } else {
                    Blends[c].position = 5;
                }
            }
        }
    }

    public Blender (int _blends) {
        int i;

        Blends = new BlenderElement[_blends];
        period = 100 / _blends;

        for (i = 0; i < Blends.Length; i++ ) {
            Blends[i] = new BlenderElement();
            fill(ref Blends[i].Range, period, i);
        }
    }

    void fill(ref int[] A, int _period, int _index) {
        int i;
        int offset = _period * _index;

        A = new int[_period];

        for (i = 0; i < _period; i++) {
            A[i] = i + offset;
        }
    }

}

public class BlenderElement {
    public int[] Range;
    public float position;
}
