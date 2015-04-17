using UnityEngine;
using System.Collections;

public class WX05_002 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool costFlag = false;
    int powerSum = 0;

    IgniAdd addIgni;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();


   }
	
	// Update is called once per frame
	void Update ()
    {
        int nowLrigID=ManagerScript.getLrigID(player);
        if(nowLrigID >= 0)
            BodyScript.useLimit = !ManagerScript.getCardScr(nowLrigID, player).Name.Contains("花代");

        //cip
        if (ManagerScript.getCipID() == ID + player * 50)
        {
            int target=player;
            for (int i = 0; i < 50; i++)
            {
                if (ManagerScript.getCardType(i, target) == 2)
                {

                    GameObject obj = ManagerScript.getFront(i, target);

                    obj.AddComponent<EnDoubleCrashScr>();
                    EnDoubleCrashScr[] scr = obj.GetComponents<EnDoubleCrashScr>();

                    foreach (var item in scr)
                    {
                        if (item.masterSerial == "")
                        {
                            item.masterSerial = ManagerScript.getSerialNum(ID, player);
                            break;
                        }
                    }
                }
            }
            
        }


        if (addIgni!=null && addIgni.upIgnition)
        {
            int target = player;
            int f = (int)Fields.LRIGTRASH;
            int num=ManagerScript.getNumForCard(f,player);
 
            for (int i = 0; i < num; i++)
			{
                int x=ManagerScript.getFieldRankID(f,num-i-1,target);

                if (x >= 0 && ManagerScript.getCardType(x, target) == 0)
                    addIgni.setIgniTarget(x, target);
			}
        }

        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            ManagerScript.targetableExceedIn(player, BodyScript);

            int exNum = 5;

            if (BodyScript.Targetable.Count >= exNum)
            {
                for (int i = 0; i < exNum; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.Exceed);
                }

                costFlag = true;
                BodyScript.effectFlag = true;
            }
            else
                BodyScript.Targetable.Clear();
        }

        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;
            powerSum = 0;

            banishTargetIn();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);

                BodyScript.TargetIDEnable = true;
            }
        }

        if (BodyScript.TargetID.Count > 0)
        {
            int tID = BodyScript.TargetID[0];
            BodyScript.TargetID.RemoveAt(0);

            powerSum += ManagerScript.getCardPower(tID % 50, tID / 50);

            banishTargetIn();

            BodyScript.Targetable.Remove(tID);

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);
            }
            else
            {
                BodyScript.TargetIDEnable = false;

                //add ingi
                if (gameObject.GetComponent<IgniAdd>() == null)
                    gameObject.AddComponent<IgniAdd>();

                addIgni = gameObject.GetComponent<IgniAdd>();
            }
        }

	}

    void banishTargetIn()
    {
        BodyScript.Targetable.Clear();

        int target = 1 - player;
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0 && ManagerScript.getCardPower(x,target)+powerSum <= 30000)
                BodyScript.Targetable.Add(x + 50 * target);
        }
    }
}
