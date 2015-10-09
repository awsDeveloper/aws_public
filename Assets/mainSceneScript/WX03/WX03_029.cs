using UnityEngine;
using System.Collections;

public class WX03_029 : MonoCard{//改修済	
	public int powerUpSize=2000;
	public int powerUpLevel=1;
		
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<FuncPowerUp>().setForMe(powerUpSize, upCondition);
	}
	
	// Update is called once per frame
	void Update () {
	}

    bool upCondition()
    {
        for (int i = 0; i < 3; i++)
        {
            int x = ms.getFieldRankID(3, i,player);
            if (ms.checkClass(x, player, cardClassInfo.精械_電機) && ms.getCardLevel(x, player) == powerUpLevel)
                return true;
        }

        return false;
    }
}
