using UnityEngine;
using System.Collections;

public class MonoCard : MonoBehaviour
{
    public DeckScript ms;
    public CardScript sc;
    public int ID = -1;
    public int player = -1;

    // Use this for initialization
    public void beforeStart()
    {
        GameObject Body = transform.parent.gameObject;
        sc= Body.GetComponent<CardScript>();

        ID = sc.ID;
        player = sc.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ms = Manager.GetComponent<DeckScript>();
    }
}