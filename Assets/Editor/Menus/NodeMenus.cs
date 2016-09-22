using UnityEngine;
using UnityEditor;
using System.Collections;

public static class NodeMenus  {

	[MenuItem ("Node Editor/Launch Editor")]
	public static void InitNodeEditor () {
		NodeEditorWindow.InitNodeEditorWindow ();
	}
}
