using UnityEngine;
using System.Collections;

public class WX09_001 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.NotSystemGrow = true;

        var gr = sc.AddEffectTemplete(EffectTemplete.triggerType.GrowEffect);
        gr.addEffect(grow, true, EffectTemplete.checkType.system);
        gr.addEffect(grow_1, false, EffectTemplete.checkType.system);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip);

        var ig = sc.AddEffectTemplete(EffectTemplete.triggerType.useAttackArts, igni, true);
        ig.addEffect(igni_1);
        ig.addEffect(igni_2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool growChe(int x, int target)
    {
        return !sc.isMe(x, target)
            && (ms.checkLrigTypeInfo(x, target, LrigTypeInfo.ウムル) || ms.checkLrigTypeInfo(x, target, LrigTypeInfo.タウィル));
    }

    void grow()
    {
        sc.funcTargetIn(player, Fields.LRIGDECK, growChe);
        sc.setSystemCardFromCard(-2, Motions.GoLrigBottom, 1, sc.Targetable, false, null);
    }

    void grow_1()
    {
        sc.setSystemCardFromCard(ID + 50 * player, Motions.Grow, 1, null, false, null);
    }

    bool cipChe(int x, int target)
    {
        return (ms.checkColor(x, target, cardColorInfo.白) || ms.checkColor(x, target, cardColorInfo.黒))
            && ms.checkType(x, target, cardTypeInfo.シグニ);
    }

    void cip()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.TRASH, cipChe);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    void igni()
    {
        ms.targetableExceedIn(player, sc);
        if (sc.Targetable.Count < 5)
        {
            sc.Targetable.Clear();
            return;
        }
        for (int i = 0; i < 5; i++)
            sc.setEffect(-2, 0, Motions.Exceed);
    }

    void igni_1()
    {
        sc.setFieldAllEffect(player, Fields.SIGNIZONE, Motions.GoTrash);
    }

    bool igniChe_2(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精像_天使) || ms.checkClass(x, target, cardClassInfo.精械_古代兵器);
    }

    void igni_2()
    {
        sc.setFuncEffect(-2, Motions.Summon, player, Fields.TRASH, igniChe_2);
        sc.setEffect(-2, 0, Motions.Summon);
        sc.setEffect(-2, 0, Motions.Summon);
    }
}

