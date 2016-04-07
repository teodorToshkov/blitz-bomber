using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer (typeof (ProbabilitySpawnerHelper))]
/*! ProbabilitySpawnerHelperEditor.cs
 * 
 * \brief This script makes the inspector layout for the ProbabilitySpawnerHelper class
 */
class ProbabilitySpawnerHelperEditor : PropertyDrawer {

	//! Draw the property inside the given rect
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty (position, label, property);

		// Draw label
		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		// Calculate rects
		Rect targetRect = new Rect (position.x, position.y, position.width * 0.7f, position.height);
		Rect probabilityRect = new Rect (position.x + position.width * 0.7f, position.y, position.width * 0.3f, position.height);

		// Draw fields - passs GUIContent.none to each so they are drawn without labels
		EditorGUI.PropertyField (targetRect, property.FindPropertyRelative ("target"), GUIContent.none);
		EditorGUI.PropertyField (probabilityRect, property.FindPropertyRelative ("probability"), GUIContent.none);

		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty ();
	}
}