using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitExpereince : MonoBehaviour {

    public static Vector3 CurrentAvatarTarget = GameGraphics.AVATAR_ZERO;

    void Start() {
        GameGraphics.loadResources();
    }

    private void Update() {
        GameGraphics.AVATAR.transform.position = Vector3.Lerp(GameGraphics.AVATAR.transform.position, CurrentAvatarTarget, Time.time * Time.deltaTime * .05f);
    }

}
