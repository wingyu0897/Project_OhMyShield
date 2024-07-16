using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    private BehaviourTreeView _treeView;
    private InspectorView _inspectorView;

    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("BehaviourTreeEditor/Editor")]
    public static void OpenWindow() 
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/08_UIToolkit/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/08_UIToolkit/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        _treeView = root.Q<BehaviourTreeView>();
        _inspectorView = root.Q<InspectorView>();
        _treeView.OnNodeSelected = OnNodeSelectionChange;

        OnSelectionChange();
    }

	private void OnSelectionChange()
	{
		BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
		{
            _treeView.PopulateView(tree);
		}
	}

    private void OnNodeSelectionChange(NodeView node)
	{
        _inspectorView.UpdateSelection(node);
	}
}
