using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler 
{
	[SerializeField] private RectTransform _boundsSpace;
	[SerializeField] private RectTransform _handle;

	[Space]
	public UnityEvent OnValueChanged;

	public void OnPointerDown(PointerEventData eventData)
	{
		
	} 

	public void OnPointerUp(PointerEventData eventData)
	{
		
	}

	public void OnDrag(PointerEventData eventData)
	{
		CalculateHandlePosition(eventData.position);
	}

	private void CalculateHandlePosition(Vector2 position)
	{
		print(Camera.main.ScreenToViewportPoint(position).ToString());
		Vector2 direction = position - (Vector2)_boundsSpace.position;
		if (direction.magnitude > _boundsSpace.rect.width * 0.5f)
		{
			direction = direction.normalized * _boundsSpace.rect.width * 0.5f;
		}
		_handle.anchoredPosition = direction;
	}
}
