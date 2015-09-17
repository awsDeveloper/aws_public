using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum ability
{
    Lancer,             //ランサー
    DoubleCrash,        //ダブルクラッシュ
    assassin,           //アサシン
    resiBanish,         //バニッシュ耐性
    resiYourEffBanish,  //相手のスペルのバニッシュ耐性
    DontSelfGoTrash,    //効果以外で墓地に送れない
    DontAttack,         //攻撃できない
    resiArts,            //アーツ耐性
    FreezeThrough,      //正面凍結時アサシン
    TwoChargeAfterCrash,//このシグニが対戦相手のライフクロスをクラッシュしたとき、あなたのデッキの上からカードを２枚エナゾーンに置く。
}


public enum parametaKey
{
    CostDownColor,
    CostDownNum,
    SpellOrArts,
    EnAbilityType,
    powerSumBanishValue,
    ClassNumBanishTarget,
    changeBaseValue,
    settingDialogNum,
}

public enum dialogUpConditions//dialogが立った時の状況
{
    non,            //登録なし
    cip,
    banished,
}

public class CardScript : MonoBehaviour
{
    cardstatus status;
    DeckScript ms;

    public string Name { get { return status.cardname; } }
    public string Text { get { return status.cardtext; } }
    public int Type { get { return status.type; } }

    public int CardColor = -1;
    
    public int OrigColor { get { return status.cardColor; } }

    public int Level { 
        get { return status.level; }
        set { status.level = value; }
    }
    public colorCostArry Cost { get { return status.Cost; } }
    public colorCostArry GrowCost { get { return status.growCost; } }
    public int Limit { get { return status.Limit; } }

    public int LrigType { get { return status.lrigType; } }
    public int LrigType_2 { get { return status.lrigType_2; } }

    public int LrigLimit { get { return status.lrigLimit; } }
    public int LrigLimit_2 { get { return status.lrigLimit_2; } }

    public int Class_1 { get { return status.cardClass[0]; } }
    public int Class_2 { get { return status.cardClass[1]; } }

    public int secondClass_1 { get { return status.secondClass[0]; } }
    public int secondClass_2 { get { return status.secondClass[1]; } } 

    public int Power = -1;
    public int OriginalPower { get { return status.power; } }
    public int basePower = -1;
    public int changePower = 0;

    public int BurstIcon = -1;

    public int origiBurstIcon
    {
        get
        {
            if (status.BurstIcon)
                return 1;
            return 0;
        }
    }
    /*	public int[] GrowCost = new int[6]{-1,0,0,0,0,0};
        public int Limit = -1;
        public int LrigType = -1;
        public int LrigType_2 = -1;
        public int LrigLimit = -1;
        public int LrigLimit_2 = 0;
        public int Class_1 = -1;
        public int Class_2 = -1;

        public int Power = -1;
        public int OriginalPower = -1;
        public int basePower = -1;
        public int changePower = 0;

        public int BurstIcon = -1;*/
	
    public int ID = -1;
	public int player = -1;
	public string SerialNumString="";
    public bool effectFlag = false;

	public int powerUpValue = -1;
	public int CharmSetRank=-1;
	public int CharmizeID=-1;

	public bool BurstFlag = false;
	public bool AttackNow = false;
	public bool Ignition = false;
	public bool TrashIgnition = false;
	public bool AntiCheck = false;

	public bool MultiEnaFlag = false;
    public bool WhiteEnaFlag = false;
    public bool GuardFlag = false;
    public bool Freeze = false;

    public bool growEffectFlag = false;
    public bool chantEffectFlag = false;

    //クロス関連
    public bool havingCross{get{return CrossLeftName!="" || CrossRightName!="";}}
    public string CrossRightName
    {
        get { return status.crossRightName; }
        set { status.crossRightName = value; }
    }
    public string CrossLeftName
    {
        get { return status.crossLeftName; }
        set { status.crossLeftName = value; }
    }


    //失われる効果
	//ランサー
//    private bool Lancer = false;
    public bool lancer
    {
        get { return checkAbility(ability.Lancer); }//Lancer && !lostEffect; }
        set { setAbilitySelf(ability.Lancer, value); }//Lancer = value; }
    }

