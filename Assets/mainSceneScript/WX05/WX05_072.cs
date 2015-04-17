using UnityEngine;
using System.Collections;

public class WX05_072 : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<WX05_058>();
        WX05_058 wx = gameObject.GetComponent<WX05_058>();

        wx.checkClass_1 = 4;
        wx.checkClass_2 = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
