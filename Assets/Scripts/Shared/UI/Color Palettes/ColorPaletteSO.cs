using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Scriptable Objects/Color Palette", order = 1)]
public class ColorPaletteSO : ScriptableObject
{
    public List<PaletteColor> defaults = PaletteColor.GetDefaults(DefaultStyles.defaultStrings, true);
    public List<PaletteColor> customs = new List<PaletteColor>();
  
    public void UpdateDefaults() {
        var newDefaults = PaletteColor.GetDefaults(DefaultStyles.defaultStrings, true);
        foreach(PaletteColor p in defaults) {
            var newP = newDefaults.Find(x => x.colorType == p.colorType);
            if (newP != null)  newP.color = p.color;
        }
        defaults = newDefaults;
    }   
}

[System.Serializable]
public class PaletteColor {
    public string colorType;
    public Color color;

    public bool isDefault;

    public PaletteColor(string colorType, bool isDefault=false) {
        this.colorType = colorType;
        this.color = new Color(0,0,0,1f);
        this.isDefault = isDefault;
    }

    public override bool Equals(object obj) {
        return colorType == ((PaletteColor)obj).colorType;
    }

    public static List<PaletteColor> GetDefaults(string[] defaultList, bool isDefault=false) {
        var list = new List<PaletteColor>();
        foreach(string i in defaultList) {
            list.Add(new PaletteColor(i, isDefault));
        }
        return list;
    }
}

public static class DefaultStyles {
    public static string[] defaultStrings = new string[] {
        "background", "fill", "border", "text", "buttonFill", "buttonBorder", "buttonText", "accent1", "accent2", "accent3"
    };
}

