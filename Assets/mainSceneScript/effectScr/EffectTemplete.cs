using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class EffectTemplete : MonoCard {
    Func<bool> trigger;
    bool useYesNo = false;

    List<effectPack> effectList = new List<effectPack>();

    bool isGotYes = false;//DialogToggleでyesを受け取った
    bool isUpper = false;

    int effectIndex = -1;

    Action FinalAciton = null;


    class effectPack
    {
        public List<checkAndEffect> funcList = new List<checkAndEffect>();
        public int max = 1;
        public bool maxSelect = false;
        public bool isCost = false;

        public effectPack(List<checkAndEffect> _funcList, int _max, bool _maxSelect, bool _isCost)
        {
            funcList = _funcList;
            max = _max;
            maxSelect = _maxSelect;
            isCost = _isCost;
        }
    }


    public class checkAndEffect
    {
        public Func<bool> check;
        public Func<bool> effect;

        public Action action;

        public string str = "";

        public int boxIndex = -1;//checkBoxのindexとの紐つけ

        public checkType myCheckType = checkType.Default;

        public checkAndEffect(Func<bool> c, Func<bool> e,string checkStr)
        {
            check = c;
            effect = e;
            action = excuteEffect;
            str = checkStr;
        }

        public checkAndEffect(Func<bool> c, Action a, string checkStr)
        {
            check = c;
            action = a;
            str = checkStr;
        }

        void excuteEffect()
        {
            effect();
        }
    }

    bool Cip()
    {
        return sc.isCiped();
    }

    bool Chant()
    {
        return sc.isChanted();
    }

    bool Burst()
    {
        return sc.isBursted();
    }

    bool Ignition()
    {
        if (!sc.Ignition)
            return false;

        sc.Ignition = false;
        return true;
    }

    bool useResona()
    {
        if (!sc.useResona)
            return false;

        sc.useResona = false;
        return true;
    }

    bool isHeavened()
    {
        return sc.isHeaven();
    }

    public enum triggerType
    {
        Cip,
        Ignition,
        Chant,
        Burst,
        useResona,
        isHeavened,
    }

    public enum checkType
    {
        True,
        Default,
        custom
    }

    public void addEffect(List<checkAndEffect> _funcList, int _max, bool _maxSelect, bool _isCost)
    {
        effectList.Add(new effectPack(_funcList, _max, _maxSelect, _isCost));
    }

    public void addEffect(Action _action, bool _isCost, Func<bool> _check)
    {
        List<checkAndEffect> list = new List<checkAndEffect>();
        list.Add(new checkAndEffect(_check, _action, ""));
        addEffect(list, 1, true, _isCost);
    }

    public void addEffect(Action _action, bool _isCost, checkType _type = checkType.Default)
    {
        switch (_type)
        {
            case checkType.True:
                addEffect(_action, _isCost, trueReturn);
                break;

            case checkType.Default:
                addEffect(_action, _isCost, null);
                break;
        }
    }

    bool trueReturn()
    {
        return true;
    }

    public void setTrigger(Func<bool> func, bool _useYesNo = false)
    {
        trigger = func;
        useYesNo = _useYesNo;
    }

    public void setTrigger(triggerType T, bool _useYesNo = false)
    {
        MethodInfo mInfo = typeof(EffectTemplete).GetMethod(T.ToString(), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        Func<bool> tri = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), this, mInfo);
        setTrigger(tri, _useYesNo);
        //        Debug.Log(trigger);
        /*
                switch (T)
                {
                    case triggerType.Chant:
                        trigger = Chant;
                        break;
                    case triggerType.Cip:
                        trigger = Cip;
                        break;
                    case triggerType.Ignition:
                        trigger = Ignition;
                        break;
                    case triggerType.Burst:
                        trigger = Burst;
                        break;
                    case triggerType.useResona:
                        trigger = useResona;
                        break;
                }*/
    }



     // Use this for initialization
    void Start()
    {
    }

	// Update is called once per frame
	void Update () {
        if (trigger == null)
            return;
        triggerCheck();

        receive();

        effect();
    }

    void triggerCheck()
    {
        if (!trigger())
            return;

        effectIndex = 0;

        if (useYesNo)
        {
            sc.setDialogNum(DialogNumType.YesNo);
            isUpper = true;
        }
        else
            afterGettingYes();
    }

    bool funcCheck(int index)
    {
        switch (effectList[effectIndex].funcList[index].myCheckType)
        {
            case checkType.True:
                return true;

            case checkType.custom:
                return effectList[effectIndex].funcList[index].check();

            case checkType.Default:
                bool flagBuf = sc.effectFlag;
                sc.effectFlag = false;

                effectList[effectIndex].funcList[index].action();
                bool flag = sc.effectFlag;

                sc.effectFlag = flagBuf;
                sc.effectTargetID.Clear();
                sc.effectMotion.Clear();
                sc.Targetable.Clear();

                return flag;
        }

        return true;
    }

    void afterGettingYes()
    {
        //checkBoxの初期化
        sc.checkStr.Clear();
        sc.checkBox.Clear();

        int count = 0;
        int lastNum = -1;

        for (int i = 0; i < effectList[effectIndex].funcList.Count; i++)
        {
            if (funcCheck(i))
            {
                lastNum = i;
                count++;

                //checkBoxの生成
                effectList[effectIndex].funcList[i].boxIndex = sc.checkBox.Count;
                sc.checkStr.Add(effectList[effectIndex].funcList[i].str);
                sc.checkBox.Add(false);
            }
            else
                effectList[effectIndex].funcList[i].boxIndex = -1;
        }

        if (count >= 2)
        {
            isUpper = true;
            sc.DialogFlag = true;
            sc.DialogNum = 2;
            sc.DialogCountMax = effectList[effectIndex].max;
            sc.DialogMaxSelect = effectList[effectIndex].maxSelect;
        }
        else if (count == 1 && lastNum >= 0)
        {
            sc.checkBox[lastNum] = true;
            isGotYes = true;
            effect();
        }
        else if(!effectList[effectIndex].isCost)
            EndEffect();
    }

    void receive()
    {
        //receive
        if (!isUpper || sc.messages.Count == 0)
            return;

        isUpper = false;

        if (sc.messages[0].Contains("Yes"))
        {
            if (sc.DialogNum == (int)DialogNumType.YesNo)
                afterGettingYes();
            else if (sc.DialogNum == (int)DialogNumType.toggle)
                isGotYes = true;
        }

        sc.messages.Clear();
    }

    void effect()
    {
        if (!isGotYes || !ms.isTargetIDCountZero(sc.ID,sc.player))
            return;

        sc.Targetable.Clear();

        for (int i = 0; i < effectList[effectIndex].funcList.Count; i++)
        {
            int index = effectList[effectIndex].funcList[i].boxIndex;
            if (index >= 0 && sc.checkBox[index])
            {
                sc.checkBox[index] = false;
                effectList[effectIndex].funcList[i].action();
                return;
            }
        }

        EndEffect();
    }

    void EndEffect()
    {
        effectIndex++;
        isGotYes = false;

        if (effectIndex < effectList.Count)
            afterGettingYes();
        else
            effectIndex = -1;
    }


}
