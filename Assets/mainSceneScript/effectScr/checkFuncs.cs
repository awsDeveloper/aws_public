using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class checkFuncs {
    enum data
    {
        Class,
        Color,
        Type,
        Max,
        Min,
        ContainsName,
        PerfectName,
    }

    DeckScript ms;
    Dictionary<data, string> myData = new Dictionary<data, string>();


    public checkFuncs(DeckScript _ms, cardColorInfo color)
    {
        ms = _ms;
        setColor(color);
    }
    public checkFuncs(DeckScript _ms, cardClassInfo _class)
    {
        ms = _ms;
        setData(data.Class, (int)_class);
    }
    public checkFuncs(DeckScript _ms, cardTypeInfo info)
    {
        ms = _ms;
        setData(data.Type, (int)info);
    }
    public checkFuncs(DeckScript _ms, string _name, bool perfectFlag = true)
    {
        ms = _ms;
        data _data= data.ContainsName;
        if(perfectFlag)
            _data= data.PerfectName;
        setData(_data, _name);
    }
    public checkFuncs(DeckScript _ms, int _val, bool lower = true)
    {
        ms = _ms;
        data _data = data.Min;
        if (lower)
            _data = data.Max;
        setData(_data, _val);
    }

    public bool check(int x, int target)
    {
        return checkClass(x, target)
            && checkColor(x, target)
            && checkType(x, target)
            && checkLevelMax(x, target)
            && checkLevelMin(x, target)
            && checkContainsName(x, target)
            && checkPerfectName(x, target)
            ;
    }

    int getMyDataInt(data key)
    {
        return int.Parse(myData[key]);
    }
    string getMyData(data key)
    {
        return myData[key];
    }

    bool checkClass(int x, int target)
    {
        data _data = data.Class;
        return !myData.ContainsKey(_data) || ms.checkClass(x, target, (cardClassInfo)getMyDataInt(_data));
    }

    bool checkColor(int x, int target)
    {
        data _data = data.Color;
        return !myData.ContainsKey(_data) || ms.checkColor(x, target, (cardColorInfo)getMyDataInt(_data));
    }

    bool checkType(int x, int target)
    {
        data _data = data.Type;
        return !myData.ContainsKey(_data) || ms.checkType(x, target, (cardTypeInfo)getMyDataInt(_data));
    }

    bool checkLevelMin(int x, int target){
        data _data = data.Min;
        return !myData.ContainsKey(_data) || ms.getCardLevel(x, target) >= getMyDataInt(_data);
    }

    bool checkLevelMax(int x, int target)
    {
        data _data = data.Max;
        return !myData.ContainsKey(_data) || ms.checkLevelLower(x, target, getMyDataInt(_data));
    }

    bool checkContainsName(int x, int target)
    {
        data _data = data.ContainsName;
        return !myData.ContainsKey(_data) || ms.checkContainsName(x, target, getMyData(_data));
    }

    bool checkPerfectName(int x, int target)
    {
        data _data = data.PerfectName;
        return !myData.ContainsKey(_data) || ms.checkName(x, target, getMyData(_data));
    }

    void setData(data _data, string val)
    {
        if (myData.ContainsKey(_data))
            myData[_data] = val;
        else
            myData.Add(_data, val);
    }

    void setData(data _data, int val){
        setData(_data, ""+val);
    }

    public void setColor(cardColorInfo color)
    {
        setData(data.Color, (int)color);
    }
    public void setMax(int _val)
    {
        setData(data.Max, _val);
    }


}

