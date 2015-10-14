using UnityEngine;
using System.Collections;

public class PR_175 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<DialogEffTemplete>().set(EffectTemplete.triggerType.Cip, DialogNumType.Level, cip, cip_1);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip(int count) {
        for (int i = 0; i < count+1; i++)
            sc.setEffect(0, player, Motions.TopGoTrash);
    }

    void cip_1(int count)
    {
        int index = 0;
        while (true)
        {
            int x = ms.getTopGoTrashListID(index,ID,player);
            if (x == -1)
                break;

            if (!ms.checkClass(x, cardClassInfo.精武_ウェポン))
                return;
            index++;
        }

        var cf=new checkFuncs(ms,count+1,false);
        cf.setMax(count+1);
        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, cf.check);
    }
}

