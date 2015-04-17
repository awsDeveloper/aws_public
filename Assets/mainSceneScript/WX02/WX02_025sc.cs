using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX02_025sc : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool bunki_1 = false;
    bool bunki_2 = false;

    List<int> equipList = new List<int>();
    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
        {
            int target = player;
            int f = 3;
            //			int num=ManagerScript.getFieldAllNum(f,target);	
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                {
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(7);
            }
        }
        //always
        checkEquip();

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int target = player;
            int f = 0;
            int num = ManagerScript.getFieldAllNum(f, target);
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                int x = ManagerScript.getFieldRankID(f, num - 1 - i, target);
                if (x >= 0)
                {
                    if (checkClass(x, target)) count++;
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                bunki_1 = true;
                BodyScript.effectFlag = true;
                if (count > 0)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add(16);
                }
                else
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add(22);
                    bunki_2 = true;
                }
            }
        }
        if (bunki_1 && (BodyScript.effectTargetID.Count == 1 && BodyScript.effectTargetID[0] >= 0 || bunki_2))
        {
            bunki_1 = false;
            int px = -1;
            if (!bunki_2) px = BodyScript.effectTargetID[0];
            if (checkClass(px % 50, px / 50) || bunki_2)
            {
                bunki_2 = false;
                int target = player;
                int f = 0;
                int num = ManagerScript.getFieldAllNum(f, target);
                for (int i = 0; i < 5; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, num - 1 - i, target);
                    if (x >= 0 && px != x + 50 * target)
                    {
                        BodyScript.effectTargetID.Add(x + 50 * target);
                        BodyScript.effectMotion.Add(7);
                    }
                }
            }
            else
            {
                bunki_1 = true;
                BodyScript.effectTargetID[0] = -2;
                BodyScript.Targetable.Add(px);
            }
        }
        field = ManagerScript.getFieldInt(ID, player);
    }
    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return c[0] == 4 && c[1] == 1;
    }

    void equip(int x, int eplayer)
    {
        if (x < 0) return;
        equipList.Add(x + 50 * eplayer);
    }
    void dequip(int index)
    {
        if (index >= equipList.Count) return;
        equipList.RemoveAt(index);
    }
    void checkEquip()
    {
        int target = player;
        int equipField = 3;
        //		int fieldAll=ManagerScript.getFieldAllNum(equipField,player);
        //check situation
        if (ManagerScript.getFieldInt(ID, player) == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(equipField, i, target);
                if (!checkExist(x, target) && checkClass(x, target)) equip(x, target);
            }
        }
        else
        {
            while (equipList.Count > 0)
            {
                dequip(0);
            }
        }
        //equip target check
        if (equipList.Count > 0)
        {
            for (int i = 0; i < equipList.Count; i++)
            {
                int x = equipList[i];
                if (ManagerScript.getFieldInt(x % 50, x / 50) != equipField)
                {
                    if (ManagerScript.getFieldInt(x % 50, x / 50) == 6) effect();
                    dequip(i);
                    i--;
                }
            }
        }
    }
    bool checkExist(int x, int player)
    {
        for (int i = 0; i < equipList.Count; i++)
        {
            if (x + 50 * player == equipList[i]) return true;
        }
        return false;
    }
    void effect()
    {
        int target = 1 - player;
        int f = 3;
        if (ManagerScript.getFieldAllNum(f, target) == 0) return;
        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0) BodyScript.Targetable.Add(x + target * 50);
        }
        BodyScript.effectFlag = true;
        BodyScript.effectTargetID.Add(-1);
        BodyScript.effectMotion.Add(5);
    }
}
