﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawable : MonoBehaviour {
    [HideInInspector] public Texture2D tex;
    [SerializeField] private int textureSize = 1024;
    Material m;
    // Start is called before the first frame update
    void Start() {
        tex = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        m = GetComponent<MeshRenderer>().material;
        m.mainTexture = tex;
        var fillColorArray = tex.GetPixels();
        for (var i = 0; i < fillColorArray.Length; ++i) {
            fillColorArray[i] = Color.white;
        }
        tex.SetPixels(fillColorArray);
        tex.Apply();
    }

    // Update is called once per frame
    void Update() {

    }
    void OnDestroy()
    {
        Destroy(m.mainTexture);
        Destroy(m);
    }
}