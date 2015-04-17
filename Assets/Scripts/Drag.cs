using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{
	bool isClicked = false;
	Vector2 currentPoint;
	RaycastHit2D movingObject;
	Vector2 normalPoint {
				get;
				set;
	}
	void Start(){
		normalPoint = this.transform.position;
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			OnMouseDown();
		}
		if(Input.GetMouseButton(0)){
			OnMouseDrag();
		}
		if(Input.GetMouseButtonUp(0)){
			OnMouseUp();
		}
	}
	
	public void OnMouseDown(){
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
		Vector3 newVector = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
		Vector2 tapPoint = new Vector2(newVector.x, newVector.y);
		Collider2D colition2d = Physics2D.OverlapPoint(tapPoint);
		
		if(colition2d) {
			RaycastHit2D hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);
			if(hitObject){
				Debug.Log("hit object is " + hitObject.collider.gameObject.name);
				currentPoint = new Vector2(tapPoint.x, tapPoint.y);
				normalPoint = hitObject.collider.gameObject.transform.position;
				movingObject = hitObject ;
				isClicked = true;
			}
		}
	}
	
	public void OnMouseDrag(){
		if(!isClicked){
			return;

		}
		
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(movingObject.collider.gameObject.transform.position);
		Vector3 newVector = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
		Vector2 tapPoint = new Vector2(newVector.x, newVector.y);
		movingObject.collider.gameObject.transform.position = new Vector3( movingObject.collider.gameObject.transform.position.x + (tapPoint.x - currentPoint.x), movingObject.collider.gameObject.transform.position.y + (tapPoint.y - currentPoint.y),-0.2f);
		currentPoint = tapPoint;
	}
	
	public GameObject OnMouseUp(){
		if (isClicked) {
		
			isClicked = false;
			movingObject.collider.gameObject.transform.position = normalPoint;
			return movingObject.collider.gameObject;
		} else
			return null;
		}


}