    //ダブルクラッシュ
//    private bool doubleCrash = false;
    public bool DoubleCrash {
        get { return checkAbility(ability.DoubleCrash); }// doubleCrash && !lostEffect; }
        set { setAbilitySelf(ability.DoubleCrash, value); }//doubleCrash = value; }
    }

    //攻撃できない
//    private bool attackable = true;
    public bool Attackable {
        get { return !checkAbility(ability.DontAttack); }//attackable || lostEffect; }
        set { setAbilitySelf(ability.DontAttack, !value); }//attackable=value;}
    }

    //アサシン
//    private bool assassin = false;
    public bool Assassin
    {
        get { return checkAbility(ability.assassin); }//assassin && !lostEffect; }
        set { setAbilitySelf(ability.assassin, value); }//assassin = value; }
    }

    //バニッシュ耐性
//    private bool resiBanish = false;
    public bool ResiBanish {
        get { return checkAbility(ability.resiBanish); }//resiBanish && !lostEffect; }
        set { setAbilitySelf(ability.resiBanish, value); }//resiBanish = value; }
    }

//    private bool resiYourEffBanish = false;
    public bool ResiYourEffBanish
    {
        get { return checkAbility(ability.resiYourEffBanish); }//resiYourEffBanish && !lostEffect; }
        set { setAbilitySelf(ability.resiYourEffBanish, value); }//resiYourEffBanish = value; }
    }

    //スペルカットイン
	public bool SpellCutIn = false;
	public bool UseSpellCutIn=false;

    //アタックアーツ
	public bool attackArts = false;
	public bool UseAttackArts = false;
	
	public bool notMainArts = false;

	public bool useLimit=false;

    public bool useResona=false;

    public bool targetableDontRemove = false;
    public bool targetableSameLevelRemove = false;
    public bool targetablePayCostRemove = false;//payCostで捨てられたやつを対象から除く

	public bool ReplaceFlag=false;


	public bool DialogFlag = false;
	public string DialogStr = string.Empty;
	public bool DialogStrEnable = false;
	public int DialogNum = 0;
    public int DialogCountMax = 0;
    public bool DialogMaxSelect = false;
    public dialogUpConditions DialogUpCondition = dialogUpConditions.non;
    public List<string> checkStr = new List<string>();
    public List<bool> checkBox = new List<bool>();
    public List<string> messages = new List<string>();

	public int effectSelecter = -1;
	public bool costBanish = false;

	public GameObject Manager;
	public GameObject Brain;
	public List<int> effectTargetID = new List<int> ();

    public List<int> effectMotion  = new List<int>();

    //targetID
    public bool TargetIDEnable = false;
    public List<int> TargetID = new List<int>();
	public List<int> Targetable = new List<int> ();

	public bool PayedCostEnable=false;
	public List<int> PayedCostList = new List<int> ();

    public bool PayedCostFlag = false;//払ったときに立つ


    public bool resiEffect = false;
    public bool resiLrigEffect = false;
/*	public bool resiSigniEffect=false;
	public bool resiArtsEffect=false;
	public bool resiSpellEffect=false;*/

    //cancel
    public bool GUIcancelEnable = false;
    public bool cursorCancel = false;
    public int inputReturn = -1;

    public bool lostEffect=false;

    public int IgniAddID = -1;

    public int BeforeCutInNum = 1;

    public bool MelhenGrowFlag = false;

    public bool costGoTrashIDenable = false;

    Func<bool> checkCanUse;
    bool checkCanUseSugukesu = true;

    Dictionary<ability,FlagSet> abilityFlags = new Dictionary<ability, FlagSet>();

    List<int>[] a = new List<int>[3];

    Fields field = Fields.Non;
    Fields oldField = Fields.Non;

    Dictionary<parametaKey, int> parametaDictionary = new Dictionary<parametaKey, int>();


    bool brainChecke = false;//brainのスクリプト取得が終わっていることを示す

    class FlagSet{
       public bool self = false;
       public bool other = false;

       public FlagSet()
       {
       }
    }

 
	// Use this for initialization
	void Start ()
	{
        if(status==null)
            LoadCardStatus(SerialNumString);

        if (Manager != null)
            ms = Manager.GetComponent<DeckScript>();

		DialogStr = Name + "の効果を発動しますか？";

        CardColor = OrigColor;
        basePower = OriginalPower;
        BurstIcon = origiBurstIcon;

        foreach (var item in System.Enum.GetValues(typeof(ability)))
            abilityFlags.Add((ability)item, new FlagSet());
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!checkBrainScr())
            return;

