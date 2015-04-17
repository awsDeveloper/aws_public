using UnityEngine;
using System.Collections;

public class WX05_027 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool costFlag = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        BodyScript.powerUpValue = 1000;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = -10000;

        //dialog
        BodyScript.checkStr.Add("天使バニッシュ");
        BodyScript.checkStr.Add("サルベージ");
        BodyScript.checkStr.Add("蘇生");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
            BodyScript.checkBox.Add(false);

    }

    // Update is called once per frame
    void Update()
    {
        //triggered
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.CrashedID != -1)
        {
            int cID = ManagerScript.CrasherID;

            if (cID == ID + 50 * player)
            {
                int target = 1-player;
                int f = (int)Fields.SIGNIZONE;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);
                    if (x >= 0)
                        BodyScript.Targetable.Add(x + 50 * target);
                }

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectFlag = true;
                    BodyScript.effectMotion.Add((int)Motions.PowerUpEndPhase);
                    BodyScript.effectTargetID.Add(-1);
                }
            }
        }

        //cip
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
        {
            BodyScript.changeColorCost(5, 3);
  
            if (ManagerScript.checkCost(ID, player))
            {
                BodyScript.DialogFlag = true;
                BodyScript.DialogNum = 0;
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            bool[] root=new bool[2];

            targetIN_b_tensi();
            root[0] = BodyScript.Targetable.Count>0;
            BodyScript.Targetable.Clear();

            targetIN_b_akuma();
            root[1] = BodyScript.Targetable.Count > 0;
            BodyScript.Targetable.Clear();

            if (root[0] || root[1])
            {
                BodyScript.DialogFlag = true;
                BodyScript.DialogNum = 2;
                BodyScript.DialogCountMax = 1;
            }
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                switch (BodyScript.DialogNum)
                {
                    case 0:
                        if (ManagerScript.checkCost(ID, player))
                        {

                            BodyScript.effectFlag = true;
                            BodyScript.effectMotion.Add((int)Motions.PayCost);
                            BodyScript.effectTargetID.Add(ID + 50 * player);

                            costFlag = true;
                        }
                        break;

                    case 2:
                        effect_0();
                        effect_1();
                        effect_2();
                        break;
                }
            }       
            BodyScript.messages.Clear();
        }

        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;
 
            CIPtargetIN();
            BodyScript.GUIcancelEnable = true;

            for (int i = 0; i < 2 && i < BodyScript.Targetable.Count; i++)
            {
                BodyScript.effectMotion.Add((int)Motions.Summon);
                BodyScript.effectTargetID.Add(-2);
            }
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    void effect_0()
    {
        if (BodyScript.checkBox[0])
        {
            targetIN_b_tensi();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;

                while (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectMotion.Add((int)Motions.EnaCharge);
                    BodyScript.effectTargetID.Add(BodyScript.Targetable[0]);
                    BodyScript.Targetable.RemoveAt(0);
                }
            }
        }
    }

    void effect_1()
    {
        if (BodyScript.checkBox[1])
        {
            targetIN_b_akuma();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.GUIcancelEnable = false;

                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add(16);
            }
        }
    }

    void effect_2()
    {
        if (BodyScript.checkBox[2])
        {
            targetIN_b_akuma();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.GUIcancelEnable = false;

                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add(6);
            }
        }
    }

    void targetIN_b_akuma()
    {
        int target = player;
        int f = 7;
        int num = ManagerScript.getFieldAllNum(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0 && checkClass_akuma(x, target))
            {
                BodyScript.Targetable.Add(x + 50 * target);
            }
        }
    }

    void targetIN_b_tensi()
    {
        for (int j = 0; j < 2; j++)
        {
            int target = j;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0 && checkClass_tensi(x, target))
                    BodyScript.Targetable.Add(x + 50 * target);
            }
        }
    }

    void CIPtargetIN()
    {
        int target = player;
        int f = 7;
        int num = ManagerScript.getFieldAllNum(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0 && ManagerScript.getCardLevel(x, target) <= 3 && checkClass_akuma(x,target))
                BodyScript.Targetable.Add(x + 50 * target);
        }
    }

    bool checkClass_tensi(int x, int cplayer)
    {
        if (x < 0)
            return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 4 && c[1] == 0);
    }

    bool checkClass_akuma(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return c[0] == 4 && c[1] == 1;
    }
}
