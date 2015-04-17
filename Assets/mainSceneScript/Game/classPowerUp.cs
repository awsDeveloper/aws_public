using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class classPowerUp : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    public int puv = 2000;

    List<classStruck> classList = new List<classStruck>();

    struct classStruck
    {
        public int class_1;
        public int class_2;

        public classStruck(int c1, int c2)
        {
            class_1 = c1;
            class_2 = c2;
        }
    }

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = puv;
    }

    // Update is called once per frame
    void Update()
    {
        //check situation
        if (BodyScript.isOnBattleField())
        {
            int target = player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            //check exist in upList
            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                //requirement add upList
                if (x >= 0 && checkClass(x, target) && ID != x && !ManagerScript.checkChanListExist(x, target, ID, player))
                    ManagerScript.alwaysChagePower(x, target, BodyScript.powerUpValue, ID, player);
            }
        }
        else
            ManagerScript.powChanListChangerClear(ID, player);
    }

    public bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);

        for (int i = 0; i < classList.Count; i++)
        {
            cardClassInfo cci = (cardClassInfo)(classList[i].class_1 * 10 + classList[i].class_2);

            if (/*c[0] == classList[i].class_1 && c[1] == classList[i].class_2 && */ManagerScript.checkClass(x, cplayer, cci))
                return true;
        }

        return false;
    }

    public void setClassList(int c1, int c2)
    {
        classList.Add(new classStruck(c1, c2));
    }

    public void setClassList(cardClassInfo cci)
    {
        int c = (int)cci;

        int c1 = c / 10;
        int c2 = c % 10;
        setClassList(c1, c2);
    }
}
