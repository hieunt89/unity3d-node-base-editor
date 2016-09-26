using UnityEngine;
using UnityEditor;
using System.Collections;

public static class NodeUtils {
	public static void CreateGraph (string name){
//		Debug.Log ("Creating graph"); 
		NodeGraph currentGraph = (NodeGraph) ScriptableObject.CreateInstance<NodeGraph> ();
		if (currentGraph != null) {
			currentGraph.graphName = name;
			currentGraph.InitGraph ();

			AssetDatabase.CreateAsset (currentGraph, "Assets/Database/" + name + ".asset");
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();

			NodeEditorWindow currentWindow = (NodeEditorWindow) EditorWindow.GetWindow <NodeEditorWindow> ();
			if (currentWindow != null) {
				currentWindow.currentGraph = currentGraph;
			}

		} else {
			EditorUtility.DisplayDialog ("Node Message", "Unable to create new graph", "OK");
		}
	}

	public static void LoadGraph () {
		NodeGraph currentGraph = null;
		string graphPath = EditorUtility.OpenFilePanel("Load Graph", Application.dataPath + "/Database/", ""); 
		int appPathLength = Application.dataPath.Length;
		string finalPath = graphPath.Substring (appPathLength - 6);
		Debug.Log (finalPath);
		currentGraph = (NodeGraph) AssetDatabase.LoadAssetAtPath (finalPath, typeof(NodeGraph));
		if (currentGraph != null) {
			NodeEditorWindow currentWindow = (NodeEditorWindow) EditorWindow.GetWindow <NodeEditorWindow> ();
			if (currentWindow != null)
				currentWindow.currentGraph = currentGraph;
		} else {
			EditorUtility.DisplayDialog ("Node Message", "Unable to load selected graph!", "OK");
		}

	}

	public static void UnloadGraph () {
		NodeEditorWindow currentWindow = (NodeEditorWindow) EditorWindow.GetWindow <NodeEditorWindow> ();
		if (currentWindow.currentGraph != null) {
			currentWindow.currentGraph = null;
		}
	}

	public static void CreateNode (NodeGraph currentGraph, NodeType nodeType, Vector2 mousePosition) {
		if (currentGraph != null) {
			NodeBase currentNode = null;
			switch (nodeType) {
			case NodeType.Float:
				currentNode = (FloatNode)ScriptableObject.CreateInstance<FloatNode> ();
				currentNode.nodeName = "Float Node";
				break;
			case NodeType.Add:
				currentNode = (AddNode)ScriptableObject.CreateInstance<AddNode> ();
				currentNode.nodeName = "Add Node";
				break;
			default:
				break;

			}

			if (currentNode != null) {
				currentNode.InitNode ();
				currentNode.nodeRect.x = mousePosition.x;
				currentNode.nodeRect.y = mousePosition.y;

				currentNode.parentGraph = currentGraph;
				currentGraph.nodes.Add (currentNode);

				AssetDatabase.AddObjectToAsset (currentNode, currentGraph);
				AssetDatabase.SaveAssets ();
				AssetDatabase.Refresh ();

			}
		}
	}

	public static void DeleteNode (int nodeId, NodeGraph currentGraph) {
		if (currentGraph != null) {
			if (currentGraph.nodes.Count >= nodeId) {
				NodeBase deleteNode = currentGraph.nodes [nodeId];
				if (deleteNode != null) {
					currentGraph.nodes.RemoveAt (nodeId);
					GameObject.DestroyImmediate (deleteNode, true);
					AssetDatabase.SaveAssets ();
					AssetDatabase.Refresh ();
				}
			}
		}
	}

	public static void DrawGrid (Rect viewRect, float gridSpacing, float gridOpacity, Color gridColor) {
		int widthDivs = Mathf.CeilToInt (viewRect.width - gridSpacing);
		int heighthDivs = Mathf.CeilToInt (viewRect.height - gridSpacing);

		Handles.BeginGUI ();
		Handles.color = new Color (gridColor.r, gridColor.b, gridColor.g, gridOpacity);

		for (int x = 0; x < widthDivs; x++) {
			Handles.DrawLine (new Vector3 (gridSpacing * x, 0f, 0f),new Vector3 (gridSpacing * x, viewRect.height, 0f));
		
		}
		for (int y = 0; y < heighthDivs; y++) {
			Handles.DrawLine (new Vector3 (0f, gridSpacing * y, 0f),new Vector3 (viewRect.width, gridSpacing * y, 0f));

		}

		Handles.color = Color.white;
		Handles.EndGUI ();
	}

}
