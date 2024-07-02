using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler 
{
	private Canvas _canvas;

	[SerializeField] private RectTransform _boundsSpace;
	[SerializeField] private RectTransform _handle;

	[Space] 
	public UnityEvent<Vector2> OnValueChanged;

	private Vector2 _value;
	public Vector2 Value => _value;

	private void Awake()
	{
		_canvas = _boundsSpace.root.GetComponent<Canvas>();
		_value = Vector2.zero;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		
	} 

	public void OnPointerUp(PointerEventData eventData)
	{
		_handle.anchoredPosition = Vector2.zero;
		_value = Vector2.zero;
	}

	public void OnDrag(PointerEventData eventData)
	{
		CalculateHandlePosition(eventData.position);
		CalculateValue();
	}

	private void CalculateHandlePosition(Vector2 position)
	{
		float scaleFactor = _canvas.scaleFactor;
		float radius = _boundsSpace.rect.width * scaleFactor * 0.5f;

		Vector2 direction = position - (Vector2)_boundsSpace.position;
		if (direction.magnitude > radius)
		{
			direction = direction.normalized * radius;
		}
		_handle.anchoredPosition = direction / scaleFactor;
	}

	private void CalculateValue()
	{
		_value = _handle.anchoredPosition / (_boundsSpace.rect.width * 0.5f);

		OnValueChanged?.Invoke(_value);
	}
}
