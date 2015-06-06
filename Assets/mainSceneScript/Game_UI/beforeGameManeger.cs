using UnityEngine;
using System.Collections;

public class beforeGameManeger : MonoBehaviour
{
    public string uiString = "";
    public GameObject replayObj;
    public GameObject firstObj;
    public GameObject ipAddres;

    public GameObject[] deckObjs = new GameObject[2];

    void Start()
    {
        var item = firstObj.GetComponent<Kender.uGUI.ComboBox>();

        if (item != null && PlayerPrefs.HasKey("firstAttack"))
            item.SelectedIndex = PlayerPrefs.GetInt("firstAttack");

        var ip = ipAddres.GetComponent<UnityEngine.UI.InputField>();
        if (ip != null && PlayerPrefs.HasKey("connectIP"))
            ip.text = PlayerPrefs.GetString("connectIP");
    }


    void OnDestroy()
    {
        var item = firstObj.GetComponent<Kender.uGUI.ComboBox>();

        if (item != null)
            PlayerPrefs.SetInt("firstAttack", item.SelectedIndex);
    }

    public string getSelectedReplay()
    {
        return getComboString(replayObj);
    }

    public string getFirstAttack()
    {
        return getComboString(firstObj);
    }

    string getComboString(GameObject obj)
    {
        if (obj == null)
            return string.Empty;

        var item = obj.GetComponent<Kender.uGUI.ComboBox>();

        if (item == null || item.SelectedIndex == 0)
            return string.Empty;

        return item.Items[item.SelectedIndex].Caption;

    }

    public string getSelectedDeck(int n)
    {
        if (n >= deckObjs.Length || deckObjs[n] == null)
            return string.Empty;

        var item = deckObjs[n].GetComponent<Kender.uGUI.ComboBox>();

        if (item == null)
            return string.Empty;

        return item.Items[item.SelectedIndex].Caption;
    }
}

