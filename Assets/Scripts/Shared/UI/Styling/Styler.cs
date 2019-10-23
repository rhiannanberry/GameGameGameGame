using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class Styler : MonoBehaviour
{
    [HideInInspector] public StyleManager _styleManager;

    public int selected = 0;
    public string customUIType = "";

    void Start()
    {   
        _styleManager = GameObject.FindObjectOfType<StyleManager>();
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateColor();
    }

    public void UpdateColor() {
        if (AttachStyleManager() == false) return;

        var color = GetColor();

        if (color == null) return;

        if (GetComponent<TextMeshProUGUI>() != null) {
            var element = GetComponent<TextMeshProUGUI>();
            element.color = (Color) color;
            
        } else {
            var element = GetComponent<Image>();
            if (element == null ) return;
            element.color = (Color) color;
        }
    }

    private Color? GetColor() {
        PaletteColor pc = _styleManager.ActivePalette.customs.Find(x => x.colorType != "" && x.colorType == customUIType);
        if ( pc == null ) {

            string type = DefaultStyles.defaultStrings[selected];
            
            pc = _styleManager.ActivePalette.defaults.Find(x => x.colorType == type);
            
            if (pc == null) return null;
        }

        return pc.color;
    }

    private bool AttachStyleManager() {
        if (_styleManager == null) {
            _styleManager = GameObject.FindObjectOfType<StyleManager>();

            if (_styleManager == null) {
                Debug.LogWarning("No StyleManager in Scene!");
                return false;
            }
        }
        
        if (_styleManager.ActivePalette == null) {
            Debug.LogWarning("No ActivePalette attached to StyleManager!");
            return false;
        }

        return true;
    }
}
