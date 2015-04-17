using UnityEngine;
using System.Collections;

public class config{


    public bool configNow = false;
    
    private bool replaySaveFlag = false;
    public bool ReplaySaveFlag
    {
        get { return replaySaveFlag; }
        set { replaySaveFlag = value; }
    }

    public config()
    {
        if (PlayerPrefs.HasKey("replaySaveFlagKey"))
            replaySaveFlag = true;
    }
}
