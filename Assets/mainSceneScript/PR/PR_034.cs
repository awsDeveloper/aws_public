using UnityEngine;
using System.Collections;

public class PR_034 : MonoCard{
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<FuncChangeBase>().set(check_lrig, 10000);

        gameObject.AddComponent<FuncPowerUp>().setForMe(2000,check_trash);
	}
	
	// Update is called once per frame
    void Update()
    {
    }

    bool check_trash()
    {
        return ms.getFieldAllNum((int)Fields.TRASH, player) >= 5;
    }

    bool checkMe(int x, int target)
    {
        Debug.Log(x == ID && target == player);
        return x == ID && target == player;
    }

    bool check_lrig()
    {
        return ms.checkLrigType(player, LrigTypeInfo.ウリス) && ms.getLrigLevel(player) == 4;
    }
}
