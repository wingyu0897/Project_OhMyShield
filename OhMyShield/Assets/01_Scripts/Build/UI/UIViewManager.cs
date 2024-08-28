using System.Collections.Generic;
using UnityEngine;

public class UIViewManager : MonoSingleton<UIViewManager>
{
    [SerializeField] private UIView _rootView;
	private UIView _currentView;

	private List<UIView> _views = new List<UIView>();
	private Stack<UIView> _history = new Stack<UIView>();

	public static UIView GetView<T>() where T : UIView
	{
		if (_instance is null) return null;

		UIView view = _instance._views.Find(view => view as T is not null);
		return view;
	}

	public static void ShowView<T>() where T : UIView
	{
		if (_instance is null) return;

		UIView view = _instance._views.Find(view => view as T is not null);
		if (view != null)
		{
			_instance._currentView?.Hide();
			view.Show();
			_instance._history.Push(view);
			_instance._currentView = view;
		}
	}

	public static void ShowView(UIView view)
	{
		if (_instance is null) return;

		if (view != null)
		{
			_instance._currentView?.Hide();
			view.Show();
			_instance._history.Push(view);
			_instance._currentView = view;
		}
	}

	public static void PopView()
	{
		if (_instance is null) return;

		if (_instance._history.Count > 0)
		{
			UIView curView = _instance._history.Pop();
			curView.Hide();
			UIView lastView = _instance._history.Peek();
			lastView?.Show();
		}
	}

	protected override void Awake()
	{
		base.Awake();

		GetComponentsInChildren(true, _views);

		for (int i = 0; i < _views.Count; ++i)
		{
			_views[i].Initialize();
			_views[i].Hide();
		}

		ShowView(_rootView);
	}
}
