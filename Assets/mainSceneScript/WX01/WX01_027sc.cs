using UnityEngine;
using System.Collections;

public class WX01_027sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool DialogFlag=false;
	bool ignition=false;
	int enaNum=0;
	bool costFlag=false;
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
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
        {
            //cip
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, player);
                if (ManagerScript.getIDConditionInt(x, player) == 2)
                {
                    BodyScript.Targetable.Add(x + 50 * player);
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.DialogFlag = true;
            }

            //equip multiEna
            int target = player;
            for (int i = 0; i < 50; i++)
            {
                GameObject obj = ManagerScript.getFront(i, target);

                obj.AddComponent<enMultiEnaScr>();
                enMultiEnaScr[] scr = obj.GetComponents<enMultiEnaScr>();

                foreach (var item in scr)
                {
                    if (item.masterID == -1)
                    {
                        item.masterID = ID + player * 50;
                        break;
                    }
                }
            }


 /*           for (int i = 0; i < enaNum; i++)
            {
                int x = ManagerScript.getFieldRankID(6, i, player);
                CardScript sc = ManagerScript.getCardScr(x, player);
                if (!sc.MultiEnaFlag)
                {
                    sc.MultiEnaFlag = true;
                    equipList.Add(x + 50 * player);
                }
            }*/
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + player * 50);
                BodyScript.effectMotion.Add(17);
                BodyScript.Cost[0] = 0;
                BodyScript.Cost[1] = 1;
                BodyScript.Cost[2] = 0;
                BodyScript.Cost[3] = 0;
                BodyScript.Cost[4] = 0;
                BodyScript.Cost[5] = 0;
                costFlag = true;
            }
            else BodyScript.Targetable.Clear();
            BodyScript.messages.Clear();
        }

        //after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;
            BodyScript.effectTargetID.Add(-1);
            BodyScript.effectMotion.Add(9);
        }

        //equip
/*        if (ManagerScript.getFieldInt(ID, player) != 3 && field == 3 && !BodyScript.BurstFlag)
        {
            while (equipList.Count > 0)
            {
                int x = equipList[0];
                CardScript sc = ManagerScript.getCardScr(x % 50, x / 50);
                sc.MultiEnaFlag = false;
                equipList.RemoveAt(0);
            }
        }
        if (equipList.Count > 0)
        {
            for (int i = 0; i < equipList.Count; i++)
            {
                int x = equipList[i];
                if (ManagerScript.getFieldInt(x % 50, x / 50) != 6)
                {
                    CardScript sc = ManagerScript.getCardScr(x % 50, x / 50);
                    sc.MultiEnaFlag = false;
                    equipList.RemoveAt(i);
                    i--;
                }
            }
        }
 
        if (ManagerScript.getFieldAllNum(6, player) > enaNum && ManagerScript.getFieldInt(ID, player) == 3)
        {
            int x = ManagerScript.getFieldRankID(6, enaNum, player);
            CardScript sc = ManagerScript.getCardScr(x, player);
            if (!sc.MultiEnaFlag)
            {
                sc.MultiEnaFlag = true;
                equipList.Add(x + 50 * player);
            }
        }*/

        //ignition
        if (BodyScript.Ignition && !ignition)
        {
            int wc = ManagerScript.getEnaColorNum(1, player);
            int mNum = ManagerScript.MultiEnaNum(player);
            if (wc + mNum >= 2)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + player * 50);
                BodyScript.effectMotion.Add(17);
                BodyScript.Cost[0] = 0;
                BodyScript.Cost[1] = 2;
                BodyScript.Cost[2] = 0;
                BodyScript.Cost[3] = 0;
                BodyScript.Cost[4] = 0;
                BodyScript.Cost[5] = 0;
                ManagerScript.enajeFlag[1-player] = true;
                BodyScript.Ignition = false;
            }
        }
        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int deckNum = ManagerScript.getFieldAllNum(0, player);
            for (int i = 0; i < deckNum; i++)
            {
                int x = ManagerScript.getFieldRankID(0, i, player);
                if (x >= 0 && ManagerScript.getCardColor(x, player) == 1)
                {
                    BodyScript.Targetable.Add(x + 50 * player);
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add(16);
            }
        }

        field = ManagerScript.getFieldInt(ID, player);
        ignition = BodyScript.Ignition;
        enaNum = ManagerScript.getFieldAllNum(6, player);
    }

}
