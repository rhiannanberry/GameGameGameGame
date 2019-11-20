using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Draw : MonoBehaviour {
    public int drawSize = 20;
    public bool HasDrawn;
    Vector2 prevCoords = new Vector2(-1, -1);
    Color32[] brush;
    // Start is called before the first frame update
    void Start() {
        brush = Enumerable.Repeat(new Color32(0,0,0,255), drawSize * drawSize).ToArray();
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        if (Input.GetButton("Fire1") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
            var drawable = hit.transform.GetComponent<Drawable>();
            if (drawable && drawable.tex) {
                HasDrawn = true;
                if (prevCoords.x >= 0) {
                    for (int i = 0; i < 10; i++) {
                        Vector2 o = Vector2.Lerp(prevCoords, hit.textureCoord, 1f / (i + 1f));
                        drawable.tex.SetPixels32((int) (o.x * drawable.tex.width), (int) (o.y * drawable.tex.width), drawSize, drawSize, brush);
                    }
                } else {
                    drawable.tex.SetPixels32((int) (hit.textureCoord.x * drawable.tex.width), (int) (hit.textureCoord.y * drawable.tex.width), drawSize, drawSize, brush);
                }
                prevCoords = hit.textureCoord;
                drawable.tex.Apply(false);
            } else {
                prevCoords = new Vector2(-1, -1);
            }
        } else {
            prevCoords = new Vector2(-1, -1);
        }
    }
}