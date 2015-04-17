using UnityEngine;
using System.Collections;

public class WX05_054 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<otherSameClassPowerUp>();
        otherSameClassPowerUp scr = gameObject.GetComponent<otherSameClassPowerUp>();

        scr.setClassList(5, 3);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
