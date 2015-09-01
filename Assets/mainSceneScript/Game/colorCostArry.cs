using UnityEngine;
using System.Collections;

public class colorCostArry
{
    int[] cost = new int[6];

    public colorCostArry(bool isAllMax)
    {
        if (isAllMax)
        {
            for (int i = 0; i < cost.Length; i++)
                cost[i] = 100;
        }
    }

    public colorCostArry(int[] a)
    {
        for (int i = 0; i < cost.Length; i++)
            cost[i] = a[i];
    }

    public colorCostArry(cardColorInfo info, int num)
    {
        for (int i = 0; i < cost.Length; i++)
            cost[i] = 0;

        cost[(int)info] = num;
    }

    public int getCost(int color)
    {
        return cost[color];
    }

    public void addCost(int color, int num)
    {
        cost[color] += num;
    }
}