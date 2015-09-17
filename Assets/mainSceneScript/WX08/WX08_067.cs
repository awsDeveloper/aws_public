using UnityEngine;
using System.Collections;

public class WX08_067 : MonoCard {
    int count = -1;
	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant).addEffect(chant_2);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chant()//まとめたほうがいいかも
    {
        sc.setEffect(ID, player, Motions.SetDialog);
        sc.addParameta(parametaKey.settingDialogNum, (int)DialogNumType.Level);
        count = -1;
    }

    void chant_2()
    {
        if(count==-1)
            count = sc.getMessageInt();
        if (count < 0)
            return;

        sc.setEffect(0, 1 - player, Motions.OpenHand);
        sc.setFuncEffect(-1, Motions.HandDeath, 1 - player, Fields.HAND, check);
        sc.setEffect(0, 1 - player, Motions.CloseHand);
    }

    bool check(int x, int target)
    {
        return ms.getCardLevel(x, target) == count+1;//カウントは0から
    }
}

