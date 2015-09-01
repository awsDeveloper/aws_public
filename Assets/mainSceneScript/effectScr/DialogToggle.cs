using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class DialogToggle : MonoBehaviour {
    CardScript BodyScript;
    DeckScript ManagerScript;
    bool started = false;

    List<checkAndEffect> funcList = new List<checkAndEffect>();

    Func<bool> trigger;
    int max = 1;
    bool maxSelect = false;

    bool isGotYes = false;

    Action FinalAciton = null;

    class checkAndEffect
    {
        public Func<bool> check;
        public Func<bool> effect;

        public Action action;

        public string str = "";

        public int boxIndex = -1;//checkBoxのindexとの紐つけ

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

    public enum triggerType
    {
        Cip,
        Ignition,
        Chant,
        Burst,
        useResona,
    }

    public enum FinalActionType
    {
        summonThisResona,
    }

    // Use this for initialization
    void Start()
    {
        if (started)
            return;
        
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();

        ManagerScript = BodyScript.Manager.GetComponent<DeckScript>();
        started = true;
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

        //checkBoxの初期化
        BodyScript.checkStr.Clear();
        BodyScript.checkBox.Clear();

        int count = 0;
        int lastNum = -1;

        for (int i = 0; i < funcList.Count; i++)
        {
            if (funcList[i].check())
            {
                lastNum = i;
                count++;

                //checkBoxの生成
                funcList[i].boxIndex = BodyScript.checkBox.Count;
                BodyScript.checkStr.Add(funcList[i].str);
                BodyScript.checkBox.Add(false);
            }
            else
                funcList[i].boxIndex = -1;
        }

        if (count >= 2)
        {
            BodyScript.DialogFlag = true;
            BodyScript.DialogNum = 2;
            BodyScript.DialogCountMax = max;
            BodyScript.DialogMaxSelect = maxSelect;
        }
        else if(count==1 && lastNum>=0)
            funcList[lastNum].action();

    }

    void receive()
    {
        //receive
        if (BodyScript.messages.Count == 0 || BodyScript.DialogNum != (int)DialogNumType.toggle)
            return;

        if (BodyScript.messages[0].Contains("Yes"))
        {
            isGotYes = true;
        }

        BodyScript.messages.Clear();
    }

    void effect()
    {
        if (!isGotYes || !ManagerScript.isTargetIDCountZero(BodyScript.ID,BodyScript.player))
            return;

/*        for (int i = 0; i < BodyScript.checkBox.Count; i++)
        {
            if (BodyScript.checkBox[i])
            {
                BodyScript.checkBox[i] = false;
                funcList[i].action();
                return;
            }
        }*/

        for (int i = 0; i <funcList.Count; i++)
        {
            int index=funcList[i].boxIndex;
            if (index>=0 && BodyScript.checkBox[index])
            {
                BodyScript.checkBox[index] = false;
                funcList[i].action();
                return;
            }
        }


        if (FinalAciton != null)
            FinalAciton();

        isGotYes = false;
    }

    public void set(string str, Func<bool> check, Func<bool> effect)
    {
        setting(new checkAndEffect(check,effect,str)); 
    }
    public void set(string str, Func<bool> effect)
    {
        set(str, trueReturn, effect);
    }
    public void setAction(string str, Action action,Func<bool> check)
    {
        setting(new checkAndEffect(check, action,str));
    }
    public void setAction(string str, Action act)
    {
        setAction(str, act, trueReturn);
    }

    void setting(checkAndEffect eff)
    {
        Start();
        funcList.Add(eff);
    }



    bool trueReturn()
    {
        return true;
    }

    public void setTrigger(Func<bool> func, int dialogMax=1, bool isMaxSelect=false)
    {
        trigger = func;
        max = dialogMax;
        maxSelect = isMaxSelect;
    }

    public void setTrigger(triggerType T, int dialogMax = 1, bool isMaxSelect = false)
    {
        max = dialogMax;
        maxSelect = isMaxSelect;


        MethodInfo mInfo = typeof(DialogToggle).GetMethod(T.ToString(), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        trigger = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), this,mInfo);
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

    bool Cip()
    {
        return BodyScript.isCiped();
    }

    bool Chant()
    {
        return BodyScript.isChanted();
    }

    bool Burst()
    {
        return BodyScript.isBursted();
    }

    bool Ignition()
    {
        if (!BodyScript.Ignition)
            return false;

        BodyScript.Ignition = false;
        return true;
    }

    bool useResona()
    {
        if (!BodyScript.useResona)
            return false;

        BodyScript.useResona = false;
        return true;
    }

    void summonThisResona()
    {
        ManagerScript.SetSystemCardFromCard(BodyScript.ID + BodyScript.player * 50, Motions.Summon, BodyScript.ID, BodyScript.player);
    }
    
    public void setFienalAction(FinalActionType T)
    {
        MethodInfo mInfo = typeof(DialogToggle).GetMethod(T.ToString(), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        FinalAciton = (Action)Delegate.CreateDelegate(typeof(Action), this, mInfo);
    }
}