        Power = basePower + changePower;

        //効果を失う
        if (lostEffect)
        {
            //effect
            effectFlag = false;
            effectMotion.Clear();
            effectTargetID.Clear();

            //dialog
            DialogFlag = false;

        }

 
        if (Manager == null)
            return;

        oldField = field;
        field = (Fields)ms.getFieldInt(ID, player);

     }

    public void AbilityClear()
    {
        foreach (var item in abilityFlags.Keys)
            setAbility(item, false);
    }

    bool checkBrainScr()
    {
        if (brainChecke || Brain == null)
            return true;

        var scr = Brain.GetComponent<getPlaneScrript>();
        if (scr == null)
        {
            brainChecke = true;
            return true;
        }

        brainChecke = scr.getSet();
        return brainChecke;
    }

    public void changeCost(int[] c)
    {
        if (c.Length != Cost.Length())
            return;

        for (int i = 0; i < Cost.Length(); i++)
        {
            Cost[i] = c[i];
        }
    }
    public void changeCost(colorCostArry c)
    {
        for (int i = 0; i < Cost.Length(); i++)
        {
            Cost[i] = c.getCost(i);
        }
    }

	public void changeColorCost(int color,int CostValue){
		for (int i = 0; i < Cost.Length(); i++)
		{
			Cost[i]=0;
		}

		Cost[color]=CostValue;
	}

    public void changeColorCost(cardColorInfo info, int CostValue)
    {
        int color = (int)info;
        changeColorCost(color, CostValue);
    }

    public void setEffect_sukinakazu(int x, int target, Motions m)
    {
        for (int i = 0; i < Targetable.Count; i++)
            setEffect(x, target, m);

        if (x == -1)
            cursorCancel = true;
        else if (x == -2)
            GUIcancelEnable = true;
    }

    public void setEffect(int x, int target, Motions m)
    {
        //入力ナンバーでターゲットなしは何もしない
        if (x < 0 && Targetable.Count == 0 
            && m != Motions.CostBanish 
            && m != Motions.ShowZoneGoTop
            && m != Motions.ShowZoneGoBottom)
            return;

        if (m == Motions.CostBanish && ms.getFieldAllNum((int)Fields.SIGNIZONE, player) == 0)
            return;

        effectFlag = true;
        effectTargetID.Add(x + 50 * target);
        effectMotion.Add((int)m);
    }

    public void setAntiCheck()
    {
        setEffect(0, player, Motions.AntiCheck);
        AntiCheck = true;
    }

    public void LoadCardStatus(string s)
    {
        status = new cardstatus(s);
    }

    public bool isOnBattleField()
    {
        if (ID < 0)
            return false;

        if (ms.checkType(ID, player, cardTypeInfo.シグニ) || ms.checkType(ID, player, cardTypeInfo.レゾナ))
            return ms.getFieldInt(ID, player) == (int)Fields.SIGNIZONE;

        if ((cardTypeInfo)Type == cardTypeInfo.ルリグ)
            return ms.getLrigID(player) == ID;

        return false;
    }

    public bool isTrashOrEna()
    {
        int fff = ms.getFieldInt(ID, player);
        return fff == (int)Fields.TRASH || fff == (int)Fields.ENAZONE;
    }



    public bool isChanted()
    {
        if (ID < 0)
            return false;


        int t = ms.getCardType(ID, player);
        if (t != (int)cardTypeInfo.スペル && t != (int)cardTypeInfo.アーツ)
            return false;

        return oldField != field && field == Fields.CHECKZONE && !BurstFlag;
    }

    public void addParameta(parametaKey pk, int n)
    {
        if (parametaDictionary.ContainsKey(pk))
        {
            parametaDictionary[pk] = n;
            return;
        }

        parametaDictionary.Add(pk, n);
    }

    public int getParameta(parametaKey pk)
    {
        if (!parametaDictionary.ContainsKey(pk))
            return -1;

        return parametaDictionary[pk];
    }

    public bool isYourEffRemoved()
    {
        if (ms.getEffecterNowID() < 0)
            return false;
        return ms.getEffecterNowID() / 50 == 1 - player && isRemovedThisSigni();//ms.getExitID((int)Fields.SIGNIZONE, -1) == ID + 50 * player;
    }
    public bool isRemovedThisSigni()
    {
        return ms.getExitID((int)Fields.SIGNIZONE, -1) == ID + 50 * player;
    }

    public bool isBursted()
    {
        if (ID < 0)
            return false;

        int t = ms.getCardType(ID, player);
        if (t != (int)cardTypeInfo.スペル && t != (int)cardTypeInfo.シグニ)
            return false;

        return oldField != field && field == Fields.CHECKZONE && BurstFlag;
    }

    public bool isCiped()
    {
        if (ID < 0)
            return false;

        int t = ms.getCardType(ID, player);

        if (t == (int)cardTypeInfo.シグニ || t == (int)cardTypeInfo.レゾナ)
            return oldField != field && field == Fields.SIGNIZONE;
        else if (t == (int)cardTypeInfo.ルリグ)
            return ms.getCipID() == ID + 50 * player;

        return false;
    }

    public bool isMyResonaCiped()
    {
        if (ID < 0 || !isOnBattleField())
            return false;

        int sID = ms.getCipSigniID();

        return sID >= 0 && sID / 50 == player && ms.checkType(sID, cardTypeInfo.レゾナ);
    }

    public bool isCenter()
    {
        return ms.getFieldInt(ID, player) == (int)Fields.SIGNIZONE && ms.getRank(ID, player) == 1;
    }

    public bool isSigniOnBattleField(int target)
    {
        return ms.getFieldAllNum((int)Fields.SIGNIZONE, target) > 0;
    }

    public bool isClassSigniOnBattleField(int target,cardClassInfo info)//自分以外
    {
        int f = (int)Fields.SIGNIZONE;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && x + 50 * target != ID + 50 * player && ms.checkClass(x, target, info))
                return true;
        }

        return false;
    }

    public int getFuncNum(Func<int, int, bool> func, int target, Fields _field = Fields.SIGNIZONE)//自分以外
    {
        int f = (int)_field;
        int num = ms.getNumForCard(f, target);
        int count = 0;

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && x + 50 * target != ID + 50 * player && func(x, target))
                count++;
        }

        return count;
    }

    public bool isFuncOnBatteField(Func<int,int,bool> func, int target)//自分以外
    {
        return getFuncNum(func, target) > 0;
    }

    public bool isResonaOnBattleField()//自分以外
    {
        return isResonaOnBattleField(player);
    }
    public bool isResonaOnBattleField(int target)//自分以外
    {
        int f = (int)Fields.SIGNIZONE;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && x + 50 * target != ID + 50 * player && ms.checkType(x,target, cardTypeInfo.レゾナ))
                return true;
        }

        return false;
    }

    public int getTargLevNum()
    {
        int num = 0;
        List<int> list = new List<int>();

        for (int i = 0; i < Targetable.Count; i++)
        {
            int lev = ms.getCardLevel(Targetable[i] % 50, Targetable[i] / 50);

            if (lev > 0 && !list.Contains(lev))
            {
                list.Add(lev);
                num++;
            }
        }

        return num;
    }

    public bool isFieldNumMany(Fields f,int target)
    {
        int num = ms.getFieldAllNum((int)f, target);

        return num > ms.getFieldAllNum((int)f, 1 - target);
    }

    public int getTargetID()
    {
        if (TargetID.Count == 0)
            return -1;

        int x = TargetID[0];
        TargetID.RemoveAt(0);
        return x;
    }

    public int getMaxPower(int target)
    {
        int f = (int)Fields.SIGNIZONE;
        int num = ms.getNumForCard(f, target);
        int max = 0;

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && ms.getCardPower(x, target) > max)
                max = ms.getCardPower(x, target);
        }

        return max;
    }

    public int getMinPower(int target)
    {
        int f = (int)Fields.SIGNIZONE;
        int num = ms.getNumForCard(f, target);
        int min = getMaxPower(target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && ms.getCardPower(x, target) < min)
                min = ms.getCardPower(x, target);
        }

        return min;
    }

    public bool isAttacking()
    {
        return ms.getAttackerID() == ID + 50 * player;
    }

    public void funcTargetIn(int target, Fields field, System.Func<int, int, bool> func, DeckScript ds)
    {
        if (ms != null)
            return;

        ms = ds;//今だけmsを登録
        if (func != null)
            funcTargetIn(target, field, func);
        else
            funcTargetIn(target, field);
        ms = null;
    }
    public void funcTargetIn(int target, Fields field, System.Func<int, int, bool> func=null)
    {
        int f = (int)field;
        int num = ms.getNumForCard(f, target);
        if (func == null)
            func = reTrue;

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && func(x, target))
                Targetable.Add(x + 50 * target);
        }
    }

    public void ClassTargetIn(int target, Fields field, cardClassInfo info)
    {
        int f = (int)field;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && ms.checkClass(x, target,info))
                Targetable.Add(x + 50 * target);
        }
    }
    public void LevelMaxTargetIn(int target, Fields field, int levelMax)
    {
        int f = (int)field;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && ms.getCardLevel(x,target)<=levelMax)
                Targetable.Add(x + 50 * target);
        }
    }


    public bool isUp()
    {
        return isOnBattleField() && ms.getIDConditionInt(ID, player) == (int)Conditions.Up;
    }

    public void maxPowerBanish(int maxPower)
    {
        maxPowerTargetIn(maxPower);
        setEffect(-1, 0, Motions.EnaCharge);
    }

    public void maxPowerTargetIn(int maxPower)
    {
        int target = 1 - player;
        int f = (int)Fields.SIGNIZONE;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && ms.getCardPower(x, target) <= maxPower)
                Targetable.Add(x + 50 * target);
        }
    }

    public void minPowerBanish(int minPower)
    {
        minPowerTargetIn(minPower);
        setEffect(-1, 0, Motions.EnaCharge);
    }

    public void minPowerTargetIn(int minPower,bool forMe=false)//デフォルトは相手
    {
        int target = 1 - player;
        if (forMe)
            target = player;

        int f = (int)Fields.SIGNIZONE;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && ms.getCardPower(x, target) >= minPower)
                Targetable.Add(x + 50 * target);
        }
    }

    public bool IgnitionDown()
    {
        if (!Ignition)
            return false;

        Ignition = false;

        if (!isUp())
            return false;

        setEffect(ID, player, Motions.Down);
        return true;

    }

    public void setDown()
    {
        setEffect(ID, player, Motions.Down);
    }

    public bool IgnitionPayCost(cardColorInfo info, int num)
    {
        if (!Ignition)
            return false;

        Ignition = false;

        changeColorCost(info, num);
        if (!myCheckCost())
            return false;

        setEffect(ID, player, Motions.PayCost);
        return true;
    }

    public bool IgnitionDownPayCost(cardColorInfo info, int num)
    {
        if (!Ignition)
            return false;

        Ignition = false;

        changeColorCost(info, num);
        if (!myCheckCost())
            return false;

        Ignition = true;//チェック後Ignitoin復活
        if (!IgnitionDown())
            return false;

        setEffect(ID, player, Motions.PayCost);
        return true;
    }

    public bool myCheckCost()
    {
        return ms.checkCost(ID, player);
    }

    public int getFreezeNum(int target)
    {//凍結数
        int num = 0;
        for (int i = 0; i < 3; i++)
        {
            int x = ms.getFieldRankID(3, i, target);
            if (x >= 0 && ms.getCardScr(x, target).Freeze)
                num++;
        }
        return num;
    }

    public bool chantedYourArts()
    {
        if (ms.getChantArtsID() == -1)
            return false;

        return ms.getChantArtsID() / 50 != player;
    }
    public bool chantedYourSpell()
    {
        if (ms.getChantSpellID() == -1)
            return false;

        return ms.getChantSpellID() / 50 != player;
    }

    public bool isStartedTurn()
    {
        return ms.getStartedPhase() == (int)Phases.UpPhase;
    }

    public void showZoneTargIn(System.Func<int ,int,bool> func)
    {
        int index = 0;
        while (true)
        {
            int x = ms.getShowZoneID(index);

            if (x < 0)
                break;

            if (func(x % 50, x / 50))
                Targetable.Add(x);

            index++;
        }
    }

    public int getFieldTypeNum(int target, Fields f, cardTypeInfo info)
    {
        int count=0;
        int num = ms.getFieldAllNum((int)f, target);
        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID((int)f, i, target);
            if (ms.checkType(x, target, info))
                count++;
        }

        return count;
    }

    public bool reTrue(int x,int target){
        return true;
    }

    public int getNameShurui(int target, Fields field,System.Func<int,int,bool> func)
    {
        int count = 0;
        List<string> nameList = new List<string>();

        int f = (int)field;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);

            if (x >= 0 && func(x, target))
            {
                string s = ms.getCardScr(x, target).Name;
                if (!nameList.Contains(s))
                    nameList.Add(s);
            }
        }

        count = nameList.Count;
        nameList.Clear();
        return count;
    }

    public bool isMessageYes(dialogUpConditions c)
    {
        if (DialogUpCondition != c)
            return false;

        return isMessageYes();
    }

    public bool isMessageYes()//yes no 専用
    {
        if (messages.Count == 0 || DialogNum != (int)DialogNumType.YesNo)
            return false;

        string s = messages[0];
        messages.RemoveAt(0);
        return s.Contains("Yes");
    }

    public int getMessageInt()
    {
        if (messages.Count == 0)
            return -1;

        string s = messages[0];
        messages.RemoveAt(0);

        int count=-1;
        if (!int.TryParse(s, out count))
            return -1;

        return count;
    }

    public void banishedDialog(cardColorInfo info, int num)
    {
        if (ms.getBanishedID() != ID + 50 * player)
            return;

        changeColorCost(info, num);
        if (!myCheckCost())
            return;

        setDialogNum(DialogNumType.YesNo);
        DialogUpCondition = dialogUpConditions.banished;
    }

    public void cipDialog(cardColorInfo info,int num)
    {

        if (!isCiped())
            return;

        changeColorCost(info, num);
        if (!myCheckCost())
            return;

        setDialogNum(DialogNumType.YesNo);
        DialogUpCondition = dialogUpConditions.cip;
    }

    public void setDialogNum(DialogNumType type)
    {
        DialogFlag = true;
        DialogNum = (int)type;
    }
    public void setDialogNum(DialogNumType type, cardColorInfo info, int num)
    {
        changeColorCost(info, num);
        if (!myCheckCost())
            return;

        DialogFlag = true;
        DialogNum = (int)type;
    }

    public int getCrossingID()
    {
        if (!havingCross || !isOnBattleField())
            return -1;

        int rank = ms.getRank(ID, player);

        int checkRank = -1;
        string checkName = "";

        if (CrossLeftName != "")
        {
            checkRank = rank - 1;
            checkName = CrossLeftName;
        }

        if (CrossRightName != "")
        {
            checkRank = rank + 1;
            checkName = CrossRightName;
        }

        int x = ms.getFieldRankID((int)Fields.SIGNIZONE, checkRank, player);
        if (x >= 0 && ms.checkName(x, player, checkName))
            return x + player * 50;

        return -1;
    }

    public bool isCrossTriggered()
    {
        int fusion = ID + player * 50;
        return fusion == ms.CrossedIDs[0] || fusion == ms.CrossedIDs[1];
    }

    public bool isCrossing()
    {
        return getCrossingID() >= 0 && !lostEffect;
    }

    public bool isHeaven()
    {
        int cID=getCrossingID();

        if(cID <0)
            return false;

        if (ms.getAttackerID() != ID + player * 50 && ms.getAttackerID() != cID)
            return false;

        return ms.getIDConditionInt(ID, player) == (int)Conditions.Down && ms.getIDConditionInt(cID % 50, cID / 50) == (int)Conditions.Down;
    }

    public bool isCrossOnBattleField(int target)
    {
        int f = (int)Fields.SIGNIZONE;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && x + 50 * target != ID + 50 * player && ms.checkCross(x,target))
                return true;
        }

        return false;

    }

   public void setSystemCardFromCard(int targetID, Motions m,int count ,List<int> targetableList, bool cancel, Func<int, bool> systemInput)
    {
        for (int i = 0; i < count; i++)
        {
            if (i == 0)
                ms.SetSystemCardFromCard(targetID, m, ID, player, targetableList, cancel, systemInput);
            else
                ms.SetSystemCardFromCard(targetID, m, ID, player);
        }
    }

    public void setFuncEffect(int x, Motions m, int target, Fields field, System.Func<int, int, bool> func)
    {
        funcTargetIn(target, field, func);
        setEffect(x, 0, m);
    }

    public bool checkAbility(ability key)
    {
        if (!abilityFlags.ContainsKey(key))
            return false;

        //FreezeThroughによるアサシンの付与
        if (key == ability.assassin && checkFreezeThrough())
            return true;

        return abilityFlags[key].other || (abilityFlags[key].self && !lostEffect);
    }

    public void setAbility(ability key, bool _value)
    {
        if (abilityFlags.ContainsKey(key))
            abilityFlags[key].other = _value;
    }
    public void setAbilitySelf(ability key, bool _value)
    {
        if (abilityFlags.ContainsKey(key))
            abilityFlags[key].self = _value;
    }

    public bool CrossingCip()
    {
        return isCiped() && isCrossing();
    }
    public bool CrossNotCip()
    {
        if (!isOnBattleField())
            return true;

        int cID = ms.getCipSigniID();
        if (cID < 0 || cID / 50 != player)
            return true;

        return !ms.checkHavingCross(cID % 50, cID / 50);
    }

    public bool mySigniNotHeaven()
    {
        if (!isOnBattleField())
            return true;

        int aID = ms.getAttackerID();
        if (aID == -1)
            return true;

        return aID / 50 == 1 - player || !ms.getCardScr(aID % 50, aID / 50).isHeaven();
    }

    public bool myColorSigniHeaven(cardColorInfo info)
    {
        if (!isOnBattleField())
            return false;

        int aID = ms.getAttackerID();
        if (aID == -1)
            return false;

        return aID / 50 == player && ms.checkColor(aID%50, aID/50, info) && ms.getCardScr(aID % 50, aID / 50).isHeaven();
    }

    public bool notResonaAndUtyu(int x, int target)
    {
        return !ms.checkType(x, target, cardTypeInfo.レゾナ) && ms.checkClass(x, target, cardClassInfo.精羅_宇宙);
    }
    public bool notResonaAndKyotyu(int x, int target)
    {
        return !ms.checkType(x, target, cardTypeInfo.レゾナ) && ms.checkClass(x, target, cardClassInfo.精生_凶蟲);
    }

    public void setFieldAllEffect(int target,Fields F, Motions m, Func<int,int,bool> che=null)
    {
        if (che == null)
            che = reTrue;

        int num = ms.getNumForCard((int)F, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID((int)F, i, target);
            if (x >= 0 && che(x,target))
                setEffect(x,target,m);
        }
    }

    public void setCanUseFunc(Func<bool> check, bool sugukesu=true)
    {
        checkCanUse = check;
        checkCanUseSugukesu = sugukesu;
    }

    public bool getCanUseFunc()
    {
        //初期値では常にTrue
        if (checkCanUse == null)
            return true;

       var f=checkCanUse;
       if (checkCanUseSugukesu)
           checkCanUse = null;
        return f();
    }

    bool classDownDownCostInputReturn(int count)
    {
        for (int i = 0; i < count; i++)
            ms.setSpellCostDown(getParameta(parametaKey.CostDownColor), getParameta(parametaKey.CostDownNum), player, getParameta(parametaKey.SpellOrArts));

        return true;
    }

    public void beforeChantClassDownDownCost(cardClassInfo cls, cardColorInfo clr, int downValue)
    {
        if (!chantEffectFlag)
            return;

        chantEffectFlag = false;

        int spellOrArs = boolParse(ms.checkType(ID, player, cardTypeInfo.アーツ));
        addParameta(parametaKey.CostDownColor, (int)clr);
        addParameta(parametaKey.CostDownNum, downValue);
        addParameta(parametaKey.SpellOrArts, spellOrArs);



        int target = player;
        int f = (int)Fields.SIGNIZONE;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && ms.checkClass(x, target, cls) && ms.getCardScr(x,target).isUp())
                Targetable.Add(x + 50 * target);
        }

        if (Targetable.Count == 0)
            return;

        setSystemCardFromCard(-1, Motions.Down, Targetable.Count, Targetable, true, classDownDownCostInputReturn);

