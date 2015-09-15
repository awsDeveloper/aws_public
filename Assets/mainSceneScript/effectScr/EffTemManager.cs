using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffTemManager :EffectTemplete {
    Dictionary<string, EffectTemplete> temps = new Dictionary<string, EffectTemplete>();
    bool upper = false;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        triggered();
        dialogReceive();	
	}

    void triggered()
    {
        if (!isTriggered())
            return;

        //checkBoxの生成
        foreach (var key in temps.Keys)
        {
            sc.checkStr.Add(key);
            sc.checkBox.Add(false);            
        }

        upper = true;
        sc.DialogFlag = true;
        sc.DialogNum = (int)DialogNumType.toggle;
        sc.DialogCountMax = 1;
    }

    void dialogReceive()
    {
        //receive
        if (!upper || sc.messages.Count == 0)
            return;

        upper = false;

        if (sc.messages[0].Contains("Yes"))
        {
            for (int i = 0; i < sc.checkBox.Count; i++)
            {
                if (sc.checkBox[i])
                {
                    string[] key=new string[temps.Count];
                    temps.Keys.CopyTo(key,0);
                    temps[key[i]].doAfterGettingYes();
                    break;
                }
            }
        }

        sc.messages.Clear();
    }

    public void setTemplete(string _key, EffectTemplete _temp)
    {
        //manual modeに変更
        _temp.setManualMode();
        temps.Add(_key, _temp);
    }
}

