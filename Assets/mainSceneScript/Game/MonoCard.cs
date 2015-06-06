using UnityEngine;
using System.Collections;

public class MonoCard : MonoBehaviour
{
    DeckScript _ms;
    public DeckScript ms
    {
        get
        {
            beforeStart();
            return _ms;
        }
    }

    CardScript _sc;
    public CardScript sc
    {
        get
        {
            beforeStart();
            return _sc;
        }
    }

    public int ID = -1;
    public int player = -1;

    bool started = false;

    // Use this for initialization
    public void beforeStart()
    {
        if (started)
            return;

        GameObject Body = transform.parent.gameObject;
        _sc = Body.GetComponent<CardScript>();

        ID = _sc.ID;
        player = _sc.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        _ms = Manager.GetComponent<DeckScript>();

        started = true;
    }
}