using UnityEngine;
using System.Collections;

public class WX05_001 : MonoBehaviour
{
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag_1 = false;
    bool costFlag_2 = false;

    int exCost = 5;
    bool afterBurst = false;
    int[] lostIDs = new int[] { -1, -1, -1 };

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        //dialog
        BodyScript.checkStr.Add("ロストエフェクト");
        BodyScript.checkStr.Add("エクストラターン");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
        {
            BodyScript.checkBox.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //useLim
        BodyScript.useLimit = uselim();

        //growEffect
        if (BodyScript.growEffectFlag)
        {
            BodyScript.growEffectFlag = false;

            int target = player;
            int f = (int)Fields.LRIGDECK;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && x != ID && TamaOrIona(x, target))
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.SetAnimation);
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add((int)Motions.GoLrigBottom);
                BodyScript.effectTargetID.Add(-3);
            }
        }

        //cip
        if (ManagerScript.getCipID() == ID + 50 * player)
        {
            int target = player;
            int f = (int)Fields.LRIGTRASH;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                {
                    if (ManagerScript.getCardType(x, target) == 0)
                    {
                        BodyScript.effectFlag = true;
                        BodyScript.effectMotion.Add((int)Motions.GoLrigBottom);
                        BodyScript.effectTargetID.Add(x + 50 * target);
                    }
                    else if (ManagerScript.getCardType(x, target) == 1)
                    {
                        int c = ManagerScript.getCardColor(ID, player);

                        if (c == 1 || c == 5)
                        {
                            BodyScript.effectFlag = true;
                            BodyScript.effectMotion.Add((int)Motions.GoLrigDeck);
                            BodyScript.effectTargetID.Add(x + 50 * target);
                        }
                    }
                }
            }
        }

        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            ManagerScript.targetableExceedIn(player, BodyScript);

            int count = BodyScript.Targetable.Count;
            BodyScript.Targetable.Clear();

            if (count >= exCost)
            {
                BodyScript.Targetable.Clear();

                BodyScript.DialogFlag = true;
                BodyScript.DialogNum = 2;
                BodyScript.DialogCountMax = 1;
            }
            else if (count >= 1)
                effect_1();

        }

        //ignition after cost
        if (costFlag_1 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_1 = false;

            if (ManagerScript.getLastMotionsRear() == (int)Motions.Exceed)
            {
                afterBurst = true;

                int target = 1 - player;
                int f = 3;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num && i < lostIDs.Length; i++)
                {
                    int gfrID = ManagerScript.getFieldRankID(f, i, target);

                    if (gfrID >= 0 && !ManagerScript.getCardScr(gfrID, target).lostEffect)
                    {
                        lostIDs[i] = gfrID;
                        ManagerScript.changeLostEffect(lostIDs[i], target, true, ID, player);
                    }

                }
            }
        }

        //ignition after cost_2
        if (costFlag_2 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_2 = false;

            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.ExtraTrun);
        }

        //down after burst
        if (afterBurst && ManagerScript.getTurnEndFlag())
        {
            afterBurst = false;

            for (int i = 0; i < lostIDs.Length; i++)
            {
                if (lostIDs[i] >= 0)
                {
                    CardScript sc = ManagerScript.getCardScr(lostIDs[i], 1 - player);
                    if (sc != null && sc.lostEffect)
                        ManagerScript.changeLostEffect(lostIDs[i], 1 - player, false, ID, player);

                    lostIDs[i] = -1;
                }
            }
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                if (BodyScript.checkBox[0]) effect_1();
                else if (BodyScript.checkBox[1]) effect_2();
            }

            BodyScript.messages.Clear();
        }
    }

    void effect_1()
    {
        costFlag_1 = true;
        ManagerScript.targetableExceedIn(player, BodyScript);
        BodyScript.effectFlag = true;
        BodyScript.effectTargetID.Add(-2);
        BodyScript.effectMotion.Add((int)Motions.Exceed);
    }

    void effect_2()
    {
        costFlag_2 = true;

        ManagerScript.targetableExceedIn(player, BodyScript);
        BodyScript.effectFlag = true;

        for (int i = 0; i < exCost; i++)
        {
            BodyScript.effectTargetID.Add(-2);
            BodyScript.effectMotion.Add((int)Motions.Exceed);
        }
    }

    bool uselim()
    {
        int target = player;
        int f = (int)Fields.LRIGDECK;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && x != ID && TamaOrIona(x, target))
                return false;
        }

        return true;
    }

    bool TamaOrIona(int x, int target)
    {
        CardScript sc = ManagerScript.getCardScr(x, target);

        if (sc == null)
            return false;

        return sc.LrigType == 1 || sc.LrigType == 9 || sc.LrigType_2 == 1 || sc.LrigType_2 == 9;
    }

}
