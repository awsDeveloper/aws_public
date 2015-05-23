using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassCountPowerUp : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool upFlag = false;

    public int puv = 1000;

    classList myClass = new classList();

 /*   List<classStruck> classList = new List<classStruck>();
 
    struct classStruck
    {
        public int class_1;
        public int class_2;

        public classStruck(int c1, int c2)
        {
            class_1 = c1;
            class_2 = c2;
        }
    }*/

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
        if (BodyScript.isOnBattleField() && getClassNum()>0)
        {
            upFlag = true;
            ManagerScript.powChanListChangerClear(ID, player);
            ManagerScript.alwaysChagePower(ID, player, BodyScript.powerUpValue*getClassNum(), ID, player);
        }
        else if (upFlag)
        {
            upFlag = false;
            ManagerScript.powChanListChangerClear(ID, player);
        }
    }

    int getClassNum()//others
    {
        int n = 0;

        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(3, i, player);
            if (x!=ID && checkClass(x, player))
                n++;
        }

        return n;
    }

    public bool checkClass(int x, int cplayer)
    {
/*        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);

        for (int i = 0; i < classList.Count; i++)
        {
            cardClassInfo cci = (cardClassInfo)(classList[i].class_1 * 10 + classList[i].class_2);

            if (ManagerScript.checkClass(x, cplayer, cci))
                return true;
        }

        return false;*/
        return myClass.checkClass(x,cplayer, ManagerScript);
    }

    public void setClassList(int c1, int c2)
    {
        cardClassInfo info = (cardClassInfo)(c1*10+c2);
        myClass.setClass(info);
    }

    public void setClassList(cardClassInfo cci)
    {
        int c = (int)cci;

        int c1 = c / 10;
        int c2 = c % 10;
        setClassList(c1, c2);
    }
}
