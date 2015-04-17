using UnityEngine;
using System.Collections;

public class WD04_011sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;

	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=2000;
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	// Update is called once per frame
	void Update () {
		if (ManagerScript.getFieldInt (ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
		{
			for (int i=0; i<3; i++)
			{
				int x = ManagerScript.getFieldRankID (3, i, player);
				if (x >= 0)
				{
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (x + 50 * player);
					BodyScript.effectMotion.Add (34);
				}
			}
		}

		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(26);
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}
