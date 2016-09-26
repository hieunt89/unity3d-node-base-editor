using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

[Serializable]
public class FloatNode : NodeBase {
	public float nodeValue;
	public NodeOutput output;

	public FloatNode () {
		output = new NodeOutput ();
	}

	public override void InitNode ()
	{
		base.InitNode ();
		nodeType = NodeType.Float;
		nodeRect = new Rect (10f, 10f, 150f, 65f);
	}

	public override void UpdateNode (Event e, Rect viewRect)
	{
		base.UpdateNode (e, viewRect);
	}

	public override void UpdateNodeGUI (Event e, Rect viewRect, GUISkin viewSkin)
	{
		base.UpdateNodeGUI (e, viewRect, viewSkin);

		if (GUI.Button (new Rect(nodeRect.x + nodeRect.width, nodeRect.y + (nodeRect.height * 0.5f) - 12f, 24f, 24f), "", viewSkin.GetStyle("NodeOutput"))) {
			if (parentGraph != null) {
				parentGraph.wantsConnection = true;
				parentGraph.connectionNode = this;
			}
		}
	}
	public override void DrawNodeProperties ()
	{
		base.DrawNodeProperties ();
		nodeValue = EditorGUILayout.FloatField ("Float Value", nodeValue);
	}
}
