using UnityEngine;
using System.Collections;

public class GoConfig : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Pouse"))
        {
            Application.LoadLevelAdditive("config");
            Singleton<config>.instance.configNow = true;
        }
	
	}
}
