using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DialogEffTemplete : MonoCard {
    int count = -1;
    DialogNumType myType;
    Action<int> countEffect;
    Action<int> afterCountEffect;
    EffectTemplete.triggerType myTriggerType= EffectTemplete.triggerType.Chant;
    int selecter = -1;
    Action beforeAction = null;


    // Use this for initialization
    void Start()
    {
        var temp= sc.AddEffectTemplete(myTriggerType);

        if (beforeAction != null)
            temp.addEffect(beforeAction, EffectTemplete.option.ifThen);

        temp.addEffect(eff);
        temp.addEffect(eff_1);
        temp.addEffect(eff_2);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void eff()
    {
        sc.setEffect(0, selecter, Motions.SetDialog);
        sc.addParameta(parametaKey.settingDialogNum, (int)myType);
        count = -1;
    }

    void eff_1()
    {
        if (count == -1)
            count = sc.getMessageInt();
        if (count < 0)
            return;

        countEffect(count);
    }

    void eff_2()
    {
        afterCountEffect(count);
    }



    public void set(EffectTemplete.triggerType _tri ,DialogNumType _type, Action<int> _cEff, Action<int> _after, int _selecter=-1, Action _bAct=null)
    {
        myTriggerType = _tri;
        myType = _type;
        countEffect = _cEff;
        afterCountEffect=_after;

        if (_selecter == -1)
            selecter = sc.player;
        else
            selecter = _selecter;

        beforeAction = _bAct;
    }
}