/*        cursorCancel = true;

        for (int i = 0; i < Targetable.Count; i++)
            setEffect(-1, 0, Motions.DownAndCostDown);*/
    }

    int boolParse(bool flag)
    {
        if (!flag)
            return 0;

        return 1;
    }

    public void setPayCost()
    {
        setEffect(ID, player, Motions.PayCost);
    }

    public void setPayCost(cardColorInfo info, int num)
    {
        changeColorCost(info, num);
        setPayCost();
    }

    public void setEnAbilityForMe(ability a)
    {
        setEffect(ID, player, Motions.EnAbility);
        addParameta(parametaKey.EnAbilityType, (int)a);
    }

    bool checkFreezeThrough()
    {
        if(!checkAbility(ability.FreezeThrough))
            return false;

        int rank = ms.getRank(ID, player);
        rank = 1 - (rank - 1);//正面のランクを得る

        int x = ms.getFieldRankID(3, rank, 1 - player);

        return x >= 0 && ms.checkFreeze(x, 1 - player);
    }

    public void resonaSummon(Func<int, int, bool> check, int num, bool isCostUse=false, Fields _field= Fields.SIGNIZONE)
    {
        if (!useResona)
            return;
        useResona = false;

        funcTargetIn(player, _field, check);
        if (Targetable.Count < num)
        {
            Targetable.Clear();
            return;
        }

        setSystemCardFromCard(-1, Motions.CostGoTrash, num, Targetable, false, null);
        if (_field == Fields.HAND)
            ms.SetSystemCardFromCard(50 * player, Motions.HandSort, ID, player);

        ms.SetSystemCardFromCard(ID + 50 * player, Motions.Summon, ID, player);

        if (isCostUse)
            costGoTrashIDenable = true;
    }

    public int getExecutedLevelSum(Motions m)
    {
        int sum=0;
        int eID = -1;

        while((eID = ms.getEffectExecutedID(ID,player,m))!=-1)
            sum += ms.getCardLevel(eID);

        return sum;
    }

    public bool checkExistTargetable()
    {
        int x = Targetable.Count;
        Targetable.Clear();
        return x > 0;
    }

    public void effectInsertOne(int targetID, Motions m)
    {
        effectTargetID.Insert(1, targetID);
        effectMotion.Insert(1, (int)m);
    }

    public int getCipYourSigniID()
    {
        int sID = ms.getCipSigniID();
        if (sID == -1 || sID / 50 == player)
            return -1;

        return sID;
    }

    public void setCostDownValue(cardColorInfo info, int num)
    {
        Cost.setDownValue(info, num);
    }

    public EffectTemplete AddEffectTemplete(Func<bool> tri)
    {
        var com = ms.getFront(ID, player).AddComponent<EffectTemplete>();
        com.setTrigger(tri);
        return com;
    }

    public EffectTemplete AddEffectTemplete(EffectTemplete.triggerType t)
    {
        var com = ms.getFront(ID, player).AddComponent<EffectTemplete>();
        com.setTrigger(t);
        return com;
    }

    public EffectTemplete AddEffectTemplete(EffectTemplete.triggerType t, Action a, bool isCost=false, bool isUseYesNo=false)
    {        
        var com = ms.getFront(ID,player).AddComponent<EffectTemplete>();
        com.setTrigger(t,isUseYesNo);
        com.addEffect(a, isCost);
        return com;
    }

    public void targetInputEffect()
    {
        setEffect(-1, 0, Motions.AntiCheck);
        TargetIDEnable = true;
    }

    void targetInputEffect_end()
    {
        setEffect(ID, player, Motions.AntiCheck);
        TargetIDEnable = false;
        TargetID.Clear();
    }

    public EffectTemplete AddEffectTemp_useTargetInput(EffectTemplete.triggerType t, Action a, Action targetableInAction, bool isCost = false, bool isUseYesNo = false)
    {
        var com = ms.getFront(ID, player).AddComponent<EffectTemplete>();
        com.setTrigger(t, isUseYesNo);
        com.addEffect(targetableInAction  , isCost);
        com.addEffect(a, isCost);
        com.addEffect(targetInputEffect_end);
        return com;
    }
}
