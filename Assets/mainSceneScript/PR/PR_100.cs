using UnityEngine;
using System.Collections;

public class PR_100 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    int artsCount = 0;
    int spellCount = 0;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.attackArts = true;
        BodyScript.notMainArts = true;

        DialogToggle dt = gameObject.AddComponent<DialogToggle>();

        dt.setTrigger(DialogToggle.triggerType.Chant);
        dt.set("シグニアタック", rTrue, effect_1);
        dt.set("ルリグアタック", rTrue, effect_2);
     }
	
	// Update is called once per frame
	void Update () 
    {
        costing();
	
	}

    bool rTrue()
    {
        return true;
    }

    bool effect_1()
    {
        BodyScript.setEffect(0, player, Motions.SigniAttackSkip);

        return true;
    }

    bool effect_2()
    {
        BodyScript.setEffect(0, player, Motions.LrigAttackSkip);

        return true;
    }

    void costing()
    {
        if (BodyScript.chantedYourArts())
            artsCount++;

        if (BodyScript.chantedYourSpell())
            spellCount++;

        if (BodyScript.isStartedTurn())
        {
            spellCount = 0;
            artsCount = 0;
        }

        if (artsCount > 0 && spellCount > 0)
            BodyScript.changeColorCost(cardColorInfo.白, 0);
        else if (artsCount > 0 || spellCount > 0)
        {
            BodyScript.changeColorCost(cardColorInfo.無色, 4);
            BodyScript.Cost[1] = 1;
        }
        else
        {
            BodyScript.changeColorCost(cardColorInfo.無色, 7);
            BodyScript.Cost[1] = 1;
        }
    }
}
