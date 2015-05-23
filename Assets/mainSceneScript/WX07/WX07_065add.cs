using UnityEngine;
using System.Collections;

public class WX07_065add : MonoCard {
    bool equipFlag = false;

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {
        //check equip
        if ( checkEquip())
        {
            if (!equipFlag && !sc.Assassin)
            {
                equipFlag = true;
                sc.Assassin = true;
            }
        }
        else
        {
            dequip();
        }

        //自爆
        if (ms.getPhaseInt() == 7)
        {
            dequip();
            Destroy(this);
        }
	
	}

    bool checkEquip()
    {
        int rank = ms.getRank(ID, player);
        rank = 1 - (rank - 1);//正面のランクを得る

        int x = ms.getFieldRankID(3, rank, 1 - player);

        return x >= 0 && ms.checkFreeze(x,1-player);
    }

    void dequip()
    {
        if (equipFlag)
        {
            equipFlag = false;
            sc.Assassin = false;
        }
    }
}

