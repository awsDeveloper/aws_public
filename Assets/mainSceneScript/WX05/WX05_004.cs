using UnityEngine;
using System.Collections;

public class WX05_004 : MonoBehaviour {
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
            BodyScript.useLimit = !ManagerScript.getCardScr(nowLrigID, player).Name.Contains("緑姫");

        //cip
        if (ManagerScript.getCipID() == ID + player * 50)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectMotion.Add((int)Motions.TopGoLifeCloth);
            BodyScript.effectTargetID.Add(50 * player);
        }

        //addigni
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

        //ignition
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

        //ignition after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;

            int f = (int)Fields.ENAZONE;

            for (int j = 0; j < 2; j++)
            {
                int target = j;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);

                    if (x >= 0)
                    {
                        int color = ManagerScript.getCardColor(x, target);

                        if (color == 1 || color == 2 || color == 3 || color == 4 || color == 5)
                        {
                            BodyScript.effectFlag = true;
                            BodyScript.effectMotion.Add((int)Motions.GoTrash);
                            BodyScript.effectTargetID.Add(x + 50 * target);
                        }
                    }
                }

                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.EnaSort);
                BodyScript.effectTargetID.Add(0);

                BodyScript.effectMotion.Add((int)Motions.EnaSort);
                BodyScript.effectTargetID.Add(50);
            }

            //add ingi
            if (gameObject.GetComponent<IgniAdd>() == null)
                gameObject.AddComponent<IgniAdd>();

            addIgni = gameObject.GetComponent<IgniAdd>();
        }
	}
}
