using UnityEngine;
using System.Collections;

public class WX04_020 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;

    bool afterChant = false;

	// Use this for initialization
	void Start ()
	{
		Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		
		Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//chant
        if (BodyScript.isChanted())
        {
            for (int i = 0; i < 4; i++)
                BodyScript.setEffect(0, player, Motions.TopGoShowZone);

            afterChant = true;
        }

        if (afterChant && BodyScript.effectTargetID.Count == 0)
        {
            afterChant = false;

            int index = 0;
            while (true)
            {
                int x = ManagerScript.getShowZoneID(index);

                if (x < 0)
                    break;

                if (ManagerScript.checkClass(x % 50, x / 50, cardClassInfo.精像_美巧))
                    BodyScript.setEffect(x % 50, x / 50, Motions.GoHand);
                else
                    BodyScript.Targetable.Add(x);

                index++;
            }

            //一旦ターゲッタブルに入れる
            for (int i = 0; i < BodyScript.Targetable.Count; i++)
            {
                int x = BodyScript.Targetable[i];
                BodyScript.setEffect(x % 50, x / 50, Motions.GoTrash);
            }

            BodyScript.Targetable.Clear();
        }
	}

}
