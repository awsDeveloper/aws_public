using UnityEngine;
using System.Collections;

public class PR_098 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!sc.isChanted())
            return;

        sc.setEffect(0, player, Motions.JerashiGeizu);
	
	}
}
