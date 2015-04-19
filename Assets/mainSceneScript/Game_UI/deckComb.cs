using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class deckComb : MonoBehaviour {
    FileInfo[] ListOfDeck;
    readonly string DeckStringKey = "DeckKey";

    public int playerNum=0;
    // Use this for initialization
    void Start()
    {
        searchAlldeck();

        var comb = gameObject.GetComponent<Kender.uGUI.ComboBox>();
        var arry = new Kender.uGUI.ComboBoxItem[ListOfDeck.Length+1];
        string s = loadDeckString();
        int index=0;

        arry[0] = new Kender.uGUI.ComboBoxItem("select Deck...");
        for (int i = 1; i < ListOfDeck.Length + 1; i++)
        {
            string name = ListOfDeck[i-1].Name.Split('.')[0];
            arry[i] = new Kender.uGUI.ComboBoxItem(name);

            if (name == s)
                index = i;
        }

        comb.Items = arry;
        comb.SelectedIndex = index;
        if (ListOfDeck.Length < 4)
            comb.ItemsToDisplay = ListOfDeck.Length;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void searchAlldeck()
    {
        DirectoryInfo di = new DirectoryInfo("./deck/");
        ListOfDeck = di.GetFiles("*.txt", System.IO.SearchOption.TopDirectoryOnly);
    }

    string loadDeckString()
    {
        string s = "";
        string key = DeckStringKey + playerNum.ToString();

        if (PlayerPrefs.HasKey(key))
            s = Singleton<saveData>.instance.getData(key, "deck");

        if (!File.Exists(@"deck/" + s + ".txt") && ListOfDeck.Length > 0)
            s = ListOfDeck[0].Name.Split('.')[0];

        return s;
    }
}

