using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VJHandler : MonoBehaviour,IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image jsContainer;
	private Image joystick;
	private Canvas joystickCanvas;
	
	public Vector3 InputDirection ;
	
	void Start(){
		
		jsContainer = GetComponent<Image>();
		joystick = transform.GetChild(0).GetComponent<Image>(); //this command is used because there is only one child in hierarchy
		InputDirection = Vector3.zero;
		joystickCanvas = transform.parent.GetComponent<Canvas>();
		joystickCanvas.enabled = false;
	}

	void Update() {
		if (Input.touchCount > 0) {
			foreach(Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began && touch.position.x <= Screen.width*0.5) {
					joystickCanvas.enabled = true;
					jsContainer.transform.position = touch.position;
				}

				if (touch.phase == TouchPhase.Moved && touch.position.x <= Screen.width*0.5) {
					PointerEventData ped = new PointerEventData(EventSystem.current);
					ped.position = touch.position;
					ped.delta = touch.deltaPosition;
					OnDrag(ped);
				}

				if (touch.phase == TouchPhase.Ended && touch.position.x <= Screen.width*0.5) {
					joystickCanvas.enabled = false;
					OnPointerUp(new PointerEventData(EventSystem.current));
				}
			}
		}
	}
	
	public void OnDrag(PointerEventData ped){
		Vector2 position = Vector2.zero;
		//To get InputDirection
		// RectTransformUtility.ScreenPointToLocalPointInRectangle
		// 		(jsContainer.rectTransform, 
		// 		ped.position,
		// 		ped.pressEventCamera,
		// 		out position);

		RectTransformUtility.ScreenPointToLocalPointInRectangle
				(jsContainer.rectTransform, 
				ped.position,
				null,	
				out position);
			position.x = (position.x/jsContainer.rectTransform.sizeDelta.x);
			position.y = (position.y/jsContainer.rectTransform.sizeDelta.y);
			
			// float x = (jsContainer.rectTransform.pivot.x == 1f) ? position.x *2 + 1 : position.x *2 - 1;
			// float y = (jsContainer.rectTransform.pivot.y == 1f) ? position.y *2 + 1 : position.y *2 - 1;
			float x = position.x * 3;
			float y = position.y * 3;
			InputDirection = new Vector3 (x,y,0);
			InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;
			
			//to define the area in which joystick can move around
			joystick.rectTransform.anchoredPosition = new Vector3 (InputDirection.x * (jsContainer.rectTransform.sizeDelta.x/3)
			                                                       ,InputDirection.y * (jsContainer.rectTransform.sizeDelta.y)/3);
			
	}
	
	public void OnPointerDown(PointerEventData ped){
		Debug.Log(ped);	
		OnDrag(ped);
	}
	
	public void OnPointerUp(PointerEventData ped){
		
		InputDirection = Vector3.zero;
		joystick.rectTransform.anchoredPosition = Vector3.zero;
	}
}