using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[Serializable]
public class NodeWorkView : ViewBase {

	private Vector2 mousePosition;
	private int deleteNodeId = 0;
	public NodeWorkView () : base ("Work View") {}

	public override void UpdateView (Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph)
	{
		base.UpdateView (editorRect, precentageRect, e, currentGraph);
//		Debug.Log ("Updating work view...");

		GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

		// draw grid
		NodeUtils.DrawGrid (viewRect, 60f, 0.15f, Color.white);
		// NodeUtils.DrawGrid (viewRect, 20f, 0.1f, Color.white);

		GUILayout.BeginArea (viewRect);
//		EditorGUILayout.LabelField ("this is a label");
		if (currentGraph != null) {
			currentGraph.UpdateGraphGUI (e, viewRect, viewSkin);
		}
		GUILayout.EndArea ();

		ProcessEvent (e);
	}

	public override void ProcessEvent (Event e)
	{
		base.ProcessEvent (e);

		if (viewRect.Contains (e.mousePosition)) {
//			Debug.Log (viewTitle);
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
					mousePosition = e.mousePosition;

					bool overNode = false;
					deleteNodeId = 0;
					if (currentGraph != null) {
						if (currentGraph.nodes.Count > 0) {
							for (int i = 0; i < currentGraph.nodes.Count; i++) {
								if (currentGraph.nodes[i].nodeRect.Contains (mousePosition)) {
									overNode = true;
									deleteNodeId = i;
								}
							}
						}
					}

					if (!overNode) {
						ProcessContextMenu (e, 0);

					} else {
						ProcessContextMenu (e, 1);
					}
				} 
			}
		}
	}

	void ProcessContextMenu (Event e, int contextId) {
		GenericMenu menu = new GenericMenu ();

		if (contextId == 0) {
			menu.AddItem (new GUIContent ("Create Graph"), false, ContextCallback, "0");
			menu.AddItem (new GUIContent ("Load Graph"), false, ContextCallback, "1");

			if (currentGraph != null) {
				menu.AddSeparator ("");
				menu.AddItem (new GUIContent ("Unload Graph"), false, ContextCallback, "2");

				menu.AddSeparator ("");
				menu.AddItem (new GUIContent ("Float Node"), false, ContextCallback, "3");
				menu.AddItem (new GUIContent ("Add Node"), false, ContextCallback, "4");
			}
		} 
		if (contextId == 1) {
			if (currentGraph != null) {
				menu.AddItem (new GUIContent ("Delete Node"), false, ContextCallback, "5");
			}
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
			NodeUtils.LoadGraph ();
			break;
		case "2":
			NodeUtils.UnloadGraph ();
			break;
		case "3":
			NodeUtils.CreateNode (currentGraph, NodeType.Float, mousePosition);
			break;
		case "4":
			NodeUtils.CreateNode (currentGraph, NodeType.Add, mousePosition);
			break;
		case "5":
			NodeUtils.DeleteNode (deleteNodeId, currentGraph);
			break;
		default:
			break;
		}
	}
}
