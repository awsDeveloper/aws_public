using UnityEngine;
using System.Collections;

public class messageScript : MonoBehaviour {
	
	public GUIText message;
	
	int count=0;

	// Use this for initialization
	void Start () {
		float x=transform.position.x;
		float y=transform.position.y;
		
		message.transform.position=new Vector3( x, y, 1f);
		
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		count++;
		
		if(count>100)Destroy(gameObject);
	}
	
	public void messageShow(string s){
		message.text=s;
	}
}
