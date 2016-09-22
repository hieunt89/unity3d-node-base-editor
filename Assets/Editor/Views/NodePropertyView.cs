using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class NodePropertyView : ViewBase {

	public NodePropertyView () : base ("Property View") {}

	public override void UpdateView (Rect editorRect, Rect precentageRect, Event e, NodeGraph curGraph) {
		base.UpdateView (editorRect, precentageRect, e, curGraph);
//		Debug.Log ("Updating Child View Class...");
		GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

		GUILayout.BeginArea (viewRect);
//		EditorGUILayout.LabelField ("this is a label");
		GUILayout.EndArea ();

		ProcessEvent (e);
	}

	public override void ProcessEvent (Event e)
	{
		base.ProcessEvent (e);
		if (viewRect.Contains (e.mousePosition)) {
			Debug.Log (viewTitle);
			if (e.button == 0) {
				if (e.type == EventType.MouseDown) {
					Debug.Log ("mouse down" + viewTitle);
				} 
				if (e.type == EventType.MouseDrag) {
					Debug.Log ("mouse drag"+ viewTitle);
				}
				if (e.type == EventType.MouseUp) {
					Debug.Log ("mouse up"+ viewTitle);
				}
			}
			if (e.button == 1) {
				if (e.type == EventType.MouseDown) {
//					Debug.Log ("mouse down"+ viewTitle);
				
				} 
			}
		}
	}
}
