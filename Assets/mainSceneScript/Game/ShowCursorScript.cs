using UnityEngine;
using System.Collections;

public class ShowCursorScript : MonoBehaviour {
	bool downFlag=false;
	int interval=10;
	int count=0;
	float downHeight=0.1f;
	int deathCount=0;

	// Use this for initialization
	void Start () {
	deathCount=interval*6;
	}
	
	// Update is called once per frame
	void Update () {
		count++;
		
		if(count >= deathCount)
		{
			
			Destroy(gameObject);
			
		}
		else if(count % interval == 0)
		{
			if(downFlag)
			{
				
				transform.position=new Vector3(transform.position.x,transform.position.y+downHeight,transform.position.z);
				
			}
			else
			{
				
				transform.position=new Vector3(transform.position.x,transform.position.y-downHeight,transform.position.z);
				
			}
			
			downFlag=!downFlag;
		}
	}
}
