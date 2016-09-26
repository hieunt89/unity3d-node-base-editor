using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class NodePropertyView : ViewBase {

	public bool showProperties = false;
	public NodePropertyView () : base ("Property View") {}

	public override void UpdateView (Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph) {
		base.UpdateView (editorRect, precentageRect, e, currentGraph);
//		Debug.Log ("Updating Child View Class...");
		GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

		GUILayout.BeginArea (viewRect);
		GUILayout.Space (60);

		GUILayout.BeginHorizontal ();
		GUILayout.Space (30);
		if (!currentGraph.showProperties) {
			EditorGUILayout.LabelField ("NONE");
		} else {
			currentGraph.selectedNode.DrawNodeProperties ();
		}
		GUILayout.Space (30);
		GUILayout.EndHorizontal ();

		GUILayout.EndArea ();

		ProcessEvent (e);

	
	}

	public override void ProcessEvent (Event e)
	{
		base.ProcessEvent (e);
		if (viewRect.Contains (e.mousePosition)) {
			if (e.button == 0) {
				if (e.type == EventType.MouseDown) {
//					Debug.Log ("mouse down" + viewTitle);
				} 
				if (e.type == EventType.MouseDrag) {
//					Debug.Log ("mouse drag"+ viewTitle);
				}
				if (e.type == EventType.MouseUp) {
//					Debug.Log ("mouse up"+ viewTitle);
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
