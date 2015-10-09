using UnityEngine;
using System.Collections;

public class WX09_046 : MonoCard
{

    // Use this for initialization
    void Start()
    {
        var ig = sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni, true);
        ig.addEffect(igni_1, true);
        ig.addEffect(igni_2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void igni()
    {
        sc.setDown();
    }

    void igni_1()
    {
        sc.setFuncEffect(-1, Motions.CostHandDeath, player, Fields.HAND, new checkFuncs(ms, cardClassInfo.精械_電機).check);
    }
    void igni_2()
    {
        sc.setEffect(0, 1 - player, Motions.oneHandDeath); 
    }
}
