using UnityEngine;
using System.Collections;

public class PR_114 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Cip();
    }

    void Cip()
    {
        if (!BodyScript.isCiped())
            return;

        BodyScript.funcTargetIn(player, Fields.MAINDECK, check);
        BodyScript.setEffect(-2, 0, Motions.GoTrash);
        BodyScript.setEffect(0, player, Motions.Shuffle);
    }

    bool check(int x, int target)
    {
        return ManagerScript.checkClass(x, target, cardClassInfo.精像_悪魔);
    }
}
