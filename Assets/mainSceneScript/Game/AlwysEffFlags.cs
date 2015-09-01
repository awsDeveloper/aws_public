using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum alwysEffs
{
    Arachne,
}

public static class AlwysEffFlags{
    static Dictionary<alwysEffs, int>[] flags = new Dictionary<alwysEffs, int>[2];

    static AlwysEffFlags()
    {
        flags[0] = new Dictionary<alwysEffs, int>();
        flags[1] = new Dictionary<alwysEffs, int>();

        foreach (var item in System.Enum.GetValues(typeof(alwysEffs)))
        {
            flags[0].Add((alwysEffs)item, -1);
            flags[1].Add((alwysEffs)item, -1);
        }
    }

    public static void flagUp(alwysEffs eff,int target, int upper)
    {
        flags[target][eff] = upper;
    }

    public static void flagDown(alwysEffs eff,int target)
    {
        flags[target][eff] = -1;
    }

    public static int getUpperID(alwysEffs eff, int target)
    {
        return flags[target][eff];
    }

    public static bool checkFlagUp(alwysEffs eff, int target)
    {
        return flags[target][eff] >= 0;
    }
}

