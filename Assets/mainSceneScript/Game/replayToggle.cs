using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class replayToggle : MonoBehaviour {

    void Start()
    {
        Toggle to = gameObject.GetComponent<Toggle>();
        to.isOn = Singleton<config>.instance.ReplaySaveFlag;
    }

    public void set(bool f)
    {
        Singleton<config>.instance.ReplaySaveFlag = f;
    }
}
