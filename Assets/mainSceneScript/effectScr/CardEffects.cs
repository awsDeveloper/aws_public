using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public enum CardEffectType
{
    GoLrigTrashLrigUnder,
    UpThisCard,
    GoLrigDeckThisCard,
}

public class CardEffects : MonoCard {
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

