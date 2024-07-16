using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using System.Collections.Generic;
using System;

public class BehaviourTreeView : GraphView
{
	public Action<NodeView> OnNodeSelected;
	public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { };
	private BehaviourTree _tree;

    public BehaviourTreeView()
	{
		Insert(0, new GridBackground());

		this.AddManipulator(new ContentZoomer());
		this.AddManipulator(new ContentDragger());
		this.AddManipulator(new SelectionDragger());
		this.AddManipulator(new RectangleSelector());
		 
		var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/08_UIToolkit/BehaviourTreeEditor.uss");
		styleSheets.Add(styleSheet);
	}

	private NodeView FindNodeView(Node node)
	{
		return GetNodeByGuid(node.guid) as NodeView;
	}

	public void PopulateView(BehaviourTree tree)
	{
		_tree = tree;

		graphViewChanged -= OnGraphViewChanged;
		DeleteElements(graphElements);
		graphViewChanged += OnGraphViewChanged;

		if (tree.rootNode == null)
		{
			tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
			EditorUtility.SetDirty(tree);
			AssetDatabase.SaveAssets();
		}

		tree.nodes.ForEach(n => CreateNodeView(n));

		_tree.nodes.ForEach(n => {
			var children = tree.GetChildren(n);
			children.ForEach(c =>
			{
				NodeView parentView = FindNodeView(n);
				NodeView childView = FindNodeView(c);

				Edge edge = parentView.output.ConnectTo(childView.input);
				AddElement(edge);
			});
		});
	}

	public void DrawView()
	{
		graphViewChanged -= OnGraphViewChanged;
		DeleteElements(graphElements);
		graphViewChanged += OnGraphViewChanged;

		_tree.nodes.ForEach(n => CreateNodeView(n));

		_tree.nodes.ForEach(n => {
			var children = _tree.GetChildren(n);
			children.ForEach(c =>
			{
				NodeView parentView = FindNodeView(n);
				NodeView childView = FindNodeView(c);

				Edge edge = parentView.output.ConnectTo(childView.input);
				AddElement(edge);
			});
		});
	}

	public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
	{
		return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
	}

	private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
	{
		if (graphViewChange.elementsToRemove != null)
		{
			graphViewChange.elementsToRemove.ForEach(elem =>
			{
				NodeView nodeView = elem as NodeView;
				if (nodeView != null)
				{
					_tree.DeleteNode(nodeView.node);
				}

				Edge edge = elem as Edge;
				if (edge != null)
				{
					NodeView parentView = edge.output.node as NodeView;
					NodeView childView = edge.input.node as NodeView;
					_tree.RemoveChild(parentView.node, childView.node);
				}
			});
		}

		if (graphViewChange.edgesToCreate != null)
		{
			graphViewChange.edgesToCreate.ForEach(edge =>
			{
				NodeView parentView = edge.output.node as NodeView;
				NodeView childView = edge.input.node as NodeView;
				_tree.AddChild(parentView.node, childView.node);
			});
		}

		return graphViewChange;
	}

	public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
	{
		//base.BuildContextualMenu(evt);
		
		{ 
			var types = TypeCache.GetTypesDerivedFrom<ActionNode>(); 
			foreach(var type in types)
			{
				evt.menu.AppendAction($"[{type.BaseType.Name}]/{type.Name}", (a) => CreateNode(type));
			}
		}

		{
			var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
			foreach(var type in types)
			{
				evt.menu.AppendAction($"[{type.BaseType.Name}]/{type.Name}", (a) => CreateNode(type));
			}
		}

		{
			var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
			foreach(var type in types)
			{
				evt.menu.AppendAction($"[{type.BaseType.Name}]/{type.Name}", (a) => CreateNode(type));
			}
		}
	}

	public void DeleteNode(NodeView nodeView)
	{
		_tree.DeleteNode(nodeView.node);
		//RemoveElement(nodeView);
		DrawView();
	}

	private void CreateNode(System.Type type)
	{
		Node node = _tree.CreateNode(type);
		CreateNodeView(node);
	}

	private void CreateNodeView(Node node)
	{
		NodeView nodeView = new NodeView(node);
		nodeView.OnNodeSelected = OnNodeSelected;
		nodeView.treeView = this;
		AddElement(nodeView);
	}
}
