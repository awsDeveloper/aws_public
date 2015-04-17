using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    public int[] Cost { get { return status.Cost; } }
    public int[] GrowCost { get { return status.growCost; } }
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
	public bool effectFlag = false;
	public int ID = -1;
	public int player = -1;
	public string SerialNumString="";

	public int powerUpValue = -1;
	public int CharmSetRank=-1;
	public int CharmizeID=-1;

	public bool BurstFlag = false;
	public bool AttackNow = false;
	public bool Ignition = false;
	public bool TrashIgnition = false;
	public bool AntiCheck = false;

	public bool MultiEnaFlag = false;
	public bool GuardFlag = false;
    public bool Freeze = false;

    public bool growEffectFlag = false;
    public bool chantEffectFlag = false;

    //失われる効果
	//ランサー
    private bool Lancer = false;
    public bool lancer
    {
        get { return Lancer && !lostEffect; }
        set { Lancer = value; }
    }

    //ダブルクラッシュ
    private bool doubleCrash = false;
    public bool DoubleCrash {
        get { return doubleCrash && !lostEffect; }
        set { doubleCrash = value; }
    }

    //攻撃できない
    private bool attackable = true;
    public bool Attackable {
        get { return attackable || lostEffect; }
        set{attackable=value;}
    }

    //アサシン
    private bool assassin = false;
    public bool Assassin
    {
        get { return assassin && !lostEffect; }
        set { assassin = value; }
    }

    //バニッシュ耐性
    private bool resiBanish = false;
    public bool ResiBanish {
        get { return resiBanish && !lostEffect; }
        set { resiBanish = value; }
    }

    private bool resiYourEffBanish = false;
    public bool ResiYourEffBanish
    {
        get { return resiYourEffBanish && !lostEffect; }
        set { resiYourEffBanish = value; }
    }

    //スペルカットイン
	public bool SpellCutIn = false;
	public bool UseSpellCutIn=false;

    //アタックアーツ
	public bool attackArts = false;
	public bool UseAttackArts = false;
	
	public bool notMainArts = false;

	public bool useLimit=false;

    public bool targetableDontRemove = false;
    public bool targetableSameLevelRemove = false;

	public bool ReplaceFlag=false;

	public bool DialogFlag = false;
	public string DialogStr = string.Empty;
	public bool DialogStrEnable = false;
	public int DialogNum = 0;
	public int DialogCountMax = 0;
    public List<string> checkStr = new List<string>();
    public List<bool> checkBox = new List<bool>();
    public List<string> messages = new List<string>();

	public int effectSelecter = -1;
	public bool costBanish = false;

	public GameObject Manager;
	public GameObject Brain;
	public List<int> effectTargetID = new List<int> ();
	public List<int> effectMotion = new List<int> ();

    //targetID
    public bool TargetIDEnable = false;
    public List<int> TargetID = new List<int>();
	public List<int> Targetable = new List<int> ();

	public bool PayedCostEnable=false;
	public List<int> PayedCostList = new List<int> ();

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

    Fields field = Fields.Non;
    Fields oldField = Fields.Non;

    string parameta = "";
    char kugiri = '#';


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
	}
	
	// Update is called once per frame
	void Update ()
	{
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

            Lancer = false;
            doubleCrash = false;
            attackable = true;
            assassin = false;
            resiBanish = false;
        }

        if (Manager == null)
            return;

        oldField = field;
        field = (Fields)ms.getFieldInt(ID, player);
	}

	public void changeCost(int[] c){
		if(c.Length != Cost.Length)
			return;

		for (int i = 0; i < Cost.Length; i++)
		{
			Cost[i]=c[i];
		}
	}

	public void changeColorCost(int color,int CostValue){
		for (int i = 0; i < Cost.Length; i++)
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

    public void setEffect(int x, int target, Motions m)
    {
        if (x < 0 && Targetable.Count == 0 && m != Motions.CostBanish && m != Motions.ShowZoneGoTop)//入力ナンバーでターゲットなしは何もしない
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

        if ((cardTypeInfo)Type == cardTypeInfo.シグニ)
            return ms.getFieldInt(ID, player) == (int)Fields.SIGNIZONE;

        if ((cardTypeInfo)Type == cardTypeInfo.ルリグ)
            return ms.getLrigID(player) == ID;

        return false;
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
        parameta += pk.ToString() + kugiri+ n.ToString() + kugiri;
    }

    public int getParameta(parametaKey pk)
    {
        int n = -1;
        string[] s = parameta.Split(kugiri);

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == pk.ToString())
            {
                n = int.Parse(s[i + 1]);
                parameta.Replace(s[i] + kugiri + s[i + 1] + kugiri, "");
                break;
            }
        }
        return n;
    }

    public bool isYourEffRemoved()
    {
        if (ms.getEffecterNowID() < 0)
            return false;
        return ms.getEffecterNowID() / 50 == 1 - player && ms.getExitID((int)Fields.SIGNIZONE, -1) == ID + 50 * player;
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

        if (t == (int)cardTypeInfo.シグニ)
            return oldField != field && field == Fields.SIGNIZONE;
        else if (t == (int)cardTypeInfo.ルリグ)
            return ms.getCipID() == ID + 50 * player;

        return false;
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
        int min = 0;

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

    public void funcTargetIn(int target, Fields field,System.Func<int,int,bool> func)
    {
        int f = (int)field;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x>=0 && func(x,target))
                Targetable.Add(x + 50 * target);
        }
    }

    public void funcTargetIn(int target, Fields field)
    {
        funcTargetIn(target, field, reTrue);
    }
    public bool isUp()
    {
        return isOnBattleField() && ms.getIDConditionInt(ID, player) == (int)Conditions.Up;
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

    public bool isMessageYes()
    {
        if (messages.Count == 0)
            return false;

        string s = messages[0];
        messages.RemoveAt(0);
        return s.Contains("Yes");
    }
}

public enum parametaKey
{
    CostDownColor,
    CostDownNum,
    SpellOrArts,
}