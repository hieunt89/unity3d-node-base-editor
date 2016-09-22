using UnityEditor;
using UnityEngine;

public class NodeEditorWindow : EditorWindow {

	public static NodeEditorWindow currentWindow;
	public NodePropertyView propertyView;
	public NodeWorkView workView;
	public NodeGraph currentGraph = null;

	private float viewPercentage=0.75f;
	public static void InitNodeEditorWindow () {
		currentWindow = (NodeEditorWindow)EditorWindow.GetWindow <NodeEditorWindow> ();
		currentWindow.titleContent = new GUIContent ("Node Editor");

		CreateViews ();
	}

	void OnEnable () {

	}

	void OnDestroy () {

	}

	void Update () {

	}

	void OnGUI () {
		if (propertyView == null || workView == null) {
			CreateViews ();
			return;
		}


		// Get and process the current event
		Event e = Event.current;
		ProcessEvents (e);
//		currentGraph = new NodeGraph ();

		EditorGUILayout.LabelField ("this is our node editor");
		workView.UpdateView (position, new Rect(0f,0f,viewPercentage,1f), e, currentGraph);
		propertyView.UpdateView (new Rect(position.width, position.y, position.width, position.height),	new Rect(viewPercentage, 0f, 1-viewPercentage, 1f), e, currentGraph);
		Repaint ();
	}

	static void CreateViews () {
		if (currentWindow != null) {
			currentWindow.propertyView = new NodePropertyView ();
			currentWindow.workView = new NodeWorkView ();
		} else {
			currentWindow = (NodeEditorWindow) EditorWindow.GetWindow<NodeEditorWindow> ();
		}
	}

	void ProcessEvents (Event e)
	{
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftArrow) {
			viewPercentage -= 0.01f;
		}
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.RightArrow) {
			viewPercentage += 0.01f;
		}
	}
}
