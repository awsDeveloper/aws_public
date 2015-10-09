using UnityEngine;
using System.Collections;

//割とゴミプログラミングだけど動くからセーフ
public class NameCostResona : MonoCard {
    string[] costNames = new string[3]{"","",""};
    
    cardClassInfo myClass;
    bool useMyClass = false;

	// Use this for initialization
	void Start () {

        var com = gameObject.AddComponent<EffectTemplete>();
        com.setTrigger(EffectTemplete.triggerType.useResona);

         com.addEffect(effect_0, true, EffectTemplete.checkType.system);

        if(costNames[1]!="")
            com.addEffect(effect_1, true, EffectTemplete.checkType.system);

        if (costNames[2] != "")
            com.addEffect(effect_2, true, EffectTemplete.checkType.system);

        if (useMyClass)
            com.addEffect(classCost, true, EffectTemplete.checkType.system);


        com.addEffect(summon, false, EffectTemplete.checkType.system);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void set(string n0, string n1 = "", string n2 = "")
    {
        costNames[0] = n0;
        costNames[1] = n1;
        costNames[2] = n2;
    }
    public void set(string n0, cardClassInfo info)//名前とクラスを指定するレゾナにも対応
    {
        costNames[0] = n0;
        myClass = info;
        useMyClass = true;
    }

    //effects
    void effect_0()
    {
        effect(check_0);
    }
    void effect_1()
    {
        effect(check_1);
    }
    void effect_2()
    {
        effect(check_2);
    }

    void effect(System.Func<int,int,bool> func)
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, func);
        sc.setSystemCardFromCard(-1, Motions.CostGoTrash, 1, sc.Targetable, false, null);
    }

    void classCost()
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, classCheck);

        if (sc.Targetable.Count == 1 && ms.checkName(sc.Targetable[0], costNames[0]))
        {
            sc.Targetable.Clear();
            return;
        }

        sc.setSystemCardFromCard(-1, Motions.CostGoTrash, 1, sc.Targetable, false, null);
    }

    bool classCheck(int x, int target)
    {
        return ms.checkClass(x, target, myClass) && !ms.checkType(x, target, cardTypeInfo.レゾナ);
    }

    void summon()
    {
        sc.setSystemCardFromCard(ID + 50 * player, Motions.Summon, 1, null, false, null);
    }


    //checks
    bool check_0(int x, int target)
    {
        return check(x, target, 0);
    }
    bool check_1(int x, int target)
    {
        return check(x, target, 1);
    }
    bool check_2(int x, int target)
    {
        return check(x, target, 2);
    }

    bool check(int x, int target, int index)
    {
        return ms.checkName(x, target, costNames[index]);
    }
}

