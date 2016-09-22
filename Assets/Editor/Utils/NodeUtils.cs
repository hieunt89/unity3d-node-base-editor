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
}
