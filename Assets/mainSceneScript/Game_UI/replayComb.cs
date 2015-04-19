using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class replayComb : MonoBehaviour {
    List<string> replayList = new List<string>();

	// Use this for initialization
    void Start()
    {
        searchReplay();

        var comb = gameObject.GetComponent<Kender.uGUI.ComboBox>();

        var arry = new Kender.uGUI.ComboBoxItem[replayList.Count + 1];
        arry[0] = new Kender.uGUI.ComboBoxItem("select replay...");

        for (int i = 1; i < replayList.Count+1; i++)
            arry[i] = new Kender.uGUI.ComboBoxItem(replayList[i-1]);

        comb.Items = arry;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void searchReplay()
    {
        if (Directory.Exists(@"replay"))
        {
            DirectoryInfo[] array = new DirectoryInfo(@"replay").GetDirectories();

            for (int i = 0; i < array.Length; i++)
                replayList.Add(array[i].Name);
        }
    }
}

