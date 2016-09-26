using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

[Serializable]
public class AddNode : NodeBase {

	public float nodeSum;
	public NodeOutput output;
	public NodeInput inputA;
	public NodeInput inputB;

	public AddNode () {
		output = new NodeOutput ();
		inputA = new NodeInput ();
		inputB = new NodeInput ();
	}

	public override void InitNode ()
	{
		base.InitNode ();
		nodeType = NodeType.Add;
		nodeRect = new Rect (10f, 10f, 200f, 65f);
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

		if (GUI.Button (new Rect(nodeRect.x -24f, (nodeRect.y + (nodeRect.height * 0.33f)) - 14f, 24f, 24f), "", viewSkin.GetStyle("NodeInput"))) {
			if (parentGraph != null) {
				inputA.inputNode = parentGraph.connectionNode;
				inputA.isOccupied = inputA.inputNode != null ? true : false;

				parentGraph.wantsConnection = false;
				parentGraph.connectionNode = null;
			}
		}

		if (GUI.Button (new Rect(nodeRect.x -24f, (nodeRect.y + (nodeRect.height * 0.33f)  * 2f) -8f, 24f, 24f), "", viewSkin.GetStyle("NodeInput"))) {
			if (parentGraph != null) {
				inputB.inputNode = parentGraph.connectionNode;
				inputB.isOccupied = inputA.inputNode != null ? true : false;

				parentGraph.wantsConnection = false;
				parentGraph.connectionNode = null;
			}
		}

		if (inputA.isOccupied && inputB.isOccupied) {
			FloatNode nodeA = (FloatNode)inputA.inputNode;
			FloatNode nodeB = (FloatNode)inputB.inputNode;

			if (nodeA != null && nodeB != null) {
				nodeSum = nodeA.nodeValue + nodeB.nodeValue;
			}
		} else {
			nodeSum = 0.0f;
		}

		DrawInputLines ();
	}

	public override void DrawNodeProperties ()
	{
		base.DrawNodeProperties ();
		EditorGUILayout.FloatField ("Sum", nodeSum);
	}

	void DrawInputLines () {
		if (inputA.isOccupied && inputA.inputNode != null) {
			DrawLine (inputA, 1f);
		} else {
			inputA.isOccupied = false;
		}
		if (inputB.isOccupied && inputB.inputNode != null) {
			DrawLine (inputB, 2f);
		} else {
			inputB.isOccupied = false;
		}

	}

	void DrawLine (NodeInput currentInput, float inputId)
	{
		Handles.BeginGUI ();
		Handles.color = Color.white;
		Handles.DrawLine (new Vector3 (currentInput.inputNode.nodeRect.x + currentInput.inputNode.nodeRect.width + 24f, currentInput.inputNode.nodeRect.y + currentInput.inputNode.nodeRect.height * 0.5f, 0f), 
			new Vector3 (nodeRect.x - 24f, (nodeRect.y + (nodeRect.height * 0.33f) * inputId), 0f)
		);
//		Handles.DrawBezier (new Vector3 (currentInput.inputNode.nodeRect.x + currentInput.inputNode.nodeRect.width + 24f, currentInput.inputNode.nodeRect.y + currentInput.inputNode.nodeRect.height * 0.5f, 0f),
//			new Vector3 (nodeRect.x - 24f, (nodeRect.y + (nodeRect.height * 0.33f) * inputId), 0f),
//			Vector3.one,
//			Vector3.one,
//			Color.white,
//			null,
//			10f
//		);
		Handles.EndGUI ();
	}
}
