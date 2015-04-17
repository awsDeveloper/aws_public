using UnityEngine;
using System.Collections;

public class WX04_050 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	bool costFlag = false;

    bool afterEffect = false;
	
	// Use this for initialization
	void Start ()
	{
		GameObject Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		GameObject Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//ignition
		if (BodyScript.Ignition)
		{
			BodyScript.Ignition = false;
			
			if (ManagerScript.getIDConditionInt (ID, player) == 1)
			{
				costFlag = true;
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID + 50 * player);
				BodyScript.effectMotion.Add (8);
			}
		}

		//after cost
		if (BodyScript.effectTargetID.Count == 0 && costFlag)
		{
			costFlag = false;
            afterEffect = true;

			int target = player;
			int f = 0;
			int num = ManagerScript.getNumForCard (f, target);

			bool flag=false;
			
			for (int i=num-1; i>=0; i--)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0){
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (x+50*target);
					BodyScript.effectMotion.Add (54);

					if(checkClass(x,target))
					{
						flag=true;
						break;
					}
				}
			}

			int c=BodyScript.effectTargetID.Count;

			if(flag)
			{
				BodyScript.effectTargetID.Add (BodyScript.effectTargetID[c-1] );
				BodyScript.effectMotion.Add (16);
			}
		}

        //after effect
        if (afterEffect && BodyScript.effectTargetID.Count == 0)
        {
            afterEffect = false;

            int index = 0;
            while (true)
            {
                int x = ManagerScript.getShowZoneID(index);

                if (x < 0)
                    break;

                BodyScript.effectTargetID.Add(x);
                BodyScript.effectMotion.Add((int)Motions.GoDeckBottom);
                index++;
            }

            BodyScript.effectTargetID.Add(50*player);
            BodyScript.effectMotion.Add((int)Motions.Shuffle);
        }
    }

	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 4 && c [1] == 2);
	}
}
