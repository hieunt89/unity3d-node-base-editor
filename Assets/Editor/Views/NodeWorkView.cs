using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[Serializable]
public class NodeWorkView : ViewBase {

	public NodeWorkView () : base ("Work View") {}

	public override void UpdateView (Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph)
	{
		base.UpdateView (editorRect, precentageRect, e, currentGraph);
//		Debug.Log ("Updating work view...");
		if (currentGraph != null) {
			viewTitle = currentGraph.graphName;
		} else {
			viewTitle = "No graph";
		}
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
					CreateContextMenu (e);
				} 
			}
		}
	}

	void CreateContextMenu (Event e) {
		GenericMenu menu = new GenericMenu ();
		menu.AddItem (new GUIContent ("Create Graph"), false, ContextCallback, "0");
		menu.AddItem (new GUIContent ("Load Graph"), false, ContextCallback, "1");

		if (curGraph != null) {
			menu.AddSeparator ("");
			menu.AddItem (new GUIContent ("Unload Graph"), false, ContextCallback, "2");
		}
		menu.ShowAsContext ();
		e.Use ();
	}

	void ContextCallback (object obj) {
		switch (obj.ToString ()) {
		case "0":
			NodePopupWindow.InitNodePopup();
			break;
		case "1":
			Debug.Log ("Loading graph");
			break;
		case "2":
			Debug.Log ("Unloading graph");
			break;
		default:
			break;
		}
	}
}
