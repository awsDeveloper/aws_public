using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class beforeGameButton : MonoBehaviour {
    GameObject panel;

    void Start()
    {
        panel = GameObject.Find("beforeGame");

        var button = this.GetComponent<UnityEngine.UI.Button>();

        button.onClick.AddListener(() =>
        {
            pushed();
        });
    }

    public void pushed()
    {
        if (panel == null)
            return;

        var bm = panel.GetComponent<beforeGameManeger>();

        var tx=transform.FindChild("Text").GetComponent<Text>();
        bm.uiString=tx.text;
    }
}

