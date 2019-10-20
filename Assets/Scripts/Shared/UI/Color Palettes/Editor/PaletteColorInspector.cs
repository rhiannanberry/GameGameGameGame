using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


[CustomPropertyDrawer(typeof(PaletteColor))]
public class PaletteColorInspector : PropertyDrawer
{

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        var container = new VisualElement();

        var colorNameField = new PropertyField(property.FindPropertyRelative("colorType"));
        var colorField = new PropertyField(property.FindPropertyRelative("color"));

        container.Add(colorNameField);
        container.Add(colorField);

        return container;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        var nameRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth - position.height, position.height);
        var colorRect = new Rect(position.x + EditorGUIUtility.labelWidth - position.height, position.y, position.width - EditorGUIUtility.labelWidth + position.height, position.height);

        if (property.FindPropertyRelative("isDefault").boolValue) {
            EditorGUI.LabelField(nameRect, property.FindPropertyRelative("colorType").stringValue);
        } else {
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("colorType"), GUIContent.none);
        }
        EditorGUI.PropertyField(colorRect, property.FindPropertyRelative("color"), GUIContent.none);
        EditorGUI.EndProperty();

        
    }
}
