using UnityEngine;
using System.Collections;

public class PR_018 : MonoCard {
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<FuncChangeBase>().set(check, 18000);
	}
	
	// Update is called once per frame
	void Update () {
        //cip
		if(sc.isCiped()){
			int handNum=ms.getFieldAllNum(2,player);	
			for(int i=0;i<handNum;i++){
				int x=ms.getFieldRankID(2,i,player);
				if(x>=0){
					sc.Targetable.Add(x+50*player);
				}
			}
			if(sc.Targetable.Count>0){
				sc.effectFlag=true;
				sc.effectTargetID.Add(-1);
				sc.effectMotion.Add(19);
			}
		}
		///burst
		if(sc.isBursted()){
			sc.effectFlag=true;
			sc.effectTargetID.Add(player*50 );
			sc.effectMotion.Add(2);
		}
	}

    bool check()
    {
        return ms.getTurnPlayer() == player;
    }
}
