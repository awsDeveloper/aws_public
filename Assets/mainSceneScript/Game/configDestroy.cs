using UnityEngine;
using System.Collections;

public class configDestroy : MonoBehaviour {
    public void destroy()
    {
        Singleton<config>.instance.configNow = false;

        foreach (var item in GameObject.FindGameObjectsWithTag("config"))
        {
            DestroyObject(item);
        }
    }
}
