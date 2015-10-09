using UnityEngine;
using System.Collections;

public class FuncChangeCost : MonoCard {
    bool upFlag = false;

    colorCostArry cost;

    System.Func<bool> func;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (func == null)
            return;

        if (func())
        {
            sc.changeCost(cost);
            upFlag = true;
        }
        else if (upFlag)
        {
            sc.changeCost(sc.OrigiCost);
            upFlag = false;
        }
    }

    public void set(System.Func<bool> f, colorCostArry _cost)
    {
        func = f;
        cost = _cost;
    }
    public void set(System.Func<bool> f, cardColorInfo info, int num)
    {
        func = f;
        cost = new colorCostArry(info, num);
    }
}
