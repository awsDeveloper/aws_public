using UnityEngine;
using System.Collections;

public class PR_214 : MonoCard {

	// Use this for initialization
	void Start () {
        var t = sc.AddEffectTemplete(tri, false, true);
        t.addEffect(triggered);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool tri()
    {
        if (!sc.isOnBattleField())
            return false;

        int _id=ms.getOneFrameID(OneFrameIDType.DoubleCrashedID);
        return _id >= 0 && _id / 50 == 1 - player;
    }

    void triggered()
    {
        sc.setEffect(ID, player, Motions.Up);
    }

}

