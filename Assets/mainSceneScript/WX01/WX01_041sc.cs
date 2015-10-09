using UnityEngine;
using System.Collections;

public class WX01_041sc : MonoCard {

	// Use this for initialization
	void Start () {

        sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni, true).addEffect(igni_1);
	}
	
	// Update is called once per frame
    void Update()
    {
        /* ------------ effectTempleteで記述 ---------------------------------------------------------
               if (BodyScript.Ignition && !IgnitionFlag)
                {
                   if (ManagerScript.getIDConditionInt(ID, player) == 1)
                    {
                        costFlag = true;
                        BodyScript.effectFlag = true;
                        BodyScript.effectTargetID.Add(ID + 50 * player);
                        BodyScript.effectMotion.Add(8);
                    }
                    else BodyScript.Ignition = false;
                }
                if (BodyScript.effectTargetID.Count == 0 && costFlag)
                {
                    costFlag = false;
                    BodyScript.Ignition = false;
                    for (int i = 0; i < 3; i++)
                    {
                        int x = ManagerScript.getFieldRankID(3, i, 1 - player);
                        if (x > 0 && ManagerScript.getCardPower(x, 1 - player) <= 7000)
                        {
                            BodyScript.Targetable.Add(x + (1 - player) * 50);
                        }
                    }
                    if (BodyScript.Targetable.Count > 0)
                    {
                        BodyScript.effectFlag = true;
                        BodyScript.effectTargetID.Add(-1);
                        BodyScript.effectMotion.Add(5);
                    }
                }
                IgnitionFlag = BodyScript.Ignition;*/
    }


    void igni()
    {
        sc.setDown();
    }

    void igni_1()
    {
        sc.maxPowerBanish(7000);
    }
}
