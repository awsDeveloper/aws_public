using UnityEngine;
using System.Collections;

public class PR_201 : MonoCard
{
    int type=-1;

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<DialogEffTemplete>().set(EffectTemplete.triggerType.Chant, DialogNumType.color, eff, eff_1, 1 - sc.player, eff_before);

    }


    // Update is called once per frame
    void Update()
    {

    }

    void eff_before()
    {
        sc.effectSelecter = player;
        sc.TargetIDEnable = true;
        sc.TargetID.Clear();
        sc.setFuncEffect(-1, Motions.AntiCheck, player, Fields.HAND, null);
    }

    void eff(int count)
    {
        sc.setEffect(0, 1 - player, Motions.SetDialog);
        sc.addParameta(parametaKey.settingDialogNum, (int)DialogNumType.Type);
        type = -1;
    }

    void eff_1(int count)
    {
        if (type == -1)
            type = sc.getMessageInt();
        if (type < 0)
            return;

        sc.TargetIDEnable = false;
        int tID = sc.TargetID[0];

        if (ms.checkType(tID, (cardTypeInfo)type) && ms.checkColor(tID % 50, tID / 50, (cardColorInfo)count))
            sc.setEffect(0, player, Motions.AllHandDeath);
        else if (ms.checkType(tID, (cardTypeInfo)type) || ms.checkColor(tID % 50, tID / 50, (cardColorInfo)count))
            sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, null);
        else
            sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.GoTrash, null);
    }
}