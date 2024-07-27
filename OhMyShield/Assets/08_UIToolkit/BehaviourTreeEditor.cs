using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;

public class BehaviourTreeEditor : EditorWindow
{
    private BehaviourTreeView _treeView;
    private InspectorView _inspectorView;
    private IMGUIContainer _blackboardView;

    private SerializedObject _treeObject;
    private SerializedProperty _blackboardProperty;

    [MenuItem("BehaviourTreeEditor/Editor")]
    public static void OpenWindow() 
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
	{
        if (Selection.activeObject is BehaviourTree)
		{
            OpenWindow();
            return true;
		}
        return false;
	}

    public void CreateGUI()
    {
        Debug.Log("CreateGUIEvent");
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/08_UIToolkit/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/08_UIToolkit/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        _treeView = root.Q<BehaviourTreeView>();
        _inspectorView = root.Q<InspectorView>();
        _blackboardView = root.Q<IMGUIContainer>();
        _blackboardView.onGUIHandler = () =>
		{
            _treeObject?.Update();
            if (_blackboardProperty != null)
                EditorGUILayout.PropertyField(_blackboardProperty);
            _treeObject?.ApplyModifiedProperties();
		};

        _treeView.OnNodeSelected = OnNodeSelectionChange;
        OnSelectionChange();
    }

	private void OnEnable()
	{
        EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
		EditorApplication.playModeStateChanged += OnPlayModeStateChange;
	}

	private void OnDisable()
	{
        EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
    }

	private void OnPlayModeStateChange(PlayModeStateChange obj)
	{
        Debug.Log("OnPlayModeStateChangeEvent");
		switch (obj)
		{
			case PlayModeStateChange.EnteredEditMode:
                Debug.Log("EnteredEditMode");
                OnSelectionChange();
				break;
			case PlayModeStateChange.ExitingEditMode:
				break;
			case PlayModeStateChange.EnteredPlayMode:
                Debug.Log("EnteredPlayMode");
                OnSelectionChange();
				break;
			case PlayModeStateChange.ExitingPlayMode:
				break;
			default:
				break;
		}
	}

    private void OnSelectionChange()
	{
		BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (!tree)
		{
            if (Selection.activeGameObject)
			{
                BehaviourTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviourTreeRunner>();
                if (runner)
				{
                    tree = runner.tree;
				}
			}
        }

        if (Application.isPlaying)
		{
            if (tree)
		    {
                _treeView.PopulateView(tree);
		    }
		}
        else
		{
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                _treeView.PopulateView(tree);
            }
        }

        if (tree != null)
		{
            _treeObject = new SerializedObject(tree);
            _blackboardProperty = _treeObject.FindProperty("blackboard");
		}
	}

    private void OnNodeSelectionChange(NodeView node)
	{
        _inspectorView.UpdateSelection(node);
	}

	private void OnInspectorUpdate()
	{
		_treeView?.UpdateNodeState();
	}
}
