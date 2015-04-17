using UnityEngine;
using System.Collections;

public class DebugCardHolder : MonoBehaviour {
	DeckMake deckmake;
	private string[] tok=new string[50];
	// Use this for initialization
	void Start () {
		deckmake = GetComponent<DeckMake>();
		TextAsset txt=(TextAsset)Resources.Load("Deck");
		tok=txt.text.Split(' ','\n');
		deckmake.SetCard (tok[0]);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
