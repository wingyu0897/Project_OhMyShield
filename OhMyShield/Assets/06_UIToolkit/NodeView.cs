
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEditor.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
	public Action<NodeView> OnNodeSelected;
    public Node node;
    public BehaviourTreeView treeView;
	public Port input;
	public Port output;

    public NodeView(Node node) : base("Assets/06_UIToolkit/NodeView.uxml")
    {
        this.node = node;
        title = node.name;
		viewDataKey = node.guid;

		style.left = node.position.x;
		style.top = node.position.y;

		CreateInputPorts();
		CreateOutputPorts();
		SetupClasses();

		Label descriptionLabel = this.Q<Label>("description");
		descriptionLabel.bindingPath = "description";
		descriptionLabel.Bind(new SerializedObject(node));
    }

	private void SetupClasses()
	{
		if (node is ActionNode)
		{
			AddToClassList("action");
		}
		else if (node is CompositeNode)
		{
			AddToClassList("composite");
		}
		else if (node is DecoratorNode)
		{
			AddToClassList("decorator");
		}
		else if (node is RootNode)
		{
			AddToClassList("root");
		}
	}

	private void CreateInputPorts()
	{
		if (node is ActionNode)
		{
			input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
		}
		else if (node is CompositeNode)
		{
			input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
		}
		else if (node is DecoratorNode)
		{
			input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
		}
		else if (node is RootNode)
		{

		}

		if (input != null)
		{
			input.portName = "";
			input.style.flexDirection = FlexDirection.Column;
			inputContainer.Add(input);
		}
	}

	private void CreateOutputPorts()
	{
		if (node is ActionNode)
		{
		}
		else if (node is CompositeNode)
		{
			output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
		}
		else if (node is DecoratorNode)
		{
			output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
		}
		else if (node is RootNode)
		{
			output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
		}

		if (output != null)
		{
			output.portName = "";
			output.style.flexDirection = FlexDirection.ColumnReverse;
			outputContainer.Add(output);
		}
	}

	public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
	{
		{
			evt.menu.AppendAction($"[Delete]", (a) => 
			{
				treeView.DeleteNode(this);
			});
		}
	}

	public override void SetPosition(Rect newPos)
	{
		base.SetPosition(newPos);
		Undo.RecordObject(node, "Behaviour Tree (Set Position)");
		node.position.x = newPos.xMin;
		node.position.y = newPos.yMin;
		EditorUtility.SetDirty(node);
	}

	public override void OnSelected()
	{
		base.OnSelected();

		OnNodeSelected?.Invoke(this);
	}

	public void SortChildren()
	{
		CompositeNode composite = node as CompositeNode;
		if (composite)
		{
			composite.children.Sort(SortByHorizontalPosition);
		}
	}

	private int SortByHorizontalPosition(Node left, Node right)
	{
		return left.position.x < right.position.x ? -1 : 1;
	}

	public void UpdateState()
	{
		RemoveFromClassList("running");
		RemoveFromClassList("failure");
		RemoveFromClassList("success");

		if (Application.isPlaying)
		{
			switch (node.state)
			{
				case Node.State.Running:
					if (node.started)
					{
						AddToClassList("running");
					}
					break;
				case Node.State.Failure:
					AddToClassList("failure");
					break;
				case Node.State.Success:
					AddToClassList("success");
					break;
			}
		}
	}
}
