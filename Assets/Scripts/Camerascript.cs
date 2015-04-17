using UnityEngine;
using System.Collections;

public class Camerascript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		Vector3 cardpoint = GetComponent<Camera>().WorldToScreenPoint (new Vector3 (-12f, 7f, 0f));
		Vector3 cardendpoint = GetComponent<Camera>().WorldToScreenPoint (new Vector3 (24f, 3f, 0f));
		GUI.DrawTexture(new Rect(cardpoint.x,Screen.height - cardpoint.y,cardendpoint.x-cardpoint.x,cardpoint.y-cardendpoint.y), (Texture)Resources.Load ("wakumade"));

		Vector3 lcardpoint = GetComponent<Camera>().WorldToScreenPoint (new Vector3 (-12f, 2f, 0f));
		Vector3 lcardendpoint = GetComponent<Camera>().WorldToScreenPoint (new Vector3 (24f, -1.9f, 0f));
		GUI.DrawTexture(new Rect(lcardpoint.x,Screen.height - lcardpoint.y,lcardendpoint.x-lcardpoint.x,lcardpoint.y-lcardendpoint.y), (Texture)Resources.Load ("wakumade"));

		Vector3 nlcardpoint = GetComponent<Camera>().WorldToScreenPoint (new Vector3 (-12f, -2.1f, 0f));
		Vector3 nlcardendpoint = GetComponent<Camera>().WorldToScreenPoint (new Vector3 (24f,-9.9f, 0f));
		GUI.DrawTexture(new Rect(nlcardpoint.x,Screen.height - nlcardpoint.y,nlcardendpoint.x-nlcardpoint.x,nlcardpoint.y-nlcardendpoint.y), (Texture)Resources.Load ("wakumade2"));

		Vector3 ncardpoint = GetComponent<Camera>().WorldToScreenPoint (new Vector3 (-12f, -10.1f, 0f));
		Vector3 ncardendpoint = GetComponent<Camera>().WorldToScreenPoint (new Vector3 (24f,-17.9f, 0f));
		GUI.DrawTexture(new Rect(ncardpoint.x,Screen.height - ncardpoint.y,ncardendpoint.x-ncardpoint.x,ncardpoint.y-ncardendpoint.y), (Texture)Resources.Load ("wakumade2"));

	}
}
