using UnityEditor;
using UnityEngine;

public static class EditorList {

    private static GUIContent moveDownButtonContent = new GUIContent("\u25BC", "move down");
    private static GUIContent moveUpButtonContent = new GUIContent("\u25B2", "move up");
    private static GUIContent deleteButtonContent = new GUIContent("\u00D7", "delete");
    private static GUIContent bottomDeleteButtonContent = new GUIContent("-", "delete last");
    private static GUIContent bottomAddButtonContent = new GUIContent("+", "add to bottom");
    

    public static void Show(SerializedProperty list, bool editable) {
        EditorGUILayout.PropertyField(list);
        
        EditorGUI.indentLevel += 1;
		
        if (list.isExpanded) {
            //EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("UI Type", EditorStyles.boldLabel, GUILayout.MaxWidth(100));
            EditorGUILayout.LabelField("Color", EditorStyles.boldLabel);
            
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < list.arraySize; i++) {
                if (editable) EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
                if (editable) {
                    ShowButtons(list, i);
                    EditorGUILayout.EndHorizontal();
                }
            }

            if (editable) {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(bottomAddButtonContent)) {
                    list.InsertArrayElementAtIndex(list.arraySize);
                }
                if(GUILayout.Button(bottomDeleteButtonContent)) {
                    list.DeleteArrayElementAtIndex(list.arraySize - 1);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUI.indentLevel -= 1;
    }

    private static void ShowButtons(SerializedProperty list, int index) {
        if (GUILayout.Button(moveUpButtonContent, EditorStyles.miniButtonLeft, GUILayout.Width(20f))) {
            list.MoveArrayElement(index, index - 1);
        }
        if (GUILayout.Button(moveDownButtonContent, EditorStyles.miniButtonMid, GUILayout.Width(20f))) {
            list.MoveArrayElement(index, index + 1);
        }
        if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, GUILayout.Width(20f))) {
            list.DeleteArrayElementAtIndex(index);
        }
    }
}