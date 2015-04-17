using UnityEngine;
using System.Collections;

public class WX04_063 : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;

	bool[] searchedColor = new bool[6];
	bool chantFlag=false;
	bool effectEnable=false;

	// Use this for initialization
	void Start ()
	{
		Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		
		Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();

		BodyScript.PayedCostEnable = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//chant
		if (ManagerScript.getFieldInt (ID, player) == 8  && field!=8 && !BodyScript.BurstFlag )
		{
			chantFlag=true;

			for (int i = 0; i < searchedColor.Length; i++) {
				searchedColor[i]=false;
			}
			
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50*player);
			BodyScript.effectMotion.Add (22);
			BodyScript.AntiCheck=true;
		}

		if(chantFlag && BodyScript.effectTargetID.Count==0){
			chantFlag=false;

			if(!BodyScript.AntiCheck)
				effectEnable=true;
			else 
				BodyScript.AntiCheck=false;
		}

		if(effectEnable && BodyScript.effectTargetID.Count==0){
			BodyScript.Targetable.Clear();

            while (BodyScript.PayedCostList.Count > 0)
            {
                int pID = BodyScript.PayedCostList[0];
                BodyScript.PayedCostList.RemoveAt(0);

                if (pID >= 0)
                {
                    int c = ManagerScript.getCardColor(pID % 50, pID / 50);
                    CardScript sc = ManagerScript.getCardScr(pID % 50, pID / 50);

                    if (sc.MultiEnaFlag || (!searchedColor[c] && c != 0))
                    {
                        if (sc.MultiEnaFlag)
                            effect(6);
                        else
                            effect(c);

                        if (BodyScript.effectTargetID.Count > 0)
                            break;
                    }
                }
            }
		}

		if(BodyScript.TargetID.Count>0)
		{
			int pID = BodyScript.TargetID [0];
			BodyScript.TargetID.RemoveAt(0);
			
			int c = ManagerScript.getCardColor (pID % 50, pID / 50);
			searchedColor[c]=true;
		}

		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}

	void effect(int color)
	{
		if (color > 0)
		{
			int target = player;
			int f = 0;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				int c=ManagerScript.getSigniColor(x, target);
				if (x >= 0 && ((color == 6 && c > 0 && !searchedColor[c]) || c == color ) )
				{
					BodyScript.Targetable.Add (x + 50 * (target));
				}
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (16);
				
				BodyScript.TargetIDEnable=true;
			}			
		}
	}
}
