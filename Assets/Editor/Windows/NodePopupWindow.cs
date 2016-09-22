using UnityEngine;
using UnityEditor;
using System.Collections;

public class NodePopupWindow : EditorWindow {
	static NodePopupWindow currentPopup;
	string graphName = "Enter a name ...";

	public static void InitNodePopup () {
		currentPopup = (NodePopupWindow) EditorWindow.GetWindow <NodePopupWindow> ();
		currentPopup.titleContent = new GUIContent ("Node Popup");
	}

	void OnGUI () {
		GUILayout.Space (20);
		GUILayout.BeginHorizontal ();

		GUILayout.BeginVertical ();
		EditorGUILayout.LabelField ("Create new graph:", EditorStyles.label);

		graphName = EditorGUILayout.TextField ("Enter name:", graphName);
		GUILayout.Space (10);
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Create Graph", GUILayout.Height(40))) {
			if (!string.IsNullOrEmpty (graphName) && graphName != "Enter a name ...") {
				NodeUtils.CreateGraph (graphName);
				currentPopup.Close ();
			} else {
				EditorUtility.DisplayDialog ("Node Message:", "Please enter a valid graph name", "OK");
			}
		}
		if (GUILayout.Button ("Cancel", GUILayout.Height(40))) {
			currentPopup.Close ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();

		GUILayout.Space (20);
		GUILayout.Space (20);
		GUILayout.EndHorizontal ();
		GUILayout.Space (20);
	}
}
