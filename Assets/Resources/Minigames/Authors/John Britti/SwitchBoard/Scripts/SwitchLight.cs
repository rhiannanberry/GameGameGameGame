using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : MonoBehaviour
{
    Material m;
    Color defaultColor;

    void Start()
    {
    }

    public void SetLight(Color c) {
        m = GetComponent<Renderer>().material;
        m.SetColor("_Emission", c);
    }

    public void SetOff() {
        m.SetColor("_Emission", defaultColor);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if (m) Destroy(m);
    }
}
