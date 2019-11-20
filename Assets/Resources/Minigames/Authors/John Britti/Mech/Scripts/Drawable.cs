using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawable : MonoBehaviour {
    public Texture2D tex;
    Material m;
    // Start is called before the first frame update
    void Start() {
        //tex = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);
        m = GetComponent<MeshRenderer>().material;
        var fillColorArray = tex.GetPixels();
        for (var i = 0; i < fillColorArray.Length; ++i) {
            fillColorArray[i] = Color.white;
        }
        tex.SetPixels(fillColorArray);
        tex.Apply();
        m.SetTexture("_MainTex", tex);
    }

    // Update is called once per frame
    void Update() {

    }
}