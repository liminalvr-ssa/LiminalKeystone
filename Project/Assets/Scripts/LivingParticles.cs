using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingParticles : MonoBehaviour {

    public int count;
    public int range = 10;

    GameObject p;
    GameObject[] P;
    Vector3[] Limits;

    Vector3 pos;
    Vector3 toOther;
    float dotVal;
    int i;

    float t;
    float speed = .0002f;

    Transform trans;
    Vector3 target;

    void Start() {
        P = new GameObject[count];
        Limits = new Vector3[count];
        p = transform.GetChild(0).gameObject;

        pos = Vector3.zero;

        for (i = 0; i < count; i++) {
            P[i] = Instantiate(p, transform);

            pos.Set(Random.Range(-range,range) * .1f, 0, Random.Range(-range, range) * .1f);

            P[i].transform.localPosition = pos;
            P[i].transform.GetChild(0).GetComponent<Rotation>().speed *= Random.value * .5f;

            Limits[i] = pos;
        }
    }

    void Update() {

        if (Camera.main == null) return;

        toOther = (transform.position - Camera.main.transform.position).normalized;
        dotVal = Vector3.Dot(Camera.main.transform.forward, toOther);

        for (i = 0; i < count; i++) {
            trans = P[i].transform;
            target = dotVal < .9f ? Limits[i] : transform.localPosition;

            trans.localPosition = Vector3.Lerp(trans.localPosition, target, t);
        }

        t += speed * Time.deltaTime;

        //Debug.Log(dotVal);

    }
}
