using UnityEngine;
using System.Collections;

public class WX04_082 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool effectFlag=false;
	int attackerID=-1;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;

		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();

	}
	
	// Update is called once per frame
	void Update () {
		//誘発のチェック
		if(attackerID!=-1 && !ManagerScript.isEffectFlagUp())
		{
			if(ManagerScript.getFieldInt(ID,player) ==3 && ManagerScript.getFieldInt(attackerID % 50,attackerID/50)==3 )
			{
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(attackerID);
				BodyScript.effectMotion.Add(27);
			}
			
			attackerID=-1;
		}
		
		//attackerID のセット
		if(ManagerScript.getFieldInt(ID,player) ==3 && ManagerScript.getAttackerID()!=-1 && ManagerScript.getAttackerID()/50==1-player)
		{
            if (ManagerScript.getAttackFrontRank() == ManagerScript.getRank(ID, player))
            {
                attackerID = ManagerScript.getAttackerID();
                ManagerScript.stopFlag = true;
            }
		}

		//UpDate
		field=ManagerScript.getFieldInt(ID,player);
	}
}
