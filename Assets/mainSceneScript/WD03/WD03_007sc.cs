using UnityEngine;
using System.Collections;

public class WD03_007sc : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
	int count=1;

	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.attackArts=true;
		BodyScript.notMainArts=true;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            //			ManagerScript.stopFlag=true;
            BodyScript.DialogFlag = true;
            BodyScript.DialogNum = 1;

            count = 0;
            for (int i = 0; i < 3; i++)
            {
                if (ManagerScript.getSigniConditionInt(i, 1 - player) == 1) count++;
            }
            if (count > 2) count = 2;
            BodyScript.DialogCountMax = count;
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            count = int.Parse(BodyScript.messages[0]);
            BodyScript.messages.Clear();

            if (count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    int x = ManagerScript.getFieldRankID(3, i, 1 - player);
                    if (x > 0 && ManagerScript.getSigniConditionInt(i, 1 - player) == 1)
                    {
                        BodyScript.Targetable.Add(x + 50 * (1 - player));
                    }
                }
                if (BodyScript.Targetable.Count > 0)
                {
                    for (int i = 0; i < BodyScript.Targetable.Count && i < count; i++)
                    {
                        BodyScript.effectTargetID.Add(-1);
                        BodyScript.effectMotion.Add((int)Motions.EffectDown);
                    }
                    BodyScript.effectFlag = true;
                }
            }
        }

        field = ManagerScript.getFieldInt(ID, player);
    }	
}
