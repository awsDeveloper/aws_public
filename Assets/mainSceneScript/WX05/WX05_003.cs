using UnityEngine;
using System.Collections;

public class WX05_003 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool costFlag = false;
 
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
            BodyScript.useLimit = !ManagerScript.getCardScr(nowLrigID, player).Name.Contains("ピルルク");

        //cip
        if (ManagerScript.getCipID() == ID + player * 50)
        {
            int target=1-player;
            int f = 2;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0)
                {
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(x + 50 * target);
                    BodyScript.effectMotion.Add((int)Motions.GoTrash);
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
                int x = ManagerScript.getFieldRankID(f, num - i - 1, target);

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

            for (int i = ManagerScript.getFieldAllNum(2,player); i < 6; i++)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(50 * player);
                BodyScript.effectMotion.Add(2);
            }

            //add ingi
            if (gameObject.GetComponent<IgniAdd>() == null)
                gameObject.AddComponent<IgniAdd>();

            addIgni = gameObject.GetComponent<IgniAdd>();
        }
	}
}
