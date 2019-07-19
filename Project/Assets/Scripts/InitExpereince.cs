﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitExpereince : MonoBehaviour {

    public static Vector3 CurrentAvatarTarget = GameGraphics.AVATAR_ZERO;
    
    public float moveSpeed;
    void Start() {
        GameGraphics.loadResources();
    }

    private void Update() 
    {
        GameGraphics.AVATAR.transform.position = Vector3.MoveTowards(GameGraphics.AVATAR.transform.position, CurrentAvatarTarget, moveSpeed * Time.deltaTime);
    }
}
