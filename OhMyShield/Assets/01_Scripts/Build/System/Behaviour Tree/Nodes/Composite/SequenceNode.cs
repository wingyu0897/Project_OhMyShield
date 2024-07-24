
public class SequenceNode : CompositeNode
{
	private int _current;

	protected override void OnStart()
	{
		_current = 0;
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		Node child = children[_current];

		State state = child.Update();

		switch (state)
		{
			case State.Running:
			case State.Failure:
				return state;
			case State.Success:
				_current++;
				break;
		}

		return _current == children.Count ? State.Success : State.Running;
	}
}
