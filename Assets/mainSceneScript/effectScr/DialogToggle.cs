using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DialogToggle : MonoBehaviour {
    CardScript BodyScript;
    bool started = false;

    List<checkAndEffect> funcList = new List<checkAndEffect>();

    Func<bool> trigger;
    int max = 1;

    class checkAndEffect
    {
        public Func<bool> check;
        public Func<bool> effect;

        public Action action;

        public checkAndEffect(Func<bool> c, Func<bool> e)
        {
            check = c;
            effect = e;
            action = excuteEffect;
        }

        public checkAndEffect(Func<bool> c, Action a)
        {
            check = c;
            action = a;
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
    }

    // Use this for initialization
    void Start()
    {
        if (started)
            return;
        
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();

        started = true;
    }

	// Update is called once per frame
	void Update () {
        if (trigger == null)
            return;

        triggerCheck();

        receive();
	}

    void triggerCheck()
    {
        if (!trigger())
            return;

        int count = 0;
        int lastNum = -1;

        for (int i = 0; i < funcList.Count; i++)
        {
            if (funcList[i].check())
            {
                lastNum = i;
                count++;
            }
        }

        if (count >= 2)
        {
            BodyScript.DialogFlag = true;
            BodyScript.DialogNum = 2;
            BodyScript.DialogCountMax = max;
        }
        else if(count==1 && lastNum>=0)
            funcList[lastNum].action();

    }

    void receive()
    {
        //receive
        if (BodyScript.messages.Count == 0)
            return;

        if (BodyScript.messages[0].Contains("Yes"))
        {
            for (int i = 0; i < BodyScript.checkBox.Count; i++)
                if (BodyScript.checkBox[i])
                    funcList[i].action();
        }

        BodyScript.messages.Clear();
    }

    public void set(string str, Func<bool> check, Func<bool> effect)
    {
        setting(str,new checkAndEffect(check,effect)); 
    }
    public void set(string str, Func<bool> effect)
    {
        set(str, trueReturn, effect);
    }
    public void setAction(string str, Action action,Func<bool> check)
    {
        setting(str, new checkAndEffect(check, action));
    }
    public void setAction(string str, Action act)
    {
        setAction(str, act, trueReturn);
    }

    void setting(string str ,checkAndEffect eff)
    {
        Start();

        BodyScript.checkStr.Add(str);
        BodyScript.checkBox.Add(false);
        funcList.Add(eff);
    }



    bool trueReturn()
    {
        return true;
    }

    public void setTrigger(Func<bool> func, int dialogMax=1)
    {
        trigger = func;
        max = dialogMax;
    }

    public void setTrigger(triggerType T)
    {
        switch (T)
        {
            case triggerType.Chant:
                trigger = chant;
                break;
            case triggerType.Cip:
                trigger = cip;
                break;
            case triggerType.Ignition:
                trigger = Ignition;
                break;
            case triggerType.Burst:
                trigger = burst;
                break;
        }
    }

    bool cip()
    {
        return BodyScript.isCiped();
    }

    bool chant()
    {
        return BodyScript.isChanted();
    }

    bool burst()
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
}
