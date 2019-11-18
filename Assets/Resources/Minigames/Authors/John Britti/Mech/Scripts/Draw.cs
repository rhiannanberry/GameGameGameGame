using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {
    public int drawSize = 20;
    public bool HasDrawn;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        if (Input.GetButton("Fire1") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
            var drawable = hit.transform.GetComponent<Drawable>();
            if (drawable) {
                HasDrawn = true;
                for (int x = -drawSize / 2; x < drawSize / 2; x++) {
                    for (int y = -drawSize / 2; y < drawSize / 2; y++) {
                        drawable.tex.SetPixel((int) (hit.textureCoord.x * drawable.tex.width) + x, (int) (hit.textureCoord.y * drawable.tex.width) + y, Color.black);
                    }
                }
                drawable.tex.Apply();
            }
        }
    }
}