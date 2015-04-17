using UnityEngine;
using System.Collections;

public class WD04_017sc : MonoBehaviour {
	GameObject Body;
	CardScript BodyScript;
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		BodyScript.MultiEnaFlag=true;
		BodyScript.GuardFlag=true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
