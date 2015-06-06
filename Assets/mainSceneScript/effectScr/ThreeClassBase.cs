using UnityEngine;
using System.Collections;

public class ThreeClassBase : MonoCard {
    bool upFlag = false;
    public int upBase=-1;
    public cardClassInfo myClass = cardClassInfo.精元;//デフォルトは精元だけど意味はない

	// Use this for initialization
	void Start () {
        if (upBase < 0)
        {
            switch (sc.Level)
            {
                case 1:
                    upBase = 6000;
                    break;
                case 2:
                    upBase = 9000;
                    break;
                case 3:
                    upBase = 14000;
                    break;
            }
        }
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (sc.isOnBattleField() && ms.getClassNum(player, Fields.SIGNIZONE, myClass) >= 3)
        {
            ms.changeBasePower(ID, player, upBase);
            upFlag = true;
        }
        else if (upFlag)
        {
            ms.changeBasePower(ID, player, sc.OriginalPower);
            upFlag = false;
        }
    }
}

