using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class EffectTemplete : MonoCard {
    Func<bool> trigger;
    bool useYesNo = false;

    bool oneceTurn = false;
    bool thisTurnUsed = false;

    List<effectPack> effectList = new List<effectPack>();

    bool isGotYes = false;//DialogToggleでyesを受け取った
    bool isUpper = false;

    int effectIndex = -1;

    bool manualMode = false;

    int triggerCount = 0;
  
    class effectPack
    {
        public List<checkAndEffect> funcList = new List<checkAndEffect>();
        public int max = 1;
        public bool maxSelect = false;
        public bool isCost { get { return hasOption(myOption, option.cost); } }
        public option myOption = option.non;

        public effectPack(List<checkAndEffect> _funcList, int _max, bool _maxSelect, bool _isCost)
        {
            funcList = _funcList;
            max = _max;
            maxSelect = _maxSelect;
            if (_isCost)
                myOption = option.cost;
        }

        public effectPack(List<checkAndEffect> _funcList, int _max, bool _maxSelect,option _option)
        {
            funcList = _funcList;
            max = _max;
            maxSelect = _maxSelect;
            myOption = _option;
        }
     }


    public class checkAndEffect
    {
        public Func<bool> check;

        public Func<bool> effect;//actionを知らなかった
        public Action action;

        public string str = "";
        public int boxIndex = -1;//checkBoxのindexとの紐つけ
        public checkType myCheckType = checkType.custom;
        public Action defaultCheckFunc = null;

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

        //check type defaultのときeffectの代わりに実行される関数を登録
        public void setDefaultCheckFunc(Action _act)
        {
            if (myCheckType != checkType.Default)
                return;

            defaultCheckFunc = _act;
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

    bool useAttackArts()
    {
        if (!sc.UseAttackArts)
            return false;

        sc.UseAttackArts = false;
        return true;
    }

    bool attack()
    {
        return sc.isAttacking();
    }

    bool mySigniHeavened()
    {
        return !sc.mySigniNotHeaven();
    }

    bool crossCip()
    {
        return sc.isCrossing() && Cip();
    }

    bool removed()
    {
        return sc.isRemovedThisSigni();
    }

    bool Banished(){
        return ms.getOneFrameID(OneFrameIDType.BanishedID) == ID + 50 * player;
    }

    public enum triggerType
    {
        Cip,
        Ignition,
        Chant,
        Burst,
        useResona,
        isHeavened,
        useAttackArts,
        attack,
        mySigniHeavened,
        crossCip,
        removed,
        Banished,
    }

    public enum checkType
    {
        True,
        Default,
        custom,
        system,
    }

    public enum option
    {
        non                 = 0,
        cost                = 1,
        ifThen              = 2,
    }

    public static bool hasOption(option target, option checker)
    {
        if (checker == option.non)
            return target == option.non;

        return (target & checker) == checker;
    }

    option isCostToOption(bool _isCost)
    {
        if (_isCost)
            return option.cost;

        return option.non;
    }

    public void setManualMode()
    {
        manualMode = true;
    }

    public void addEffect(List<checkAndEffect> _funcList, int _max, bool _maxSelect, option _option)
    {
        effectList.Add(new effectPack(_funcList, _max, _maxSelect, _option));
    }
    public void addEffect(Action _action, option _option, Func<bool> _check)
    {
        List<checkAndEffect> list = new List<checkAndEffect>();
        list.Add(new checkAndEffect(_check, _action, ""));
        addEffect(list, 1, true, _option);
    }

    public void addEffect(Action _action, option _option, checkType _type = checkType.Default)
    {
        addEffect(_action, _option, CheckTypeToAction(_type));
        effectList[effectList.Count - 1].funcList[0].myCheckType = _type;
    }

    public void addEffect(List<checkAndEffect> _funcList, int _max, bool _maxSelect, bool _isCost)
    {
        addEffect(_funcList, _max, _maxSelect, isCostToOption(_isCost));
    }
    public void addEffect(Action _action, bool _isCost, Func<bool> _check)
    {
        addEffect(_action, isCostToOption(_isCost), _check);
    }
    public void addEffect(Action _action, bool _isCost=false, checkType _type = checkType.Default)
    {
        addEffect(_action, isCostToOption(_isCost), _type);
    }

    public void addEffectDefault(Action _action, Action defaultCheckFunc,bool _isCost = false)
    {
        addEffect(_action, isCostToOption(_isCost), checkType.Default);
        effectList[effectList.Count - 1].funcList[0].setDefaultCheckFunc(defaultCheckFunc);
    }

    public void addFuncList(int index,Action _action, checkType _type = checkType.Default) {
        var _var = new checkAndEffect(CheckTypeToAction(_type), _action, "");
        _var.myCheckType = _type;
        effectList[index].funcList.Add(_var);
    }

    public void changeCheckStr(int index, string[] str)
    {
        for (int i = 0; i < effectList[index].funcList.Count; i++)
            effectList[index].funcList[i].str = str[i];
    }

    Func<bool> CheckTypeToAction(checkType _type)
    {
        
        switch (_type)
        {
            case checkType.True:
                return trueReturn;

            case checkType.Default:
                return null;
        }

        return null;//別の値返した方がいいかも
    }

    bool trueReturn()
    {
        return true;
    }

    public void setTrigger(Func<bool> func, bool _useYesNo = false, bool _oneceTurn=false)
    {
        trigger = func;
        useYesNo = _useYesNo;
        oneceTurn = _oneceTurn;
    }

    public void setTrigger(triggerType T, bool _useYesNo = false, bool _oneceTurn = false)
    {
        MethodInfo mInfo = typeof(EffectTemplete).GetMethod(T.ToString(), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        Func<bool> tri = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), this, mInfo);
        setTrigger(tri, _useYesNo,_oneceTurn);
        if (T == triggerType.useAttackArts)
            sc.attackArts = true;

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

        if (ms.getStartedPhase() == (int)Phases.UpPhase)
            thisTurnUsed = false;
    }

    public bool isTriggered()
    {
        if (trigger == null)
            return false;

        return trigger();
    }

    bool onceTurnCheck()
    {
        return !oneceTurn || !thisTurnUsed;
    }

    void triggerCheck()
    {
        if (manualMode || !onceTurnCheck())//manual modeではトリガーチェックで実行されない
            return;

        if (trigger())
            triggerCount++;

        if (triggerCount == 0 || !ms.isTargetIDCountZero(sc.ID, sc.player))
            return;

        triggerCount--;

        triggerInit();

        if (!checkCost() || !checkEffectFlagUp())
            return;


        if (useYesNo)
        {
            sc.setDialogNum(DialogNumType.YesNo);
            isUpper = true;
        }
        else
            afterGettingYes();
    }

    void triggerInit()
    {
        effectIndex = 0;
        thisTurnUsed = true;
    }

    bool checkCost()
    {
        for (int i = 0; i < effectList.Count; i++)
        {
            if (!hasOption(option.cost | option.ifThen, effectList[i].myOption))
                continue;


            bool flag = false;
            for (int j = 0; j < effectList[i].funcList.Count; j++)
            {
                if (funcCheck(i, j))
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
                return false;
        }

        return true;
    }

    bool checkEffectFlagUp()//どれかしらではeffectFlagが立つことを確認する
    {
        for (int i = 0; i < effectList.Count; i++)
            for (int j = 0; j < effectList[i].funcList.Count; j++)
                if (funcCheck(i, j))
                    return true;

        return false;
    }

    bool funcCheck(int eIndex,int fIndex)
    {
        switch (effectList[eIndex].funcList[fIndex].myCheckType)
        {
            case checkType.True:
                return true;

            case checkType.custom:
                return effectList[eIndex].funcList[fIndex].check();

            case checkType.Default:
                return DefauldCheck(sc, eIndex, fIndex);

            case checkType.system:
                return DefauldCheck(ms.getSystemScr(ID,player), eIndex, fIndex);
 
        }

        return true;
    }

    bool DefauldCheck(CardScript _sc, int eIndex, int fIndex)
    {
        bool flagBuf = _sc.effectFlag;
        List<int> targetBuf = new List<int>(_sc.effectTargetID);
        List<int> motionBuf = new List<int>(_sc.effectMotion);
        List<int> targetableBuf = new List<int>(_sc.Targetable);

        _sc.effectFlag = false;
        _sc.effectTargetID.Clear();
        _sc.effectMotion.Clear();
        _sc.Targetable.Clear();

        //check時用の関数が登録してあるか確認する
        var item = effectList[eIndex].funcList[fIndex];
        if (item.defaultCheckFunc != null)
            item.defaultCheckFunc();
        else
            item.action();


        bool flag = _sc.effectFlag;

        for (int i = 0; i < _sc.effectMotion.Count; i++)
        {
            Motions m = (Motions)_sc.effectMotion[i];
            int x = _sc.effectTargetID[i] % 50;
            int target = _sc.effectTargetID[i] / 50;
            switch (m)
            {
                case Motions.PayCost:
                    if (!ms.checkCost(x, target))
                        flag = false;
                    break;

                case Motions.Down:
                    if (!ms.getCardScr(x, target).isUp())
                        flag = false;
                    break;

            }
        }

        _sc.effectFlag = flagBuf;
        _sc.effectTargetID = new List<int>(targetBuf);
        _sc.effectMotion = new List<int>(motionBuf);
        _sc.Targetable = new List<int>(targetableBuf);

        return flag;
    }

    public void doAfterGettingYes()//manager用
    {
        triggerInit();
        afterGettingYes();
    }

    void afterGettingYes()
    {
        //checkBoxの初期化
        sc.checkStr.Clear();
        sc.checkBox.Clear();

        int count = 0;
        //たぶんlastNumいらない
//        int lastNum = -1;

        for (int i = 0; i < effectList[effectIndex].funcList.Count; i++)
        {
            if (funcCheck(effectIndex,i))
            {
//                lastNum = i;
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
        else if (count == 1)// && lastNum >= 0)
        {
            sc.checkBox[0] = true;
            //            sc.checkBox[lastNum] = true;
            isGotYes = true;
            effect();
        }
        else if (!effectList[effectIndex].isCost && !hasOption(effectList[effectIndex].myOption, option.ifThen))
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
        if (!isGotYes || !ms.isTargetIDCountZero(sc.ID,sc.player) || !ms.isSystemTargetIDCountZero())
            return;

        sc.Targetable.Clear();

        for (int i = 0; i < effectList[effectIndex].funcList.Count; i++)
        {
            int index = effectList[effectIndex].funcList[i].boxIndex;
            if (index >= 0 && sc.checkBox[index])
            {
                effectList[effectIndex].funcList[i].action();
                sc.checkBox[index] = false;
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
