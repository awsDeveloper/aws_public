using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public enum CardEffectType
{
    GoLrigTrashLrigUnder,
    UpThisCard,
    GoLrigDeckThisCard,
    DownThisCard,
    OneDraw,
    ThisSigniGoHand,
    TopEnaCharge,
    ThreeTopEnaCharge,
    PayCostOneGreen,
}

public class CardEffects : MonoCard {
    void PayCostOneGreen()
    {
        sc.setPayCost(cardColorInfo.緑, 1);
    }

    void ThreeTopEnaCharge()
    {
        TopEnaCharge();
        TopEnaCharge();
        TopEnaCharge();
    }

    void TopEnaCharge()
    {
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }

    void ThisSigniGoHand()
    {
        sc.setEffect(ID, player, Motions.GoHand);
    }

    void OneDraw()
    {
        sc.setEffect(0, player, Motions.Draw);
    }

    void DownThisCard()
    {
        if (sc.isUp())
            sc.setDown();
    }

    void GoLrigTrashLrigUnder()
    {
        ms.targetableExceedIn(player, sc);
        sc.setEffect(-2, 0, Motions.GOLrigTrash);
    }

    void UpThisCard()
    {
        sc.setEffect(ID, player, Motions.Up);
    }

    void GoLrigDeckThisCard()
    {
        sc.setEffect(ID, player, Motions.GoLrigDeck);
    }


	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Action getEffect(CardEffectType T)
    {
        MethodInfo mInfo = typeof(CardEffects).GetMethod(T.ToString(), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        Action act = (Action)Delegate.CreateDelegate(typeof(Action), this, mInfo);
        return act;
    }
}

