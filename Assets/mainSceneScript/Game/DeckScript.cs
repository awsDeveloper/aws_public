using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum Fields
{
	MAINDECK,  		//0
	LRIGDECK,  		//1
	HAND,      		//2
	SIGNIZONE, 		//3
	LRIGZONE, 		//4
	LIFECLOTH,		//5
	ENAZONE,  		//6
	TRASH,    		//7
	CHECKZONE,		//8
	LRIGTRASH,		//9
	Non,			//10
	CharmZone,		//11
	YOURCHECKZONE,	//12
    UnderZone,      //13
    Removed,        //14
}

enum Phases
{
	PrePhase,	//0
	UpPhase,	//1
	DrawPhase,	//2
	EnaPhase,	//3
	GrowPhase,	//4
	MainPhase,	//5
	AttackPhase,//6
	EndPhase	//7
}

public enum Motions
{
	NonMotion,			// 0
	LrigSet,			// 1
	Draw,				// 2
	LifeClothSet,		// 3
	LrigOpen,			// 4
	EnaCharge,			// 5
	Summon,				// 6
	GoTrash,			// 7
	Down,				// 8
	Up,					// 9
	SigniAttack,		//10
	LrigAttack,			//11
	Grow,				//12
	ChantSpell,			//13
	CheckBack,			//14
	ChantArts,			//15
	GoHand,				//16
	PayCost,			//17
	DontAttack,			//18
	HandDeath,			//19
	EnaSort,			//20
	PowerUp,			//21
	AntiCheck,			//22
	Mulligan,			//23
	Shuffle,			//24
	GoDeck,				//25
	TopEnaCharge,		//26
	FREEZE,				//27
	Crash,				//28
	Open,				//29
	Close,				//30
	SameSummon,			//31
	RandomHandDeath,	//32
	CostBanish,			//33
	PowerUpEndPhase,	//34
	RebornEndPhase,		//35
	GOLrigTrash,		//36
	LrigSummon,			//37
	onePlayerOpen,		//38
	onePlayerLifeOpen,	//39
	onePlayerLifeClose,	//40
	TopGoLifeCloth,		//41
	LifeSetFromHand,	//42
	HandSort,			//43
	checkBackLifeCloth,	//44
	oneHandDeath,		//45
	ColorHandDeath,		//46
	DontShuffleGoHand,	//47
	GoDeckBottom,		//48
	GoLrigDeck,			//49
	Exceed,				//50
	TopCrash,			//51
	AntiSpell,			//52
	EnDoubleCrashEnd,	//53
	GoShowZone,			//54
	MoveAttackFront,	//55
	ViolenceEffect,		//56
	ChangePosition,		//57
	RePosition,			//58
	NextTurnArtsLim,	//59
	DownAndFreeze,		//60
	DoublePowerEnd,		//61
	EnLancerEnd,		//62
	SetCharm,			//63
	TopSetCharm,		//64
	CostGoTrash,		//65
	OpenHand,			//66
	CloseHand,			//67
	ChantYourSpell,		//68
	UpMirrorMirage,		//69
	EnResiBanishEnd,	//70
    Damaging,           //71
    UpIgnition,         //72
    stopAttack,         //73
    NotBurstTopCrash,   //74
    SigniRise,          //75
    SigniFall,          //76
    GoUnderZone,        //77
    GoLrigBottom,       //78
    ExtraTrun,          //79
    SetAnimation,       //80
    LifeClothSort,      //81
    TopLifeGohand,      //82
    GoNextTurn,         //83
    DeckTopOpen,        //84
    EffectDown,         //85
    AddIgniEnd,         //86
    TopGoTrash,         //87
    CostDown,           //88 decrease
    TopGoShowZone,      //89
    PowerUpLevelEnd,    //90
    DownAndCostDown,    //91
    GridCountUp,        //92
    TraIgniCostZeroEnd, //93
    PowerUpAllEnd,      //94
    SigniAttackSkip,    //95
    LrigAttackSkip,     //96
    ShowZoneGoTop,      //97
    JerashiGeizu,       //98
    AllHandDeath,       //99
    MaxPowerBounce,     //100
    DownSummonFromTrash,
    LevelSumCostGoTrash,
    CounterSpell,
    EnAbility,
    GoEnaZone,
    Exclude,            //除外
    LrigDown,
    PowerSumBanish,
    CostHandDeath,  
    ClassNumBanish,
    changeBaseEnd,
    CostPowerUpEnd,
    trashCardAkumaCharm,
    trashCardAkumaCharm_2,//連鎖処理用
    SetCharmizeID,
    TempResonaSummon,
    LostEffectEnd,
    JupiterResona,
    NextMinusLimit,
    NotCipSummon,
    NotCipTempResona,
    DontGrow,
    NotDamageThisTurn,
    WarmHole,
    ShowZoneGoBottom,
    SetDialog,              //効果の途中でダイアログフラグを立てる
    SigniZonePowerUp,
}

public enum OneFrameIDType
{
    TrashSummonID,
    IgnitionUpID,
    TraIgniUpID,
    BanishedID,
    BanishingID,
    removingID,
    crashedID,
    effectDownedID,
    downID,
    stopAttackedID,
    exitID,
    EffectGoTrashID,
    ChantSpellID,
    ChantArtsID,
    TargetNowID,
    CounterID,
    cipID,
    BurstEffectID,
    AttackerID,
    FreezedID,
}

public enum DialogNumType
{
    YesNo,
    targetNum,
    toggle,
    color,
    TopBottom,
    UpDown,
    Level
}


enum positionInfo
{
    Top,
    Bottom
}

enum Conditions
{
	no,
	Up,
	Down
}

enum constance
{
	MovingNum=8
}

struct TargetCursor
{
	public GameObject targetCursor;
	public int ID;
	public int player;

	public TargetCursor (GameObject obj, int x)
	{
		targetCursor = obj;
		ID = x;
		player = 0;
	}

	public TargetCursor (GameObject obj, int x, int p)
	{
		targetCursor = obj;
		ID = x;
		player = p;
	}
};

struct NewCard//ミルルンの残骸
{
	public GameObject NewCardObj;
	public int TrueID;
	public int TruePlayer;
	public Fields MyField;
	
	public NewCard (GameObject obj, int x, int p,Fields f)
	{
		NewCardObj = obj;
		TrueID = x;
		TruePlayer = p;
		MyField=f;
	}
};

struct powerChange
{
	public int changedID;
	public int changerID;
	public int changeValue;
    public System.Func<int, int, bool> check;
    public powerChange(int changed, int changer, int value)
    {
        changedID = changed;
        changerID = changer;
        changeValue = value;
        check = null;
    }

    public powerChange(int changed, int changer, int value, System.Func<int, int, bool> checkFunc)
    {
        changedID = changed;
        changerID = changer;
        changeValue = value;
        check = checkFunc;
    }
}

class IgniCostDecrease
{
    public int downerID = -1;
    public int targetID = -1;
    public bool isEndFinishFlag = false;
    public Fields igniField;
    public colorCostArry downCost;

    public IgniCostDecrease(int x,int target,int ID,int player,colorCostArry cost,bool isEndFinish, Fields applyField) 
    {
        downerID = ID + player * 50;
        targetID = x + target * 50;
        isEndFinishFlag = isEndFinish;
        downCost = cost;
        igniField = applyField;
    }
}

class zonePowrUp
{
    public int powerUpValue = -1;
    public int zoneRank = -1;
    public int changer = -1;
    public int target = -1;
    public int turnCount = -1;

    public zonePowrUp(int _powerUpValue, int _zoneRank, int _changer, int _target, int _turnCount)
    {
        powerUpValue = _powerUpValue;
        zoneRank = _zoneRank;
        changer = _changer;
        target = _target;
        turnCount = _turnCount;
    }
}

public class DeckScript : MonoBehaviour
{
	GameObject SystemCard;
    CardScript SystemCardScr;
    int SystemCardClientID = -1;

	private GameObject[,] card = new GameObject[2, 50];
	private GameObject[,] front = new GameObject[2, 50];
	private string[,] SerialNumString = new string[2, 50];
	private Fields[,] field = new Fields[2, 50];
	private int[,] fieldRank = new int[2, 50];
//	private Texture[,] cardTexture = new Texture[2, 50];
	private Vector3 MAINDECK = new Vector3 (6.484179f, 0.1f, -3.031561f);
	private Vector3 TRASH = new Vector3 (5.736527f, 0.1f, -6.471652f);
	private Vector3 LRIGDECK = new Vector3 (3.374744f, 0.1f, -6.52284f);
	private Vector3 LRIGZONE = new Vector3 (0.05453968f, 0.1f, -6.55267f);
	private Vector3 HAND = new Vector3 (0f, 1f, -11.4f);
	private Vector3 LRIGTRASH = new Vector3 (8.3f, 0.1f, -6.5f);
	private Vector3 ENAZONE = new Vector3 (-7f, 0.1f, -3.69f);
	private Vector3 LIFECLOTH = new Vector3 (-2.65f, 0.1f, -9.56f);
	private Vector3 SIGNIZONE = new Vector3 (-3.39f, 0.1f, -3f);
	private Vector3 STORAGEZONE = new Vector3 (0.05453968f, -0.1f, -6.55267f);
	private Vector3 CHECKZONE = new Vector3 (-3.4f, 0.1f, -6.5f);
	private Vector3 SHOWZONE = new Vector3 (9.3f, 0.1f, -3.0f);
	private const float LifeClothWidth = 0.76f;
	private const float EnaWidth = 0.75f;
	private const float SigniWidth = 3.42f;

    int _standartTime = 5;
    private int standartTime
    {
        get { return _standartTime; }
        set { 
            _standartTime = value;
            Debug.Log("standartTimeが更新されました");
        }
    }

    private int[] deckNum = new int[2] { 0, 0 };
	private int[] lrig_deckNum = new int[2]{0,0};
	private int[] enaNum = new int[2]{0,0};
	private int[] trashNum = new int[2]{0,0};
	private int[] lrigTrashNum = new int[2]{0,0};
	private int[] handNum = new int[2]{0,0};
	private int[] drawNum = new int[2]{5,5};
	private int[] lrigZoneNum = new int[2]{0,0};
	private int[] LifeClothNum = new int[2]{0,0};

	private List<int> ShowZoneIDList = new List<int>();
	private int[] ShowZoneNum = new int[2]{0,0};

	private Phases phase = Phases.PrePhase;
	private Phases phaseBuf = Phases.PrePhase;

	private int GUImoveID = -1;
	private int GUImoveIDbuf = -1;
	private int[] moveID = new int[(int)constance.MovingNum];
	private Vector3[] destination = new Vector3[(int)constance.MovingNum];
	private int[] moveTime = new int[(int)constance.MovingNum];
	private int[] movePhase = new int[(int)constance.MovingNum];
	private int[] moveCount = new int[(int)constance.MovingNum];
	private int[] rotaID = new int[(int)constance.MovingNum];
	private float[,] angle = new float[(int)constance.MovingNum, 3];
	private int[] rotaTime = new int[(int)constance.MovingNum];
	private int[] rotaPhase = new int[(int)constance.MovingNum];
	private int[] rotaCount = new int[(int)constance.MovingNum];

	private Motions motion = Motions.NonMotion;
	private int[] handMoveCount = new int[2]{0,0};
	private int[] enaMoveCount = new int[2]{0,0};

//	private Texture showTexture;
	private int turn = 0;
	private GameObject SelectedCard = null;
	private int selectedID = -1;
	private int LabelCount = 0;
	private GUIStyle LabelStyle = new GUIStyle ();
	private GUIStyle NextStyle = new GUIStyle ();
	private GUIStyle PhaseStyle = new GUIStyle ();
	private bool selectCardFlag = true;
	private int slideNum = 0;
	private float slideFloat = 0f;
	private bool selectSigniZoneFlag = false;
	private int selectSigniZone = -1;

	private Conditions[,] signiCondition = new Conditions[2, 3];
	private Conditions[,] signiConditionBuf = new Conditions[2, 3];

	private Conditions[] LrigCondition = new Conditions[2] {
				Conditions.Up,
				Conditions.Up
		};
	private bool[] handSortFlag = new bool[2]{false,false};


    private string showSerialNum = "";
	private GameObject[] TargetSigniZoneCursor = new GameObject[3];
	private int selectPlayer = 0;
	private int selectClickID = -1;
	private bool hakken = false;
	private int hakkenNum = 0;
	private string hakkenStr = "";
	private List<TargetCursor> targetCursorList = new List<TargetCursor> ();
	private bool selectCursorFlag = false;
	private bool cursorCancel = false;
	private List<int> clickCursorID = new List<int> ();
	private List<int> clickCursorIDbuf = new List<int> ();
	private int selectNum = 0;
	private int player2RandNumder = 0;
	private int[] chainMotion = new int[2]{-1,-1};
	private int[,] chainMotionBuf = new int[2, 2]{{-1,-1},{-1,-1}};
	private int waitTime = 0;
	private List<int> selectCardList = new List<int> ();
	private bool selectEnaFlag = false;
	private List<int> enaColorList = new List<int> ();
	private int selectEnaNum = 0;
	private int[,] enaColorNum = new int[2, 6];
	private bool shortageFlag = false;
	private bool[] lrigSummonFlag = new bool[2]{false,false};

	private bool effectFlag = false;
	private bool SystemEffectFlag = false;
    private List<int> effectSelectIDbuf = new List<int>();
    private List<string> dialogSelectbuf = new List<string>();
	private int[] effecter = new int[2]{-1,-1};

	private static int firstAttack = 1;//先行後攻
	private static string firstAttackStr = "後攻";
	private int selectEnaPlayer = -1;
	private int selectSigniPlayer = -1;
	private bool guardSelectflag = false;
	private int guardPlayer = -1;
	private List<int> dontAttackList = new List<int> ();
	private bool showGUIFlag = false;
	private bool lancerFlag = false;

	bool SpellCutInSelectFlag = false;
	bool SpellCutInFlag = false;
	bool AskSpellCutInFlag = false;
	int SpellCutInPlayer = -1;
	int SpellCutInID = -1;
	bool SpellCutInEffect = false;
	bool effectUsableFlag = false;
	bool spellFieldChanged = false;
	bool useSpellCutInUp = false;
	bool useAttackArtsUp = false;

	int attackedID = -1;
	int muliganNum = 0;
	bool whichShow = false;
	bool[] deckRefleshFlag = new bool[2]{false,false};
	bool[] RefleshedFlag = new bool[2]{false,false};
	int[] refleshMotion = new int[2];
	bool selectTrashFlag = false;
	int summonCount = 0;
	List<int> sameSummonList = new List<int> ();
	private FileInfo[] ListOfDeck;
	private Vector2 scrollViewVector = new Vector2 (0, 0);
	bool[] preDeckCreat = new bool[]{true,true};
	bool[] DeckSelectplayer = new bool[2]{true,false};

	static string[] DeckString = new string[2]{"Deck","Deck2"};
	readonly string[] DeckStringKey =new string[2]{"DeckKey1","DeckKey2"};

	bool isTrans = true;
	int prePlayer = 0;
	const string sprt = "sprt";
	const string nextStr = "next";
	const string SummonStr = "Summon";
	const string SpellStr = "Spell";
	const string ArtsStr = "arts";
	const string GoTrashStr = "trash";
	const string YesStr = "Yes";
	const string NoStr = "No";
	const string AttackStr = "Attack";
	const string IgnitionStr = "Ignition";
	const string TrashIgnitionStr = "traIgnition";
    const string endStr = "end";
    const string cancelStr = "cancel";

	const string errorStr = "error";

	bool notMuligan = false;
	bool startedFlag = false;

	bool DialogFlag = false;
	bool DialogCrashFlag = false;
	int DialogID = -1;
	CardScript DialogScript=null;
	string DialogStr = string.Empty;
	int DialogNum = 0;
	int DialogCount = 0;
	int DialogCountMax = 0;
	List<int> checkID = new List<int> ();
	List<bool> checkBox = new List<bool> ();
	List<string> checkStr = new List<string> ();

	int[] LrigType = new int[2]{-1,-1};
	int[] LrigType2 = new int[2]{-1,-1};
	int[] LrigLevel = new int[2]{-1,-1};
//	int[] LevelLimit = new int[2]{0,0};
	bool connectButtun = true;
	int GoTrashID = -1;
	bool selectAttackAtrs = false;
	int attackAtrsPlayer = -1;
	int attackClickID = -1;
	bool receiveFlag = false;
	bool artsAsk = false;
	int HandDeathID = -1;

    int[,] SigniNotSummonCount = new int[2, 3];

    int[,] SigniPowerUpValue = new int[2, 3];
	int[,] SigniPowerUpValueBuf = new int[2, 3];
	List<int> RebornList = new List<int> ();

    class abilitySet {
        public int receiver = -1;
        public int giver = -1;
        public ability _ability;

        public abilitySet(int receiverID, int giverID, ability a)
        {
            receiver = receiverID;
            giver = giverID;
            _ability = a;
        }
    }
    List<abilitySet> enAbilityList = new List<abilitySet>();

    List<int> LostEffectIDList = new List<int>();

    List<powerChange> AddIgniList = new List<powerChange>();


	List<int> ReposiID = new List<int>();
	bool ReposiFlag=false;
	bool[] Repositioned=new bool[3];

	bool effectInitialized=false;
    bool systemCardInitialized = false;


    //１フレーム系　統一化の際見栄えが悪くなった
    Dictionary<OneFrameIDType, int> OneFrameIDs = new Dictionary<OneFrameIDType, int>();
    int TrashSummonID { get { return getOneFrameID(OneFrameIDType.TrashSummonID); } set { setOneFrameID(OneFrameIDType.TrashSummonID, value); } }

    int IgnitionUpID { get { return getOneFrameID(OneFrameIDType.IgnitionUpID); } set { setOneFrameID(OneFrameIDType.IgnitionUpID, value); } }
    int TraIgniUpID { get { return getOneFrameID(OneFrameIDType.TraIgniUpID); } set { setOneFrameID(OneFrameIDType.TraIgniUpID, value); } }
    int IgniEffID = -1;//1フレちがう

    bool turnEndFlag = false;
    bool turnStartFlag = false;

    int BanishedID { get { return getOneFrameID(OneFrameIDType.BanishedID); } set { setOneFrameID(OneFrameIDType.BanishedID, value); } }
    int BanishingID { get { return getOneFrameID(OneFrameIDType.BanishingID); } set { setOneFrameID(OneFrameIDType.BanishingID, value); } }

    int[] crossedIDs = new int[] { -1, -1 };
    public int[] CrossedIDs { get { return crossedIDs; } }

    int removingID { get { return getOneFrameID(OneFrameIDType.removingID); } set { setOneFrameID(OneFrameIDType.removingID, value); } }
    public int RemovingID { get { return removingID; } }

    int crashedID { get { return getOneFrameID(OneFrameIDType.crashedID); } set { setOneFrameID(OneFrameIDType.crashedID, value); } }
    public int CrashedID { get { return crashedID; } }
    bool lancerCrashed = false;
    public int LancerCrashedID
    {
        get
        {
            if (lancerCrashed)
                return crashedID;
            return -1;
        }
    }

    int crasherID = -1;//１フレ違う
    public int CrasherID { get { return crasherID; } }

    int effectDownedID { get { return getOneFrameID(OneFrameIDType.effectDownedID); } set { setOneFrameID(OneFrameIDType.effectDownedID, value); } }
    public int EffectDownedID { get { return effectDownedID; } }

    int downID { get { return getOneFrameID(OneFrameIDType.downID); } set { setOneFrameID(OneFrameIDType.downID, value); } }
    public int DownID { get { return downID; } }

    int stopAttackedID { get { return getOneFrameID(OneFrameIDType.stopAttackedID); } set { setOneFrameID(OneFrameIDType.stopAttackedID, value); } }
    public int StopAttackedID
    {
        get
        {
            return stopAttackedID;
        }
    }

    int exitID { get { return getOneFrameID(OneFrameIDType.exitID); } set { setOneFrameID(OneFrameIDType.exitID, value); } }
    Fields exitField = Fields.Non;

    int EffectGoTrashID { get { return getOneFrameID(OneFrameIDType.EffectGoTrashID); } set { setOneFrameID(OneFrameIDType.EffectGoTrashID, value); } }
    int ChantSpellID { get { return getOneFrameID(OneFrameIDType.ChantSpellID); } set { setOneFrameID(OneFrameIDType.ChantSpellID, value); } }
    int ChantArtsID { get { return getOneFrameID(OneFrameIDType.ChantArtsID); } set { setOneFrameID(OneFrameIDType.ChantArtsID, value); } }

    int TargetNowID { get { return getOneFrameID(OneFrameIDType.TargetNowID); } set { setOneFrameID(OneFrameIDType.TargetNowID, value); } }
	int EffecterNowID= -1;//１フレちがう


    int CounterID { get { return getOneFrameID(OneFrameIDType.CounterID); } set { setOneFrameID(OneFrameIDType.CounterID, value); } }

    int cipID { get { return getOneFrameID(OneFrameIDType.cipID); } set { setOneFrameID(OneFrameIDType.cipID, value); } }
    int notCipID = -1;//１フレちがう

    int BurstEffectID { get { return getOneFrameID(OneFrameIDType.BurstEffectID); } set { setOneFrameID(OneFrameIDType.BurstEffectID, value); } }
	int BurstEffectIDBuf = -1;//１フレちがう

	bool BurstUpFlag = false;
	bool LifeAddFlag = false;
	bool[] costDownResetFlag = new bool[2];

    int AttackerID { get { return getOneFrameID(OneFrameIDType.AttackerID); } set { setOneFrameID(OneFrameIDType.AttackerID, value); } }
    int AttackFrontRank = -1;//１フレちがう

    int[] BattledID = new int[2] { -1, -1 };
    int[] BattleFinishID = new int[2] { -1, -1 };
    int[] tellBattleFinishID = new int[2] { -1, -1 };

    bool isCrossExited = false;

    //まで

	int[,,] spellCostDown = new int[2, 6, 2]; // [player, color, spell or arts]
	bool[] spellCostDownFlag = new bool[2];
	
	GameObject targetShowCursor = null;
	List<int> targetShowList = new List<int> ();
	GameObject MessageWindow = null;
	bool DebugFlag = true;//choice debug mode
	
	int animationCount = 0;
	int animationID = -1;

	bool DuelEndFlag = false;
	bool goEndPhaseFlag = false;
	bool StopAttackFlag=false;

	int tellEffectID=-1;
	int ViolenceCount=0;

	int PosiChangeID= -1;

	bool[] NextTurnAtrsLimFlag=new bool[2];
	bool[] ArtsLimitFlag=new bool[2];

	bool ImmortalFlag=false;

	bool toldBanishing=false;
    bool toldRemoving = false;

	int effectShortageID = -1;

	bool toldPayedCost = false;

	int HandOpenCount=0;

	List<NewCard> NewCardList = new List<NewCard>();//ミルルンの残骸
	bool NewCardEffectFlag=false;
	int NewCardEffecterCount=-1;
    bool NewCardCreated = false;

	int[] myTime=new int[2]{60,60};
	const int myStandardtime=60;
	int[] myFrameTime=new int[2];
	int thinkingPlayer=0;
	bool thinkChangeFlag=false;

	List<string> receivedList=new List<string>();
	List<string> sentList=new List<string>();

	bool replayMode=false;
	string[] replayList;
	string replayName="";
	int receivedIndex=0;
	int sentIndex=0;
	int readTargetPlayer=-1;//読み出し先
	int attackThinkPlayer=-1;

//	bool replaySaveFlag=true;
//	bool canReplaySend=true;

	bool playerDissconnected=false;

	List<powerChange> powChanList=new List<powerChange>();//常時効果による打点増減を保存

	bool[] MirrorMirageFlag=new bool[2];

	bool[] NowLoading=new bool[2];

    int trueIgnitionID = -1;
    bool trueIgniSeted = false;

    List<Motions> LastMotions = new List<Motions>();

    bool GUIcancel = false;
    bool GUIcancelEnabel = false;

    bool notBurst = false;

    List<List<int>> underCards = new List<List<int>>();
    int risingID = -1;

    int ExtraTurnCount = 0;
    int NextExtraTurnCount = 0;

    bool[] DontGrowFlag = new bool[2];
    bool[] NextDontGrowFlag = new bool[2];

    int[] HandSummonMaxLim = new int[2] { -1, -1 };
    bool[] JerashiGeizuFlag = new bool[2];

    bool GoNextTrunFlag = false;

    bool goNextPhaseFlag = false;
    int startedPhase = -1;

    List<powerChange> TopGoTrashList = new List<powerChange>();
    List<powerChange> costGoTrashIDList = new List<powerChange>();
    List<IgniCostDecrease> IgniCostDecList = new List<IgniCostDecrease>();

    bool toldChantEffFlag = false;

    int[] GridCount = new int[2] { 0, 0 };
    bool apllyGrid = false;

    int TraIgniSelectedID = -1;

    bool SigniAttackSkipFlag = false;
    bool LrigAttackSkipFlag = false;

    GameObject beforGame;
    GameObject canvasObj;
    GameObject panelObj;
    GameObject YesNoObj;
    bool YesNoGUIFlag = false;
    bool uiYes=false;
    bool uiNo = false;
    int oldAttackArtsP = 0;

    UnityEngine.UI.Text showCardText;
    UnityEngine.UI.RawImage showCardImage;

    System.Func<int, bool> SystemCardInputReturnFunc;

    List<int> exitedList = new List<int>();
    bool dualEffectTriggerd = false;

    int targetLevelSum = -1;
    System.Func<int,int,bool> targetLevelSumCheck = null;

    int powerSum = 0;

    List<int> changeBasedList = new List<int>();
    List<int> TempResonaList = new List<int>();


    int[] nextMinusLimitCount = new int[] { 0, 0 };
    int[] MinusLimitCount = new int[] { 0, 0 };

    List<powerChange> effectExecuteList = new List<powerChange>();

    int oneHandDeathSelecter = -1;

    List<string> usedList = new List<string>();

    bool[] notDamagingFlag = new bool[2];

    List<zonePowrUp> zonePowerList = new List<zonePowrUp>();

    List<int> colorChangeIDs = new List<int>();//エナ以外の色を変更するカードのIDのリスト
    List<cardColorInfo> colorChangeColor = new List<cardColorInfo>();//対応するカラーのリスト

    cardstatus[,] cardStatusBuf = new cardstatus[2, 50];
	//最後尾

	//通信
	public GameObject NetworkManager;
	private networkScript NetScr;
	private static string  connectionIP = "127.0.0.1";
	private const string  roopIP = "127.0.0.1";
	private const string  connectionKey = "connectIP";
	private const int     portNumber = 56777;
	private bool    connected = false;
	public bool wasServerAndClient=false;

	// メッセージを管理するリスト
	private List<string> messages = new List<string> ();
	private List<string> messagesBuf = new List<string> ();
	private List<string> shuffleBuf = new List<string> ();
	private bool Sender = false;
	private bool isServer = false;

	//public zone
	public bool cipCheck = false;
	public int UnblockLevel = -1;
    public bool[] enajeFlag = new bool[2] { false, false };
	public bool sheilaFlag = false;
	public bool[] ionaFlag = new bool[2]{false,false};

    bool antiFlag = false;
    bool counterSpellFlag = false;

	public bool stopFlag = false;
    public bool[] EnaPhaseSkip = new bool[2] { false, false };
    public bool[] GrowPhaseSkip = new bool[2] { false, false };
	public bool AttackPhaseSkip = false;
	public bool NoCostGrow = false;
	public bool notCrashFlag = false;
	public bool[,] signiRockFlag = new bool[2, 3];
	public bool FafnirFlag = false;
    public bool LrigAttackNow = false;
    public int[] normalDrawNum = new int[2] { 2, 2 };
    public bool[] ACGFlag = new bool[2];
    public bool[] CMRFlag = new bool[2];
    public bool[] Rian_pow_Flag = new bool[2];
    public bool[] Rian_eff_Flag = new bool[2];
    public bool[] kyomuFlag = new bool[2];
    public bool[] mirutychoFlag = new bool[2];//スペル発動不可
    public bool[] nickelFlag = new bool[2];

    public bool[] melhenFlag = new bool[2];
    bool melhenUpFlag = false;

	public bool requipFlag=false;
	public bool requipUpFlag=false;//次フレームでrequipを立てるフラグ

    public int[] signiSumLimit = new int[2] { 3, 3 };
    int[] signiSumLimitChangedID = new int[2] { -1, -1 };


    //funcs
    public int getOneFrameID(OneFrameIDType _type)
    {
        if (!OneFrameIDs.ContainsKey(_type))
            return -1;

        return OneFrameIDs[_type];
    }
    void setOneFrameID(OneFrameIDType _type, int _id)
    {
        if (!OneFrameIDs.ContainsKey(_type))
            OneFrameIDs.Add(_type, -1);

        OneFrameIDs[_type] = _id;
    }

    public CardScript getSystemScr(int myID, int myPlayer)//できるだけ使わないように
    {
        return SystemCardScr;
    }

    public void setColorChangeIDs(int ID, int player, cardColorInfo color)
    {
        if (colorChangeIDs.Contains(getFusionID(ID, player)))
            return;

        colorChangeIDs.Add(getFusionID(ID, player));
        colorChangeColor.Add(color);
    }

    public int getStartedPhase()
    {
        return startedPhase;
    }

    public bool checkIsCrossExited()
    {
        return isCrossExited;
    }

    public bool isUsedThis(int ID, int player)
    {
        for (int i = 0; i < usedList.Count; i++)
        {
            if (checkName(ID, player, usedList[i]))
                return true;
        }

        return false;
    }

    public void upAlwaysFlag(alwysEffs eff, int target, int ID, int player)
    {
        CardScript sc = getCardScr(ID,player);
        if (sc != null && sc.isOnBattleField() && !sc.lostEffect)
        {
            AlwysEffFlags.flagUp(eff, target, ID + 50 * player);
            return;
        }

        if(AlwysEffFlags.getUpperID(eff, target) == getFusionID(ID,player))
            AlwysEffFlags.flagDown(eff, target);
    }

    public bool isSystemTargetIDCountZero()
    {
        return SystemCardScr.effectTargetID.Count == 0;
    }

    public bool isTargetIDCountZero(int ID, int player)
    {//正直わかんない
/*        if (SystemEffectFlag)
            return SystemCardScr.effectTargetID.Count == 0;
//        else if (EffecterNowID == -1)
//            return true;

//        return getCardScr(EffecterNowID).effectTargetID.Count == 0;*/
        return getCardScr(ID,player).effectTargetID.Count == 0;
    }

    public int getDeckTopID(int player)
    {
        int id = fieldRankID(Fields.MAINDECK, deckNum[player % 2] - 1, player);
        return id;
    }

    public int getShowZoneID(int index)
    {
        if (ShowZoneIDList.Count <= index)
            return -1;

        return ShowZoneIDList[index];
    }

    public int getShowZoneID(int index, bool isFromBack)
    {
        if (!isFromBack)
            return getShowZoneID(index);

        int k = ShowZoneIDList.Count - 1 - index;
        if (k < 0)
            return -1;

        return getShowZoneID(k);
    }

    public int getTopGoTrashListID(int index, int changerID,int chanerPlayer)
    {
        if (TopGoTrashList.Count <= index)
            return -1;

        if(TopGoTrashList[index].changerID != changerID + 50*chanerPlayer)
            return -2;

        return TopGoTrashList[index].changedID;
    }

    public int getCostGoTrashID(int changerID, int chanerPlayer)
    {
        for (int index = 0; index < costGoTrashIDList.Count; index++)
        {
            if (costGoTrashIDList[index].changerID == changerID + 50 * chanerPlayer)
            {
                int x = costGoTrashIDList[index].changedID;
                costGoTrashIDList.RemoveAt(index);
                return x;
            }          
        }

        return -1;
    }

    public int getEffectExecutedID(int changerID, int chanerPlayer, Motions m)
    {
        for (int index = 0; index <effectExecuteList.Count; index++)
        {
            if (effectExecuteList[index].changerID == changerID + 50 * chanerPlayer && effectExecuteList[index].changeValue==(int)m)
            {
                int x = effectExecuteList[index].changedID;
                effectExecuteList.RemoveAt(index);
                return x;
            }
        }

        return -1;
    }

    public int getLastMotions(int index)
    {
        if (LastMotions.Count <= index)
            return (int)Motions.NonMotion;

        return (int)LastMotions[index];
    }

    public int getLastMotionsRear()
    {
        int index = LastMotions.Count - 1;

        if (index < 0)
            return (int)Motions.NonMotion;

        return (int)LastMotions[index];
    }

	public int[] getChangedArray(int ID,int player){
		List<int> list=new List<int>();

		for (int i = 0; i < powChanList.Count; i++)
		{
			if(ID + 50*player == powChanList[i].changerID) 
				list.Add(powChanList[i].changedID);

		}

		int[] array=new int[list.Count];

		for (int i = 0; i < list.Count; i++)
		{
			array[i]=list[i];
		}

		return array;
	}

    public void powChanListChangerClear(int changerFusionID)
    {
        powChanListChangerClear(changerFusionID % 50, changerFusionID / 50);
    }
	public void powChanListChangerClear(int changerID, int changerPlayer)
	{
		for (int i = 0; i < powChanList.Count; i++)
		{
			if(powChanList[i].changerID == changerID + 50*changerPlayer){
				powChanListRemove(i);
				i--;
			}
		}
	}

    public void powChanListPlayerClear(int player)
    {
        for (int i = 0; i < powChanList.Count; i++)
        {
            if (powChanList[i].changerID /50 == player)
            {
                powChanListRemove(i);
                i--;
            }
        }
    }

	public bool getIsTrans ()
	{
		return isTrans;
	}

	public int getPhaseInt ()
	{
        if (GoNextTrunFlag)
            return (int)Phases.EndPhase;

		return (int)phase;
	}

	public GameObject getCardObj (int ID, int player)
	{
		return card [player, ID];
	}
	
	public GameObject getFront (int ID, int player)
	{
		return front [player, ID];
	}

    public CardScript getCardScr(int ID, int player)
    {
        if (ID < 0)
            return null;
        return card[player, ID].GetComponent<CardScript>();
    }
    public CardScript getCardScr(int fusionID)
    {
        if (fusionID < 0 || fusionID>=100)
            return null;

        int ID = fusionID % 50;
        int player = fusionID / 50;

        return getCardScr(ID,player);
    }


	public int getFieldInt (int ID, int player)
	{
		if(ID>=100)
		{
			return (int)NewCardList[ID-100].MyField;
		}

		return (int)field [player, ID];
	}

	public int getRank (int ID, int player)
	{
		return fieldRank [player, ID];
	}

	public int getEnaColorNum (int color, int player)
	{
		return enaColorNum[player, color];
	}

    public int getExitID(int from, int to)
    {
        if (exitID >= 0 && (from < 0 || from == (int)exitField) && (to < 0 || to == (int)field[exitID / 50, exitID % 50]))
            return exitID;

        return -1;
    }

    public int getExitID(Fields from, Fields to)
    {
        return getExitID((int)from, (int)to);
    }
    public int getCipSigniID()
    {
        return getExitID(-1, (int)Fields.SIGNIZONE);
    }

	void enaColorNumUpdate()
	{
		//初期化
		for (int j = 0; j < enaColorNum.GetLength(1); j++)
		{
			enaColorNum[0,j]=0;
			enaColorNum[1,j]=0;
		}

		//カウント
		for (int j = 0; j < 2; j++)
		{
			int player=j;
			int num=fieldAllNum(Fields.ENAZONE, player);

			for (int i = 0; i < num; i++)
			{
				int id=fieldRankID(Fields.ENAZONE,i,player);
				
				enaColorNum[player,getCardColor(id,player)]++;
			}
		}
	}

    public int getSigniFront(int ID, int player)
    {
        if (field[player, ID] != Fields.SIGNIZONE)
            return -1;

        int rank = fieldRank[player, ID];
        rank = 1 - (rank - 1);

        int x = fieldRankID(Fields.SIGNIZONE, rank, 1 - player);

        return x;
    }

	public int getSigniConditionInt (int rank, int player)
	{
		return (int)signiCondition [player, rank];
	}

    public int getLrigConditionInt(int player)
    {
        return (int)LrigCondition[player];
    }

	public int getIDConditionInt (int ID, int player)
	{
        if (trueIgnitionID != -1)
        {
            if(field[trueIgnitionID/50,trueIgnitionID%50]== Fields.SIGNIZONE)
                return (int)signiCondition[player, fieldRank[trueIgnitionID/50,trueIgnitionID%50]];

            if (field[trueIgnitionID / 50, trueIgnitionID % 50] == Fields.LRIGZONE)
                return (int)LrigCondition[trueIgnitionID / 50];
        }

		int rank = -1;
		for (int i=0; i<3; i++)
		{
            int x=fieldRankID (Fields.SIGNIZONE, i, player);
            if (x == ID)
            {
                rank = i;
                return (int)signiCondition[player, rank];
            }
    	}

		if (ID == getLrigID (player))
			return (int)LrigCondition [player];		

		return -1;
	}

	public int getLrigID (int player)
	{
		int rank = fieldAllNum (Fields.LRIGZONE, player) - 1;
		return fieldRankID (Fields.LRIGZONE, rank, player);
	}

	public int getLrigType (int player)
	{
		int id = getLrigID (player);
		if (id < 0)
			return -1;
		CardScript sc = getCardScr (id, player);
		return sc.LrigType;
	}

    public int getLrigLevelLimit(int player)
    {
        int id = getLrigID(player);
        if (id < 0)
            return -1;
        CardScript sc = getCardScr(id, player);
        return sc.Limit-MinusLimitCount[player];
    }

    public int getLrigColor(int player)
	{
		int id = getLrigID (player);
		if (id < 0)
			return -1;
		CardScript sc = getCardScr (id, player);
		return sc.CardColor;
	}

	public int getLrigLevel (int player)
	{
		int id = getLrigID (player);
		if (id < 0)
			return -1;
		CardScript sc = getCardScr (id, player);
		return sc.Level;
	}
	
	public int getFieldRankID (int f, int rank, int player)
	{
		Fields fld = (Fields)f;
		return fieldRankID (fld, rank, player);
	}

	public int getFieldAllNum (int f, int player)
	{
		Fields fld = (Fields)f;
		return fieldAllNum (fld, player);
	}

	public int getCharmNum(int player)
	{
		int num=0;

		for (int i = 0; i < 3; i++)
		{
			int x=fieldRankID(Fields.CharmZone,i,player);

			if(x>=0)
				num++;
		}
		return num;
	}

	public int getCostSum (int ID, int player)
	{
		int num = 0;
		CardScript sc = getCardScr (ID, player);

		for (int i=0; i<sc.Cost.Length(); i++)
		{
			num += sc.Cost [i];
		}
		return num;
	}

	public string getSerialNum (int ID, int player)
	{
		if (ID < 0)
			return "";
		return SerialNumString [player, ID];
	}

	public int getCardPower (int ID, int player)
	{
		if (ID < 0)
			return -1;
		return card [player, ID].GetComponent<CardScript> ().Power;
	}

	public int getCardColor (int x, int target)
	{
		if (x < 0)
			return -1;

        if (field[target, x] != Fields.ENAZONE)
        {
            for (int i = 0; i < colorChangeIDs.Count; i++)
            {
                int index = colorChangeIDs.Count - 1 - i;
                int _id=colorChangeIDs[index] % 50;
                int _player=colorChangeIDs[index] / 50;

                if(!checkResistance(x,target,_id,_player))
                    return (int)colorChangeColor[index];
            }
        }

		return card [target, x].GetComponent<CardScript> ().CardColor;
	}

	public int getSigniColor (int ID, int player)
	{
		if (ID < 0 || getCardType (ID, player) != 2)
			return -1;
		return card [player, ID].GetComponent<CardScript> ().CardColor;
	}

	public int getSpellColor (int ID, int player)
	{
		if (ID < 0 || getCardType (ID, player) != 3)
			return -1;
		return card [player, ID].GetComponent<CardScript> ().CardColor;
	}

	public int getCardType (int ID, int player)
	{
		if (ID < 0)
			return -1;
        else if (ID >= 100)
            return getCardType(NewCardList[ID - 100].TrueID,NewCardList[ID - 100].TruePlayer);

        return card[player, ID].GetComponent<CardScript>().Type;
	}

    public int getCardLevel(int fusionID)
    {
        return getCardLevel(fusionID % 50, fusionID / 50);
    }
	public int getCardLevel (int ID, int player)
	{
		if (ID < 0)
			return -1;
		return card [player, ID].GetComponent<CardScript> ().Level;
	}

 
	public int[] getCardClass (int ID, int player)
	{
		if (ID < 0)
			return new int[2]{-1,-1};
		CardScript sc = getCardScr (ID, player);
		return new int[2]{sc.Class_1,sc.Class_2};
	}

    public bool checkController(int fID, int controller)
    {
        return fID / 50 == controller;
    }

    
    public bool checkCross(int ID, int player)
    {
        var com = getCardScr(ID, player);

        if (com == null)
            return false;

        return com.isCrossing();
    }

    public bool checkAbility(int x, int target, ability a)
    {
        if (x < 0)
            return false;
        return getCardScr(x, target).checkAbility(a);
    }


    public bool checkClass(int fusionID, cardClassInfo info)
    {
         return checkClass(fusionID % 50, fusionID / 50, info);
    }
    public bool checkClass(int x, int target,cardClassInfo clss)
    {
        if (x < 0)
            return false;

        CardScript sc = getCardScr(x, target);

        if(sc == null)
            return false;
        
        foreach (var info in cuttingClassInfo(clss))
        {
            if ((sc.secondClass_1 == (int)info / 10 && sc.secondClass_2 == (int)info % 10) 
            || ( sc.Class_1 == (int)info / 10 && sc.Class_2 == (int)info % 10)) 
                return true;

/*            int[] c = getCardClass(x, target);
            if (c[0] == (int)info / 10 && c[1] == (int)info % 10)
                return true;*/
        }

        return false;
    }

    List<cardClassInfo> cuttingClassInfo(cardClassInfo info)
    {
        int _info = (int)info;
        List<cardClassInfo> list = new List<cardClassInfo>();

        //負数の場合
        if (info == cardClassInfo.精元)
        {
            list.Add(cardClassInfo.精元);
            return list;
        }

        //それ以外
        while (_info > 0)
        {
            list.Add((cardClassInfo)(_info % 100));
            _info /= 100;
        }

        return list;
    }

    public int getClassNum(int target, Fields f, cardClassInfo info)
    {
        int count = 0;
        int num =getNumForCard((int)f,target);

        for (int i = 0; i < num; i++)
        {
            int x = fieldRankID(f, i, target);
            if (checkClass(x, target, info))
                count++;
        }

        return count;
    }

    public int getFreezeNum(int target)
    {
        return getFuncNum(target, Fields.SIGNIZONE, checkFreeze);
    }

    public int getFuncNum(int target, Fields f, System.Func<int, int, bool> check)
    {
        int count = 0;
        int num = fieldAllNum(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = fieldRankID(f, i, target);
            if (check(x,target))
                count++;
        }

        return count;
    }

    public bool checkBurstIcon(int x, int target)
    {
        CardScript sc= getCardScr(x, target);

        if (sc == null || sc.BurstIcon != 1)
            return false;

        return true;
    }

    public bool checkLrigSameColor(int x,int target)
    {
        return getCardColor(x,target) == getLrigColor(target);
    }

    public bool checkLrigColor(int target, cardColorInfo info)
    {
        return getLrigColor(target) == (int)info;
    }

    public bool checkColorType(int x, int target, cardColorInfo color, cardTypeInfo type)
    {
        return checkColor(x, target, color) && checkType(x, target, type);
    }

    public bool checkColor(int x,int target, cardColorInfo info)
    {
        return getCardColor(x,target) == (int)info;
    }

    public bool checkType(int x,int target, cardTypeInfo info)
    {
        return getCardType(x,target) == (int)info;
    }

    public bool checkType(int fusionID, cardTypeInfo info)
    {
        return checkType(fusionID%50, fusionID/50,info);
    }

    public bool checkFreeze(int x, int target)
    {
        var com = getCardScr(x, target);

        if (com == null)
            return false;

        return com.Freeze;
    }

    public bool checkContainsName(int x, int target, string key)
    {
        CardScript sc = getCardScr(x, target);
        if (sc == null) 
            return false;

        return sc.Name.Contains(key);
    }

    public bool checkName(int fusionID, string key)
    {
        return checkName(fusionID % 50, fusionID / 50, key);
    }
    public bool checkName(int x, int target, string key)
    {
        CardScript sc = getCardScr(x, target);
        if (sc == null)
            return false;

        System.Globalization.CompareInfo ci =
           System.Globalization.CultureInfo.CurrentCulture.CompareInfo;

        return ci.IndexOf(sc.Name, key,
            System.Globalization.CompareOptions.IgnoreWidth |
            System.Globalization.CompareOptions.IgnoreKanaType | System.Globalization.CompareOptions.IgnoreCase) >= 0;
//        return sc.Name == key;
    }

    public void changeLostEffect(int x, int target, bool change, int ID, int player, bool isEndClear=false)
    {
        if (checkResistance(x, target, ID, player))
            return;

        CardScript sc=getCardScr(x, target);

        if (sc.lostEffect != change)
        {
            if (change == true)
            {
                powChanListChangerClear(x, target);
                sc.AbilityClear();

                if (isEndClear)
                    LostEffectIDList.Add(getFusionID(x, target));
            }

            sc.lostEffect = change;
        }
    }

	//常時効果用
	public bool alwaysChagePower (int x, int target, int up, int ID ,int player)
	{
        return alwaysChagePower(x, target, up, ID, player, null);
	}

    public bool alwaysChagePower(int x, int target, int up, int ID, int player, System.Func<int, int, bool> check, bool isZoneTarget = false)
    {

        if (Rian_pow_Flag[1 - player] || checkResistance(x, target, ID, player))
            return false;

        CardScript sc = card[target, x].GetComponent<CardScript>();
        CardScript effecterScr = card[player, ID].GetComponent<CardScript>();

        if (effecterScr.lostEffect && !isZoneTarget)//zoneを対象としたダウンは効果を失っても続く
            return false;

        //violence
        for (int i = 0; i < ViolenceCount && up < 0; i++)
            up *= 2;

        sc.changePower += up;

        powChanList.Add(new powerChange(x + 50 * target, ID + 50 * player, up, check));

        return true;
    }

    public bool checkChanListExist(int x, int target,int ID,int player)
    {
        int[] array = getChangedArray(ID, player);

        for (int i = 0; i < array.Length; i++)
        {
            if (x + 50 * target == array[i])
                return true;
        }

        return false;
    }

	//発動する効果とかその他
    public void upCardPower (int ID, int player, int up)
	{
        CardScript sc = card[player, ID].GetComponent<CardScript>();
		sc.changePower += up;
	}

    //ベースパワーの変更
    public void changeBasePower(int ID, int player, int up)
    {
        CardScript sc = card[player, ID].GetComponent<CardScript>();

        if (sc.lostEffect)
            sc.basePower = sc.OriginalPower;
        else
            sc.basePower = up;
    }


	//常時効果の色変え
	public bool alwaysChangeColor(int ID, int player, int color, int effecter ,int effectPlayer)
	{
		//resistは立てるときだけ
		if(checkResistance(ID,player,effecter,effectPlayer))
			return false;

		CardScript sc = getCardScr(ID,player);
		sc.CardColor = color;

		return true;
	}

	//常時効果の色戻し
	public bool alwaysReturnColor(int ID, int player, int effecter ,int effectPlayer)
	{
		CardScript sc = getCardScr(ID,player);
		sc.CardColor = sc.OrigColor;
		
		return true;
	}

	//常時効果 resistance
	public bool alwaysChangeResiLrig(int ID, int player, bool flag, int effecter ,int effectPlayer)
	{
		//resistは立てるときだけ
		if(checkResistance(ID,player,effecter,effectPlayer) && flag)
			return false;

		CardScript sc = getCardScr(ID,player);
		
		sc.resiLrigEffect = flag;
		
		return true;
	}

	//耐性の有無
	bool checkResistance(int ID, int player, int effecter ,int effectPlayer)
	{
        return CheckEffectResist(ID, player, effecter, effectPlayer);//effectと併合
	}


    public bool checkHavingCross(int x, int target)
    {
        var com = getCardScr(x, target);

        if (com == null)
            return false;

        return com.havingCross;
    }


	public int getTurnPlayer ()
	{
		return (turn + firstAttack + 1 - ExtraTurnCount) % 2;
	}

	public int getTurn ()
	{
		return turn;
	}
	
	public bool checkCost (int ID, int player)
	{
/*		if (ID < 0)
			return false;
		CardScript sc = getCardScr (ID, player);
		int multi = MultiEnaNum (player);
		int costSum = 0;
		
		for (int i=0; i<enaColorNum.GetLength(1); i++)
		{
            int hosei = IgniCostDecSum(ID, player, i);
            int trueCost = sc.Cost[i] - hosei;
			
            if(trueCost>0)
                costSum += trueCost;

			if (i != 0 && trueCost > enaColorNum [player, i])
				multi -= trueCost - enaColorNum [player, i];
		}
		
		if (multi < 0 || costSum > enaNum [player])
			return false;
		return true;*/

        return checkModeCost(ID, player, false);
	}

    //色代用効果に対応するべくアルゴリズムを変更したver.
    public bool checkCost2nd(int player, List<int> costColorList, List<int> payedCardList,bool isMelhenGrow)
    {
        if (costColorList.Count == 0)
            return true;

        if(payedCardList==null)
            payedCardList=new List<int>();

        cardColorInfo color = (cardColorInfo)costColorList[costColorList.Count - 1];
        costColorList.RemoveAt(costColorList.Count - 1);

        List<int> subCostColorList=new List<int>(costColorList);
        List<int> subPayedCardList=new List<int>(payedCardList);

        int num = fieldAllNum(Fields.ENAZONE, player);
        bool flag = false;
        for (int i = 0; i < num; i++)
        {
            int x = fieldRankID(Fields.ENAZONE, i, player);
            int fID=x + 50 * player;
            if (!checkPayingTarget(color, fID, isMelhenGrow))
                continue;

            subPayedCardList.Add(fID);
            if (!payedCardList.Contains(fID) && checkCost2nd(player, subCostColorList, subPayedCardList,isMelhenGrow))
            {
                payedCardList.Add(fID);
                flag = true;
                break;
            }
        }

        if (!flag)
            return false;

        return checkCost2nd(player, costColorList, payedCardList, isMelhenGrow);
    }

    bool checkPayingTarget(cardColorInfo info, int fusionID, bool isMelhenGrow)
    {
        CardScript sc = getCardScr(fusionID);
        if (info == cardColorInfo.無色
            || sc.MultiEnaFlag
            || info == cardColorInfo.白 && checkClass(fusionID, cardClassInfo.精像_美巧) && (melhenFlag[fusionID/50] || isMelhenGrow || sc.WhiteEnaFlag))
            return true;

        return checkColor(fusionID % 50, fusionID / 50, info);
    }

    List<int> creatCostColorList(int ID,int player, colorCostArry array)
    {
        List<int> list = new List<int>();

        for (int i = 0;true; i--)
        {
            int hosei = IgniCostDecSum(ID, player, i);
            for (int k = 0; k < array.getCost(i) - hosei; k++)
                list.Add(i);

            if (i == 0)
                i = array.Length();

            if (i == 1)
                break;
        }
        return list;
    }

    public bool checkModeCost(int ID, int player,bool isGrow)
    {
         if (ID < 0)
            return false;
        CardScript sc = getCardScr(ID, player);
        if (isGrow)
            return checkCost2nd(player, creatCostColorList(ID, player, sc.GrowCost), null, sc.MelhenGrowFlag && isGrow);
        else
            return checkCost2nd(player, creatCostColorList(ID, player, sc.Cost), null, sc.MelhenGrowFlag && isGrow);

/*        int multi = MultiEnaNum(player);
        int costSum = 0;

        int bikou = 0;
        if (melhenFlag[player] || (sc.MelhenGrowFlag && isGrow))
            bikou = BikouEnaNum(player);


        colorCostArry costArry;

        if (isGrow)
            costArry =copyCostArray( sc.GrowCost);
        else
            costArry =copyCostArray( sc.Cost);

        for (int i = 0; i < enaColorNum.GetLength(1); i++)
        {
            int hosei = IgniCostDecSum(ID, player, i);
            int trueCost = costArry.getCost(i) - hosei;

            if (trueCost > 0)
                costSum += trueCost;

            if (i != 0 && trueCost > enaColorNum[player, i])
            {
                int k=trueCost - enaColorNum[player, i];
                if (i == (int)cardColorInfo.白)
                    bikou -= k;
                else 
                    multi -= k;
            }
        }

        if (bikou < 0)
            multi += bikou;

        if (multi < 0 || costSum > enaNum[player])
            return false;
        return true;*/
    }
	
	public void setSpellCostDown (int color, int num, int player, int SpellOrArts)
	{
		spellCostDown [player, color, SpellOrArts] += num;
	}

    public bool getTurnStartFlag()
    {
        return turnStartFlag;
    }

    public bool getTurnEndFlag()
    {
        return turnEndFlag;
    }

    public int getTrashSummonID()
    {
        return TrashSummonID;
    }
	
	public int getIgnitionUpID ()
	{
		return IgnitionUpID;
	}

	public int getBanishedID ()
	{
		return BanishedID;
	}

	public int getBanishingID ()
	{
		return BanishingID;
	}

	public int getEffectGoTrashID ()
	{
		return EffectGoTrashID;
	}

	public int getSigToTraID ()
	{
        return getExitID(Fields.SIGNIZONE, Fields.TRASH);
	}

	public int getWentTrashID ()
	{
        return getExitID(-1, (int)Fields.TRASH);
	}

	public int getWentTrashFrom ()
	{
		return (int)exitField;
	}

	public int getAttackerID ()
	{
		return AttackerID;
	}

	public int getBattledID (int player)
	{
		return BattledID[player];
	}

    public int getBattleFinishID(int player)
    {
        return BattleFinishID[player];
    }

	public int getAttackFrontRank ()
	{
		return AttackFrontRank;
	}

	public int getChantSpellID ()
	{
		return ChantSpellID;
	}

    public int getChantArtsID()
    {
        return ChantArtsID;
    }

	public int getEffecterNowID ()
	{
        if (trueIgnitionID >= 0)
            return trueIgnitionID;

		return EffecterNowID;
	}

	public int getTargetNowID ()
	{
		return TargetNowID;
	}

	public int getCipID ()
	{
		return cipID;
	}
	
	public int getCounterID ()
	{
		int id = CounterID;
		CounterID = -1;
		return id;
	}

	public int getBurstEffectID ()
	{
		return BurstEffectID;
	}

	public bool getLifeAddFlag ()
	{
		return LifeAddFlag;
	}

	public bool getCostDownResetFlag (int SpellOrArts)
	{
		return costDownResetFlag[SpellOrArts];
	}

	public int getNumForCard (int f, int player)
	{
		if (f == 3)
			return 3;
		else
			return getFieldAllNum (f, player);
	}
	
	public bool getStartedFlag ()
	{
		return startedFlag;
	}
	
	public bool isColorSigni (int color, int player)
	{
		for (int i=0; i<3; i++)
		{
			int id = fieldRankID (Fields.SIGNIZONE, i, player);
			if (id >= 0 && color == getCardColor (id, player))
				return true;
		}
		return false;
	}

	public bool getRefleshedFlag(int player)
	{
		return RefleshedFlag[player];
	}

	public void Replace(CardScript sc)
	{
		if(!sc.ReplaceFlag)
			return;

		CardScript EffecterScript=null;

		if(EffecterNowID!=-1)
			EffecterScript=getCardScr(EffecterNowID % 50,EffecterNowID / 50);
		else 
			EffecterScript=SystemCard.GetComponent<CardScript>();

		if(sc.effectTargetID.Count>0 && EffecterScript.effectTargetID.Count>0)
		{
            Replace(EffecterScript, sc.effectTargetID[0], (Motions)sc.effectMotion[0], sc.Targetable);

            //powerUpValue
            EffecterScript.powerUpValue = sc.powerUpValue;

            toldBanishing = false;//置き換えられたら終わり
        }

		//end
		sc.ReplaceFlag=false;
		sc.Targetable.Clear();
		sc.effectTargetID.Clear();
		sc.effectMotion.Clear();
	}

    //powerUpValueは省略
    void Replace(CardScript EffecterScript,int changeTargetID, Motions changeMotion, List<int> Targetable)
    {
        if (Targetable != null)
        {
            //targetable
            EffecterScript.Targetable.Clear();


            for (int i = 0; i < Targetable.Count; i++)
            {
                EffecterScript.Targetable.Add(Targetable[i]);
            }
        }

        //targetID
        EffecterScript.effectTargetID[0] = changeTargetID;

        //targetMotion
        EffecterScript.effectMotion[0] = (int)changeMotion;
    }
		
	// Use this for initialization
	void Start ()
	{
		startedFlag = true;

        SystemCard = (GameObject)Instantiate(
            Resources.Load("BlackBack"),
            transform.position,
            Quaternion.identity
            );
        SystemCardScr = SystemCard.GetComponent<CardScript>();

		//moving 初期化 
		for (int i=0; i<(int)constance.MovingNum; i++)
		{ 
			moveID [i] = -1;
			movePhase [i] = -1;
			rotaID [i] = -1;
			rotaPhase [i] = -1;
		}

		//NetScr の取得
		if (NetworkManager != null)
		{
			NetScr = NetworkManager.GetComponent<networkScript> ();
		}

		//フレームレート60に
		Application.targetFrameRate = 60;

		//GUIStyleの設定
		LabelStyle.fontSize = Screen.height / 8;
		LabelStyle.normal.textColor = Color.white;
		
		NextStyle.fontSize = Screen.width / 62;
		NextStyle.normal.textColor = Color.white;

		PhaseStyle.fontSize = Screen.width / 20;
		PhaseStyle.normal.textColor = Color.white;

		//signiZone cursor
		for (int i=0; i<TargetSigniZoneCursor.Length; i++)
		{
			TargetSigniZoneCursor [i] = (GameObject)Instantiate (
				Resources.Load ("targetCursor"),
				STORAGEZONE,
				Quaternion.identity
			);
			
			TargetSigniZoneCursor [i].transform.parent = this.transform;
		}

		//deck file search
		searchAlldeck ();

		//playerPrefs からのロード
		loadPlayerPrefs();

		//リプレイの検索
		searchReplay();

        for (int i = 0; i < 3; i++)
            underCards.Add(new List<int>());

        canvasObj = GameObject.Find("Canvas");
        panelObj = GameObject.Find("Panel");
        beforGame = GameObject.Find("beforeGame");
        showCardText = GameObject.Find("showCardText").GetComponent<UnityEngine.UI.Text>();
        showCardImage = GameObject.Find("showCardImage").GetComponent<UnityEngine.UI.RawImage>();
	}

    bool loadingPics()
    {
        for (int player = 0; player < 2; player++)
        {
            if (NowLoading[player])
            {
                bool flag = true;
                int count = 0;

                for (int i = 0; i < card.GetLength(1); i++)
                {
                    string key = SerialNumString[player, i];
                    Texture tex = Singleton<pics>.instance.getTexture(key);
                    if (tex == null)
                    {
                        flag = false;
                    }
                    else
                        count++;
                }

                if (flag)
                    DeckCreat(player);
            }
        }
        return NowLoading[0] || NowLoading[1];
    }

    void zoneTargetPowerUp()
    {
        foreach (var item in zonePowerList)
        {
            int x = fieldRankID(Fields.SIGNIZONE, item.zoneRank, item.target);
            if (x >= 0 && !checkChanListExist(x, item.target, item.changer % 50, item.changer / 50))
                alwaysChagePower(x, item.target, item.powerUpValue, item.changer % 50, item.changer / 50, null, true);
        }

    }

    void alwaysPowerUpCheck()
    {
        zoneTargetPowerUp();

        for (int i = 0; i < powChanList.Count; i++)
        {
            int x = powChanList[i].changedID;

            if (powChanList[i].check == null || powChanList[i].check(x % 50, x / 50))
                continue;

            powChanListRemove(i);
            i--;
        }
    }

    void colorChangeCheck()
    {
        for (int i = 0; i < colorChangeIDs.Count; i++)
        {
            if (!getCardScr(colorChangeIDs[i]).isOnBattleField())
            {
                colorChangeIDs.RemoveAt(i);
                colorChangeColor.RemoveAt(i);
                i--;
            }
        }
    }

    void cardStatusBufSet()
    {
        for (int i = 0; i < cardStatusBuf.Length; i++)
        {
            int l=cardStatusBuf.GetLength(1);
            if (cardStatusBuf[i / l, i % l] == null)
                cardStatusBuf[i / l, i % l] = new cardstatus();

            cardStatusBuf[i / l, i % l].cardColor = getCardColor(i % l, i / l);
        }
    }
		
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit ();
		}
				
		//バトル終了
		if (DuelEndFlag)
		{
			if (MessageWindow == null)
			{
				NetScr.resetManager ();
				return;
			}
			else
				return;
		}

		if(messages.Count == 0 && notMoving() && playerDissconnected){
			afterPlayerDisconnected();
			return;
		}
		
/*		if (Input.GetMouseButtonDown (2) && Application.platform==RuntimePlatform.WindowsEditor)
		{
			saveReplay();
		}
/*		if(Input.GetMouseButtonDown(2)){
			int id=fieldRankID(Fields.SIGNIZONE,0,1);
			if(id>=0)card[1,id].GetComponent<CardScript>().Power-=2000;
		}
/*		if(Input.GetMouseButtonDown(2))
			DestoryCursorAll();
*/		

		//loading pics

		if(loadingPics())
			return;

        checkUgui();
        YesNoGUI();

		if (preDeckCreat[0] || preDeckCreat[1])
			return;

		if(isTrans && phase != Phases.PrePhase)
		{
			myFrameTime[ thinkingPlayer ]++;

			if( myFrameTime[ thinkingPlayer ]%60 == 0 )
			{
				myFrameTime[ thinkingPlayer ] = 0;
				myTime[ thinkingPlayer ]--;

				if(thinkingPlayer == 0)
					NetScr.rpcOthers( "timeSynchro", new object[]{ myTime[0] } );
			}
		}
		MouseToWorld ();
		LabelCount++;

		//１フレーム系の更新
       oneFrameUpdate();

        //alwaysPowerUpのチェック
       alwaysPowerUpCheck();

       //color changeのチェック
       colorChangeCheck();

        //この時点のカードの情報を保存する
       cardStatusBufSet();
		
		//showCursor
		if (targetShowList.Count > 0 && targetShowCursor == null)
		{
			setTargetShowCursor (targetShowList [0] % 50, targetShowList [0] / 50);
			targetShowList.RemoveAt (0);
		}
		
		
		//wait time
		if (waitTime > 0)
			waitTime--;
		if (waitTime > 0)
			return;
		
		//animationCount
		if (animationCount > 0)
		{
			animationCount--;
			return;
		}

		//ダイアログが終わって1フレーム待つ
		if (DialogCrashFlag)
		{
			DialogCrashFlag = false;
			DialogFlag = false;
			return;
		}
		
		//stopFlagによる１フレームストップ
		if (stopFlag)
		{
			stopFlag = false;
			return;
		}

		//各種フラグによるreturn
		if (FlagUpcheck ())
			return;

		//エナカラーの更新
		enaColorNumUpdate();

		//effectShortage
		if(effectShortageID!=-1){
			if(getCardScr(effectShortageID %50, effectShortageID/50).effectTargetID.Count>0)
			{
				negate(effectShortageID %50, effectShortageID/50);
				return;
			}
			else 
				effectShortageID=-1;
		}
	
		//Preparation
		if (phase == Phases.PrePhase)
			PrePhaseMove ();
		
		//goEnd
		if (notMoving () && goEndPhaseFlag)
		{
			goEndPhaseFlag = false;
			phase = Phases.EndPhase;
		}
		
		//フェイズが変わるごとの処理
		if (phaseBuf != phase)
		{
			if (phase == Phases.UpPhase)
				turn++;

            if (phase == Phases.MainPhase)
            {
                int p=getTurnPlayer();
                MinusLimitCount[p] = nextMinusLimitCount[p];//ここから
                nextMinusLimitCount[p] = 0;
            }

            if (phase == Phases.AttackPhase)
            {
                int p = getTurnPlayer();
                attackThinkPlayer = p;
                MinusLimitCount[p] = 0;//ここまで
            }

			phaseBuf = phase;
			LabelCount = 0;
			movePhase [0] = 0;
			rotaPhase [0] = -1;
			movePhase [1] = 0;
			rotaPhase [1] = -1;
			motion = Motions.NonMotion;
		}

		//_check
		
		
		//デッキリフレッシュのチェック
		if (!deckRefleshFlag [0] && !deckRefleshFlag [1])
		{
			for (int i=0; i<2; i++)
			{
				if (deckNum [i] == 0 && !effectFlag)
				{
					deckRefleshFlag [i] = true;
					refleshMotion [i] = 0;
					movePhase [i + 2] = 0;
					rotaPhase [i + 2] = -1;
				}
			}
			
		}

        //マイナスのバニッシュ
        if (!SystemEffectFlag)
            CheckMinusPower();

        //シグニ数の上限のチェック
        if (!SystemEffectFlag)
            CheckSigniSumLim();

		//systemCardのチェック
		if(!SystemEffectFlag && !effectFlag && !SpellCutInEffect)
			SystemEffectCheck();


		//スペルカットインの効果のチェック
		if (!SpellCutInEffect)
			SpellCutInEffectCheck ();

		
		//ライフバーストのチェック
		if (BurstUpFlag)
		{
			BurstUpFlag = false;
			BurstEffectFlagCheck ();
		}
		
		//ダイアログのチェック
		if (!DialogFlag && !SystemEffectFlag)
		{
			for (int i=0; i<100 + NewCardList.Count; i++)
			{
				CardScript sc;

				if(i>=100)
					sc = NewCardList[i-100].NewCardObj.GetComponent<CardScript>();
				else 
					sc = card [i / 50, i % 50].GetComponent<CardScript> ();

				if ((!effectFlag || i == effecter [0] || sc.ReplaceFlag) && isUpDialog(sc))
				{
					DialogFlag = true;

					DialogID = i;
					if(i>=100)
						DialogID=NewCardList[i-100].TrueID+NewCardList[i-100].TruePlayer*50;

					DialogScript=sc;
					DialogStr = sc.DialogStr;
					DialogNum = sc.DialogNum;

                    //countMax
                    if (DialogNum == 3)//色
                        DialogCountMax = 5;
                    else if (DialogNum == 6)//レベル
                        DialogCountMax = 4;
                    else if (DialogNum == 4 || DialogNum == 5)//上下、アップダウン
                        DialogCountMax = 1;
                    else 
                        DialogCountMax = sc.DialogCountMax;

                    //dialogCount
                    if (DialogNum == 3)
                        DialogCount = 0;
                    else
                        DialogCount = DialogCountMax;
						
					if (DialogNum == 2)
					{
						for (int k=0; k<sc.checkBox.Count; k++)
						{
							sc.checkBox [k] = false;
						}
							
						checkID.Clear ();
						checkBox.Clear ();
						checkStr.Clear ();
						for (int t=0; t<sc.checkStr.Count; t++)
						{
							checkStr.Add (sc.checkStr [t]);
							checkBox.Add (false);
						}
					}

   				return;
				}
			}
		}

        //効果のチェック
        if (!deckRefleshFlag[0] && !deckRefleshFlag[1] && !SystemEffectFlag)
        {
            bool effectFlagBuf = effectFlag;
            if (!effectFlag)
                EffectFlagCheck();
            if (effectFlag && !effectFlagBuf)
            {
                return;
            }
        }


        if (SpellCutInEffect)
		{//スペルカットイン（効果）
			Effect ();
		}
		else if (SpellCutInFlag)
		{//スペルカットイン（アーツ）
			spellCutIn ();
		}
		else if (effectFlag)
		{
			//効果実行
			Effect ();
		}
		else if (deckRefleshFlag [0] || deckRefleshFlag [1])
		{
			//デッキリフレッシュ
			for (int i=0; i<2; i++)
				DeckReflesh (i);
		}
        else if (SystemEffectFlag)
        {
            CardScript sc = SystemCard.GetComponent<CardScript>();
            int playerNum = 2;
            if (sc.effectTargetID.Count == 0)
            {
                sc.effectFlag = false;
                sc.Targetable.Clear();

                for (int j = 0; j < chainMotion.Length; j++)
                {
                    chainMotion[j] = chainMotionBuf[playerNum / 6, j];
                    chainMotionBuf[playerNum / 6, j] = -1;
                }

                SystemEffectFlag = false;

                GUImoveID = GUImoveIDbuf;
                GUImoveIDbuf = -1;

                while (clickCursorIDbuf.Count > 0)
                {
                    clickCursorID.Add(clickCursorIDbuf[0]);
                    clickCursorIDbuf.RemoveAt(0);
                }
            }
            else
            {
                //-1入力
                if (!effectCursorInput(sc, playerNum))
                    return;

                //-2入力
                if (!effectGUIInput(sc, playerNum))
                    return;

                if (!systemCardInitialized)
                {
                    if (sc.effectTargetID.Count > 0 && sc.effectTargetID[0] / 50 + playerNum < (int)constance.MovingNum)
                    {
                        movePhase[sc.effectTargetID[0] / 50 + playerNum] = 0;
                        rotaPhase[sc.effectTargetID[0] / 50 + playerNum] = -1;
                        systemCardInitialized = true;

                        return;
                    }
                }

                //systemInputReturn
                if (sc.inputReturn >= 0)
                {
                    DoSystemInputReturn(sc.inputReturn);
                    sc.inputReturn = -1;
                }

                effectBody(sc, playerNum);
            }
        }
        else
        {
            //Duel Start
            if (!goNextPhaseFlag)
            {
                int player = getTurnPlayer();
                //エンドフェイズ
                if (phase == Phases.EndPhase || GoNextTrunFlag)
                    EndPhaseMove(player);
                //アップフェイズ
                else if (phase == Phases.UpPhase)
                    UpPhaseMove(player);
                //ドローフェイズ
                else if (phase == Phases.DrawPhase)
                    DrawPhaseMove(player);
                //エナフェイズ
                else if (phase == Phases.EnaPhase && !EnaPhaseSkip[player])
                    EnaPhaseMove(player);
                //グロウフェイズ
                else if (phase == Phases.GrowPhase && !isGrowPhaseSkip(player))
                    GrowPhaseMove(player);
                //メインフェイズ
                else if (phase == Phases.MainPhase)
                    MainPhaseMove(player);
                //アタックフェイズ
                else if (phase == Phases.AttackPhase && !AttackPhaseSkip)
                    AttackPhaseMove(player);
                else if (phase != Phases.PrePhase && LabelCount > standartTime * 5)
                    phase = (Phases)((int)phase % 7 + 1);
            }

            //next phase 
            if (goNextPhaseFlag)
            {
                goNextPhaseFlag = false;
                phase++;
            }

            if (phaseBuf != phase)
                startedPhase = (int)phase;

            if (waitTime > 0)
                return;//motion にて waitTime が変わった時用
            for (int i = 0; i < 2; i++)
                Moving(i);
        }		
	}

    void igniUpDown(int mode)
    {
        int id=TraIgniUpID;
        if (mode == 0)
            id = IgnitionUpID;

        if (id < 0)
            return;
        CardScript sc = getCardScr( id% 50, id / 50);

        if (isUpEffect(sc))
            IgniEffID = id;

        if (mode == 0) IgnitionUpID = -1;
        else if (mode == 1) TraIgniUpID = -1;
    }

    void oneFrameIDsClear()
    {
        igniUpDown(0);//igniUp
        igniUpDown(1);//traIgniUp

        OneFrameIDs.Clear();
    }
    
    void oneFrameUpdate()
    {
        oneFrameIDsClear();

/*        igniUpDown(0);//igniUp
        igniUpDown(1);//traIgniUp

        exitID = -1;
        TrashSummonID = -1;
        cipID = -1;
        BurstEffectID = -1;
        CounterID = -1;
        TargetNowID = -1;
        ChantSpellID = -1;
        ChantArtsID = -1;
        AttackerID = -1;
        EffectGoTrashID = -1;
        stopAttackedID = -1;
        BanishedID = -1;
        BanishingID = -1;
        removingID = -1;
        effectDownedID = -1;
        downID = -1;

        crashedID = -1;*/
        lancerCrashed = false;

        crossedIDs[0] = -1;
        crossedIDs[1] = -1;

        LifeAddFlag = false;
        turnEndFlag = false;
        turnStartFlag = false;

        startedPhase = -1; 

        costDownResetFlag[0] = false;
        costDownResetFlag[1] = false;

        BattledID[0] = -1;
        BattledID[1] = -1;

        BattleFinishID[0] = -1;
        BattleFinishID[1] = -1;

        LrigAttackNow = false;

        if (requipUpFlag)
        {
            requipUpFlag = false;
            requipFlag = true;
        }
        else
            requipFlag = false;

        if (notCipID != -1)
        {
            CardScript sc=getCardScr(notCipID);
            if (isUpEffect(sc) || isUpDialog(sc))
                negate(notCipID % 50, notCipID / 50);
            else
                notCipID = -1;
        }

        isCrossExited = false;
    }
	
	private Vector3 vec3Add (Vector3 vec1, Vector3 vec2)
	{
		Vector3 vec = new Vector3 (vec1.x + vec2.x, vec1.y + vec2.y, vec1.z + vec2.z);
		return vec;
	}

	private Vector3 vec3Addx (Vector3 vec1, float flo)
	{
		Vector3 vec = new Vector3 (vec1.x + flo, vec1.y, vec1.z);
		return vec;
	}

	private Vector3 vec3Addy (Vector3 vec1, float flo)
	{
		Vector3 vec = new Vector3 (vec1.x, vec1.y + flo, vec1.z);
		return vec;
	}

	private Vector3 vec3Addz (Vector3 vec1, float flo)
	{
		Vector3 vec = new Vector3 (vec1.x, vec1.y, vec1.z + flo);
		return vec;
	}

	private Vector3 vec3Hiku (Vector3 vec1, Vector3 vec2)
	{
		Vector3 vec = new Vector3 (vec1.x - vec2.x, vec1.y - vec2.y, vec1.z - vec2.z);
		return vec;
	}

	private Vector3 vec3Kakeru (Vector3 vec1, float a)
	{
		Vector3 vec = new Vector3 (vec1.x * a, vec1.y * a, vec1.z * a);
		return vec;
	}

	private Vector3 vec3Waru (Vector3 vec1, float a)
	{
		if (a == 0)
			return new Vector3 (0f, 0f, 0f);
		Vector3 vec = new Vector3 (vec1.x / a, vec1.y / a, vec1.z / a);
		return vec;
	}

	private Vector3 vec3Player2 (Vector3 vec)
	{
		Vector3 vecbuf = new Vector3 (-vec.x, vec.y, -vec.z);
		return vecbuf;
	}

	private int fieldRankID (Fields fieldID, int rank, int player)
	{
		for (int i=0; i<50; i++)
		{
			if (field [player, i] == fieldID && fieldRank [player, i] == rank)
				return i;
		}
		return -1;
	}

	private int fieldRankID_p1 (Fields fieldID, int rank)
	{
		for (int i=0; i<50; i++)
		{
			if (field [0, i] == fieldID && fieldRank [0, i] == rank)
				return i;
		}
		return -1;
	}

	private int fieldRankID_p2 (Fields fieldID, int rank)
	{
		for (int i=0; i<50; i++)
		{
			if (field [1, i] == fieldID && fieldRank [1, i] == rank)
				return i;
		}
		return -1;
	}
	
	private int cardID (GameObject obj)
	{
		GameObject objBuf;
		if (obj != null && obj.transform.parent != null)
		{
			objBuf = obj.transform.parent.gameObject;
		}
		else
			objBuf = obj;
		for (int i=0; i<50; i++)
		{
			if (objBuf == card [0, i] || objBuf == front [0, i])
				return i;
		}
		return -1;
	}

	private int cardID_p2 (GameObject obj)
	{
		GameObject objBuf;
		if (obj != null && obj.transform.parent != null)
		{
			objBuf = obj.transform.parent.gameObject;
		}
		else
			objBuf = obj;
		for (int i=0; i<50; i++)
		{
			if (objBuf == card [1, i] || objBuf == front [1, i])
				return i;
		}
		return -1;
	}
	
	private bool isCardFront (GameObject obj)
	{
		for (int i=0; i<100; i++)
		{
			if (obj == front [i / 50, i % 50])
				return true;
		}
		return false;
	}

    void CheckSigniSumLim()
    {
        for (int n = 0; n < 2; n++)
        {
            //ターンプレイヤーからみる
            int i = getTurnPlayer(); 
            if (n != 0)
                i = 1 - i;


            if (fieldAllNum(Fields.SIGNIZONE, i) > signiSumLimit[i] || (getLrigLevelLimit(i) < SigniLevelSum(i) && !DebugFlag))
            {
                systemSigniOneGoTrash(i);
                return;
            }

//            Debug.Log("LevelLimit[i]" + LevelLimit[i] + " : " + "SigniLevelSum(i)" + SigniLevelSum(i));
        }
    }

    void systemSigniOneGoTrash(int target)
    {

        CardScript sc = SystemCardScr;

        sc.funcTargetIn(target, Fields.SIGNIZONE, null, this);
        sc.setEffect(-1, 0, Motions.GoTrash);
        sc.effectSelecter = target;
    }

  	void CheckMinusPower()
{
		if(SystemCard.GetComponent<CardScript>().effectFlag)
			return;

		for (int i = 0; i < 6; i++)
		{
			int x=fieldRankID(Fields.SIGNIZONE,i%3,i/3);
			if(x>=0 && getCardPower(x,i/3)<=0)
			{
				//violence
				int k=5;
				if(ViolenceCount>0)
					k=7;

				SetSystemCard(x+i/3*50,(Motions)k);
			}			
		}
	}
	
	//GUI
	void OnGUI ()
	{
        if (Singleton<config>.instance.configNow)
            return;

		float w = Screen.width;
		float h = Screen.height;
		float x = Input.mousePosition.x;
		float y = Screen.height - Input.mousePosition.y;
		
		//メインボックスの描画
//		GUI.Box (new Rect (0, 0, Screen.width / 5, Screen.height), "");
		
		//サレンダーボタン
/*		if (GUI.Button (new Rect (w / 5 + w / 50, 10, w / 10, w / 10 * 2 / 3), "サレンダー"))
		{
			pushSurrenderButton ();
			
			return;
		}
        */
		if (preDeckCreat[0] || preDeckCreat[1] )
		{
			if( !NowLoading[0] && !NowLoading[1] )
				preDeckCreatGUI ();
			return;
		}
		
		
		//show power
		string sss = "\n";
		if (summonCount == 0)
		{
			for (int i=0; i<6; i++)
			{
				if (signiCondition [i / 3, i % 3] != Conditions.no)
				{
					int id = fieldRankID (Fields.SIGNIZONE, i % 3, i / 3);
					float z_posi = -2f;
					if (i / 3 == 0)
						z_posi = 2.5f;
					Vector3 v = vec3Add (card [i / 3, id].transform.position, new Vector3 (-0.8f, 0f, z_posi));
					Vector3 vector = Camera.main.WorldToScreenPoint (v);
					int pow = +card [i / 3, id].GetComponent<CardScript> ().Power;
					int opow = +card [i / 3, id].GetComponent<CardScript> ().OriginalPower;
					
					GUIStyle g = new GUIStyle ();
					if (pow == opow)
						g.normal.textColor = Color.white;
					else if (pow > opow)
						g.normal.textColor = Color.yellow;
					else if (pow < opow)
						g.normal.textColor = Color.red;
					//				GUI.Box(new Rect(vector.x,h-vector.y,50,20),"");
					
					GUI.Label (new Rect (vector.x, h - vector.y, 50, 20), "" + pow, g);
				}
			}
		}
		
		//num　の表示
		float haba = 0f;
		for (int i=0; i<2; i++)
		{
			if (deckNum [i] >= 0)
			{
				Vector3 v = MAINDECK;
				if (i == 1)
					v = vec3Player2 (v);
				v = vec3Addz (v, -1.5f);
				v = Camera.main.WorldToScreenPoint (v);
				GUIStyle g = new GUIStyle ();
				g.normal.textColor = Color.white;
				GUI.Label (new Rect (v.x, h - v.y, 50, 20), "" + deckNum [i], g);
			}
			if (trashNum [i] >= 0)
			{
				Vector3 v = TRASH;
				if (i == 1)
					v = vec3Player2 (v);
				v = vec3Addz (v, -1.5f);
				v = Camera.main.WorldToScreenPoint (v);
				GUIStyle g = new GUIStyle ();
				g.normal.textColor = Color.white;
				GUI.Label (new Rect (v.x, h - v.y, 50, 20), "" + trashNum [i], g);
			}
			if (trashNum [i] >= 0)
			{
				Vector3 v = ENAZONE;
				v = vec3Addx (v, -1.5f);
				v = vec3Addz (v, 1.5f);
				if (i == 1)
					v = vec3Player2 (v);
				else
					v = vec3Addz (v, 1.5f);
				v = Camera.main.WorldToScreenPoint (v);
				GUIStyle g = new GUIStyle ();
				g.normal.textColor = Color.white;
				GUI.Label (new Rect (v.x, h - v.y, 50, 20), "" + enaNum [i], g);
				
				if (i == 0)
					haba = (v.x - (w / 5 + w / 50)) / 5;
				for (int j=0; j<enaColorNum.GetLength(1); j++)
				{
					switch (j)
					{
					case 0:
						g.normal.textColor = Color.white;
						break;
					case 1:
						g.normal.textColor = Color.yellow;
						break;
					case 2:
						g.normal.textColor = Color.red;
						break;
					case 3:
						g.normal.textColor = Color.blue;
						break;
					case 4:
						g.normal.textColor = Color.green;
						break;
					
					case 5:
						g.normal.textColor = Color.magenta;
						break;
					}
					if (i == 0)
						GUI.Label (new Rect (v.x - haba * (5 - j), h - v.y + h / 40, 50, 20), "" + enaColorNum [i, j], g);
					else
						GUI.Label (new Rect (v.x + haba * j, h - v.y + h / 40, 50, 20), "" + enaColorNum [i, j], g);			
				}
			}
		}

		sss += "\n" + hakken + " " + hakkenNum + "\n";
		sss += hakkenStr;
		if (selectedID >= 0)
			sss += "\nfield=" + field [selectPlayer, selectedID] + " ID=" + selectedID + " fieldRnak=" + fieldRank [selectPlayer, selectedID];
		sss += "\nmoveID[0]=" + moveID [0] + " movePhase[0]=" + movePhase [0];
		sss += "\nmoveID[1]=" + moveID [1] + " movePhase[1]=" + movePhase [1];
		sss += "\nmoveID[2]=" + moveID [2] + " movePhase[2]=" + movePhase [2];
		sss += "\nmoveID[3]=" + moveID [3] + " movePhase[3]=" + movePhase [3];
		sss += "\nchainMontion[0]=" + chainMotion [0] + "\neffectFlag=" + effectFlag;
		sss += "\nmotion=" + motion;
		sss += "\nGUImoveID=" + GUImoveID;
		sss += "\nselectEnaFlag=" + selectEnaFlag;
		sss += "\nstopFlag=" + stopFlag;
		sss += "\nselectCursorFlag=" + selectCursorFlag;
		sss += "\nshortageFlag=" + shortageFlag;
		sss += "\ncipCheck=" + cipCheck;
		sss += "\ndeckRefleshFlag[0]=" + deckRefleshFlag [0];
		sss += "\nrefleshMotion[0]=" + refleshMotion [0];

		if (effecter [0] >= 0)
		{
			sss += "\neffectTargetID=";
			CardScript sc2 = card [effecter [0] / 50, effecter [0] % 50].GetComponent<CardScript> ();
			for (int i=0; i<sc2.effectTargetID.Count; i++)
			{
				sss += sc2.effectTargetID [i] + " ";
			}
		}
		if (effectSelectIDbuf.Count > 0)
		{
			sss += "\neffectSelectIDbuf[0]=";
			for (int i=0; i<effectSelectIDbuf.Count; i++)
			{
				sss += effectSelectIDbuf [i] + " ";
			}
		}
/*		sss="\n";
		sss="LevelLimit[0] = "+LevelLimit[0];
		sss+="\n";
		sss+="LevelLimit[1] = "+LevelLimit[1];
		sss+="\n";
		sss+="SigniLevelSum(0) = "+SigniLevelSum(0);
		sss+="\n";
		sss+="SigniLevelSum(1) = "+SigniLevelSum(1);
	
/*		for(int i=0;i<trashNum[1];i++){
			int d=fieldRankID(Fields.TRASH,i,1);
			sss+=card[1,d].GetComponent<CardScript>().Name/*SerialNumString[0,d]+"\n";
		}*/
/*		sss="\n\n";
		for(int i=0;i<deckNum[0];i++){
			sss+=fieldRankID(Fields.MAINDECK,i,0)+" ";
		}*/
		
		//移動しました
//		int size = Screen.width / 6;
//		if (showTexture != null)
//			GUI.DrawTexture (new Rect (10, 10, size, size * 244 / 175), showTexture);
//		if(clickCursorID.Count>0)GUI.Label(new Rect(10,w/2,170,100),""+LabelCount/60 + " "+clickCursorID[0]);

 //       string showString = ""; 

//        showString = Singleton<DataToString>.instance.SerialNumToString(showSerialNum);

/*		Rect tRect=new Rect(w / 5 + w / 50, 10 + w / 10 * 2 / 3 + 10, w / 10, w / 10 * 2 / 3);
		if (DebugFlag)
			whichShow = GUI.Toggle (tRect, whichShow, "表示切替");
		else{
			string s="設定";
			if(whichShow)
				s="カード情報";

			whichShow = GUI.Toggle (tRect, whichShow, s);
		}
        */

		if (whichShow){
			Rect rect=new Rect (10, h / 3 + 20, 170, 400);

			if(DebugFlag)
				GUI.Label (rect, sss);

			else
			{
				//config
/*				Vector2 v = new Vector2();
				float dy = h / 20;

				v = GUI.BeginScrollView (rect,
				                         v,
				                         new Rect (0, 0, w / 50 - 10, 10 * dy));

				SoundPlayer sp = Singleton<SoundPlayer>.instance;

				GUI.Label(new Rect(10f, 10f, rect.width, dy),"音量");
				sp.VOLUME = GUI.HorizontalSlider(new Rect(0f, dy, rect.width, dy), sp.VOLUME, 0f, 1f);

				replaySaveFlag = GUI.Toggle(new Rect(0f, dy*3, rect.width, dy), replaySaveFlag, "リプレイ出力");

				canReplaySend = GUI.Toggle(new Rect(0f, dy*4, rect.width, dy), canReplaySend, "結果データの送信を許可");

				GUI.EndScrollView();*/
			}
		}
//		else
//			GUI.Label (new Rect (10, size * 244 / 175 + 30, w / 5 - 20, h * 3 / 4), showString);
       
		
		
		//右上のフェイズのやつ
        if (PhaseStyle != null && phase != Phases.PrePhase)
		{
			int l = PhaseStyle.fontSize;
            GUI.Label(new Rect(w - l * 6, 20, l * 6, l), phase.ToString(), PhaseStyle);
		}


		//持ち時間の表示
		if(isTrans)
		{
			GUIStyle g=new GUIStyle();
			g.fontSize =  Mathf.RoundToInt( w/50 );
			g.normal.textColor=Color.white;

			int fs=g.fontSize;

			GUI.Label (new Rect (w * 5 / 6, 100,100, 100),"自分 : " + myTime[0],g);
			GUI.Label (new Rect (w * 5 / 6, 100 + fs,100, 100),"相手 : " + myTime[1],g);
		}

        //nextPhase
//        nextPhaseButton();
		
//      uGUI化		
		//_guard _spellCutIn _attackArts
/*		if (guardSelectflag || SpellCutInSelectFlag || selectAttackAtrs)
		{
			int sw = Screen.width;
			int sh = Screen.height;
			int size_x = sw / 6;
			int size_y = size_x / 2;
			int buttunSize_x = size_x * 4 / 10;
			int buttunSize_y = buttunSize_x / 3;
			Vector3 pos = vec3Addx (SIGNIZONE, SigniWidth);
			Vector3 v = Camera.main.WorldToScreenPoint (pos);//new Vector3(0f,0f,-0.5f));
			Rect boxRect = new Rect (v.x - size_x / 2, sh - v.y - size_y / 2, size_x, size_y);
			Rect buttunRect1 = new Rect (
				boxRect.x + (size_x - buttunSize_x * 2) / 4,
				boxRect.y + size_y - buttunSize_y - 5,
				buttunSize_x,
				buttunSize_y
			);
			Rect buttunRect2 = new Rect (
				boxRect.x + size_x - (size_x - buttunSize_x * 2) / 4 - buttunSize_x,
				buttunRect1.y,
				buttunSize_x,
				buttunSize_y
			);
			string str3 = "ガードしますか？";
			if (SpellCutInSelectFlag)
				str3 = "スペルカットインしますか？";
			else if (selectAttackAtrs)
			{
				str3 = "プレイヤー " + (attackAtrsPlayer + 1) + "\nアーツを使用しますか？";
			}
			GUI.Box (boxRect, "");
			GUI.Label (boxRect, str3);
			if (GUI.Button (buttunRect1, YesStr))
			{
				//通信
				if (isTrans)
				{
					messagesBuf.Add (YesStr);
				}
				
				//フラグによる分岐
				if (guardSelectflag)
				{
					guardSelectflag = false;
					if (GuardNum (guardPlayer) == 1)
					{
						clickCursorID.Add (GuardRankID (0, guardPlayer) + 50 * guardPlayer);
						if (isTrans)
							messagesBuf.Add ("" + clickCursorID [0]);
					}
					else
					{
						selectCursorFlag = true;
						selectNum = 1;
						for (int i=0; i<GuardNum(guardPlayer); i++)
						{
							setTargetCursorID (GuardRankID (i, guardPlayer), guardPlayer);
						}
					}
				}
				else if (SpellCutInSelectFlag)
				{
					SpellCutInSelectFlag = false;
					SpellCutInFlag = true;
					selectCardFlag = true;
					selectCardListSpellCutIn (SpellCutInPlayer);
				}
				else if (selectAttackAtrs)
				{
					selectAttackAtrs = false;
					selectCardFlag = true;
//					selectCardListIn(Fields.LRIGDECK,attackAtrsPlayer);
					selectCardListAttackArtsIn (attackAtrsPlayer);
					artsAsk = true;
				}
			}
			if (GUI.Button (buttunRect2, NoStr))
			{
				//通信
				if (isTrans)
				{
					messagesBuf.Add (NoStr);
					sendMessageBuf ();
				}
				
				//フラグによる分岐
				if (guardSelectflag)
					guardSelectflag = false;
                if (SpellCutInSelectFlag)
                    SpellCutInSelectFlag = false;
				if (selectAttackAtrs)
				{
					attackAtrsPlayer = 1 - attackAtrsPlayer;
					if (isTrans)
					{
						selectAttackAtrs = false;
						if (attackAtrsPlayer != getTurnPlayer ())
							receiveFlag = true;
					}
//					else if(attackAtrsPlayer==getTurnPlayer()){
//						attackAtrsPlayer=1-attackAtrsPlayer;
//					}
					else if (attackAtrsPlayer == getTurnPlayer ())
						selectAttackAtrs = false;
				}
			}
		}*/


		//ダイアログ _dialog
        if (DialogFlag && !DialogCrashFlag)
            dialogInput();
		
		//SelectCard
		if (selectCardFlag)
		{
			if (lrigZoneNum [prePlayer] == 0 && selectCardList.Count == 1)
			{
				GUImoveID = selectCardList [0];
				selectCardList.Clear ();
				selectCardFlag = false;
				//通信
				if (isTrans)
					messagesBuf.Add ("" + GUImoveID);
			}
			else
			{
				Rect selectBoxRect =  new Rect (w * 4 / 15 - w / 100, h / 4, w * 2 / 3 + w / 50, (w * 2 / 3 / 5 - w / 100) * 244 / 175 + h / 10);
				Rect sliderRect = new Rect (selectBoxRect.x + w / 100, h / 4 + (w * 2 / 3 / 5 - w / 100) * 244 / 175 + h / 20, w * 2 / 3, h / 40);

				GUI.Box (selectBoxRect, "");

				int num = selectCardList.Count;

				if (num > 5)
				{
					int Max = num - 5;
					slideFloat = GUI.HorizontalSlider (sliderRect, slideFloat, 0, Max);

					slideFloat -= Input.GetAxis("Mouse ScrollWheel") / 3;

					if(slideFloat > (float)Max)
						slideFloat = (float)Max;
					else if(slideFloat < 0)
						slideFloat = 0;
				}
				else
					slideFloat = 0f;
				
				slideNum = Mathf.RoundToInt (slideFloat);

				
				int[] GUIcardID = new int[5]{-1,-1,-1,-1,-1};

				for (int i=0; i<5 && i<selectCardList.Count; i++)
				{
					if (slideNum >= 0)
						GUIcardID [i] = selectCardList [i + slideNum];
				}
	
				for (int i=0; i<5; i++)
				{
					if (GUIcardID [i] != -1)
					{
						Rect rect = new Rect (w * 4 / 15 + w * 2 / 3 / 5 * i,
						                      h / 4 + h / 100,
						                      w * 2 / 3 / 5 - w / 100,
						                      (w * 2 / 3 / 5 - w / 100) * 244 / 175);

						string key=SerialNumString[GUIcardID [i] / 50, GUIcardID [i] % 50];
						Texture tex = Singleton<pics>.instance.getTexture(key);
						if(tex!=null)
							GUI.DrawTexture (rect, tex);
					}
				}

				for (int i=0; i<5; i++)
				{
					if (w * 4 / 15 + w * 2 / 3 / 5 * i < x 
					    && x < w * 4 / 15 + w * 2 / 3 / 5 * i + w * 2 / 3 / 5 - w / 100 && h / 4 + h / 100 < y 
					    && y < h / 4 + h / 100 + (w * 2 / 3 / 5 - w / 100) * 244 / 175)
					{
						if (GUIcardID [i] != -1)
						{
							showUpDate (GUIcardID [i] % 50, GUIcardID [i] / 50);
							if (Input.GetMouseButtonDown (0))
							{
								if (!showGUIFlag)
								{
									GUImoveID = GUIcardID [i];
									
									//通信
									if (isTrans)
										messagesBuf.Add ("" + GUImoveID);
								}
								showGUIFlag = false;
								selectCardFlag = false;
								selectCardList.Clear ();
								slideNum = 0;
								slideFloat = 0;
								if (selectTrashFlag)
								{
                                    TraIgniSelectedID = GUIcardID[i];
                                    selectTrashFlag = false;
									
									//通信
									if (isTrans)
									{
										messagesBuf.Add (TrashIgnitionStr);
										messagesBuf.Add ("" + GUIcardID [i]);
										sendMessageBuf ();
									}
								}
							}
						}
					}
				}

				if (Input.GetMouseButtonDown (1) && phase != Phases.PrePhase)
				{
					showGUIFlag = false;
					selectCardFlag = false;
					selectTrashFlag = false;
					selectCardList.Clear ();
					slideNum = 0;
					slideFloat = 0f;

                    if(GUIcancelEnabel)
                        GUIcancel = true;
				}
			}
		}
		
		//animation
		if (animationCount > 0 && animationID >= 0)
		{
			int i = 2;
			Rect rect = new Rect (w * 4 / 15 + w * 2 / 3 / 5 * i,
			                      h / 4 + h / 100,
			                      w * 2 / 3 / 5 - w / 100,
			                      (w * 2 / 3 / 5 - w / 100) * 244 / 175);

			string key=SerialNumString[animationID / 50, animationID % 50];
			Texture tex = Singleton<pics>.instance.getTexture(key);

			GUI.DrawTexture (rect, tex);
		}
		
		//フェイズの表示
		if (phase != Phases.PrePhase)
		{
			if (LabelCount < standartTime)
				GUI.Label (new Rect (w / standartTime / 3 * LabelCount, h / 3, 0, 0), phase.ToString (), LabelStyle);
			else if (LabelCount < standartTime * 5)
				GUI.Label (new Rect (w / 3, h / 3, 0, 0), phase.ToString (), LabelStyle);
			else
				GUI.Label (new Rect (w / standartTime / 3 * (LabelCount - standartTime * 4), h / 3, 0, 0), phase.ToString (), LabelStyle);
		}
	}

    void nextPhaseButton()
    {
        //nextPhase
        Vector3 buttonPos = Camera.main.WorldToScreenPoint(new Vector3(0f, 0f, 0.5f));

        if (!isTrans || getTurnPlayer() == 0)
        {
            if (/*GUI.Button(new Rect(buttonPos.x - w / 20, h - buttonPos.y, w / 10, w / 60), "") && Input.GetMouseButtonUp(0)
                && */moveID[0] == -1 && moveID[1] == -1 && moveID[2] == -1 && moveID[3] == -1
                && (!FlagUpcheck() || (selectCursorFlag && (phase == Phases.EnaPhase || phase == Phases.AttackPhase)))
                && !selectSigniZoneFlag && !selectCardFlag && /*(!selectCursorFlag||phase==Phases.EnaPhase) && */!guardSelectflag && !SpellCutInSelectFlag
                && !stopFlag && !deckRefleshFlag[0] && !deckRefleshFlag[1] && !receiveFlag && isAttackEnd()
                && phase != Phases.UpPhase && phase != Phases.DrawPhase && phase != Phases.EndPhase && phase != Phases.PrePhase)
            {
                if ((phase == Phases.EnaPhase || phase == Phases.AttackPhase) && selectCursorFlag)
                {
                    selectCursorFlag = false;
                    selectNum = 0;
                    DestoryCursorAll();
                }

                //phase = p;
                goNextPhaseFlag = true;

                if (isTrans)
                {
                    messagesBuf.Add(nextStr);
                    sendMessageBuf();
                }
            }
 //           GUI.Label(new Rect(buttonPos.x - w / 20 + w / 100, h - buttonPos.y, w / 10, w / 60), "NextPhase", NextStyle);
        }

    }

    void YesNoGUI()
    {
        bool flag = guardSelectflag || SpellCutInSelectFlag || selectAttackAtrs;

        string str3 = "ガードしますか？";
        if (SpellCutInSelectFlag)
            str3 = "スペルカットインしますか？";
        else if (selectAttackAtrs)
            str3 = "プレイヤー " + (attackAtrsPlayer + 1) + "\nアーツを使用しますか？";


        //attackArtsPlayer変更時の処理
        if (YesNoObj != null && oldAttackArtsP != attackAtrsPlayer)
        {
            var com = YesNoObj.GetComponent<RectTransform>();
            YesNoObj.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>().text = str3;
        }
        oldAttackArtsP = attackAtrsPlayer;

        if (flag != YesNoGUIFlag)
        {
            YesNoGUIFlag = flag;

            if (YesNoGUIFlag)
            {
                YesNoObj = Instantiate(Resources.Load("Prefab/YesNoPannel")) as GameObject;
                YesNoObj.transform.SetParent(canvasObj.transform);

                var com = YesNoObj.GetComponent<RectTransform>();
                com.localPosition = new Vector3(60f, -30f, 0f);

                YesNoObj.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>().text = str3;
                YesNoObj.transform.FindChild("yes").GetComponent<beforeGameButton>().setPannel(beforGame); 
                YesNoObj.transform.FindChild("no").GetComponent<beforeGameButton>().setPannel(beforGame);
            }
            else if (YesNoObj != null)
                Destroy(YesNoObj);
        }

        if (!flag)
            return;

/*        int sw = Screen.width;
        int sh = Screen.height;
        int size_x = sw / 6;
        int size_y = size_x / 2;
        int buttunSize_x = size_x * 4 / 10;
        int buttunSize_y = buttunSize_x / 3;
        Vector3 pos = vec3Addx(SIGNIZONE, SigniWidth);
        Vector3 v = Camera.main.WorldToScreenPoint(pos);//new Vector3(0f,0f,-0.5f));
        Rect boxRect = new Rect(v.x - size_x / 2, sh - v.y - size_y / 2, size_x, size_y);
        Rect buttunRect1 = new Rect(
            boxRect.x + (size_x - buttunSize_x * 2) / 4,
            boxRect.y + size_y - buttunSize_y - 5,
            buttunSize_x,
            buttunSize_y
        );
        Rect buttunRect2 = new Rect(
            boxRect.x + size_x - (size_x - buttunSize_x * 2) / 4 - buttunSize_x,
            buttunRect1.y,
            buttunSize_x,
            buttunSize_y
        );*/
 //       GUI.Box(boxRect, "");
 //       GUI.Label(boxRect, str3);
 
//        if (GUI.Button(buttunRect1, YesStr))
        if(uiYes)
        {
            uiYes = false;
            //通信
            if (isTrans)
            {
                messagesBuf.Add(YesStr);
            }

            //フラグによる分岐
            if (guardSelectflag)
            {
                guardSelectflag = false;
                if (GuardNum(guardPlayer) == 1)
                {
                    clickCursorID.Add(GuardRankID(0, guardPlayer) + 50 * guardPlayer);
                    if (isTrans)
                        messagesBuf.Add("" + clickCursorID[0]);
                }
                else
                {
                    selectCursorFlag = true;
                    selectNum = 1;
                    for (int i = 0; i < GuardNum(guardPlayer); i++)
                    {
                        setTargetCursorID(GuardRankID(i, guardPlayer), guardPlayer);
                    }
                }
            }
            else if (SpellCutInSelectFlag)
            {
                SpellCutInSelectFlag = false;
                SpellCutInFlag = true;
                selectCardFlag = true;
                selectCardListSpellCutIn(SpellCutInPlayer);
            }
            else if (selectAttackAtrs)
            {
                selectAttackAtrs = false;
                selectCardFlag = true;
                //					selectCardListIn(Fields.LRIGDECK,attackAtrsPlayer);
                selectCardListAttackArtsIn(attackAtrsPlayer);
                artsAsk = true;
            }
        }

//        if (GUI.Button(buttunRect2, NoStr))
        if(uiNo)
        {
            uiNo = false;
            //通信
            if (isTrans)
            {
                messagesBuf.Add(NoStr);
                sendMessageBuf();
            }

            //フラグによる分岐
            if (guardSelectflag)
                guardSelectflag = false;
            if (SpellCutInSelectFlag)
                SpellCutInSelectFlag = false;
            if (selectAttackAtrs)
            {
                attackAtrsPlayer = 1 - attackAtrsPlayer;
                if (isTrans)
                {
                    selectAttackAtrs = false;
                    if (attackAtrsPlayer != getTurnPlayer())
                        receiveFlag = true;
                }
                //					else if(attackAtrsPlayer==getTurnPlayer()){
                //						attackAtrsPlayer=1-attackAtrsPlayer;
                //					}
                else if (attackAtrsPlayer == getTurnPlayer())
                    selectAttackAtrs = false;
            }
        }

    }

    void traIgniUp(int upID)
    {
        CardScript sc = getCardScr(upID % 50, upID / 50);
        sc.TrashIgnition = true;
        TraIgniUpID = upID;
    }

    void dialogRead(CardScript sc, bool isSpellCutInMode)
    {
        string r = dialogGetMessage(isSpellCutInMode);

        sc.messages.Add(r);
        DialogCrashFlag = true;
        sc.DialogFlag = false;

        if (DialogNum != 2 || r == NoStr)
            return;


        //num2 checkbox
        for (int i = 0; i < DialogCountMax; i++)
        {
            string s = dialogGetMessage(isSpellCutInMode);
            int index = int.Parse(s);
            if (index >= 0)
                sc.checkBox[index] = true;
        }
    }

    string dialogGetMessage(bool isSpellCutInMode)
    {
        if (!isSpellCutInMode)
        {
            string r = readMessage();
            dialogSelectbuf.Add(r);
            return r;
        }

        string s = dialogSelectbuf[0];
        dialogSelectbuf.RemoveAt(0);
        return s;
    }

    void dialogInput()
    {
        int sw = Screen.width;
        int sh = Screen.height;
        int size_x = sw / 6;
        int size_y = size_x / 2;
        int buttunSize_x = size_x * 4 / 10;
        int buttunSize_y = buttunSize_x / 3;
        Vector3 v = Camera.main.WorldToScreenPoint(card[DialogID / 50, DialogID % 50].transform.position);
        Rect boxRect = new Rect(v.x - size_x / 2, sh - v.y - size_y / 2, size_x, size_y);
        Rect buttunRect1 = new Rect(
            boxRect.x + (size_x - buttunSize_x * 2) / 4,
            boxRect.y + size_y - buttunSize_y - 5,
            buttunSize_x,
            buttunSize_y
        );
        Rect buttunRect2 = new Rect(
            boxRect.x + size_x - (size_x - buttunSize_x * 2) / 4 - buttunSize_x,
            buttunRect1.y,
            buttunSize_x,
            buttunSize_y
        );
        Rect buttunRect_plus = new Rect(
            boxRect.x + (size_x - buttunSize_x * 2) / 4,
            boxRect.y + size_y / 2 - buttunSize_y / 2,
            buttunSize_x,
            buttunSize_y
        );
        Rect buttunRect_minus = new Rect(
            boxRect.x + size_x - (size_x - buttunSize_x * 2) / 4 - buttunSize_x,
            buttunRect_plus.y,
            buttunSize_x,
            buttunSize_y
        );
        Rect buttunRect_ok = new Rect(
            boxRect.x + size_x / 2 - buttunSize_x / 2,
            boxRect.y + size_y - buttunSize_y - 5,
            buttunSize_x,
            buttunSize_y
        );

        CardScript sc = DialogScript;


        if( effectUsableFlag)
                dialogRead(sc, true);
        else if (waitYou(sc.effectSelecter))
        {
            if (canRead())
                dialogRead(sc,false);
        }
        else if (DialogNum == 0)
        {
            GUI.Box(boxRect, "");
            GUI.Label(boxRect, DialogStr);

            if (GUI.Button(buttunRect1, YesStr))
            {
                DialogCrashFlag = true;
                sc.messages.Add(YesStr);
                sc.DialogFlag = false;

                dialogSelectbuf.Add(YesStr);

                //通信
                if (isTrans)
                {
                    messagesBuf.Add(YesStr);
                    sendMessageBuf();
                }
            }

            if (GUI.Button(buttunRect2, NoStr))
            {
                DialogCrashFlag = true;
                sc.messages.Add(NoStr);
                sc.DialogFlag = false;

                //dialog buf
                dialogSelectbuf.Add(NoStr);

                //通信
                if (isTrans)
                {
                    messagesBuf.Add(NoStr);
                    sendMessageBuf();
                }
            }
        }
        else if (DialogNum == 1 || DialogNum == 3 || DialogNum == 4 || DialogNum == 5 || DialogNum == 6)
        {
            GUI.Box(boxRect, "");

            string dstr = "対象の数 -> " + DialogCount;

            switch (DialogNum)
            {
                case 3:
                    dstr = "宣言する色";
                    dstr += " -> " + (cardColorInfo)DialogCount;
                    break;

                case 4:
                    dstr = "Top or Bottom";
                    dstr += " -> " + (positionInfo)DialogCount;
                    break;

                case 5:
                    dstr = "Up or Down";
                    dstr += " -> " + (Conditions)(DialogCount + 1);
                    break;

                case 6:
                    dstr = "宣言するレベル";
                    dstr += " -> " + (DialogCount + 1);
                    break;
            }

            if (sc.DialogStrEnable)
                dstr = sc.DialogStr;

            GUI.Label(boxRect, dstr);

            if (GUI.Button(buttunRect_plus, "+"))
            {
                DialogCount = (DialogCount + 1) % (DialogCountMax + 1);
            }
            if (GUI.Button(buttunRect_minus, "-"))
            {
                DialogCount = (DialogCount + DialogCountMax) % (DialogCountMax + 1);
            }

            if (GUI.Button(buttunRect_ok, "OK"))
            {
                DialogCrashFlag = true;
                sc.messages.Add("" + DialogCount);
                sc.DialogFlag = false;

                //dialog buf
                dialogSelectbuf.Add("" + DialogCount);

                //通信
                if (isTrans)
                {
                    messagesBuf.Add("" + DialogCount);
                    sendMessageBuf();
                }

            }
        }
        else if (DialogNum == 2)
        {
            GUI.Box(boxRect, "");

            for (int i = 0; i < checkBox.Count; i++)
            {
                float height = (size_y - buttunSize_y - 5) / checkBox.Count;
                Rect rect = new Rect(buttunRect1.x, boxRect.y + height * i, size_x, height);

                bool flag = checkBox[i];
                checkBox[i] = GUI.Toggle(rect, checkBox[i], checkStr[i]);

                if (checkBox[i] && !flag)
                {
                    checkID.Add(i);
                    if (checkID.Count > DialogCountMax)
                        checkBox[checkID[0]] = false;
                }

                if (!checkBox[i])
                    checkID.Remove(i);
            }

            if (GUI.Button(buttunRect1, "OK") && (!sc.DialogMaxSelect || sc.DialogCountMax==checkID.Count))
            {
                DialogCrashFlag = true;
                sc.messages.Add(YesStr);
                sc.DialogFlag = false;

                for (int i = 0; i < checkID.Count; i++)
                {
                    sc.checkBox[checkID[i]] = true;
                }

                //buf
                if (!SpellCutInFlag)
                {
                    dialogSelectbuf.Add(YesStr);
                    for (int i = 0; i < DialogCountMax; i++)
                    {
                        if (i < checkID.Count)
                            dialogSelectbuf.Add("" + checkID[i]);
                        else
                            dialogSelectbuf.Add("" + (-1));
                    }
                }

                //通信
                if (isTrans)
                {
                    messagesBuf.Add(YesStr);
                    for (int i = 0; i < DialogCountMax; i++)
                    {
                        if (i < checkID.Count)
                            messagesBuf.Add("" + checkID[i]);
                        else
                            messagesBuf.Add("" + (-1));
                    }
                    sendMessageBuf();
                }
            }
            //cancel
            if (GUI.Button(buttunRect2, "Cancel"))
            {
                DialogCrashFlag = true;
                sc.messages.Add(NoStr);
                sc.DialogFlag = false;

                //dialog buf
                dialogSelectbuf.Add(NoStr);

                //通信
                if (isTrans)
                {
                    messagesBuf.Add(NoStr);
                    sendMessageBuf();
                }
            }
        }
    }
	
	void DrawCard (int player)
	{
		if (handMoveCount [player % 2] == handNum [player % 2] && movePhase [player] == 0)
		{
			if (drawNum [player % 2] > 0)
				movePhase [player] = 2;
			if (handNum [player % 2] == 0 && handSortFlag [player % 2])
				handSortFlag [player % 2] = false;
		}

		if (movePhase [player] == 0 && moveID [player] < 0 && rotaID [player] < 0)
		{
			moveTime [player] = standartTime / 2;

			moveID [player] = fieldRankID (Fields.HAND, handMoveCount [player % 2], player % 2);

			if (handNum [player % 2] + drawNum [player % 2] == 1)
			{
				destination [player] = HAND;
			}
			else
			{
				float width = 9f - 14f / (drawNum [player % 2] + handNum [player % 2] + 1);
                Vector3 vecBuf = new Vector3();

                while (true)
                {
                    moveID[player] = fieldRankID(Fields.HAND, handMoveCount[player % 2], player % 2);
                    if (moveID[player] < 0)
                        break;

                    destination[player] = vec3Add(HAND,
                        new Vector3(-width + width * 2f / (drawNum[player % 2] + handNum[player % 2] - 1) * handMoveCount[player % 2], 0f, 0f));

                    vecBuf = destination[player];
                    if (player % 2 == 1)
                        vecBuf = vec3Player2(vecBuf);

                    if (card[player % 2, moveID[player]].transform.position != vecBuf)
                        break;
                    else
                    {
                        handMoveCount[player % 2]++;

                        if (handMoveCount[player % 2] == handNum[player % 2])
                        {
                            handMoveCount[player % 2]--;
                            movePhase[player] = 1;
                            break;
                        }
                    }
                }
			}
		}

		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			handMoveCount [player % 2]++;
			if (handMoveCount [player % 2] == handNum [player % 2])
			{
				handSortFlag [player % 2] = false;
				moveID [player] = -1;
				if (drawNum [player % 2] > 0)
					movePhase [player] = 2;
				else
					handMoveCount [player % 2] = 0;
			}
			else
			{
				movePhase [player] = 0;
				moveID [player] = -2;
			}
		}

		if (movePhase [player] == 2 && moveCount [player] == 0)
		{
			//soundDraw
			Singleton<SoundPlayer>.instance.playSE("draw");

			moveID [player] = fieldRankID (Fields.MAINDECK, deckNum [player % 2] - 1, player % 2);
			if (drawNum [player % 2] + handNum [player % 2] > 1)
			{
				float width = 9f - 14f / (drawNum [player % 2] + handNum [player % 2] + 1);
				destination [player] = vec3Add (HAND, new Vector3 (-width + width * 2f / (drawNum [player % 2] + handNum [player % 2] - 1) * handNum [player % 2], 0f, 0f));
			}
			else
				destination [player] = HAND;
			moveTime [player] = standartTime;
			rotaPhase [player] = 0;
			rotaID [player] = moveID [player];				
		}

		if (movePhase [player] == 3 && moveID [player] != -1)
		{
            ExitFunction(moveID[player], player % 2);
//			deckNum [player % 2]--;

			handNum [player % 2]++;
			handMoveCount [player % 2] = handNum [player % 2];

			field [player % 2, moveID [player]] = Fields.HAND;
			fieldRank [player % 2, moveID [player]] = handNum [player % 2] - 1;

			drawNum [player % 2]--;
			if (drawNum [player % 2] > 0)
			{
				movePhase [player] = 0;
				moveID [player] = -2;
			}
			else
			{
				moveID [player] = -1;
				handMoveCount [player % 2] = 0;
			}
		}

		if (rotaPhase [player] == 0 && rotaCount [player] == 0)
		{
			setHandAngle (player);
			rotaTime [player] = moveTime [player];
		}

		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}				
	}

	void EnaCharge (int ID, int player)
	{

		bool flag = false;
		if (enajeFlag[player % 2] && toldBanishing/* && player % 2 != getTurnPlayer () && field [player % 2, ID] == Fields.SIGNIZONE*/)
			flag = true;
		
		if (flag)
			GoTrash (ID, player);
		else
		{
			if (movePhase [player] == 0 && moveID [player] == -1)
			{
				if(toldBanishing)
					Singleton<SoundPlayer>.instance.playSE("banish");
				else 
					Singleton<SoundPlayer>.instance.playSE("draw");

				moveID [player] = ID;
				destination [player] = vec3Add (ENAZONE, new Vector3 (0f, 0.025f * enaNum [player % 2], -EnaWidth * enaNum [player % 2]));
				moveTime [player] = standartTime;
				rotaPhase [player] = 0;

			}

			if (movePhase [player] == 1 && moveID [player] != -1)
			{
				if (field [player % 2, moveID [player]] == Fields.CHECKZONE)
				{
					card [player % 2, moveID [player]].GetComponent<CardScript> ().BurstFlag = false;
				}
				ExitFunction (moveID [player], player % 2);
				field [player % 2, moveID [player]] = Fields.ENAZONE;
				fieldRank [player % 2, moveID [player]] = enaNum [player % 2];
				enaColorNum [player % 2, card [player % 2, moveID [player]].GetComponent<CardScript> ().CardColor]++;
				moveID [player] = -1;
				enaNum [player % 2]++;
			}

			if (rotaPhase [player] == 0 && rotaID [player] == -1)
			{
				rotaID [player] = moveID [player];
				angle [player, 0] = 0f;
				angle [player, 1] = -90f;
				angle [player, 2] = 0f;
				rotaTime [player] = moveTime [player];
			}
			if (rotaPhase [player] == 1 && rotaID [player] != -1)
			{
				rotaID [player] = -1;
			}
		}
	}
	public bool summonLim(int ID,int player)
	{
		CardScript sc = getCardScr (ID, player % 2);
		
		if (checkLrigLim (ID, player) 
            || sc.useLimit
            || sc.Level > LrigLevel [player % 2]
            || sc.Level + SigniLevelSum (player) > getLrigLevelLimit (player % 2))
			return true;

		return false;
	}

    bool checkHandSummonLim(int ID, int player)
    {
        if (HandSummonMaxLim[player % 2] == -1)
            return false;

        return field[player % 2, ID] == Fields.HAND && getCardPower(ID,player%2) >= HandSummonMaxLim[player % 2];
    }

    void Summon(int ID, int player)
    {
        Summon(ID, player, false);
    }
	void Summon (int ID, int player, bool rotaFlag)
	{
        if(checkHandSummonLim(ID,player))
            return;

        if (!DebugFlag && summonLim(ID, player % 2))
            return;

		if (movePhase [player] == 0 && moveID [player] == -1 && selectSigniZone != -1)
		{
			Singleton<SoundPlayer>.instance.playSE("signiSummon");

			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = vec3Add (SIGNIZONE, new Vector3 (SigniWidth * selectSigniZone, 0f, 0f));
			rotaPhase [player] = 0;
		}
		
		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			
			if (field [player % 2, moveID [player]] == Fields.TRASH)
				TrashSummonID = moveID [player] + player % 2 * 50;
			
			ExitFunction (moveID [player], player);
			field [player % 2, moveID [player]] = Fields.SIGNIZONE;
			fieldRank [player % 2, moveID [player]] = selectSigniZone;
			signiCondition [player % 2, selectSigniZone] = Conditions.Up;

            //クロス
            int cross = getCardScr(moveID[player], player % 2).getCrossingID();
            if (cross >= 0)
            {
                crossedIDs[0] = moveID[player] + player % 2 * 50;
                crossedIDs[1] = cross;
            }


			moveID [player] = -1;

			selectSigniZone = -1;
		}

        //回転を行わない場合
        if (rotaFlag)
            return;

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 0f;
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
	}

    void DownSummon(int ID, int player)
    {
        Summon(ID, player,true);
        Down(ID, player);
    }

	void SetCharm (int ID, int player,int setRank)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			if(GetCharm(fieldRankID(Fields.SIGNIZONE,setRank,player%2),player%2)!=-1)
				return;
			
			moveID [player] = fieldRankID(Fields.SIGNIZONE,setRank,player%2);
			moveTime [player] = standartTime;
			destination [player] = vec3Add (SIGNIZONE, new Vector3 (SigniWidth * setRank, 1f, 0f));
		}

		if (movePhase [player] == 1 && moveCount[player]==0)
		{
			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = vec3Add (SIGNIZONE, new Vector3 (SigniWidth * setRank + 0.5f, 0f, 0f));
			rotaPhase [player] = 0;
		}

		if (movePhase [player] == 2 && moveCount[player]==0)
		{
			ExitFunction (moveID [player], player);

			field [player % 2, moveID [player]] = Fields.CharmZone;
			fieldRank [player % 2, moveID [player]] = setRank;

			moveID [player] = fieldRankID(Fields.SIGNIZONE,setRank,player%2);
			destination [player] = vec3Add (SIGNIZONE, new Vector3 (SigniWidth * setRank, 0.025f, 0f));
		}

		if(movePhase [player] == 3 && moveCount[player]==0)
		{
			moveID [player]=-1;
		}

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 180f;
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
	}

    void GoUnderZone(int ID, int player, int setRank)
    {
        if (movePhase[player] == 0 && moveCount[player] == 0)
        {
            moveID[player] = ID;
            moveTime[player] = standartTime;
            destination[player] = vec3Add(SIGNIZONE, new Vector3(SigniWidth * setRank + 0.5f, 0.025f * underCards[setRank].Count, 0f));
            rotaPhase[player] = 0;
        }

        if (movePhase[player] == 1 && moveCount[player] == 0)
        {
            ExitFunction(moveID[player], player);

            field[player % 2, moveID[player]] = Fields.UnderZone;
            fieldRank[player % 2, moveID[player]] = setRank;

            underCards[setRank].Add(moveID[player] + player % 2 * 50);

            moveID[player] = -1;
        }

        if (rotaPhase[player] == 0 && rotaID[player] == -1)
        {
            rotaID[player] = moveID[player];
            rotaTime[player] = moveTime[player];
            angle[player, 0] = 0f;
            angle[player, 1] = 0f;
            angle[player, 2] = 0f;
        }

        if (rotaPhase[player] == 1 && rotaID[player] != -1)
        {
            rotaID[player] = -1;
        }
    }

    void SigniRise(int player, int rank)
    {
        if (movePhase[player] == 0 && moveID[player] == -1)
        {
            moveID[player] = fieldRankID(Fields.SIGNIZONE, rank, player % 2);

            if (moveID[player] == -1)
                return;

            moveTime[player] = standartTime;
            destination[player] = vec3Add(SIGNIZONE, new Vector3(SigniWidth * rank, 1f, 0f));
        }

        if (movePhase[player] == 1 && moveID[player] != -1)
            moveID[player] = -1;
    }

    void SigniFall(int player, int rank)
    {
        if (movePhase[player] == 0 && moveID[player] == -1)
        {
            moveID[player] = fieldRankID(Fields.SIGNIZONE, rank, player % 2);

            if (moveID[player] == -1)
                return;

            moveTime[player] = standartTime;
            destination[player] = vec3Add(SIGNIZONE, new Vector3(SigniWidth * rank, underCards[rank].Count*0.025f, 0f));
        }

        if (movePhase[player] == 1 && moveCount[player] == 0)
            moveID[player] = -1;
    }

	void copySigInfo(){
		for (int i = 0; i < 3; i++)
		{
			signiConditionBuf[0,i] = signiCondition[0,i];
			signiConditionBuf[1,i] = signiCondition[1,i];

			SigniPowerUpValueBuf[0,i] = SigniPowerUpValue[0,i];
			SigniPowerUpValueBuf[1,i] = SigniPowerUpValue[1,i];
		}
	}

	void SetCardParent(int parentID,int childID,int player)
	{
		card[player,childID].transform.parent = card[player,parentID].transform;
	}

	void CharmDoking(int player)
	{
		for (int i = 0; i < 3; i++)
		{
			int p=fieldRankID(Fields.SIGNIZONE,i,player);
			int c=fieldRankID(Fields.CharmZone,i,player);

			if(p>=0 && c>=0)
			{
				SetCardParent(p,c,player);
			}
		}
	}

	void CharmIndependence(int player)
	{
		for (int i = 0; i < 3; i++)
		{
			int c=fieldRankID(Fields.CharmZone,i,player);

			if(c>=0 && card[player,c].transform.parent!=null)
			{
				int id=GetCardObjID(player,card[player,c].transform.parent.gameObject);

				fieldRank[player,c] = fieldRank[player,id];

				card[player,c].transform.parent=null;
			}
		}
	}

	int GetCardObjID(int player, GameObject obj)
	{
		for (int i = 0; i < 50; i++)
		{
			if(card[player,i]==obj)
				return i;
		}

		return -1;
	}

	void RePosition(int player,int selecter){
		if(!isTrans && !replayMode)
			selecter=0;

		int mode=( selecter + player % 2 ) % 2;

		if (movePhase [player] == 0 && moveCount [player] == 0)
		{
			CharmDoking(player%2);
			moveTime [player] = standartTime;
			
			if(mode==0)//player%2==0 || isTrans)
				ReposiParts_1(0,player);
			else ReposiParts_1(2,player);
		}

		if (movePhase [player] == 1 && moveCount [player] == 0)
		{
			ReposiParts_1(1,player);
		}

		if (movePhase [player] == 2 && moveCount [player] == 0)
		{
			if(mode==0)//player%2==0 || isTrans)
				ReposiParts_1(2,player);
			else ReposiParts_1(0,player);
		}

		if (movePhase [player] == 3 && moveCount [player] == 0)
		{
			moveID [player]=-1;
			ReposiFlag=true;
		}
	}

	void ReposiParts_1(int rank,int player){
		moveID [player] = fieldRankID(Fields.SIGNIZONE,rank,player % 2);
		
		if(moveID [player]==-1)
			movePhase [player]++;
		else
		{
			ReposiID.Add(moveID [player] );



			destination [player] = vec3Add (card [player % 2, moveID [player]].transform.position, new Vector3 (0f, 1f, 0f));
			
			if (player % 2 == 1)
			{
				destination [player] = vec3Player2 (destination [player]);	

				destination [player]=vec3Addz(destination [player],-1f);
			}
			else 
				destination [player]=vec3Addz(destination [player],-3f);
		}
	}

	void MoveSigniPosition (int ID, int player,int rank)
	{
		if (movePhase [player] == 0 )
		{
			CharmDoking(player%2);

			if(fieldRankID(Fields.SIGNIZONE,rank,player%2)!=-1)
			{
				PosiChangeID=fieldRankID(Fields.SIGNIZONE,rank,player%2);

				movePhase [player] = 1;
			}
			else {
				movePhase [player] = 2;
				moveID [player]=-2;
			}
		}

		
		if (movePhase [player] ==  1 && moveID [player] == -1)
		{
			if(ReposiFlag)
				movePhase [player]++;
			else{
				moveID [player] = PosiChangeID;
				destination [player] = vec3Add (card [player % 2, moveID [player]].transform.position, new Vector3 (0f, 1f, 0f));

				if (player % 2 == 1)
					destination [player] = vec3Player2 (destination [player]);

				moveTime [player] = standartTime;
			}
		}

		if (movePhase [player] == 2 && moveCount [player] == 0 )
		{
			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = vec3Add (SIGNIZONE, new Vector3 (SigniWidth * rank, 0f, 0f));

			copySigInfo();
		}
		
		if (movePhase [player] == 3 && moveID [player] != -1)
		{
			int origiRank=fieldRank [player % 2, moveID [player]];

			signiCondition[player%2,rank]=signiConditionBuf[player%2,origiRank];
			signiCondition[player%2,origiRank]=Conditions.no;

			SigniPowerUpValue[player%2,rank]=SigniPowerUpValueBuf[player%2,origiRank];
			SigniPowerUpValue[player%2,origiRank]=0;

			fieldRank [player % 2, moveID [player]] = rank;

			if(PosiChangeID<0)
			{
				CharmIndependence(player%2);
				moveID [player] = -1;
				Repositioned[rank]=true;
			}
			else
			{	
				moveID [player] = PosiChangeID;
				PosiChangeID=-1;

				signiCondition[player%2,origiRank]=signiConditionBuf[player%2,rank];		
				SigniPowerUpValue[player%2,origiRank]=SigniPowerUpValueBuf[player%2,rank];

				fieldRank [player % 2, moveID [player]] = origiRank;

				if(ReposiFlag)
				{
					Repositioned[rank]=true;
					moveID [player] = -1;
					CharmIndependence(player%2);
				}
				else{
					destination [player] = vec3Add (SIGNIZONE, new Vector3 (SigniWidth * origiRank, 0f, 0f));
					movePhase [player] = 4 ;
				}
			}
		}

		if (movePhase [player] == 5 && moveID [player] != -1){
			moveID [player] = -1;
			CharmIndependence(player%2);
		}
	}


	void GoLrigTrash (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = vec3Add (LRIGTRASH, new Vector3 (0f, 0.025f * lrigTrashNum [player % 2], 0f));
			rotaPhase [player] = 0;
		}

		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			ExitFunction (moveID [player], player);
			field [player % 2, moveID [player]] = Fields.LRIGTRASH;
			fieldRank [player % 2, moveID [player]] = lrigTrashNum [player % 2];
			lrigTrashNum [player % 2]++;
			moveID [player] = -1;
		}

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 0f;
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
	}

	void GoShowZone (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = vec3Add (SHOWZONE, new Vector3 (EnaWidth * ShowZoneNum [player % 2], 0.025f * ShowZoneNum [player % 2], 0f ));
			rotaPhase [player] = 0;
		}

		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			ShowZoneIDList.Add(moveID [player] + player%2*50);
			ShowZoneNum [player % 2]++;
			moveID [player] = -1;
		}

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 0f;
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
	}

	void GoLrigDeck (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = vec3Add (LRIGDECK, new Vector3 (0f, 0.025f * lrig_deckNum [player % 2], 0f));
			rotaPhase [player] = 0;
		}
		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			ExitFunction (moveID [player], player);
			field [player % 2, moveID [player]] = Fields.LRIGDECK;
			fieldRank [player % 2, moveID [player]] = lrig_deckNum [player % 2];
			lrig_deckNum [player % 2]++;
			moveID [player] = -1;
		}
		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 180f;
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
	}
	
	void GoTrash (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			Singleton<SoundPlayer>.instance.playSE("draw");

			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = vec3Add (TRASH, new Vector3 (0f, 0.025f * trashNum [player % 2], 0f));
			rotaPhase [player] = 0;
		}
		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			if(tellEffectID==ID+player%2*50)
				EffectGoTrashID=tellEffectID;


			//本題
			ExitFunction (moveID [player], player % 2);
			field [player % 2, moveID [player]] = Fields.TRASH;
			fieldRank [player % 2, moveID [player]] = trashNum [player % 2];
			trashNum [player % 2]++;
			moveID [player] = -1;
		}
		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 0f;
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
	}
	

/*	void GoTrash_p1(int ID){
		if(movePhase[0]==0 && moveID[0]==-1){
			moveID[0]=ID;
			moveTime[0]=standartTime;
			destination[0]=vec3Add(TRASH,new Vector3(0f,0.025f*trashNum[0],0f));
			rotaPhase[0]=0;
		}
		if(movePhase[0]==1 && moveID[0]!=-1){
			ExitFunction(moveID[0],0);
			field[0,moveID[0]]=Fields.TRASH;
			fieldRank[0,moveID[0]]=trashNum[0];
			trashNum[0]++;
			moveID[0]=-1;
		}
		if(rotaPhase[0]==0 && rotaID[0]==-1){
			rotaID[0]=moveID[0];
			rotaTime[0]=moveTime[0];
			angle[0,0]=0f;
			angle[0,1]=0f;
			angle[0,2]=0f;
		}
		if(rotaPhase[0]==1 && rotaID[0]!=-1){
			rotaID[0]=-1;
		}
	}*/
	
	void GoCheckZone (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = CHECKZONE;
			rotaPhase [player] = 0;
		}

		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			if (field [player % 2, moveID [player]] == Fields.LIFECLOTH)
			{
                if (!notBurst)
                {
                    card[player % 2, moveID[player]].GetComponent<CardScript>().BurstFlag = true;
                    BurstUpFlag = true;
                }

                crashedID = moveID[player] + player % 2 * 50;
			}

			ExitFunction (moveID [player], player);

            if (notBurst)
            {
                field[player % 2, moveID[player]] = Fields.Non;
                fieldRank[player % 2, moveID[player]] = 0;
            }
            else
            {
                field[player % 2, moveID[player]] = Fields.CHECKZONE;
                fieldRank[player % 2, moveID[player]] = 0;
            }

			moveID [player] = -1;
		}

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 0f;
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}		
	}

	void GoYourCheckZone (int ID, int player)
	{
		GoCheckZone(ID,player);

		if (movePhase [player] == 0 && moveCount[player]==0)
		{
			destination[player]=vec3Player2(destination[player]);
		}

		if (movePhase [player] == 1 && moveCount [player] == 0)
		{
			field [player % 2, ID] = Fields.YOURCHECKZONE;

			creatCopyCard(ID,player % 2,Fields.CHECKZONE);
		}
	}

	void Down (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1 && rotaPhase [player] == -1)
		{
			rotaPhase [player] = 0;
		}

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = ID;
			rotaTime [player] = standartTime;
			angle [player, 0] = 0f;
			angle [player, 1] = -90f;
			angle [player, 2] = 0f;
		}

		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			if (field [player % 2, rotaID [player]] == Fields.SIGNIZONE)
			{
				signiCondition [player % 2, fieldRank [player % 2, rotaID [player]]] = Conditions.Down;
			}
			else if (field [player % 2, rotaID [player]] == Fields.LRIGZONE)
			{
				LrigCondition [player % 2] = Conditions.Down;
			}
			rotaID [player] = -1;
		}		
	}
	
	void Up (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1 && rotaPhase [player] == -1)
		{
			rotaPhase [player] = 0;
		}

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = ID;
			rotaTime [player] = standartTime;
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 0f;
		}

		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			if (field [player % 2, ID] == Fields.SIGNIZONE)
				signiCondition [player % 2, fieldRank [player % 2, ID]] = Conditions.Up;

			else if (field [player % 2, ID] == Fields.LRIGZONE)
				LrigCondition [player % 2] = Conditions.Up;

			rotaID [player] = -1;
		}				
	}

	void OpenHand (int player)
	{
		if (movePhase [player] == 0 && moveCount [player] == 0)
		{
			int ID=fieldRankID(Fields.HAND,HandOpenCount,player%2);

			if(ID<0)return;

			moveID [player] = ID;
			destination [player] = vec3Add (card [player % 2, ID].transform.position, new Vector3 (0f, 1f, 0f));
			if (player % 2 == 1)
				destination [player] = vec3Player2 (destination [player]);

			rotaID [player] = moveID [player];
			rotaPhase [player] = 0;
		}

		if (movePhase [player] == 1 && moveCount [player] == 0)
		{
			int ID=fieldRankID(Fields.HAND,HandOpenCount,player%2);

			destination [player] = vec3Add (card [player % 2, ID].transform.position, new Vector3 (0f, -1f, 0f));
			if (player % 2 == 1)
				destination [player] = vec3Player2 (destination [player]);
		}

		if (movePhase [player] == 2)
		{
			moveID [player] = -1;

			if(rotaID[player]==-2)
				movePhase[player]=0;
		}		

		if (rotaPhase [player] == 0 && rotaCount [player] == 0)
		{
//			int ID = rotaID [player];

			setHandAngle(player);

			angle [player, 2] = 0f;
			
			rotaTime [player] = standartTime;
		}
		
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			if(fieldAllNum(Fields.HAND,player%2)>HandOpenCount+1)
			{
				HandOpenCount++;
				rotaID[player]=-2;
				rotaPhase[player]=-1;
			}
			else{
				HandOpenCount=0;
				rotaID [player] = -1;
			}
		}
	}

	void CloseHand (int player)
	{
		if (movePhase [player] == 0 && moveCount [player] == 0)
		{
			int ID=fieldRankID(Fields.HAND,HandOpenCount,player%2);

			if(ID<0)return;

			moveID [player] = ID;
			destination [player] = vec3Add (card [player % 2, ID].transform.position, new Vector3 (0f, 1f, 0f));
			if (player % 2 == 1)
				destination [player] = vec3Player2 (destination [player]);
			
			rotaID [player] = moveID [player];
			rotaPhase [player] = 0;
		}
		
		if (movePhase [player] == 1 && moveCount [player] == 0)
		{
			int ID=fieldRankID(Fields.HAND,HandOpenCount,player%2);
			
			destination [player] = vec3Add (card [player % 2, ID].transform.position, new Vector3 (0f, -1f, 0f));
			if (player % 2 == 1)
				destination [player] = vec3Player2 (destination [player]);
		}
		
		if (movePhase [player] == 2)
		{
			moveID [player] = -1;
			
			if(rotaID[player]==-2)
				movePhase[player]=0;
		}		
		
		if (rotaPhase [player] == 0 && rotaCount [player] == 0)
		{
//			int ID = rotaID [player];
			
			setHandAngle(player);
			
			rotaTime [player] = standartTime;
		}
		
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			if(fieldAllNum(Fields.HAND,player%2)>HandOpenCount+1)
			{
				HandOpenCount++;
				rotaID[player]=-2;
				rotaPhase[player]=-1;
			}
			else{
				HandOpenCount=0;
				rotaID [player] = -1;
			}
		}
	}

	void Open (int ID, int player)
	{
        if (ID >= 0 && field[player % 2, ID] == Fields.HAND && (DebugFlag || player % 2 == 0))
        {
            setAnimation(ID + player % 2 * 50);
            return;
        }

        if (movePhase[player] == 0 && moveCount[player] == 0)
		{
			moveID [player] = ID;
			destination [player] = vec3Add (card [player % 2, ID].transform.position, new Vector3 (0f, 1f, 0f));
			if (player % 2 == 1)
				destination [player] = vec3Player2 (destination [player]);
			rotaID [player] = moveID [player];
			rotaPhase [player] = 0;
		}

		if (rotaPhase [player] == 0 && rotaCount [player] == 0)
		{
			angle [player, 0] = card [player % 2, ID].transform.localEulerAngles.x;
            if (player % 2 == 1)
                angle[player, 0] *= -1;

			angle [player, 1] = card [player % 2, ID].transform.localEulerAngles.y;
			if (player % 2 == 1)
				angle [player, 1] += 180;

			angle [player, 2] = 0f;

			rotaTime [player] = moveTime [player];
		}

		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}

		if (movePhase [player] == 1 && moveCount [player] == 0)
		{
			destination [player] = vec3Add (card [player % 2, ID].transform.position, new Vector3 (0f, -1f, 0f));
			if (player % 2 == 1)
				destination [player] = vec3Player2 (destination [player]);
		}
		if (movePhase [player] == 2)
		{
			setAnimation (moveID [player] + player % 2 * 50);
			moveID [player] = -1;
		}		
	}

	void Close (int ID, int player)
	{
        if (ID >= 0 && field[player % 2, ID] == Fields.HAND && (DebugFlag || player%2 == 0))
            return;

		if (movePhase [player] == 0 && moveCount [player] == 0)
		{
			moveID [player] = ID;
			destination [player] = vec3Add (card [player % 2, ID].transform.position, new Vector3 (0f, 1f, 0f));
			if (player % 2 == 1)
				destination [player] = vec3Player2 (destination [player]);
			rotaID [player] = moveID [player];
			rotaPhase [player] = 0;
		}

		if (rotaPhase [player] == 0 && rotaCount [player] == 0)
		{
			angle [player, 0] = card [player % 2, ID].transform.localEulerAngles.x;
            if (player % 2 == 1)
                angle[player, 0] *= -1;

            angle[player, 1] = card[player % 2, ID].transform.localEulerAngles.y;
			if (player % 2 == 1)
				angle [player, 1] += 180;
			angle [player, 2] = 180f;
			rotaTime [player] = moveTime [player];
		}

		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}

		if (movePhase [player] == 1 && moveCount [player] == 0)
		{
			destination [player] = vec3Add (card [player % 2, ID].transform.position, new Vector3 (0f, -1f, 0f));
			if (player % 2 == 1)
				destination [player] = vec3Player2 (destination [player]);
		}
		if (movePhase [player] == 2)
		{
			moveID [player] = -1;
		}		
	}
	
	void rankDown (int ID, int player)
	{
/*		if (player == 0)
			rankDown (ID);
		else if (player == 1)
			rankDown_p2 (ID);
*/        
        int rank = fieldRank[player, ID];
        int num = fieldAllNum(field[player, ID],player);
        for (int i = rank + 1; i < num; i++)
        {
            int DownID = fieldRankID(field[player, ID], i,player);
            if (DownID >= 0)
                fieldRank[player, DownID]--;
        }

	}

/*	void rankDown (int ID)
	{
		int rank = fieldRank [0, ID];
		int num = fieldAllNum (field [0, ID]);
		for (int i=rank+1; i<num; i++)
		{
			int DownID = fieldRankID_p1 (field [0, ID], i);
			if (DownID >= 0)
				fieldRank [0, DownID]--;
		}
	}

	void rankDown_p2 (int ID)
	{
		int rank = fieldRank [1, ID];
		int num = fieldAllNum_p2 (field [1, ID]);
		for (int i=rank+1; i<num; i++)
		{
			int DownID = fieldRankID_p2 (field [1, ID], i);
			if (DownID >= 0)
				fieldRank [1, DownID]--;
		}
	}*/
	
	int fieldAllNum (Fields f, int player)
	{
        int num = 0;
        for (int i = 0; i < 50; i++)
        {
            if (field[player, i] == f)
                num++;
        }
        return num;
    }

/*	int fieldAllNum (Fields f)
	{
		int num = 0;
        int player=0;

	}

	int fieldAllNum_p2 (Fields f)
	{
		int num = 0;
		for (int i=0; i<50; i++)
		{
			if (fieldRankID_p2 (f, i) != -1)
				num++;
		}
		return num;
	}*/


	//テキストから変換 DataToStringにまとめた
/*	string TypeToString (int x)
	{
		if (x == 0)
			return "ルリグ";
		else if (x == 1)
			return "アーツ";
		else if (x == 2)
			return "シグニ";
		else if (x == 3)
			return "スペル";
		return "";
	}

	string ColorToString (int x)
	{
		if (x == 0)
			return "無";
		else if (x == 1)
			return "白";
		else if (x == 2)
			return "赤";
		else if (x == 3)
			return "青";
		else if (x == 4)
			return "緑";
		else if (x == 5)
			return "黒";
		return "";
	}

	string CostToString (int[] x)
	{
		int num = 0;
		string reString = "【";//コスト: ";
		for (int i=0; i<x.Length; i++)
		{
			if (x [i] > 0)
			{
				num++;
				reString += ColorToString (i) + "×" + x [i] + " ";
			}
		}
		if (num == 0)
			reString += "×0";
		reString += "】\n";
		return reString;
	}

	string LrigTypeToString (int x)
	{
		if (x == 0)
			return "なし";
		else if (x == 1)
			return "タマ";
		else if (x == 2)
			return "花代";
		else if (x == 3)
			return "ピルルク";
		else if (x == 4)
			return "緑子";
		else if (x == 5)
			return "ウリス";
		else if (x == 6)
			return "エルドラ";
		else if (x == 7)
			return "ユヅキ";
		else if (x == 8)
			return "ウムル";
		else if (x == 9)
			return "イオナ";
		else if (x == 10)
			return "リメンバ";
		else if (x == 11)
			return "ミルルン";
		else if (x == 12)
			return "アン";
		return "";
	}

	string ClassToString (int x, int y)
	{
		if (x == 0)
			return "精元";
		else if (x == 1)
		{
			if (y == 0)
				return "精武:アーム";
			else if (y == 1)
				return "精武:ウェポン";
			else if (y == 2)
				return "精武:毒牙";
		}
		else if (x == 2)
		{
			if (y == 0)
				return "精羅:鉱石";
			else if (y == 1)
				return "精羅:宝石";
			else if (y == 2)
				return "精羅:植物";
			else if (y == 3)
				return "精羅:原子";			
		}
		else if (x == 3)
		{
			if (y == 0)
				return "精械:電機";
			else if (y == 1)
				return "精械:古代兵器";
			else if (y == 2)
				return "精械:迷宮";
		}
		else if (x == 4)
		{
			if (y == 0)
				return "精像:天使";
			else if (y == 1)
				return "精像:悪魔";
			else if (y == 2)
				return "精像:美巧";	
		}
		else if (x == 5)
		{
			if (y == 0)
				return "精生:水獣";
			else if (y == 1)
				return "精生:地獣";
			else if (y == 2)
				return "精生:空獣";
			else if (y == 3)
				return "精生:龍獣";
		}
		return "";
	}*/
	
	void setComponent (int ID, int player)
	{
		CardScript script = card [player, ID].GetComponent<CardScript> ();
		script.ID = ID;
		script.player = player;		
		script.Brain = front [player, ID];

		setLoadText(ID,player,script);
	}

	void setLoadText(int ID,int player,CardScript script){
//		string[] str = SerialNumString [player, ID].Split ('-');		
//		TextAsset textAsset = (TextAsset)Resources.Load (str [0] + "/" + SerialNumString [player, ID] + "data");
		
		script.Manager = this.gameObject;
		
		script.SerialNumString=SerialNumString [player, ID];

        script.effectSelecter = script.player;

        script.LoadCardStatus(script.SerialNumString);
/*		string[] s = textAsset.text.Split (' ', '\n', '\r');
		
		for (int i=0; i<s.Length; i++)
		{
			if (s [i] == "#Name")
			{
				script.Name = s [i + 1];

//				if(!s [i + 2].Contains("#") && s[i+2] != string.Empty)
//					script.Name += " "+s[ i+2 ];
				 
			}
			else if (s [i] == "#Type")
				script.Type = int.Parse (s [i + 1]);
			else if (s [i] == "#Color"){
				script.CardColor = int.Parse (s [i + 1]);
				script.OrigColor = script.CardColor;
			}
			else if (s [i] == "#Level")
				script.Level = int.Parse (s [i + 1]);
			else if (s [i] == "#Cost")
			{
				string[] cost = s [i + 1].Split ('/');
				for (int j=0; j<script.Cost.Length; j++)
					script.Cost [j] = int.Parse (cost [j]);
			}
			else if (s [i] == "#GrowCost")
			{
				string[] cost = s [i + 1].Split ('/');
				for (int j=0; j<script.GrowCost.Length; j++)
				{
					script.GrowCost [j] = int.Parse (cost [j]);
				}
			}
			else if (s [i] == "#Limit")
				script.Limit = int.Parse (s [i + 1]);
			else if (s [i] == "#LrigType")
				script.LrigType = int.Parse (s [i + 1]);
			else if (s [i] == "#LrigType2")
				script.LrigType_2 = int.Parse (s [i + 1]);
			else if (s [i] == "#LrigLimit")
				script.LrigLimit = int.Parse (s [i + 1]);
			else if (s [i] == "#LrigLimit2")
				script.LrigLimit_2 = int.Parse (s [i + 1]);
			else if (s [i] == "#Class")
			{
				string[] classString = s [i + 1].Split (':');
				script.Class_1 = int.Parse (classString [0]);
				script.Class_2 = int.Parse (classString [1]);
			}
			else if (s [i] == "#Power")
			{
				script.basePower = int.Parse (s [i + 1]);
				script.OriginalPower = script.basePower;
			}
			else if (s [i] == "#BurstIcon")
			{
				if (s [i + 1].Contains ("True"))
					script.BurstIcon = 1;
				else if (s [i + 1].Contains ("False"))
					script.BurstIcon = 0;
			}
			else if (s [i].Contains("#Text"))
			{
				
				for (int j=i+1; j<s.Length; j++)
				{
					if(s[j] != "")
                        script.Text += s [j] + "\n";
				}
				
				break;
			}
		}	*/
	}

	void showUpDate (int ID, int player)
	{	
		string key=SerialNumString[player, ID];
//		Texture tex = Singleton<pics>.instance.getTexture(key);
//		showTexture = tex;

//		CardScript script = card [player, ID].GetComponent<CardScript> ();

        showSerialNum = key;
        if (showCardText != null && key != "")
            showCardText.text = Singleton<DataToString>.instance.SerialNumToString(key);

        if (showCardImage != null && key != "")
            showCardImage.texture = Singleton<pics>.instance.getTexture(key);


/*		showCardName = script.Name;
		showCardText = script.Text;
		showCardType = script.Type;
		showCardColor = script.CardColor;
		showCardLevel = script.Level;

		for (int i=0; i<showCardCost.Length; i++)
			showCardCost [i] = script.Cost [i];

		for (int i=0; i<showCardGrowCost.Length; i++)
			showCardGrowCost [i] = script.GrowCost [i];

		showCardLimit = script.Limit;
		showCardLrigType = script.LrigType;
		showCardLrigType2 = script.LrigType_2;


		showCardLrigLimit = script.LrigLimit;
		showCardLrigLimit_2 = script.LrigLimit_2;

		showCardClass_1 = script.Class_1;	
		showCardClass_2 = script.Class_2;	
		showCardPower = script.OriginalPower;
//		showCardBurstIcon=script.BurstIcon;*/
	}
	
	void SigniZoneUp (int player, bool all=false)
	{
		bool flag = checkSigniRockFlag (player);
		
		for (int i=0; i<TargetSigniZoneCursor.Length; i++)
		{
            bool check = signiCondition [player, i] == Conditions.no 
                && SigniNotSummonCount[player, i] == 0
                && (!flag || signiRockFlag [player, i]);

            if (all)
                check = true;

			if (check)
			{
				if (player == 0)
					TargetSigniZoneCursor [i].transform.position = vec3Add (SIGNIZONE, new Vector3 (SigniWidth * i, 0.025f, 0f));
				else
					TargetSigniZoneCursor [i].transform.position = vec3Player2 (vec3Add (SIGNIZONE, new Vector3 (SigniWidth * i, 0.025f, 0f)));
			}
		}
	}

	void RePosiZoneUp (int player)
	{
		for (int i=0; i<TargetSigniZoneCursor.Length; i++)
		{
			if (Repositioned[i]==false)
			{
				if (player == 0)
					TargetSigniZoneCursor [i].transform.position = vec3Add (SIGNIZONE, new Vector3 (SigniWidth * i, 0f, 0f));
				else
					TargetSigniZoneCursor [i].transform.position = vec3Player2 (vec3Add (SIGNIZONE, new Vector3 (SigniWidth * i, 0f, 0f)));
			}
		}
	}

	void SigniZoneDown ()
	{
		for (int i=0; i<TargetSigniZoneCursor.Length; i++)
		{
			TargetSigniZoneCursor [i].transform.position = STORAGEZONE;
		}
	}
	
	bool checkSigniRockFlag (int player)
	{
		for (int i=0; i<3; i++)
		{
			if (signiRockFlag [player, i] && signiCondition [player, i] == Conditions.no)
				return true;
		}
		return false;
	}

	void setTargetCursorField (Fields f, int player)
	{
		if (f != Fields.SIGNIZONE)
		{
			int length = fieldAllNum (f,player);

			for (int i = 0; i < length; i++)
			{
				int id = fieldRankID (f, i, player);
				setTargetCursorID (id, player);
			}
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				int id = fieldRankID (f, i, player);
				if (id > 0)
				{
					setTargetCursorID (id, player);
				}
			}
		}
	}
	
	void setTargetCursorUp (int player)
	{
		//signi
        if (!SigniAttackSkipFlag)
        {
            for (int i = 0; i < 3; i++)
            {
                int id = fieldRankID(Fields.SIGNIZONE, i, player);
                if (id >= 0 && getIDConditionInt(id, player) == 1 && getCardScr(id, player).Attackable)
                    setTargetCursorID(id, player);
            }
        }

		//lrig
		if (LrigCondition [player] == Conditions.Up 
            && !LrigAttackSkipFlag
		    && getCardScr(getLrigID(player),player).Attackable
		    && (!ionaFlag [getTurnPlayer ()] || (ionaFlag [getTurnPlayer ()] && isAttackEnd ())))
			setTargetCursorID (getLrigID (player), player);
	}
	
	GameObject creatTargetCursor (Vector3 vec)
	{
		GameObject obj = (GameObject)Instantiate (
			Resources.Load ("targetCursor"),
			vec,
			Quaternion.identity
		);
		obj.transform.parent = this.transform;
		
		return obj;
	}

	void setTargetCursorID (int id, int player)
	{
		targetCursorList.Add (
			creatStructCursor (vec3Addy (card [player, id].transform.position, 0.03f), id, player)
		);
		if (field [player, id] == Fields.HAND)
		{// && player==0){
			targetCursorList [targetCursorList.Count - 1].targetCursor.transform.rotation = Quaternion.AngleAxis (-20f, new Vector3 (1, 0, 0));
		}
		else if (field [player, id] == Fields.LIFECLOTH || field [player, id] == Fields.ENAZONE)
		{
			targetCursorList [targetCursorList.Count - 1].targetCursor.transform.rotation = Quaternion.AngleAxis (90f, new Vector3 (0, 1, 0));
		}
		else if (field [player, id] == Fields.SIGNIZONE && signiCondition [player, fieldRank [player, id]] == Conditions.Down)
		{
			targetCursorList [targetCursorList.Count - 1].targetCursor.transform.rotation = Quaternion.AngleAxis (90f, new Vector3 (0, 1, 0));
		}
	}

	TargetCursor creatStructCursor (Vector3 vec3, int id, int player)
	{
		return new TargetCursor (creatTargetCursor (vec3), id, player);
	}
	
	void DestoroyCursor (int a)
	{
		if (targetCursorList.Count > a)
		{
			DestroyObject (targetCursorList [a].targetCursor);
			targetCursorList.RemoveAt (a);
		}
	}

	void DestoryCursorAll ()
	{
		while (targetCursorList.Count>0)
			DestoroyCursor (0);
	}
	
	void PrePhaseMove ()
	{
		//motion決定
		if (moveID  [0] == -1 && rotaID [0] == -1 && moveID [1] == -1 && rotaID [1] == -1)
		{
			Motions motionBuf = motion;
			if (lrigZoneNum [prePlayer] == 0 && GUImoveID != -1)
			{
				motion = Motions.LrigSet;
			}
			else if (motion == Motions.Mulligan && clickCursorID.Count == 0)
			{
				motion = Motions.LifeClothSet;
			}
			else if (motion == Motions.Draw && drawNum [prePlayer] == 0)
			{
				if (notMuligan)
				{
					motion = Motions.LifeClothSet;
					notMuligan = false;
				}
				else 
				{
					motion = Motions.Mulligan;

					if(replayMode && prePlayer == 0)
					{
						while(sentList.Count > sentIndex && sentList[sentIndex]!=sprt)
						{
							clickCursorID.Add(int.Parse(replayRead(0)));
						}

						//sprtを捨てる
						replayRead(0);

						//shuffleのID
						while (replayNextReading(0)!=errorStr)
						{
							string s = replayRead (0);
							if (s == sprt)
							{
								break;
							}
							shuffleBuf.Add (s);
						}

						//入力がなければマリガンしない
						if(clickCursorID.Count==0)
							return;
					}
				}
			}
			else if (motion == Motions.LrigSet && GUImoveID == -1)
			{
				motion = Motions.Draw;
			}
			else if (motion == Motions.LifeClothSet && movePhase [prePlayer] == 1)
			{
				if (prePlayer == 0)//replay
				{
					if(replayMode)
					{
						prePlayer = 1;
						//ルリグのID
						GUImoveID = IdTrans( int.Parse( replayRead(1) ) );	

						//マリガンのID
						while (receivedList.Count > receivedIndex && receivedList[receivedIndex]!=sprt)
						{
							int id=int.Parse( replayRead(1) );
							clickCursorID.Add ( IdTrans(id) );
						}
						
						if (clickCursorID.Count == 0)
						{
							notMuligan = true;
						}
						
						if (receivedList.Count > receivedIndex && receivedList[receivedIndex]==sprt)
						{
							replayRead (1);
						}
						
						//shuffleのID
						while (receivedList.Count > receivedIndex)
						{
							string s = replayRead (1);
							if (s == sprt)
							{
								break;
							}
							shuffleBuf.Add (s);
						}

						
					}
					else if (isTrans)//通信
					{
						if (canRead ())
						{
							prePlayer = 1;
							//ルリグのID
							GUImoveID = readParseMessage();

							//マリガンのID
							while (messages.Count>0 && messages[0]!=sprt)
							{
                                int clID = readParseMessage();
                                if (clID >= 0)
                                    clickCursorID.Add(clID);
							}

							if (clickCursorID.Count == 0)
							{
								notMuligan = true;
							}
							
							if (messages.Count > 0 && messages [0] == sprt)
							{
								readMessage ();
							}

							//shuffleのID
							while (messages.Count>0)
							{
								string s = readMessage ();
								if (s == sprt)
								{
									break;
								}
								shuffleBuf.Add (s);
							}
							//自分のデータ送信
							if (!isServer)
							{
								messagesBuf.Add (sprt);
								sendMessageBuf ();
							}
						}
						else if (isServer)
						{
							if (messages.Count == 0)
							{
								messagesBuf.Add (sprt);
								sendMessageBuf ();
							}
						}
					}
					else
					{
						prePlayer = 1;
						selectCardFlag = true;
						selectCardListLrigIn (1);
					}
					return;
				}
				else //if(Input.GetMouseButtonDown(0)){
					motion = Motions.LrigOpen;
//				}
			}
			else if (motion == Motions.LrigOpen)
			{
				phase = Phases.UpPhase;
				motion = Motions.NonMotion;
			}

			//movePhase 初期化
			if (motion != motionBuf)
			{
				movePhase [0] = 0;
				rotaPhase [0] = -1;
				movePhase [1] = 0;
				rotaPhase [1] = -1;
			}
		}

		//motionごとに行先などを決める
		if (prePlayer == 0)
		{
			//ドロー
			if (motion == Motions.Draw)
			{
				DrawCard (0);
			}

			//マリガン
			if (motion == Motions.Mulligan)
			{
				if (clickCursorID.Count > 0 && messagesBuf.Count > 0 && messagesBuf [messagesBuf.Count - 1] != sprt)
				{
					messagesBuf.Add (sprt);
				}

				if (moveID [1] == -1 && rotaID [1] == -1)
					Muligan (0, 5);
			}

			//ルリグセット
			if (motion == Motions.LrigSet)
			{
				if (movePhase [0] == 0 && moveID [0] == -1)
				{
					moveID [0] = GUImoveID;
					destination [0] = LRIGZONE;
					moveTime [0] = standartTime;
					GUImoveID = -1;
					waitTime = 1;
					return;
				}

				if (movePhase [0] == 1 && moveID [0] != -1)
				{
					rankDown (moveID [0],0);
					field [0, moveID [0]] = Fields.LRIGZONE;
					fieldRank [0, moveID [0]] = lrigZoneNum [0];
					lrigZoneNum [0]++;
					moveID [0] = -1;
					lrig_deckNum [0]--;
				}
			}

			//ライフクロス　セット
			if (motion == Motions.LifeClothSet)
			{
				LifeClothSet (fieldRankID (Fields.MAINDECK, deckNum [0] - 1, 0), 0);
				if (moveID [0] == -1 && rotaID [0] == -1)
				{
					if (LifeClothNum [0] < 7)
					{
						movePhase [0] = 0;
						moveID [0] = -2;
					}
				}
			}
		}
		else
		{
			if (motion == Motions.Draw)
			{
				DrawCard (1);
			}
			if (motion == Motions.Mulligan)
			{
				if (moveID [0] == -1 && rotaID [0] == -1 && !selectCursorFlag)
					Muligan (1, 5);
			}
			if (motion == Motions.LrigSet)
			{
				if (movePhase [1] == 0 && moveID [1] == -1 && GUImoveID >= 0)
				{
					moveID [1] = GUImoveID % 50;
					GUImoveID = -1;
					destination [1] = LRIGZONE;
					moveTime [1] = standartTime;
				}
				if (movePhase [1] == 1 && moveID [1] != -1)
				{
					rankDown (moveID [1],1);
					field [1, moveID [1]] = Fields.LRIGZONE;
					fieldRank [1, moveID [1]] = lrigZoneNum [1];
					lrigZoneNum [1]++;
					moveID [1] = -1;
					lrig_deckNum [1]--;
				}
			}
			if (motion == Motions.LifeClothSet)
			{
				LifeClothSet (fieldRankID (Fields.MAINDECK, deckNum [1] - 1, 1), 1);
				if (moveID [1] == -1 && rotaID [1] == -1)
				{
					if (LifeClothNum [1] < 7)
					{
						movePhase [1] = 0;
						moveID [1] = -2;
					}
				}
			}
			if (motion == Motions.LrigOpen)
			{
				Open (fieldRankID (Fields.LRIGZONE, 0, 0), 0);
				Open (fieldRankID (Fields.LRIGZONE, 0, 1), 1);
				UpdateLrigData (0);
				UpdateLrigData (1);

				if(moveCount [0] == 0 && movePhase [0]==0)
					Singleton<SoundPlayer>.instance.playSE("summon");
					
			}
		}
	}

	void MouseToWorld ()
	{
		//マウスのあたり判定
		Ray ray;
		RaycastHit hit;
		float distance = 100.0f; //光線を伸ばす距離

		//メインカメラのスクリーン上のポイントを光線に変換
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//光線がhitしているオブジェクトがあるかチェック
		//もし該当のオブジェクトがあればhitに格納される
		if (Physics.Raycast (ray, out hit, distance))
		{
			if (SelectedCard != hit.collider.gameObject)
			{	
				SelectedCard = hit.collider.gameObject;
				selectedID = cardID (SelectedCard);
				
				int ID = selectedID;
				bool frontFlag = isCardFront (SelectedCard);
				
				if (ID >= 0)
				{
					if (DebugFlag 
						|| (field [0, ID] != Fields.MAINDECK && field [0, ID] != Fields.LRIGDECK && field [0, ID] != Fields.LIFECLOTH))
					{
						showUpDate (ID, 0);
						selectPlayer = 0;
					}
				}
				if (selectedID < 0)
				{
					selectedID = cardID_p2 (SelectedCard);
					ID = selectedID;
					if (ID >= 0)
					{
						if (DebugFlag || frontFlag || (field [1, ID] == Fields.SIGNIZONE || field [1, ID] == Fields.LRIGZONE || field [1, ID] == Fields.ENAZONE))
						{
							showUpDate (ID, 1);
							selectPlayer = 1;
						}
					}
				}
				if (selectedID < 0)
				{
					for (int i=0; i<targetCursorList.Count; i++)
					{
						if (SelectedCard == targetCursorList [i].targetCursor)
						{
//							showUpDate(targetCursorList[i].ID,targetCursorList[i].player);
							
							ID = targetCursorList [i].ID;
							if (targetCursorList [i].player == 0)
							{
								if (DebugFlag ||
									(field [0, ID] != Fields.MAINDECK && field [0, ID] != Fields.LRIGDECK && field [0, ID] != Fields.LIFECLOTH))
								{
									showUpDate (targetCursorList [i].ID, targetCursorList [i].player);
								}
							}
							else
							{
								if (DebugFlag || field [1, ID] == Fields.SIGNIZONE || field [1, ID] == Fields.LRIGZONE || field [1, ID] == Fields.ENAZONE)
								{
									showUpDate (targetCursorList [i].ID, targetCursorList [i].player);
								}
							}
						}
					}
				}
			}
			//signizone select
			if (selectSigniZoneFlag)
			{
				int num = 0;
				for (int i=0; i<TargetSigniZoneCursor.Length; i++)
				{
					if (SelectedCard == TargetSigniZoneCursor [i] && Input.GetMouseButtonDown (0))
					{
						selectSigniZoneFlag = false;
						selectSigniZone = i;
						if (isTrans)
							messagesBuf.Add ("" + selectSigniZone);
						SigniZoneDown ();
						break;
					}
					if (signiCondition [selectSigniPlayer, i] != Conditions.no)
						num++;
				}
				if (num == signiSumLimit[selectSigniPlayer])
				{
					selectSigniZoneFlag = false;
					SigniZoneDown ();
				}
				if (selectSigniZoneFlag && Input.GetMouseButtonDown (1))
				{
					selectSigniZoneFlag = false;
					SigniZoneDown ();
				}
				if (!selectSigniZoneFlag && selectSigniZone == -1)
				{
					selectClickID = -1;
					messagesBuf.Clear ();
				}
			}

			//Ena select
			if (selectEnaFlag)
			{
				if (targetCursorList.Count == 0 && enaColorList.Count > 0)
				{
                    for (int i = 0; i < fieldAllNum(Fields.ENAZONE,selectEnaPlayer); i++)
                    {
                        int x = fieldRankID(Fields.ENAZONE, i, selectEnaPlayer);
                        int fID = getFusionID(x, selectEnaPlayer);
                        if (clickCursorID.Contains(fID) || !checkPayingTarget((cardColorInfo)enaColorList[selectEnaNum - 1], fID, melhenFlag[selectEnaPlayer]))
                            continue;

                        List<int> sub_payed = new List<int>(clickCursorID);
                        sub_payed.Add(fID);

                        List<int> sub_color = new List<int>();
                        for (int k = 0; k < selectEnaNum - 1; k++)
                            sub_color.Add(enaColorList[k]);

                        if (checkCost2nd(selectEnaPlayer, sub_color, sub_payed, melhenFlag[selectEnaPlayer]))
                            setTargetCursorID(x, selectEnaPlayer);
                    }
                    //アルゴリズムを変更した
/*					int mNum = MultiEnaNum (selectEnaPlayer);


                    int bNum = 0;
                    if(melhenFlag[selectEnaPlayer])
                        bNum = BikouEnaNum(selectEnaPlayer);


					int[] sum = new int[6];

                    for (int k = 1; selectEnaNum - 1 - k >= 0; k++)
                        sum[enaColorList[selectEnaNum - 1 - k]]++;

                    for (int j = 0; j < clickCursorID.Count; j++)
                    {
                        int x = clickCursorID[j];
                        CardScript sc = card[x / 50, x % 50].GetComponent<CardScript>();

                        if (sc.MultiEnaFlag)
                            mNum--;

                        if (checkClass(x, cardClassInfo.精像_美巧))
                            bNum--;
                        
                        sum[sc.CardColor]++;
                    }

					for (int k=1; k<6; k++)
					{
                        if (sum[k] > enaColorNum[selectEnaPlayer, k])
                        {
                            if (bNum > 0 && k == (int)cardColorInfo.白)
                                bNum--;
                            else
                                mNum -= (sum[k] - enaColorNum[selectEnaPlayer, k]);
                        }
					}

					bool multiEnable = mNum > 0;
                    bool bikouEnable = bNum > 0;
					
					for (int i=0; i<enaNum[selectEnaPlayer]; i++)
					{
						int id = fieldRankID (Fields.ENAZONE, i, selectEnaPlayer);

						int clr = card [selectEnaPlayer, id].GetComponent<CardScript> ().CardColor;
                        int targetEnacolor=enaColorList[selectEnaNum - 1];

                        if (!clickCursorID.Contains(id + selectEnaPlayer * 50))
                        {
                            if ((card[selectEnaPlayer, id].GetComponent<CardScript>().MultiEnaFlag && multiEnable)
                                || ( targetEnacolor == 1 && checkClass(id,selectEnaPlayer, cardClassInfo.精像_美巧) && bikouEnable)
                                || targetEnacolor == 0 
                                || clr == enaColorList[selectEnaNum - 1])
                                setTargetCursorID(id, selectEnaPlayer);
                        }
					}*/
				}
				
				if (Input.GetMouseButtonDown (0))
				{
					for (int i=0; i<targetCursorList.Count; i++)
					{
						if (SelectedCard == targetCursorList [i].targetCursor)
						{
							if (targetCursorList [i].player == 0)
								clickCursorID.Add (targetCursorList [i].ID);
							else
								clickCursorID.Add (targetCursorList [i].ID + 50);
							selectEnaNum--;
							DestoryCursorAll ();
							break;
						}
					}
				}

				if (selectEnaNum == 0)
				{
					//通信
					if (isTrans)
					{
						for (int i=0; i<clickCursorID.Count; i++)
						{
							messagesBuf.Add ("" + clickCursorID [i]);
						}
					}
					
					DestoryCursorAll ();
					enaColorList.Clear ();
					selectEnaFlag = false;
				}
			}
		}
		else
			SelectedCard = null;
		
		//cursor select
		if (selectCursorFlag)
		{
			if (Input.GetMouseButtonDown (0))
			{
				for (int i=0; i<targetCursorList.Count; i++)
				{
					if (SelectedCard == targetCursorList [i].targetCursor)
					{
						if (targetCursorList [i].player == 0)
							clickCursorID.Add (targetCursorList [i].ID);
						else
							clickCursorID.Add (targetCursorList [i].ID + 50);
						selectNum--;
						DestoroyCursor (i);
						break;
					}
				}
			}
			if (selectNum == 0 || (cursorCancel && Input.GetMouseButtonDown (1)))
			{
                while (selectNum > 0)
                {
                    clickCursorID.Add(-1);
                    selectNum--;
                }

                //通信
				if (isTrans)
				{
					for (int i=0; i<clickCursorID.Count; i++)
					{
						messagesBuf.Add ("" + clickCursorID [i]);
					}
				}

 				
				DestoryCursorAll ();
				selectCursorFlag = false;
			}
		}
	}
	
	int[] RandDeckNum (int num)
	{
		int[] deck = new int[num];
		int k = Random.Range (0, num);
		for (int i = 0; i < num; i++)
		{
			deck [i] = -1;
		}
		for (int i = 0; i < num; i++)
		{
			int count = 0;
			while (count<2)
			{
				int rand = Random.Range (0, num);
				if (deck [rand] == -1)
				{
					deck [rand] = k;
					break;
				}
				else
					count++;
			}
			if (count == 2)
			{
				for (int j=Random.Range(0,num); j<num; j=(j+1)%num)
				{
					if (deck [j] == -1)
					{
						deck [j] = k;
						break;
					}
				}
			}
			k = (k + 1) % num;
		}
		return deck;
	}

	Texture getIDtexure(int ID, int player)
	{
		string key = SerialNumString[player,ID];
		return Singleton<pics>.instance.getTexture(key);
	}

	void setFront (int ID, int player, string s, Vector3 v)
	{
		front[player,ID] = creatFront( getLoadString(ID,player),v);

		Texture tex=getIDtexure(ID,player);

		MaterialPropertyBlock matb=new MaterialPropertyBlock();
		matb.AddTexture("_MainTex", tex);

		front[player,ID].GetComponent<Renderer>().SetPropertyBlock(matb);

//		front[player,ID].renderer.material.mainTexture=null;
/*		front[player,ID].AddComponent<getTextureScr>();

		getTextureScr scr = front[player,ID].GetComponent<getTextureScr>();
		scr.serial = getSerialNum(ID,player);*/
	}

	string getLoadString(int ID, int player)
	{
		string[] s = SerialNumString [player, ID].Split ('-');
		string loadString="";
		
		if (checkSerialNum (s[0], SerialNumString [player, ID]))//例外
		{
			loadString="CardData/"+s[0] + "/" + SerialNumString [player, ID] + "p";
		}
		else {
			loadString="card";
		}

		return loadString;
	}

	GameObject creatFront(string loadString,Vector3 v){
		var obj= (GameObject)Instantiate (
			Resources.Load (loadString),
			v,
			Quaternion.identity
			);

        if (loadString != "caed")
        {
            var scr = obj.AddComponent<getPlaneScrript>();
            scr.notGetScr = true;
        }

        return obj;
	}

	bool checkSerialNum(string s,string serialNum){
		if(
			s=="WD01"
			||s=="WD02"
			||s=="WD03"
			||s=="WD04"
			||s=="WD05"
			||s=="WD06"
			||s=="WD07"
			||s=="WX01"
			||s=="WX02"
			||s=="WX03"
			)
			return true;

		if(s=="PR"){
			string[] num=serialNum.Split('-');
			int n=-1;
			int.TryParse(num[1],out n);

			if( n>=0 && n<=40)
				return true;
		}

		return false;
	}

	void creatCopyCard(int ID,int player,Fields f)//ミルルンの残骸
	{
		GameObject obj = (GameObject)Instantiate (
			Resources.Load ("BlackBack"),
			STORAGEZONE,
			Quaternion.identity
			);

		obj.transform.parent=this.transform;

		int count=NewCardList.Count;
		NewCardList.Add(new NewCard(obj,ID,player,f));

		GameObject frt=creatFront( getLoadString(ID,player),STORAGEZONE);

		frt.transform.parent = obj.transform;

		CardScript script = obj.GetComponent<CardScript> ();
		script.ID = 100 + count;
		script.player = 1 - player;		
		script.Brain = frt;
		
		setLoadText(ID,player,script);

        NewCardCreated = true;
	}

	void loadReplay()
	{
		try{
			using (StreamReader r = new StreamReader(@"replay/"+replayName+"/receivedLog.txt"))
			{
				string line;
				
				while ((line = r.ReadLine()) != null)
				{ // 1行ずつ読み出し。
					receivedList.Add(line);
				}
			}

			using (StreamReader r = new StreamReader(@"replay/"+replayName+"/sentLog.txt"))
			{
				string line;
				
				while ((line = r.ReadLine()) != null)
				{ // 1行ずつ読み出し。
					sentList.Add(line);
				}
			}
		}
		catch(FileNotFoundException e)
		{
			messageShow("not fount replay file");
			NetScr.resetManager();
		}
	}
	
	void setSerialNumString(int player)
	{
		try{
			if(replayMode)
			{
				for (int i = 0; i < 50; i++) 
				{
					SerialNumString [player, i] = replayRead(player);
				}
			}
			else if (isTrans && player == 1)
			{
				//デッキ内容を受け取る
				for (int i=0; i<50; i++)
				{
					SerialNumString [1, i] = readMessage ();		
				}
			}
			else
			{
				using (StreamReader r = new StreamReader(@"deck\"+DeckString[player]+".txt"))
				{
					int i = 0;
					string line;

					while ((line = r.ReadLine()) != null)
					{ // 1行ずつ読み出し。
						SerialNumString [player, i] = line;
						i++;
					}
				}
			}
		}
		catch(FileNotFoundException e)
		{
			messageShow("not fount deck file");
			NetScr.resetManager();
		}
	}

	void DeckCreat (int player)
	{
		if(!NowLoading[player]){
			setSerialNumString(player);
			NowLoading[player] = true;

			if(isTrans && player == 0)
				for (int i=0; i<50; i++)
					messagesBuf.Add (SerialNumString [0, i]);

			return;
		}
		else
		{
			//MainDeck
			for (deckNum[player]=0; deckNum[player]<40; deckNum[player]++)
			{
				//card
				Vector3 dv=new Vector3 (MAINDECK.x, 0.1f + (float)deckNum [player] * 0.025f, MAINDECK.z);
				if(player==1)
					dv=vec3Player2(dv);

				card [player, 10 + deckNum [player]] = (GameObject)Instantiate (
					Resources.Load ("BlackBack"),
					dv,
					Quaternion.identity
					);
				card [player, 10 + deckNum [player]].transform.parent = this.transform;
				setComponent (10 + deckNum [player], player);			
				
				string[] s = SerialNumString [player, 10 + deckNum [player]].Split ('-');
				
				Vector3 dfv=new Vector3 (MAINDECK.x, 0.12f + (float)deckNum [player] * 0.025f, MAINDECK.z);
				if(player==1)
					dfv=vec3Player2(dfv);
				setFront(10+deckNum[player],player,s[0],dfv);

				front [player, 10 + deckNum [player]].transform.parent = card [player, 10 + deckNum [player]].transform;
				front [player, 10 + deckNum [player]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));

				Quaternion q=Quaternion.AngleAxis (180f, new Vector3 (0, 0, 1));
				if(player==1)
					q *= Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));
				card [player, 10 + deckNum [player]].transform.rotation = q;

				field [player, 10 + deckNum [player]] = Fields.MAINDECK;
				fieldRank [player, 10 + deckNum [player]] = deckNum [player];
			}
			
			//LrigDeck
			for (lrig_deckNum[player]=0; lrig_deckNum[player]<10; lrig_deckNum[player]++)
			{
				Vector3 lv=new Vector3 (LRIGDECK.x, 0.1f + (float)lrig_deckNum [player] * 0.025f, LRIGDECK.z);
				if(player==1)
					lv=vec3Player2(lv);

				card [player, lrig_deckNum [player]] = (GameObject)Instantiate (
					Resources.Load ("WhiteBack"),
					lv,
					Quaternion.identity
					);
				
				card [player, lrig_deckNum [player]].transform.parent = this.transform;
				setComponent (lrig_deckNum [player], player);
				
				string[] s = SerialNumString [player, lrig_deckNum [player]].Split ('-');
				
				Vector3 lfv=new Vector3 (LRIGDECK.x, 0.12f + (float)lrig_deckNum [player] * 0.025f, LRIGDECK.z);
				if(player==1)
					lfv=vec3Player2(lfv);
				setFront(lrig_deckNum [player],player,s[0],lfv);
				front [player, lrig_deckNum [player]].transform.parent = card [player, lrig_deckNum [player]].transform;
				front [player, lrig_deckNum [player]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));

				Quaternion q=Quaternion.AngleAxis (180f, new Vector3 (0, 0, 1));
				if(player==1)
					q *= Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));
				card [player, lrig_deckNum [player]].transform.rotation = q;
				
				field [player, lrig_deckNum [player]] = Fields.LRIGDECK;
				fieldRank [player, lrig_deckNum [player]] = lrig_deckNum [player];
			}
		}

		NowLoading[player]=false;
		preDeckCreat[player] = false;

		//shuffle buf
		if(isTrans && player == 1)
		{
			while (messages.Count>0)
				shuffleBuf.Add (readMessage ());
		}

		if(replayMode)
		{
			for (int i = 0; i < deckNum[player]; i++) {
				shuffleBuf.Add(replayRead(player));
			}

			if(player==0)
			{
				//ルリグの決定
				GUImoveID = int.Parse(replayRead(0));
				selectCardFlag=false;
			}
		}

		Shuffle(player);

		if(isTrans &&((player == 0 && !isServer )|| (isServer && player == 1)))
			sendMessageBuf();

		if(player == 0 && !replayMode)
			selectCardListLrigIn(0);
	}

/*	void DeckCreat ()
	{
		setSerialNumString(0);

		//MainDeck
		for (deckNum[0]=0; deckNum[0]<40; deckNum[0]++)
		{
			//card
			card [0, 10 + deckNum [0]] = (GameObject)Instantiate (
				Resources.Load ("BlackBack"),
				new Vector3 (MAINDECK.x, 0.1f + (float)deckNum [0] * 0.025f, MAINDECK.z),
				Quaternion.identity
			);
			card [0, 10 + deckNum [0]].transform.parent = this.transform;
			setComponent (10 + deckNum [0], true);			
			
			string[] s = SerialNumString [0, 10 + deckNum [0]].Split ('-');

			setFront(10+deckNum[0],0,s[0],new Vector3 (MAINDECK.x, 0.12f + (float)deckNum [0] * 0.025f, MAINDECK.z));

			front [0, 10 + deckNum [0]].transform.parent = card [0, 10 + deckNum [0]].transform;
			front [0, 10 + deckNum [0]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));
			card [0, 10 + deckNum [0]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 0, 1));
			
			field [0, 10 + deckNum [0]] = Fields.MAINDECK;
			fieldRank [0, 10 + deckNum [0]] = deckNum [0];
		}

		//LrigDeck
		for (lrig_deckNum[0]=0; lrig_deckNum[0]<10; lrig_deckNum[0]++)
		{
			card [0, lrig_deckNum [0]] = (GameObject)Instantiate (
				Resources.Load ("WhiteBack"),
				new Vector3 (LRIGDECK.x, 0.1f + (float)lrig_deckNum [0] * 0.025f, LRIGDECK.z),
				Quaternion.identity
			);

			card [0, lrig_deckNum [0]].transform.parent = this.transform;
			setComponent (lrig_deckNum [0], true);

			string[] s = SerialNumString [0, lrig_deckNum [0]].Split ('-');

			setFront(lrig_deckNum [0],0,s[0],new Vector3 (LRIGDECK.x, 0.12f + (float)lrig_deckNum [0] * 0.025f, LRIGDECK.z));
			front [0, lrig_deckNum [0]].transform.parent = card [0, lrig_deckNum [0]].transform;
			front [0, lrig_deckNum [0]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));
			card [0, lrig_deckNum [0]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 0, 1));
			
			field [0, lrig_deckNum [0]] = Fields.LRIGDECK;
			fieldRank [0, lrig_deckNum [0]] = lrig_deckNum [0];
		}
	}

	void DeckCreat_p2 ()
	{
		if (isTrans)
		{
			//デッキ内容を受け取る
			for (int i=0; i<50; i++)
			{
				SerialNumString [1, i] = readMessage ();//messages[0];		
			}
		}
		else
		{
			setSerialNumString(1);
		}
		
		for (deckNum[1]=0; deckNum[1]<40; deckNum[1]++)
		{
			card [1, 10 + deckNum [1]] = (GameObject)Instantiate (
				Resources.Load ("BlackBack"),
				new Vector3 (-MAINDECK.x, 0.1f + (float)deckNum [1] * 0.025f, -MAINDECK.z),
				Quaternion.identity
			);
			card [1, 10 + deckNum [1]].transform.parent = this.transform;
			setComponent (10 + deckNum [1], false);
			
			string[] s = SerialNumString [1, 10 + deckNum [1]].Split ('-');
			setFront(10 + deckNum [1],1,s[0],new Vector3 (-MAINDECK.x, 0.12f + (float)deckNum [1] * 0.025f, -MAINDECK.z));
			front [1, 10 + deckNum [1]].transform.parent = card [1, 10 + deckNum [1]].transform;
			front [1, 10 + deckNum [1]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));
			card [1, 10 + deckNum [1]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 0, 1))
				* Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));
			
			field [1, 10 + deckNum [1]] = Fields.MAINDECK;
			fieldRank [1, 10 + deckNum [1]] = deckNum [1];
//			cardTexture [1, 10 + deckNum [1]] = (Texture)Resources.Load (s [0] + "/" + SerialNumString [1, 10 + deckNum [1]]);
		}
		//相手player
		//LrigDeck
		for (lrig_deckNum[1]=0; lrig_deckNum[1]<10; lrig_deckNum[1]++)
		{
			card [1, lrig_deckNum [1]] = (GameObject)Instantiate (
				Resources.Load ("WhiteBack"),
				new Vector3 (-LRIGDECK.x, 0.1f + (float)lrig_deckNum [1] * 0.025f, -LRIGDECK.z),
				Quaternion.identity
			);
			card [1, lrig_deckNum [1]].transform.parent = this.transform;
			setComponent (lrig_deckNum [1], false);

			string[] s = SerialNumString [1, lrig_deckNum [1]].Split ('-');
			setFront(lrig_deckNum [1],1,s[0],new Vector3 (-LRIGDECK.x, 0.12f + (float)lrig_deckNum [1] * 0.025f, -LRIGDECK.z));
			front [1, lrig_deckNum [1]].transform.parent = card [1, lrig_deckNum [1]].transform;
			front [1, lrig_deckNum [1]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));
			card [1, lrig_deckNum [1]].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 0, 1))
				* Quaternion.AngleAxis (180f, new Vector3 (0, 1, 0));
			
			field [1, lrig_deckNum [1]] = Fields.LRIGDECK;
			fieldRank [1, lrig_deckNum [1]] = lrig_deckNum [1];
//			cardTexture [1, lrig_deckNum [1]] = (Texture)Resources.Load (s [0] + "/" + SerialNumString [1, lrig_deckNum [1]]);
		}

	}*/

	void UpPhaseMove (int player)
	{
        if (moveID[player] == -1 && rotaID[player] == -1)
		{
            if (motion == Motions.NonMotion && LabelCount == 0)
			{

				//ターン開始時
                turnStartFlag = true;

				for (int i = 0; i < 2; i++)
				{
					ArtsLimitFlag[i] = NextTurnAtrsLimFlag[i];
					NextTurnAtrsLimFlag[i]=false;
				}


				int c = 0;
				for (int i=0; i<3; i++)
				{
					if (signiCondition [player, i] != Conditions.no)
					{
						int id = fieldRankID (Fields.SIGNIZONE, i, player);
						if (getCardScr (id, player).Freeze)
							getCardScr (id, player).Freeze = false;
						else if (signiCondition [player, i] == Conditions.Down)
						{
							clickCursorID.Add (id);
							c++;
						}
					}
				}

				if (LrigCondition [player] != Conditions.no)
				{
					int id = fieldRankID (Fields.LRIGZONE, lrigZoneNum [player] - 1, player);
					if (getCardScr (id, player).Freeze)
						getCardScr (id, player).Freeze = false;
					else if (LrigCondition [player] == Conditions.Down)
					{
						clickCursorID.Add (id);
						c++;
					}
				}
				if (c > 0)
					motion = Motions.Up;
			}
			else if (motion == Motions.Up && clickCursorID.Count > 1)
			{
				clickCursorID.RemoveAt (0);
				movePhase [player] = 0;
				rotaPhase [player] = -1;
			}
			else if (LabelCount >= standartTime * 5)
			{
				clickCursorID.Clear ();
				phase = Phases.DrawPhase;
				if (turn == 1)
					drawNum [player] = 1;
				else
					drawNum [player] = normalDrawNum [player];
			}
		}
		
		if (motion == Motions.Up && clickCursorID.Count > 0)
		{
			Up (clickCursorID [0], player);
		}	
	}
	
	void DrawPhaseMove (int player)
	{
		if (moveID [player] == -1 && rotaID [player] == -1 && drawNum [player] == 0)
		{
			phase = Phases.EnaPhase;
			movePhase [player] = 0;
			rotaPhase [player] = -1;
		}
		else
			DrawCard (player);
	}
	
	void EnaPhaseMove (int player)
	{
		if (moveID [player] == -1 && rotaID [player] == -1)
		{
			if (clickCursorID.Count == 0 && motion == Motions.NonMotion)
			{
				if ( waitYou(player) )//通信
				{
					if (canRead ())
					{
						if (getNextMessage(player) == nextStr)
						{
							readMessage ();
							phase = Phases.GrowPhase;
						}
						else
						{
							string s = readMessage ();
							int id = -1;

							if(int.TryParse (s,out id))
								clickCursorID.Add (IdTrans (id));
							else 
								Debug.Log(s);
						}
					}
				}
				else
				{
					selectNum = 1;
					selectCursorFlag = true;
					setTargetCursorField (Fields.HAND, player);
					setTargetCursorField (Fields.SIGNIZONE, player);
					return;
				}
			}
			else if (clickCursorID.Count > 0 && motion == Motions.NonMotion)
			{
				if (isTrans && player == 0)
				{
//					if(messages.Count==0){
					sendMessageBuf ();
					motion = Motions.EnaCharge;
					movePhase [player] = 0;
					rotaPhase [player] = -1;
//					}
				}
				else
				{
					motion = Motions.EnaCharge;
					movePhase [player] = 0;
					rotaPhase [player] = -1;
				}
			}
			else if (motion == Motions.EnaCharge)
			{
				clickCursorID.Clear ();
				if (handSortFlag [player])
				{
					motion = Motions.Draw;
					movePhase [player] = 0;
					rotaPhase [player] = -1;
				}
				else
				{
					phase = Phases.GrowPhase;
					return;
				}
			}
			else if (motion == Motions.Draw)
			{
				motion = Motions.NonMotion;
				phase = Phases.GrowPhase;
				return;
			}
		}
		
		if (motion == Motions.EnaCharge)
		{
            if(checkType(clickCursorID [0] % 50, player, cardTypeInfo.レゾナ))
                GoLrigDeck(clickCursorID [0] % 50, player);
            else
                EnaCharge (clickCursorID [0] % 50, player);
		}
		if (motion == Motions.Draw)
		{
			DrawCard (player);
		}
	}

	void GrowPhaseMove (int player)
	{
		//入力のチェック
		if (waitYou(player) && motion == Motions.NonMotion)
		{
			if (canRead ())
			{
				if (getNextMessage(player) == nextStr)
				{
					readMessage ();
					phase++;
				}
				else
				{
                    GUImoveID = readParseMessage();
                    motion = Motions.Grow;
				}
			}
		}
		else if (GUImoveID == -1 && moveID [player] == -1 && LabelCount == 1)
		{
			selectCardListLrigIn (player);
			if (selectCardList.Count > 0)
			{
				selectCardFlag = true;
				return;
			}
			else
			{
				GrowPhaseSkip[player] = true;

				if(isTrans)
				{
					messagesBuf.Add(nextStr);
					sendMessageBuf();
				}

			}
		}
		else if (Input.GetMouseButtonDown (0) && selectedID >= 0 && GUImoveID == -1)
		{
			if (field [player, selectedID] == Fields.LRIGDECK)
			{
				selectCardListLrigIn (player);
				if (selectCardList.Count > 0)
				{
					selectCardFlag = true;
					return;
				}
			}
		}
		//motion切り替え
		if (GUImoveID != -1 && motion == Motions.NonMotion)
		{
			if (isTrans && player == 0)
				sendMessageBuf ();
			motion = Motions.Grow;
		}
		else if (lrigSummonFlag [player] && moveID [player] == -1 && rotaID [player] == -1)
		{
			phase = Phases.MainPhase;
			lrigSummonFlag [player] = false;
			return;
		}
		
		if (motion == Motions.Grow && GUImoveID >= 0)
		{
			if (card [player, GUImoveID % 50].GetComponent<CardScript> ().Type == 0)
			{
				Grow (GUImoveID % 50, player);
				if (moveID [player] == -1 && rotaID [player] == -1)
				{
					if (!lrigSummonFlag [player])
						motion = Motions.NonMotion;
					GUImoveID = -1;
				}
			}
			else
			{
				motion = Motions.NonMotion;
				GUImoveID = -1;
			}
		}
	}
	
	void MainPhaseMove (int player)
	{
		if (moveID [player] == -1 && rotaID [player] == -1 && GUImoveID == -1)
		{
			
			//受信
			if (waitYou(player))
			{
				if (handSortFlag [player])
				{
					motion = Motions.Draw;
					movePhase [player] = 0;
					rotaPhase [player] = -1;

				}
				else if (canRead ())
				{
					string r = readMessage ();
					movePhase [player] = 0;
					rotaPhase [player] = -1;
					motion = Motions.NonMotion;

					if (r == nextStr)
					{
						phase++;
						return;
					}
					else if (r == SummonStr)
					{
						selectClickID = readParseMessage ();
						selectSigniZone = int.Parse (readMessage ());
						motion = Motions.Summon;
					}
					else if (r == SpellStr)
					{
						selectClickID = readParseMessage () % 50;
						motion = Motions.ChantSpell;
					}
					else if (r == ArtsStr)
					{
						GUImoveID = readParseMessage ();
						motion = Motions.ChantArts;
					}
					else if (r == GoTrashStr)
					{
						GoTrashID = readParseMessage () % 50;
						motion = Motions.GoTrash;
					}
					else if (r == IgnitionStr)
					{
						int readID = readParseMessage ();
						UpIgnition (readID % 50, readID / 50);
					}
					else if (r == TrashIgnitionStr)
					{
						int readID = readParseMessage ();
                        traIgniUp(readID);
					}
				}
			}
            else if (TraIgniSelectedID != -1)
            {
                traIgniUp(TraIgniSelectedID);
                TraIgniSelectedID = -1;               
            }
            else if (Input.GetMouseButtonDown(0) && selectedID >= 0)
            {
                if (field[selectPlayer, selectedID] == Fields.TRASH)
                {
                    selectCardFlag = true;
                    showGUIFlag = true;
                    if (player == selectPlayer)
                        selectTrashFlag = true;
                    selectCardListIn(Fields.TRASH, selectPlayer);
                    return;
                }
                else if (selectPlayer == player)
                {
                    if (field[player, selectedID] == Fields.HAND && selectClickID == -1)
                    {
                        if (card[player, selectedID].GetComponent<CardScript>().Type == 2)
                        {
                            selectSigniZoneFlag = true;
                            selectClickID = selectedID;
                            SigniZoneUp(player);
                            selectSigniPlayer = player;


                            //通信用
                            if (isTrans && player == 0)
                            {
                                messagesBuf.Add(SummonStr);
                                messagesBuf.Add("" + selectClickID);
                            }

                            return;
                        }
                        else if (card[player, selectedID].GetComponent<CardScript>().Type == 3)
                        {
                            selectClickID = selectedID;
                            motion = Motions.ChantSpell;
                            movePhase[player] = 0;
                            rotaPhase[player] = -1;

                            //通信用
                            if (isTrans && player == 0)
                            {
                                messagesBuf.Add(SpellStr);
                                messagesBuf.Add("" + selectClickID);
                                sendMessageBuf();
                            }
                        }
                    }
                    else if (field[player, selectedID] == Fields.SIGNIZONE ||
                        (field[player, selectedID] == Fields.LRIGZONE) && fieldRank[player, selectedID] == lrigZoneNum[player] - 1)
                    {

                        //						card[player,selectedID].GetComponent<CardScript>().Ignition=true;
                        UpIgnition(selectedID, player);

                        //通信
                        if (isTrans && player == 0)
                        {
                            messagesBuf.Add(IgnitionStr);
                            messagesBuf.Add("" + selectedID);
                            sendMessageBuf();
                        }

                    }
                    else if (field[player, selectedID] == Fields.LRIGDECK)
                    {
                        motion = Motions.ChantArts;
                        movePhase[player] = 0;
                        rotaPhase[player] = -1;
                        selectCardFlag = true;
                        selectCardListIn(Fields.LRIGDECK, player);

                        //通信用
                        if (isTrans && player == 0)
                        {
                            messagesBuf.Add(ArtsStr);
                        }

                        return;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1) && selectedID >= 0 && selectPlayer == player && !checkACG(selectedID,player))
            {
                if (field[player, selectedID] == Fields.SIGNIZONE && !checkType(selectedID,player, cardTypeInfo.レゾナ))
                {
                    motion = Motions.GoTrash;
                    movePhase[player] = 0;
                    rotaPhase[player] = -1;
                    GoTrashID = selectedID;

                    //通信用
                    if (isTrans && player == 0)
                    {
                        messagesBuf.Add(GoTrashStr);
                        messagesBuf.Add("" + GoTrashID);
                        sendMessageBuf();
                    }

                }
            }
            else if (selectSigniZone != -1)
            {
                if (isTrans && player == 0)
                {
                    sendMessageBuf();
                }
                motion = Motions.Summon;
                movePhase[player] = 0;
                rotaPhase[player] = -1;
            }
            else if (handSortFlag[player])
            {
                movePhase[player] = 0;
                rotaPhase[player] = -1;
                motion = Motions.Draw;
            }
		}
		
		if (motion == Motions.Summon && selectClickID != -1)
		{
			Summon (selectClickID % 50, player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				selectClickID = -1;
			}
		}
		else if (motion == Motions.ChantSpell && selectClickID != -1)
		{
			ChantSpell (selectClickID, player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				selectClickID = -1;
				if (shortageFlag)
					shortageFlag = false;
			}
		}
		else if (motion == Motions.ChantArts)
		{
			if (GUImoveID >= 0 &&
                ( checkType(GUImoveID%50,GUImoveID/50, cardTypeInfo.レゾナ) || //レゾナまたはメインに使えるアーツ
				(card [player, GUImoveID % 50].GetComponent<CardScript> ().Type == 1
				&& !card [player, GUImoveID % 50].GetComponent<CardScript> ().notMainArts)))
			{
		
				//通信用
				if (isTrans && player == 0 && messagesBuf.Count > 0 && messagesBuf [0] == ArtsStr)
				{
					sendMessageBuf ();
				}
				
				ChantArts (GUImoveID % 50, player);
				if (moveID [player] == -1 && rotaID [player] == -1)
				{
					GUImoveID = -1;
					if (shortageFlag)
						shortageFlag = false;
				}
			}
			else
			{
				GUImoveID = -1;
				//通信用
				if (isTrans && player == 0)
				{
					messagesBuf.Clear ();
				}
			}
		}
		else if (motion == Motions.Draw){
			DrawCard (player);

			if(notMoving())
				motion = Motions.NonMotion;
		}
		else if (motion == Motions.GoTrash && GoTrashID >= 0)
		{
			GoTrash (GoTrashID, player);
			if (notMoving ())
			{
				GoTrashID = -1;
			}
		}
	}
	
	void AttackPhaseMove (int player)
	{
		if (notMoving () && GUImoveID == -1 && attackClickID == -1)
		{
			if (artsAsk)
			{
				artsAsk = false;
				selectAttackAtrs = true;
				if (isTrans)
					messagesBuf.Clear ();
				return;
			}
			else if ( waitYou(attackThinkPlayer) )
			{
				if (canRead ())
				{
					string r = readMessage ();

					if (r == nextStr)
					{
						phase++;
						return;
					}
					else if (r == YesStr)
					{
						attackAtrsPlayer = attackThinkPlayer;


						motion = Motions.ChantArts;
						GUImoveID = readParseMessage ();
						movePhase [attackAtrsPlayer] = 0;
						rotaPhase [attackAtrsPlayer] = -1;
					}
					else if (r == NoStr)
					{
						if(replayMode)
						{
							attackThinkPlayer = 1-attackThinkPlayer;
							return;
						}
						else{
							selectAttackAtrs = true;
							attackAtrsPlayer = 0;
							motion = Motions.ChantArts;
							return;
						}
					}
					else if (r == AttackStr)
					{
						movePhase [player] = 0;
						rotaPhase [player] = -1;
						int id = readParseMessage ();
						if (field [id / 50, id % 50] == Fields.SIGNIZONE)
						{
							motion = Motions.SigniAttack;
							attackClickID = id;
						}
						else if (field [id / 50, id % 50] == Fields.LRIGZONE)
						{
							motion = Motions.LrigAttack;
							attackClickID = id;
						}						
					}
				}				
			}
			else if (receiveFlag)
			{
				if (canRead ())
				{
					string r = readMessage ();
					if (r == YesStr)
					{
						motion = Motions.ChantArts;
						GUImoveID = readParseMessage ();
						movePhase [attackAtrsPlayer] = 0;
						rotaPhase [attackAtrsPlayer] = -1;
					}
					else if (r == NoStr)
					{
						receiveFlag = false;
						return;
					}
				}
			}
			else if (LabelCount == 0)
			{
				selectAttackAtrs = true;
				attackAtrsPlayer = player;
				motion = Motions.ChantArts;
				return;
			}
			else if (clickCursorID.Count > 0)
			{

				movePhase [player] = 0;
				rotaPhase [player] = -1;

				int id = clickCursorID [0];

				if (field [id / 50, id % 50] == Fields.SIGNIZONE)
				{
					motion = Motions.SigniAttack;
					attackClickID = id;
					clickCursorID.RemoveAt (0);
				}
				else if (field [id / 50, id % 50] == Fields.LRIGZONE)
				{
					motion = Motions.LrigAttack;
					attackClickID = id;
					clickCursorID.RemoveAt (0);
				}
				//通信
				if (player == 0 && isTrans)
				{
					string s = messagesBuf [messagesBuf.Count - 1];
					messagesBuf.RemoveAt (messagesBuf.Count - 1);

					messagesBuf.Add (AttackStr);
					messagesBuf.Add (s);

					sendMessageBuf ();
				}
			}
			else
			{
				setTargetCursorUp (player);
				if (targetCursorList.Count > 0)
				{
					selectNum = 1;
					selectCursorFlag = true;

					return;
				}
			}
		}

		if (motion == Motions.SigniAttack && attackClickID >= 0)
		{
			SigniAttack (attackClickID % 50, attackClickID / 50);
			if (notMoving ())
			{
				attackClickID = -1;
			}
		}
		else if (motion == Motions.LrigAttack && attackClickID >= 0)
		{
			LrigAttack (attackClickID % 50, attackClickID / 50);
			if (notMoving ())
			{
				attackClickID = -1;
				if (LrigCondition [player] == Conditions.Down)
					phase++;
			}
		}
		else if (motion == Motions.ChantArts)
		{
			if (GUImoveID >= 0)
			{
				artsAsk = false;
				//通信
				if (messagesBuf.Count > 1 && isTrans && messagesBuf [1] == "" + GUImoveID)
					sendMessageBuf ();
				CardScript sc = card [GUImoveID / 50, GUImoveID % 50].GetComponent<CardScript> ();
				if(sc.Type == 1)
					ChantArts (GUImoveID % 50, GUImoveID / 50);

				else if(!useAttackArtsUp)
				{
					sc.UseAttackArts=true;
					useAttackArtsUp=true;
					return;
				}
				else 
					useAttackArtsUp=false;

				if (moveID [GUImoveID / 50] == -1 && rotaID [GUImoveID / 50] == -1)
				{

					GUImoveID = -1;
					if (shortageFlag)
						shortageFlag = false;
					

					if ((!isTrans || attackAtrsPlayer == 0) && !replayMode)
					{
						//アーツ2発目
						selectAttackAtrs = true;
						movePhase [attackAtrsPlayer] = 0;
						rotaPhase [attackAtrsPlayer] = -1;
					}
				}
			}
		}
	}

    void enAbilityListRemoveAt(int index)
    {
        abilitySet a = enAbilityList[index];
        getCardScr(a.receiver).setAbility(a._ability, false);
        enAbilityList.RemoveAt(index);
    }

    void enAbilityListAdd(int receiver, int giver, ability a)
    {
        var scr = getCardScr(receiver);
        if (scr == null || scr.checkAbility(a))
            return;

        scr.setAbility(a, true);
        enAbilityList.Add(new abilitySet(receiver, giver, a));
    }

	void EndPhaseMove (int player)
	{
 		//エンドまでの処理の解決
        for (int i = 0; i < IgniCostDecList.Count;)
        {
            if (IgniCostDecList[i].isEndFinishFlag)
                IgniCostDecList.RemoveAt(i);
            else
                i++;
        }

		enajeFlag[player] = false;

		UnblockLevel = -1;
		NoCostGrow = false;	
		notCrashFlag = false;
		AttackPhaseSkip = false;
		GrowPhaseSkip[player] = false;
        EnaPhaseSkip[player] = false;
        SigniAttackSkipFlag = false;
        LrigAttackSkipFlag = false;

		if(ViolenceCount>0){
			ViolenceCount=0;
			requipFlag=true;
		}

		thinkingPlayer = 1 - player;
		myTime[0]=myStandardtime;
		myTime[1]=myStandardtime;
		myFrameTime[0]=0;
		myFrameTime[1]=0;

		//二人用
		for (int i=0; i<2; i++)
		{
			RefleshedFlag [i] = false;
			normalDrawNum [i] = 2;
			signiSumLimit [i] = 3;
			ArtsLimitFlag [i] =false;
			MirrorMirageFlag[i]=false;
            GridCount[i] = 0;
            HandSummonMaxLim[i] = -1;
            notDamagingFlag[i] = false;
 		}

		
		//ファフニール関連（コスト関連）
		resetSpellCostDown (0,0);//my spell
		resetSpellCostDown (0,1);//my arts
		resetSpellCostDown (1,0);//your spell
		resetSpellCostDown (1,1);//your arts
		FafnirFlag = false;

        while (changeBasedList.Count > 0)
        {
            int x = changeBasedList[0] % 50;
            int target = changeBasedList[0] / 50;
            changeBasedList.RemoveAt(0);

            var c = getCardScr(x,target);
            changeBasePower(x, target, c.OriginalPower);
        }
		
        //こっちにまとめた
        while (enAbilityList.Count > 0)
            enAbilityListRemoveAt(0);

        //ロストした効果の復活
        while (LostEffectIDList.Count > 0)
        {
            int lID = LostEffectIDList[0];
            LostEffectIDList.RemoveAt(0);

            changeLostEffect(lID % 50, lID / 50, false, -1, -1);
        }

        AddIgniList.Clear();


        //レゾナぐっばい
        while (TempResonaList.Count > 0)
        {
            int x=TempResonaList[0];
            TempResonaList.RemoveAt(0);
            if(field[x/50,x%50] == Fields.SIGNIZONE)
                SetSystemCard(x, Motions.GoLrigDeck);
        }

        //end powerUp
		for (int i=0; i<6; i++)
		{
			if (SigniPowerUpValue [i / 3, i % 3] != 0)
			{
				int id = fieldRankID (Fields.SIGNIZONE, i % 3, i / 3);
				nomalizePower (id, i / 3);
			}
		}

        //終了時にチェックゾーンにあるスペルを墓地に送る
        for (int j = 0; j < 2; j++)
        {
            Fields f = Fields.CHECKZONE;

            for (int i = 0; i < fieldAllNum(f, j); i++)
            {
                int x = fieldRankID(f, i, j);
                if (x >= 0 && getCardType(x, j) == 3)
                    SetSystemCard(x + 50 * j, Motions.GoTrash);
            }
        }

        //モーションの選択
        if (moveID[player] == -1 && rotaID[player] == -1)
        {
            if (handSortFlag[player] && clickCursorID.Count == 0)
            {
                motion = Motions.Draw;
                movePhase[player] = 0;
                rotaPhase[player] = -1;
            }
            else if (RebornList.Count > 0)
            {
                //end reborn
                for (int i = 0; i < RebornList.Count; i++)
                {
                    if (field[RebornList[i] / 50, RebornList[i] % 50] == Fields.SIGNIZONE)
                        clickCursorID.Add(RebornList[i]);
                    else
                    {
                        RebornList.RemoveAt(i);
                        i--;
                    }
                }

                if (RebornList.Count > 0){
                    movePhase[player] = 0;
                    rotaPhase[player] = -1;
                    motion = Motions.GoTrash;                
                }
            }
            else if (waitYou(player))
            {
                movePhase[player] = 0;
                rotaPhase[player] = -1;

                if (handNum[player] <= 6)
                {
                    if (LabelCount >= standartTime * 5)
                    {
                        nextTurnFunc();
                        return;
                    }
                }
                else if (canRead())
                {
                    for (int i = 0; i < handNum[player] - 6; i++)
                    {
                        int id = readParseMessage();
                        clickCursorID.Add(id);
                    }
                    motion = Motions.GoTrash;
                }
            }
            else if (handNum[player] > 6 && clickCursorID.Count == 0)
            {
                selectNum = handNum[player] - 6;
                selectCursorFlag = true;
                setTargetCursorField(Fields.HAND, player);
                return;
            }
            else if (clickCursorID.Count > 0)
            {
                motion = Motions.GoTrash;
                movePhase[player] = 0;
                rotaPhase[player] = -1;
                if (isTrans && player == 0)
                {
                    sendMessageBuf();
                }
            }
            else if (LabelCount >= standartTime * 5 && handNum[player] <= 6)
            {
                nextTurnFunc();
            }
        }
		
		if (motion == Motions.GoTrash && clickCursorID.Count > 0)
		{
			int trashPlayer = player;
			//reborn
			if (RebornList.Count > 0)
				trashPlayer = RebornList [0] / 50;
			
			GoTrash (clickCursorID [0] % 50, trashPlayer);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				clickCursorID.RemoveAt (0);
				
				//reborn
				if (RebornList.Count > 0)
					RebornList.RemoveAt (0);
			}
		}
		else if (motion == Motions.Draw)
		{
			DrawCard (player);
		}
	}

    void nextTurnFunc()
    {
        phase = Phases.UpPhase;
        turnEndFlag = true;

        //extra turn
        if (NextExtraTurnCount > 0)
        {
            NextExtraTurnCount--;
            ExtraTurnCount++;
        }

        //dont grow
        for (int i = 0; i < 2; i++)
        {
            DontGrowFlag[i] = NextDontGrowFlag[i];
            NextDontGrowFlag[i] = false;
        }

        //goNextTurn
        if (GoNextTrunFlag)
        {
            chainMotion[0] = -1; 
            GoNextTrunFlag = false;
        }

        if(JerashiGeizuFlag[getTurnPlayer()])
        {
            JerashiGeizuFlag[getTurnPlayer()] = false;
            HandSummonMaxLim[1 - getTurnPlayer()] = 12000;
        }

        //ワームホールのカウント更新
        for (int i = 0; i < SigniNotSummonCount.Length; i++)
        {
            if(SigniNotSummonCount[i / 3, i % 3]>0)
                SigniNotSummonCount[i / 3, i % 3]--;            
        }

        for (int i = 0; i < zonePowerList.Count; i++)
        {
            zonePowerList[i].turnCount--;
            if (zonePowerList[i].turnCount == 0)
            {
                powChanListChangerClear(zonePowerList[i].changer);
                zonePowerList.RemoveAt(i);
                i--;
            }
        }
    }
/*	void EndPhaseMove_p1(){
		if(moveID[0]==-1 && rotaID[0]==-1){
			if(handSortFlag[0] && clickCursorID.Count==0){
				motion=Motions.Draw;
				movePhase[0]=0;
				rotaPhase[0]=-1;						
			}
			else if(handNum[0]>6 && clickCursorID.Count==0){
				selectNum=handNum[0]-6;
				selectCursorFlag=true;
				setTargetCursorField(Fields.HAND,0);
				return;
			}
			else if(clickCursorID.Count>0){
				motion=Motions.GoTrash;
				movePhase[0]=0;
				rotaPhase[0]=-1;
			}
			else if(LabelCount>=standartTime*5 && handNum[0]<=6){
				phase=Phases.UpPhase;
				clickCursorID.Clear();
			}
		}
		
		if(motion==Motions.GoTrash){
			GoTrash_p1(clickCursorID[0]);
			if(moveID[0]==-1 && rotaID[0]==-1 && clickCursorID.Count>0)
				clickCursorID.RemoveAt(0);
		}
		else if(motion==Motions.Draw){
			DrawCard(0);
		}
	}*/

    void setBattleID(int player, int id1, int id2)
    {
        BattledID[player] = id1;
        BattledID[1 - player] = id2;

        tellBattleFinishID[player] = id1;
        tellBattleFinishID[1 - player] = id2;
    }

	bool isAttackerPowerHeigh(int attacker)
	{
		int player=attacker/50;
		int attacked=1-player;

		int rank = 1 - (fieldRank [player, attacker % 50] - 1);//exchange 0,1,2
		int defender=fieldRankID(Fields.SIGNIZONE,rank,attacked);

		int[] power = new int[2];
		
		power [attacked] = card [attacked, defender].GetComponent<CardScript> ().Power;
		power [player] = card [player, attacker % 50].GetComponent<CardScript> ().Power;

		return (power [player] >= power [attacked]);
	}

	bool isBattling(int attacker)
	{
		int player=attacker/50;
		int attacked=1-player;

		CardScript sc=card [player, attacker % 50].GetComponent<CardScript> ();
		int rank = 1 - (fieldRank [player, attacker % 50] - 1);//exchange 0,1,2

		return signiCondition [attacked, rank] != Conditions.no && !sc.Assassin && !StopAttackFlag;
	}

	int getDefenderID(int attacker){
		int player=attacker/50;
		int attacked=1-player;
		
		int rank = 1 - (fieldRank [player, attacker % 50] - 1);//exchange 0,1,2
		int defender=fieldRankID(Fields.SIGNIZONE,rank,attacked);

		return defender+attacked*50;
	}

	void SigniAttack (int ID, int player)
	{
		int attacked = -player + 1;//exchange 0,1
		int[] BufID = new int[2]{moveID [player],moveID [attacked]};
		if (BufID [player] < 0)
			BufID [player] = rotaID [player];
		if (BufID [attacked] < 0)
			BufID [attacked] = rotaID [attacked];
		
		if (chainMotion [1] == -1 && movePhase [player] == 0 && rotaPhase [player] == -1){
			chainMotion [1] = 0;
		}

		if (chainMotion [1] == 0)
		{
			Down (ID, player);

			if(rotaPhase[player]==0 && rotaCount[player]==0){
				Singleton<SoundPlayer>.instance.playSE("attack");
			}

			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [1] = 1;
				card [player, BufID [player]].GetComponent<CardScript> ().AttackNow = true;

				AttackerID=BufID [player] + player % 2 * 50;
				AttackFrontRank = 1 - (fieldRank [player, BufID [player]] - 1);
			}
		}

		if (chainMotion [1] == 1)
		{
			if (rotaID [player] == -1)
			{
				rotaID [player] = BufID [player];
				waitTime=1;
			}
			else
			{
 
				CardScript sc=card [player, BufID [player]].GetComponent<CardScript> ();
				sc.AttackNow = false;
				rotaID [player] = -1;
				int rank = 1 - (fieldRank [player, BufID [player]] - 1);//exchange 0,1,2

                //分岐
                if (field[player, BufID[player]] != Fields.SIGNIZONE)
                {
                    chainMotion[1] = -1;
                    return;
                }
				else if(StopAttackFlag)
				{
					StopAttackFlag=false;
					chainMotion [1] = -1;

                    stopAttackedID = BufID[player] + player % 2 * 50;
				}
				else if (isBattling(BufID [player] + player % 2 * 50 ) )
				{
					int ID2 = fieldRankID (Fields.SIGNIZONE, rank, attacked);

					setBattleID(player,BufID [player],ID2);

					if (isAttackerPowerHeigh(BufID [player]+ player % 2 * 50))
					{
						lancerFlag = card [player, BufID [player]].GetComponent<CardScript> ().lancer;

                        if (lancerFlag)
                            crasherID = BufID[player] + 50 * player;

						chainMotion [1] = 2;
						movePhase [attacked] = 0;
						rotaPhase [attacked] = -1;
						attackedID = ID2;

						waitTime=1;
					}
					else
					{
						CounterID = BufID [player] + 50 * player;


						chainMotion [1] = -1;
					}
				}
                else if(notDamagingFlag[attacked])
                {
                    chainMotion[1] = -1; 
                    return;
                }
				else if (LifeClothNum [attacked] > 0)
				{
                    crasherID = BufID[player] + 50 * player;

					movePhase [attacked] = 0;
					rotaPhase [attacked] = -1;
					moveID [attacked] = -2;

					if (isTrans || replayMode || Application.platform != RuntimePlatform.WindowsEditor)
					{
						clickCursorID.Add (fieldRankID (Fields.LIFECLOTH, LifeClothNum [attacked] - 1, attacked) + 50 * attacked);
						chainMotion [1] = 3;
						if (card [player, BufID [player]].GetComponent<CardScript> ().DoubleCrash && LifeClothNum [attacked] >= 2)
						{							
							clickCursorID.Add (fieldRankID (Fields.LIFECLOTH, LifeClothNum [attacked] - 2, attacked) + 50 * attacked);
							chainMotion [1] = 4;
						}
					}
					else
					{
						selectCursorFlag = true;
						setTargetCursorField (Fields.LIFECLOTH, attacked);
						if (card [player, BufID [player]].GetComponent<CardScript> ().DoubleCrash && LifeClothNum [attacked] >= 2)
						{
							chainMotion [1] = 4;
							selectNum = 2;
						}
						else
						{
							chainMotion [1] = 3;
							selectNum = 1;
						}
					}
				}
				else
				{
					EndMessage (player);
//					chainMotion[1]=-1;
				}
			}
		}
		attackAfterChoice (attacked);
	}

	void LrigAttack (int ID, int player)
	{
		int attacked = -player + 1;//exchange 0,1
		int[] BufID = new int[2]{moveID [player],moveID [attacked]};
		if (BufID [player] < 0)
			BufID [player] = rotaID [player];
		if (BufID [attacked] < 0)
			BufID [attacked] = rotaID [attacked];

		if (chainMotion [1] == -1 && movePhase [player] == 0 && rotaPhase [player] == -1)
			chainMotion [1] = 0;
		if (chainMotion [1] == 0)
		{
			Down (ID, player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				card [player, BufID [player]].GetComponent<CardScript> ().AttackNow = true;

                LrigAttackNow = true;
				chainMotion [1] = 1;
				movePhase [attacked] = 0;
				rotaPhase [attacked] = -1;

				if (GuardNum (attacked) > 0)
				{
					if ((isTrans && player == 0) || replayMode)
					{
						chainMotion [1] = 10;

						if(replayMode)
							readTargetPlayer = 1 - player;
					}
					else
					{
						guardSelectflag = true;
						guardPlayer = attacked;
					}
				}
			}
		}

		if (chainMotion [1] == 1)
		{			
			if (rotaID [player] == -1)
			{
				rotaID [player] = BufID [player];
			}
			else
			{
				rotaCount [player] = 0;
				rotaID [player] = -1;
				card [player, ID].GetComponent<CardScript> ().AttackNow = false;

				if (clickCursorID.Count > 0)
				{
					chainMotion [1] = 8;
					if (isTrans && player == 1 && messagesBuf.Count > 0)
					{
						sendMessageBuf ();
					}
				}
                else if (StopAttackFlag)
                {
                    StopAttackFlag = false;
                    chainMotion[1] = -1;

                    stopAttackedID = BufID[player] + player % 2 * 50;
                }
                else if (notDamagingFlag[attacked])
                {
                    chainMotion[1] = -1;
                    return;
                }
                else if (LifeClothNum[attacked] > 0)
				{
                    crasherID = ID + 50 * player;

                    if (card[player, ID].GetComponent<CardScript>().DoubleCrash && LifeClothNum[attacked] >= 2)
					{
						chainMotion [1] = 4;
						clickCursorID.Add (fieldRankID (Fields.LIFECLOTH, LifeClothNum [attacked] - 1, attacked) + 50 * attacked);
						clickCursorID.Add (fieldRankID (Fields.LIFECLOTH, LifeClothNum [attacked] - 2, attacked) + 50 * attacked);						
					}
					else
					{
						chainMotion [1] = 3;
						clickCursorID.Add (fieldRankID (Fields.LIFECLOTH, LifeClothNum [attacked] - 1, attacked) + 50 * attacked);
					}
				}
				else
				{
					//勝利メッセージ
					EndMessage (player);
				}
			}	
		}
		
		//通信待ち
		if (chainMotion [1] == 10)
		{
			moveID [player] = -2;
			if (canRead ())
			{
				moveID [player] = -1;
				string r = readMessage ();
				if (r == YesStr)
				{
					
					clickCursorID.Add (readParseMessage ());
					chainMotion [1] = 8;
				}
				else if (r == NoStr)
				{
					if (LifeClothNum [attacked] > 0)
					{
						if (card [player, ID].GetComponent<CardScript> ().DoubleCrash && LifeClothNum [attacked] >= 2)
						{
							chainMotion [1] = 4;
							clickCursorID.Add (fieldRankID (Fields.LIFECLOTH, LifeClothNum [attacked] - 2, attacked) + 50 * attacked);						
							clickCursorID.Add (fieldRankID (Fields.LIFECLOTH, LifeClothNum [attacked] - 1, attacked) + 50 * attacked);
						}
						else
						{
							chainMotion [1] = 3;
							clickCursorID.Add (fieldRankID (Fields.LIFECLOTH, LifeClothNum [attacked] - 1, attacked) + 50 * attacked);
						}
					}
					else
					{
						//勝利メッセージ
						EndMessage (player);
					}
				}
			}
		}
		//実行部
		attackAfterChoice (attacked);
	}
	
	void attackAfterChoice (int attacked)
	{
        if (BattledID[0] != -1 || BattledID[1] != -1)
        {
            moveID[attacked] = -2;
            return;
        }

        if (tellBattleFinishID[0] != -1 && tellBattleFinishID[1] != -1)
        {
            moveID[attacked] = -1;

            for (int i = 0; i < 2; i++)
            {
                BattleFinishID[i] = tellBattleFinishID[i];
                tellBattleFinishID[i] = -1;
            }
        }

		if (chainMotion [1] == 2)
		{
			chainMotion [1] = -1;

            int rank = fieldRank[attacked, attackedID];
            rank = 2 - rank;
            if (fieldRankID(Fields.SIGNIZONE, rank, 1 - attacked) == -1)
                return;

			if(!ImmortalFlag)
			{
				SetSystemCard(attackedID + attacked*50,Motions.EnaCharge);
				attackedID = -1;

				if (lancerFlag)
				{
					chainMotion [1] = 3;
					movePhase [attacked] = 0;
					rotaPhase [attacked] = -1;

					clickCursorID.Add(fieldRankID(Fields.LIFECLOTH,LifeClothNum[attacked]-1,attacked)+50*attacked);
					moveID [attacked] = -2;
					waitTime=1;
				}
			}
		}

		if (chainMotion [1] == 3 && !selectCursorFlag)
		{
			if (moveID [attacked] == -2)
				moveID [attacked] = -1;

			Crash (clickCursorID [0] % 50, attacked);

            if (lancerFlag && crashedID >= 0)
                lancerCrashed = true;

			if (moveID [attacked] == -1 && rotaID [attacked] == -1)
			{
                lancerFlag = false;//ランサーの効果でクラッシュしたら終わり

                endCrash();
/*                clickCursorID.RemoveAt(0);
				chainMotion [1] = -1;

                crasherID = -1;*/
            }
		}

		//double crash open 1
		if (chainMotion [1] == 4 && !selectCursorFlag)
		{
			if (moveID [attacked] == -2)
				moveID [attacked] = -1;

			Open (clickCursorID [0] % 50, attacked);

			if (moveID [attacked] == -1 && rotaID [attacked] == -1)
			{
				movePhase [attacked] = 0;
				rotaPhase [attacked] = -1;
				chainMotion [1] = 5;
				waitTime=1;
			}
		}

		//double crash open	2
		if (chainMotion [1] == 5 && !selectCursorFlag)
		{
			Open (clickCursorID [1] % 50, attacked);

			if (moveID [attacked] == -1 && rotaID [attacked] == -1)
			{
				movePhase [attacked] = 0;
				rotaPhase [attacked] = -1;
				moveID [attacked] = -2;

				if( (!isTrans && !replayMode) || (isTrans && attacked==0)){
					chainMotion [1] = 6;
					for (int i=0; i<clickCursorID.Count; i++)
					{
						setTargetCursorID (clickCursorID [i] % 50, clickCursorID [i] / 50);
						selectNum++;
					}
					selectCursorFlag = true;
				}
				else 
				{
					chainMotion [1] = 11;

					if(replayMode)
						readTargetPlayer = attacked;
				}

				clickCursorID.Clear ();
			}
		}

		//double crash 通信待ち
		if (chainMotion [1] == 11)
		{
			moveID [attacked] = -2;
			if (canRead ())
			{
				clickCursorID.Add (readParseMessage ());
				clickCursorID.Add (readParseMessage ());
				chainMotion [1] = 6;
			}
		}

		if (chainMotion [1] == 6 && !selectCursorFlag)
		{
			if (moveID [attacked] == -2)
			{
				moveID [attacked] = -1;

				if(isTrans && attacked==0)
					sendMessageBuf();
			}

			Crash (clickCursorID [0] % 50, attacked);
			if (moveID [attacked] == -1 && rotaID [attacked] == -1)
			{
				movePhase [attacked] = 0;
				rotaPhase [attacked] = -1;
				chainMotion [1] = 7;
			}
		}

        //choice 7
		if (chainMotion [1] == 7 && !selectCursorFlag)
		{
			Crash (clickCursorID [1] % 50, attacked);
			if (moveID [attacked] == -1 && rotaID [attacked] == -1)
			{
                endCrash();
/*				clickCursorID.Clear ();
				chainMotion [1] = -1;

                crasherID = -1;*/
			}
		}

        //choice 8
		if (chainMotion [1] == 8)
		{
			GoTrash (clickCursorID [0] % 50, clickCursorID [0] / 50);
			if (moveID [attacked] == -1 && rotaID [attacked] == -1)
			{
				chainMotion [1] = 9;
				movePhase [attacked] = 0;
				rotaPhase [attacked] = -1;
				clickCursorID.Clear ();
			}
		}

        //choice 9
		if (chainMotion [1] == 9)
		{
			DrawCard (attacked);
            if (moveID[attacked] == -1 && rotaID[attacked] == -1)
            {
                chainMotion[1] = -1;

                stopAttackedID = getLrigID(1 - attacked) + 50 * (1 - attacked);
             }
		}

	}

    void endCrash()
    {
        clickCursorID.Clear();
        chainMotion[1] = -1;

        if (checkAbility(crasherID % 50, crasherID / 50, ability.TwoChargeAfterCrash))
        {
            getCardScr(crasherID).setEffect(crasherID, 0, Motions.TopEnaCharge);
            getCardScr(crasherID).setEffect(crasherID, 0, Motions.TopEnaCharge);
        }
        crasherID = -1;

    }

    int isAddedIgniID(int ID, int player)
    {
        for (int i = 0; i < AddIgniList.Count; i++)
        {
            if (AddIgniList[i].changedID == ID + player % 2 * 50)
                return i;
        }

        return -1;
    }


	bool isShowZoneID(int ID,int player)
	{
		for(int i=0;i<ShowZoneIDList.Count;i++)
		{
			if(ShowZoneIDList[i]==ID+player%2*50)
				return true;
		}

		return false;
	}

	void removeShowZoneID(int ID,int player)
	{
		if (isShowZoneID (ID, player))
		{
			ShowZoneIDList.Remove (ID + player % 2 * 50);
			ShowZoneNum [player % 2] --;
		}
	}

    public int GetCharm(int ID, int player)
    {
        player = player % 2;

        if (ID < 0)
            return -1;

        if (field[player, ID] != Fields.SIGNIZONE)
            return -1;

        int rank = fieldRank[player, ID];
        int x = fieldRankID(Fields.CharmZone, rank, player);

        return x;
    }

    public bool havingCharm(int ID, int player)
    {
        return GetCharm(ID, player) >= 0;
    }

    public void setSystemCostGoTrashLevelSumResona(int sum, System.Func<int, int, bool> chck, int myID,int myPlayer)
    {
         SystemCardScr.funcTargetIn(myPlayer, Fields.SIGNIZONE, chck,this);

        int levSum = 0;
        foreach (var id in SystemCardScr.Targetable)
            if(id>=0)
                levSum += getCardLevel(id % 50, id / 50);

        if (levSum < sum)
        {
            SystemCardScr.Targetable.Clear();
            return;
        }


         targetLevelSum = sum;
         targetLevelSumCheck = chck;

         SetSystemCardFromCard(-1, Motions.LevelSumCostGoTrash, myID, myPlayer);

         SetSystemCard(myID + myPlayer * 50, Motions.Summon);
    }

	void SetSystemCard(int targetID,Motions m)
	{
        SetSystemCard(targetID, m, null);
	}
    void SetSystemCard(int targetID, Motions m, List<int> targetable)
    {
        SetSystemCard(targetID, m, targetable, false, null);
     }
    void SetSystemCard(int targetID, Motions m, List<int> targetable, bool cancel, System.Func<int, bool> systemInput)
    {
        CardScript sc = SystemCard.GetComponent<CardScript>();

        //targetable 入れたら削除
        if (targetable != null)
        {
            if (targetable.Count==0)
                return;

            for (int i = 0; i < targetable.Count; i++)
                sc.Targetable.Add(targetable[i]);
            targetable.Clear();
        }

        sc.effectFlag = true;
        sc.effectTargetID.Add(targetID);
        sc.effectMotion.Add((int)m);

        //cancel
        if (cancel)
        {
            SystemCardInputReturnFunc = systemInput;

            if (targetID == -1)
                sc.cursorCancel = true;
            else if (targetID == -2)
                sc.GUIcancelEnable = true;
        }

    }

    public void SetSystemCardFromCard(int targetID, Motions m,int myID,int myPlayer)
    {
        SetSystemCardFromCard(targetID, m,myID,myPlayer,null);
    }
    public void SetSystemCardFromCard(int targetID, Motions m, int myID, int myPlayer, List<int> targetable)
    {
        SetSystemCardFromCard(targetID, m,myID,myPlayer,targetable,false, null);
    }
    public void SetSystemCardFromCard(int targetID, Motions m, int myID, int myPlayer, List<int> targetable, bool cancel, System.Func<int, bool> inputReturn)
    {
        SetSystemCard(targetID, m, targetable,cancel,inputReturn);
        //selecter
        if (targetID < 0)
            SystemCardScr.effectSelecter = myPlayer;

        //依頼人の情報を保存
        SystemCardClientID = getFusionID(myID,myPlayer);
        SystemCardScr.costGoTrashIDenable = getCardScr(SystemCardClientID).costGoTrashIDenable;
    }
	
	void ExitFunction (int ID, int player)
	{
		removeShowZoneID(ID,player);

        //exitID
        exitID = ID + player % 2 * 50;
        exitField = field[player % 2, ID];     

        if (/*EffecterNowID >= 0 &&*/dualEffectTriggerd)
            exitedList.Add(exitID);

		if (field [player % 2, ID] == Fields.LIFECLOTH)
		{
			rankDown (ID, player % 2);
			LifeClothNum [player % 2]--;
		}
		if (field [player % 2, ID] == Fields.TRASH)
		{
			rankDown (ID, player % 2);
			trashNum [player % 2]--;
			field [player % 2, ID] = Fields.ENAZONE;//避難
			TrashSort (player);
		}
		else if (field [player % 2, ID] == Fields.HAND)
		{
			rankDown (ID, player % 2);
			handSortFlag [player % 2] = true;
			handNum [player % 2]--;
		}
		else if (field [player % 2, ID] == Fields.SIGNIZONE)
		{
			//ダブルクラッシュ
/*            card[player % 2, ID].GetComponent<CardScript>().DoubleCrash = false;
			if(isEnDoubleCrashID(ID,player)>=0)
				EnDoubleCrashID.Remove(ID+player % 2 *50);

			//バニッシュ耐性
			card [player % 2, ID].GetComponent<CardScript> ().ResiBanish = false;
            if (isEnResiBanishID(ID, player) >= 0)
                EnResiBanishID.Remove(ID + player % 2 * 50);

            //ランサー
            card[player % 2, ID].GetComponent<CardScript>().lancer = false;
            if (isEnLancerID(ID, player) >= 0)
                EnLancerID.Remove(ID + player % 2 * 50);

            //アタックできない
            card[player % 2, ID].GetComponent<CardScript>().Attackable = true;
            if (dontAttackList.Contains(ID + player % 2 * 50))
                dontAttackList.RemoveAt(ID + player % 2 * 50);
            */
            //こっちにまとめた
            for (int i = 0; i < enAbilityList.Count;)
            {
                abilitySet set = enAbilityList[i];
                if (set.receiver == getFusionID(ID, player % 2))
                    enAbilityListRemoveAt(i);
                else i++;
            }
 

            //起動効果付与
            if (AddIgniList.Count > 0)
            {
                int index = isAddedIgniID(ID,player);

                if (index >= 0)
                    AddIgniList.RemoveAt(index);
            }


            //効果失う
            card[player % 2, ID].GetComponent<CardScript>().lostEffect = false;

			//フリーズ
			card [player % 2, ID].GetComponent<CardScript> ().Freeze = false;
			signiCondition [player % 2, fieldRank [player % 2, ID]] = Conditions.no;

			//パワー（エンドフェイズまでのやつ）
			nomalizePower (ID, player);

			//パワー（永続効果のやつ）
			powChanListChangedClear(ID,player%2);

			//チャームの墓地送り
			int k = GetCharm(ID,player);
			if(k >= 0)
				SetSystemCard(k+player%2*50,Motions.GoTrash);

            //underCardの墓地送り
            while (underCards[fieldRank[player % 2, ID]].Count > 0)
            {
                int id = underCards[fieldRank[player % 2, ID]][0];
                SetSystemCard(id, Motions.GoTrash);
                underCards[fieldRank[player % 2, ID]].RemoveAt(0);
            }

            if (checkCross(ID, player % 2))
                isCrossExited = true;

		}
		else if (field [player % 2, ID] == Fields.MAINDECK)
		{
			rankDown (ID, player % 2);
			deckNum [player % 2]--;
		}
		else if (field [player % 2, ID] == Fields.ENAZONE)
		{
			rankDown (ID, player % 2);
			enaColorNum [player % 2, card [player % 2, ID].GetComponent<CardScript> ().CardColor]--;
			enaNum [player % 2]--;
		}
		else if (field [player % 2, ID] == Fields.CHECKZONE)
		{
			card [player % 2, ID].GetComponent<CardScript> ().BurstFlag = false;
		}
		else if (field [player % 2, ID] == Fields.LRIGDECK)
		{
			rankDown (ID, player % 2);
			lrig_deckNum [player % 2]--;
		}
		else if (field [player % 2, ID] == Fields.LRIGTRASH)
		{
			rankDown (ID, player % 2);
			lrigTrashNum [player % 2]--;
		}
        else if (field[player % 2, ID] == Fields.LRIGZONE)
        {
            rankDown(ID, player % 2);
            lrigZoneNum[player % 2]--;

            field[player % 2, ID] = Fields.Non;
            LrigZoneSort(player % 2);
            field[player % 2, ID] = Fields.LRIGZONE;
        }
        else if (field[player % 2, ID] == Fields.UnderZone)
        {
            if(underCards[fieldRank[player%2,ID]].Contains(ID+player%2*50))
                underCards[fieldRank[player%2,ID]].Remove(ID+player%2*50);
        }
    }

	int getChangedIndex(int ID, int player)
	{
		for (int i = 0; i < powChanList.Count; i++)
		{
			if(powChanList[i].changedID == ID + 50*player)
				return i;
		}
		return -1;
	}

	void powChanListChangedClear(int ID, int player)
	{
		while(true){
			int index = getChangedIndex(ID,player);

			if(index<0)
				break;

			powChanListRemove(index);
		}
	}

	void powChanListRemove(int index)
	{
		int ID = powChanList[index].changedID % 50;
		int player = powChanList[index].changedID / 50;

		upCardPower(ID, player, -powChanList[index].changeValue);		
		powChanList.RemoveAt(index);

	}

	void Moving (int player)
	{
		//move relation
		if (moveID [player] < card.GetLength (1) && moveID [player] >= 0)
		{
			Vector3 pos = card [player % 2, moveID [player]].transform.position;

			Vector3 vectorBuf = new Vector3 (0f, 0f, 0f);
			if (player % 2 == 0)
				vectorBuf = destination [player];
			else if (player % 2 == 1)
				vectorBuf = vec3Player2 (destination [player]);

			if (moveCount [player] < moveTime [player])
			{
				card [player % 2, moveID [player]].transform.position = vec3Add (
				pos,
				vec3Waru (vec3Hiku (vectorBuf, pos), (float)(moveTime [player] - moveCount [player]))
				);
				moveCount [player]++;
			}
			else
			{
                card[player % 2, moveID[player]].transform.position = vectorBuf;
				moveCount [player] = 0;
				movePhase [player]++;

			}
		}

		//rota relation
		if (rotaID [player] < card.GetLength (1) && rotaID [player] >= 0)
		{
			float[] angleNow = new float[3];
			float[] angleBuf = new float[3];
			angleNow [0] = card [player % 2, rotaID [player]].transform.localEulerAngles.x;
			angleNow [1] = card [player % 2, rotaID [player]].transform.localEulerAngles.y;
			angleNow [2] = card [player % 2, rotaID [player]].transform.localEulerAngles.z;
			for (int i=0; i<3; i++)
			{
				float hosei = 0f;
				if (i == 1 && player % 2 == 1)
					hosei = 180f;
				if (Mathf.Abs (angle [player, i] - hosei - angleNow [i]) >= 190f)
					angleBuf [i] = 360f - Mathf.Abs (angle [player, i] - hosei);
				else
					angleBuf [i] = angle [player, i] - hosei;
			}
			if (rotaCount [player] < rotaTime [player])
			{
				card [player % 2, rotaID [player]].transform.rotation = Quaternion.AngleAxis (
					angleNow [0] + (angleBuf [0] - angleNow [0]) / (float)(rotaTime [player] - rotaCount [player]), new Vector3 (1, 0, 0))
					* Quaternion.AngleAxis (angleNow [1] + (angleBuf [1] - angleNow [1]) / (float)(rotaTime [player] - rotaCount [player]), new Vector3 (0, 1, 0))
					* Quaternion.AngleAxis (angleNow [2] + (angleBuf [2] - angleNow [2]) / (float)(rotaTime [player] - rotaCount [player]), new Vector3 (0, 0, 1));
				rotaCount [player]++;
			}
			else
			{
				card [player % 2, rotaID [player]].transform.rotation = Quaternion.AngleAxis (angle [player, 0], new Vector3 (1, 0, 0))
					* Quaternion.AngleAxis (angle [player, 1] - 180f * (player % 2), new Vector3 (0, 1, 0))
					* Quaternion.AngleAxis (angle [player, 2], new Vector3 (0, 0, 1));
				rotaCount [player] = 0;
				rotaPhase [player]++;
			}

			if (rotaID [player] == getLrigID(player % 2))
			{
				for (int i=0; i<lrigZoneNum[player%2]; i++)
				{
					int id = fieldRankID (Fields.LRIGZONE, i, player % 2);
                    if (id >= 0)
                        card[player % 2, id].transform.rotation = card[player % 2, rotaID[player]].transform.rotation;
				}
			}
		}
	}
	
	void selectCardListIn (Fields f, int player)
	{
		for (int i=0; i<fieldAllNum(f,player); i++)
		{
			selectCardList.Add (fieldRankID (f, i, player) + player * 50);
		}
	}

	void selectCardListLrigIn (int player)
	{
		for (int i=0; i<fieldAllNum(Fields.LRIGDECK,player); i++)
		{
			int id = fieldRankID (Fields.LRIGDECK, i, player);

			if (getCardType (id, player) == 0)
			{
				if (DebugFlag || getCardLevel (id, player) <= LrigLevel [player] + 1)
				{
					if(!getCardScr(id,player).useLimit)
						selectCardList.Add (id + player * 50);
				}
			}
		}
	}

	void selectCardListSpellCutIn (int player)
	{
		//lrig
		int lID=getLrigID(player);
		
		if(getCardScr(lID,player).SpellCutIn)
			selectCardList.Add(lID+50*player);

		//atrs
		for (int i=0; i<fieldAllNum(Fields.LRIGDECK,player); i++)
		{
			int x = fieldRankID (Fields.LRIGDECK, i, player);
			if (card [player, x].GetComponent<CardScript> ().SpellCutIn && getCardType(x,player)==1)
				selectCardList.Add (x + player * 50);
		}

	}

	void selectCardListAttackArtsIn (int player)
	{
		int lID=getLrigID(player);
		
		if(getCardScr(lID,player).attackArts)
			selectCardList.Add(lID+50*player);

        for (int i = 0; i < 3; i++)
        {
            int x = fieldRankID(Fields.SIGNIZONE, i, player);
            if(x >= 0 && getCardScr(x,player).attackArts)
                selectCardList.Add(x + 50 * player);
        }

		for (int i=0; i<fieldAllNum(Fields.LRIGDECK,player); i++)
		{
			int x = fieldRankID (Fields.LRIGDECK, i, player);
			if (card [player, x].GetComponent<CardScript> ().attackArts)
				selectCardList.Add (x + player * 50);
		}
	}
		
	void PayCostAndSort (int ID, int player)
	{
		if (chainMotion [0] == -1 && movePhase [player] == 0 && rotaPhase [player] == -1)
			chainMotion [0] = 0;
		if (chainMotion [0] == 0)
		{
			PayModeCost (ID, player, false);

			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				if (shortageFlag)
				{
					chainMotion [0] = -1;
					movePhase [player] = -1;
					return;
				}
				else
				{
					chainMotion [0] = 1;
					movePhase [player] = 0;
					rotaPhase [player] = -1;
				}
			}
		}
		if (chainMotion [0] == 1)
		{
			EnaSort (player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = -1;
			}
		}
	}

 /*   int getIgniCostTargIndex(int x,int target)
    {
        int fusionID = x + 50 * target;

        if (fusionID != IgniEffID && fusionID!=TraIgniUpID && fusionID!=IgnitionUpID)
            return -1;

        for (int i = 0; i < IgniCostDecList.Count; i++)
        {
            if (fusionID == IgniCostDecList[i].targetID && IgniCostDecList[i].igniField == field[target % 2, x])
                return i;
        }

        return -1;
    }*/

    int getFusionID(int x, int target)
    {
        return x + target % 2 * 50;
    }

    int IgniCostDecSum(int x, int target,int color)
    {
        int fusionID = getFusionID(x, target);
        int sum = 0;

        for (int i = 0; i < IgniCostDecList.Count; i++)
        {

            if (fusionID == IgniCostDecList[i].targetID && IgniCostDecList[i].igniField == field[target % 2, x])
                sum += IgniCostDecList[i].downCost.getCost(color);
        }
        return sum;
    }

    colorCostArry copyCostArray(colorCostArry ori)
    {
        colorCostArry array = new colorCostArry(cardColorInfo.無色, 0);

        for (int i = 0; i < array.Length(); i++)
            array[i] = ori[i];

        return array;
    }

	void PayModeCost (int ID, int player, bool isGrow)
	{
        if (waitYou(player % 2) && clickCursorID.Count == 0)
		{
			moveID [player] = -2;
			if (canRead ())
			{
				string r = readMessage ();
				if (r == YesStr)
				{
					while (getNextMessage(readTargetPlayer)!=sprt)
					{
						clickCursorID.Add (readParseMessage ());
					}

					readMessage ();
					moveID [player] = -1;
				}
				else if (r == NoStr)
				{
					shortageFlag = true;
					moveID [player] = -1;
					return;
				}
			}
		}
		else if (movePhase [player] == 0 && moveID [player] == -1)
		{
            if (clickCursorID.Count == 0)
			{
				//toldPayedCost　の初期化
				toldPayedCost=false;

                //check
                if(!checkModeCost(ID,player%2,isGrow))
                {
                    shortageFlag = true;

                    //通信
                    if (isTrans && player % 2 == 0)
                    {
                        messagesBuf.Add(NoStr);
                        sendMessageBuf();
                    }

                    return;
                }

                colorCostArry cost;
                if (isGrow)
                {
                    cost = copyCostArray( card[player % 2, ID].GetComponent<CardScript>().GrowCost);
                }
                else
                    cost = copyCostArray(card[player % 2, ID].GetComponent<CardScript>().Cost);

                //まとめた
                enaColorList = creatCostColorList(ID, player, cost);
/*				for (int i=0; i>=0; i--)
				{
                    int hosei = IgniCostDecSum(ID, player, i);
 
					for (int j=0; j<cost[i] - hosei; j++)
						enaColorList.Add (i);

					if (i == 0)
						i = 6;
					if (i == 1)
						i = -1;
				}*/

				if (enaColorList.Count > 0)
				{
					//数
/*					if (enaNum [player % 2] < enaColorList.Count)
					{
						enaColorList.Clear ();
						shortageFlag = true;
						
						//通信
						if (isTrans && player % 2 == 0)
						{
							messagesBuf.Add (NoStr);
							sendMessageBuf ();
						}
						
						return;
					}

					//色
					int Multi = MultiEnaNum (player % 2);
					for (int i=1; i<cost.Length; i++)
					{
						if (cost [i] > enaColorNum [player % 2, i] + Multi)
						{
							enaColorList.Clear ();
							shortageFlag = true;
							
							//通信
							if (isTrans && player % 2 == 0)
							{
								messagesBuf.Add (NoStr);
								sendMessageBuf ();
							}
							
							return;
						}
						else if (cost [i] > enaColorNum [player % 2, i])
							Multi -= cost [i] - enaColorNum [player % 2, i];
					}*/
					
					//通信
					if (isTrans && player % 2 == 0)
						messagesBuf.Add (YesStr);

                    if (isGrow && getCardScr(ID, player % 2).MelhenGrowFlag && !melhenFlag[player % 2])
                    {
                        melhenUpFlag = true;
                        melhenFlag[player % 2] = true;
                    }

					selectEnaFlag = true;
					selectEnaPlayer = player % 2;
					selectEnaNum = enaColorList.Count;
					moveID [player] = -2;
				}
				else if (isTrans && player % 2 == 0)
				{
					//通信
					messagesBuf.Add (YesStr);
					messagesBuf.Add (sprt);
					sendMessageBuf ();
				}
			}
		}

		if (clickCursorID.Count > 0)
		{
            if (melhenUpFlag)
            {
                melhenUpFlag = false;
                melhenFlag[player % 2] = false;
            }

			if(!toldPayedCost)
			{
				toldPayedCost=true;

				CardScript sc=getCardScr(ID,player % 2);

				if(sc.PayedCostEnable){
					for (int i = 0; i < clickCursorID.Count; i++) {
						sc.PayedCostList.Add(clickCursorID[i]);;
					}
				}
			}

			//通信
			if (isTrans && player % 2 == 0)
			{
				if (messagesBuf.Count > 0)
				{
					messagesBuf.Add (sprt);
					sendMessageBuf ();
				}
			}
			
			if (moveID [player] == -2)
				moveID [player] = -1;

			GoTrash (clickCursorID [0] % 50, player);

			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				clickCursorID.RemoveAt (0);
				if (clickCursorID.Count > 0)
				{
					movePhase [player] = 0;
					rotaPhase [player] = -1;
					moveID [player] = -2;
				}
			}
		}		
	}

    void LrigZoneRankUp(int player)
    {
        int num = lrigZoneNum[player];
        for (int i = num - 1; i >= 0; i--)
        {
            int id = fieldRankID(Fields.LRIGZONE, i, player);
            fieldRank[player, id]++;
            card[player, id].transform.position = vec3Addy(card[player, id].transform.position, 0.025f);
        }
    }

    void GoLrigBottom(int id, int player)
    {
        if (movePhase[player] == 0 && moveID[player] == -1)
        {
            LrigZoneRankUp(player % 2);
            moveID[player] = id;
            destination[player] = vec3Add(LRIGZONE, new Vector3(0f, 0f, 0f));
            moveTime[player] = standartTime;
            rotaPhase[player] = 0;
        }

        if (movePhase[player] == 1 && moveID[player] != -1)
        {
            ExitFunction(moveID[player], player);
            field[player % 2, moveID[player]] = Fields.LRIGZONE;
            fieldRank[player % 2, moveID[player]] = 0;
            lrigZoneNum[player % 2]++;

            moveID[player] = -1;
        }

        if (rotaPhase[player] == 0 && rotaID[player] == -1)
        {
            rotaID[player] = moveID[player];
            rotaTime[player] = moveTime[player];
            angle[player, 0] = 0f;
            angle[player, 1] = 0f;
            angle[player, 2] = 0f;
        }

        if (rotaPhase[player] == 2 && rotaID[player] != -1)
        {
            rotaID[player] = -1;
        }
    }

	void LrigSummon (int id, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			Singleton<SoundPlayer>.instance.playSE("summon");

			moveID [player] = id;
			destination [player] = vec3Add (LRIGZONE, new Vector3 (0f, 0.025f * lrigZoneNum [player % 2], 0f));
			moveTime [player] = standartTime;
			rotaPhase [player] = 0;
		}

		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			cipID = moveID [player] + player % 2 * 50;
			
//			rankDown (moveID [player], player);
//            lrig_deckNum[player % 2]--;
            ExitFunction(moveID[player], player);
            field[player % 2, moveID[player]] = Fields.LRIGZONE;
			fieldRank [player % 2, moveID [player]] = lrigZoneNum [player % 2];
			lrigZoneNum [player % 2]++;
			
			UpdateLrigData (player % 2);
			
			moveID [player] = -1;
			if (phase == Phases.GrowPhase)
				lrigSummonFlag [player] = true;
		}

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 0f;
		}

		if (rotaPhase [player] == 2 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
	}
	
	void Grow (int id, int player)
	{
        //dont grow
        if (DontGrowFlag[player % 2])
            return;

		if (chainMotion [0] == -1 && movePhase [player] == 0 && rotaPhase [player] == -1)
			chainMotion [0] = 0;

		if (chainMotion [0] == 0)
		{
			if (NoCostGrow)
			{
				NoCostGrow = false;
				chainMotion [0] = 2;
				movePhase [player] = 0;
				rotaPhase [player] = -1;				
			}
			else
			{
				//			PayGrowCost(id,player);
				PayModeCost (id, player, true);
				if (shortageFlag)
				{
					shortageFlag = false;
					chainMotion [0] = -1;
					return;
				}
				else if (moveID [player] == -1 && rotaID [player] == -1)
				{
					chainMotion [0] = 1;
					movePhase [player] = 0;
					rotaPhase [player] = -1;
				}
			}
		}
		
		if (chainMotion [0] == 1)
		{
			EnaSort (player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = 2;
				movePhase [player] = 0;
				rotaPhase [player] = -1;
			}
		}

        if (chainMotion[0] == 2)
        {
            if (moveID[player] == -1)
            {
                moveID[player] = -2;

                getCardScr(id, player).growEffectFlag = true;
            }
            else if (moveID[player] == -2)
            {
                moveID[player] = -1;
                chainMotion[0] = 3;
            }
        }

        if (chainMotion[0] == 3)
        {
			LrigSummon (id, player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = -1;
			}
		}
	}
	
	void EnaSort (int player)
	{
		if (enaNum [player % 2] == 0)
			return;

		if (movePhase [player] == 0 && moveID [player] < 0 && rotaID [player] < 0)
		{
			moveTime [player] = standartTime / 2;
			int count = 0;
			moveID [player] = fieldRankID (Fields.ENAZONE, count, player % 2);
			destination [player] = vec3Add (ENAZONE, new Vector3 (0f, 0.025f * count, -EnaWidth * count));
			Vector3 vecBuf = destination [player];
			if (player % 2 == 1)
				vecBuf = vec3Player2 (vecBuf);
			while (count<enaNum[player%2]-1 && card[player%2,moveID[player]].transform.position==vecBuf)
			{
				count++;
				moveID [player] = fieldRankID (Fields.ENAZONE, count, player % 2);
				destination [player] = vec3Add (ENAZONE, new Vector3 (0f, 0.025f * count, -EnaWidth * count));
				vecBuf = destination [player];
				if (player % 2 == 1)
					vecBuf = vec3Player2 (vecBuf);
			}
			if (card [player % 2, moveID [player]].transform.position == destination [player])
				return;
		}
		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			enaMoveCount [player % 2]++;
			if (fieldRank [player % 2, moveID [player]] == enaNum [player % 2] - 1)
			{
				moveID [player] = -1;
				enaMoveCount [player % 2] = 0;
			}
			else
			{
				movePhase [player] = 0;
				moveID [player] = -2;
			}
		}
	}

    bool setSortID(Fields f, int player, int count)
    {
        int num = fieldAllNum(f, player % 2);
        if (num < 0)
            return false;

        moveTime[player] = standartTime / 2;

        int firstCount = count;
        Vector3 vecBuf = new Vector3(0f, 0f, 0f);

        while (count==firstCount || (count < num  && card[player % 2, moveID[player]].transform.position == vecBuf))
        {
            moveID[player] = fieldRankID(f, count, player % 2);
            destination[player] = setSortDestination(f, count);

            vecBuf = destination[player];
            if (player % 2 == 1)
                vecBuf = vec3Player2(vecBuf);

            count++;
        }

        if (card[player % 2, moveID[player]].transform.position == destination[player])
            return false;
 
        return true;
    }

    Vector3 setSortDestination(Fields f, int count)
    {
        switch (f)
        {
            case Fields.ENAZONE:
                return vec3Add(ENAZONE, new Vector3(0f, 0.025f * count, -EnaWidth * count));

            case Fields.LIFECLOTH:
                return vec3Add(LIFECLOTH, new Vector3(LifeClothWidth * count, 0.025f * count, 0f));
        }

        return new Vector3();
    }

    void ZoneSort(Fields f,int player)
    {
        if (fieldAllNum(f,player%2) == 0)
            return;

        if (movePhase[player] == 0 && moveID[player] == -1 && rotaID[player] == -1)
        {
            setSortID(f, player, 0);
        }

        if (movePhase[player] == 1 && moveID[player] != -1)
        {
            int count = fieldRank[player % 2, moveID[player]];

            if (count + 1 == fieldAllNum(f, player%2))
                moveID[player] = -1;
            else
            {
                movePhase[player] = 0;
                if (!setSortID(f, player, count))
                    moveID[player] = -1;
            }
        }
    }

	void TrashSort (int player)
	{
		for (int i=0; i<trashNum[player%2]; i++)
		{
			int id = fieldRankID (Fields.TRASH, i, player % 2);
			Vector3 vec = vec3Add (TRASH, new Vector3 (0f, 0.025f * i, 0f));
			if (player % 2 == 1)
				vec = vec3Player2 (vec);
			card [player % 2, id].transform.position = vec;
		}
	}

	void ChantYourSpell (int ID, int player)
	{
		if (chainMotion [0] == -1)
		{
			chainMotion[0]=6;
		}

		otherChant(ID,player,4);

/*		if(moveID[player] == -1 && rotaID[player]==-1){
			CardScript sc=getCardScr(ID,player%2);
			sc.player=1-sc.player;
		}*/
	}

	void ChantSpell (int ID, int player)
	{
		if (!routingChant (ID, player,0) || mirutychoFlag[player%2])
			return;

		if (chainMotion [0] == 0)
		{
			
			//適応
			applySpellCostDown (ID, player % 2, 0);
			
			PayModeCost (ID, player, false);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				if (shortageFlag)
				{
					//適応失敗
					restrateSpellCostDown (ID, player % 2, 0);

					chainMotion [0] = -1;
					movePhase [player] = -1;
					return;
				}
				else
				{
					//適応成功
					restrateSpellCostDown (ID, player % 2, 0);
					resetSpellCostDown (player % 2, 0);
					
					chainMotion [0] = 1;
					movePhase [player] = 0;
					rotaPhase [player] = -1;
				}
			}
		}
		otherChant(ID,player,4);
	}
	
	void ChantArts (int ID, int player)
	{
		if(ArtsLimitFlag[player%2])
			return;

		if (!routingChant (ID, player,0))
			return;

		if(!costingChantArts(ID,player))
			return;

		otherChant(ID,player,5);
	}

	bool routingChant (int ID, int player,int lastRoute)
	{
		if (chainMotion [0] == -1)
		{
            if(checkType(ID,player%2, cardTypeInfo.レゾナ)){
                getCardScr(ID, player % 2).useResona = true;
                return false;
            }
			else if (useLimit (ID, player % 2))
				return false;
            else if(!toldChantEffFlag && EffecterNowID==-1)
            {
                toldChantEffFlag = true;
                moveID[player] = -2;

                CardScript sc = getCardScr(ID, player % 2);
                sc.chantEffectFlag = true;
            }
            else if (movePhase[player] == 0 && rotaPhase[player] == -1)
            {
                toldChantEffFlag = false;
                moveID[player] = -1;
                chainMotion[0] = lastRoute;
            }
		}
		return true;
	}

	bool costingChantArts(int ID,int player){
		if (chainMotion [0] == 0)
		{
			
			//適応
			applySpellCostDown (ID, player % 2, 1);
			
			PayModeCost (ID, player, false);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				if (shortageFlag)
				{
					//適応失敗
					restrateSpellCostDown (ID, player % 2, 1);
					
					chainMotion [0] = -1;
					movePhase [player] = -1;
					return false;
				}
				else
				{
					//適応成功
					restrateSpellCostDown (ID, player % 2, 1);
					resetSpellCostDown (player % 2, 1);
					
					chainMotion [0] = 1;
					movePhase [player] = 0;
					rotaPhase [player] = -1;
				}
			}
		}

		return true;
	}

	void otherChant(int ID, int player, int lastRoute){
		if (chainMotion [0] == 1)
		{
			EnaSort (player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = 2;
				movePhase [player] = 0;
				rotaPhase [player] = -1;
			}
		}

		if (chainMotion [0] == 2)
		{
			GoCheckZone (ID, player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = 3;
				movePhase [player] = 0;
				rotaPhase [player] = -1;
			}
		}

		if (chainMotion [0] == 6)//ミルルン用
		{
			GoYourCheckZone (ID, player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = 3;
				movePhase [player] = 0;
				rotaPhase [player] = -1;
			}
		}

		if (chainMotion [0] == 3)
		{
			if (moveID [player] == -1)
				moveID [player] = -2;
			else
			{
				moveID [player] = -1;

				chainMotion [0] = lastRoute;
			}
		}

		if (chainMotion [0] == 4)
		{
			if(isUsedSpellGoTrash(ID,player))
				GoTrash (ID, player);

			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = -1;

				ChantSpellID = ID + player % 2 * 50;
			}

		}
		
		if (chainMotion [0] == 5)
		{
            if (isUsedSpellGoTrash(ID, player))
                GoLrigTrash(ID, player);

			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = -1;

                ChantArtsID = ID + player % 2 * 50;
			}
		}
	}

    bool isUsedSpellGoTrash(int ID,int player)
    {
        return field[player % 2, ID] == Fields.CHECKZONE || field[player % 2, ID] == Fields.YOURCHECKZONE || field[player % 2, ID] == Fields.Non;
    }

	
	void Crash (int ID, int player)
	{
		if (notCrashFlag)
			return;
		
		int bufID = moveID [player];
		if (bufID < 0)
			bufID = rotaID [player];
		
		if (chainMotion [0] == -1 && movePhase [player] == 0 && rotaPhase [player] == -1)
			chainMotion [0] = 0;
		if (chainMotion [0] == 0)
		{
			GoCheckZone (ID, player);

			if(movePhase[player] == 0 && moveCount[player]==0)
				Singleton<SoundPlayer>.instance.playSE("crash");

			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = 1;
				movePhase [player] = 0;
				rotaPhase [player] = -1;
				waitTime = standartTime;
			}
		}
		if (chainMotion [0] == 1)
		{//クッション
			if (moveID [player] == -1)
			{
				moveID [player] = bufID;
			}
			else
			{
				if (sheilaFlag)
				{
					sheilaFlag = false;
					chainMotion [0] = 3;//sheila
				}
				else
					chainMotion [0] = 2;
				moveID [player] = -1;
			}
		}
		if (chainMotion [0] == 2)
		{
			EnaCharge (bufID, player);
			if (moveID [player] == -1 && rotaID [player] == -1 && waitTime == 0)
			{
				chainMotion [0] = -1;
			}
		}
		
		//sheila mode
		if (chainMotion [0] == 3)
		{
			GoTrash (bufID, player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				movePhase [player] = 0;
				rotaPhase [player] = -1;
				chainMotion [0] = 4;
			}
		}		
		if (chainMotion [0] == 4)
		{
			LifeClothSet (fieldRankID (Fields.MAINDECK, deckNum [player % 2] - 1, player % 2), player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = -1;
			}
		}
		
	}
	
	void SpellCutInEffectCheck ()
	{
		if (SpellCutInID >= 0)
		{
			CardScript sc = card [SpellCutInID / 50, SpellCutInID % 50].GetComponent<CardScript> ();
			if (sc.effectFlag)
			{
				SpellCutInEffect = true;
				effecter [1] = SpellCutInID;

				//animation
				setAnimation (SpellCutInID);
				//sound
				Singleton<SoundPlayer>.instance.playSE("effect");

				for (int j=0; j<chainMotion.Length; j++)
				{
					chainMotionBuf [1, j] = chainMotion [j];
					chainMotion [j] = -1;
				}

				if (sc.effectTargetID [0] >= 0)
				{
					movePhase [sc.effectTargetID [0] / 50 + 6] = 0;
					rotaPhase [sc.effectTargetID [0] / 50 + 6] = -1;
				}
			}
		}

	}

	void BurstEffectFlagCheck ()
	{
		for (int i=0; i<100; i++)
		{
			CardScript sc = card [i / 50, i % 50].GetComponent<CardScript> ();
			if (sc.effectFlag && sc.BurstFlag)
			{
				BurstEffectIDBuf = i;
				return;
			}
		}
	}

	void SystemEffectCheck()
	{
		CardScript sc=SystemCard.GetComponent<CardScript>();

		if(sc.effectFlag){
			effectFlagSetBuf(sc);
			SystemEffectFlag=true;
			movePhase [sc.effectTargetID [0] / 50 + 2] = 0;
			rotaPhase [sc.effectTargetID [0] / 50 + 2] = -1;
		}
	}


    bool isUpEffect(CardScript sc)
    {
        if (sc == null)
            return false;

        return sc.effectFlag && !sc.lostEffect;
    }

    bool isUpDialog(CardScript sc)
    {
        if (sc == null)
            return false;

        return sc.DialogFlag && !sc.lostEffect;
    }

	void EffectFlagCheck ()
	{
		//通常のカード
		for (int n=0; n<100; n++)
		{
			//iの計算
			int i=n;

			if(getTurnPlayer()==1)
				i = (n+50)%100;

			//本題
			CardScript sc = card [i / 50, i % 50].GetComponent<CardScript> ();

            if (isUpEffect(sc) && effectFlag)
            {
                dualEffectTriggerd = true;
                return;
            }
		
			if(isUpEffect(sc) && !effectFlag)
			{
				effectFlag = true;
				effecter [0] = i;
 				//animation
                if (sc.effectMotion.Count > 0 && sc.effectMotion[0] != (int)Motions.UpIgnition)
                {
                    if (trueIgnitionID == -1)
                        setAnimation(i);
                    else
                        setAnimation(trueIgnitionID);

                    //sound
                    Singleton<SoundPlayer>.instance.playSE("effect");
                }
			
				if (i == BurstEffectIDBuf)
				{
					BurstEffectIDBuf = -1;
					BurstEffectID = i;
				}

				if (card [effecter [0] / 50, effecter [0] % 50].GetComponent<CardScript> ().Type == 3 
				    && !card [effecter [0] / 50, effecter [0] % 50].GetComponent<CardScript> ().BurstFlag
				    && field [effecter [0] / 50, effecter [0] % 50] == Fields.CHECKZONE)
				{
					selectCardListSpellCutIn (1 - effecter [0] / 50);
					if (selectCardList.Count > 0)
					{
						selectCardList.Clear ();
						AskSpellCutInFlag = true;
						SpellCutInPlayer = 1 - effecter [0] / 50;
					}
				}

				effectFlagSetBuf(sc);
			}
		}

        //trueID終わり
        if (trueIgnitionID != -1)
            trueIgnitionID = -1;

		//追加カード
		for (int i = 0; i < NewCardList.Count; i++)
		{
			//本題
			CardScript sc = NewCardList[i].NewCardObj.GetComponent<CardScript> ();
			
			if(sc.effectFlag)
			{
				effectFlag = true;
				effecter [0] = NewCardList[i].TrueID+NewCardList[i].TruePlayer*50;

				NewCardEffecterCount=i;
				NewCardEffectFlag=true;
				
				//animation
				setAnimation (NewCardList[i].TrueID+NewCardList[i].TruePlayer*50);
				
				if (i == BurstEffectIDBuf)
				{
					BurstEffectIDBuf = -1;
					BurstEffectID = i;
				}
				
				if (sc.Type == 3 
				    && !sc.BurstFlag
				    && NewCardList[i].MyField == Fields.CHECKZONE)
				{
					selectCardListSpellCutIn (1-sc.player);
					if (selectCardList.Count > 0)
					{
						selectCardList.Clear ();
						AskSpellCutInFlag = true;
						SpellCutInPlayer = 1-sc.player;
					}
				}
				
				effectFlagSetBuf(sc);
				
				break;
			}
		}

        //effectFlagの立ちがなし
        if (!effectFlag)
        {
            dualEffectTriggerd = false;
            exitedList.Clear();

        }
	}

	void effectFlagSetBuf(CardScript sc)
	{
		if (sc.effectFlag)
		{							
			GUImoveIDbuf = GUImoveID;
			GUImoveID = -1;

			for (int j=0; j<chainMotion.Length; j++)
			{
				chainMotionBuf [0, j] = chainMotion [j];
				chainMotion [j] = -1;
			}

			while (clickCursorID.Count>0)
			{
				clickCursorIDbuf.Add (clickCursorID [0]);
				clickCursorID.RemoveAt (0);
			}

		}

	}

    bool checkEffTagInput(bool isWaiting)
    {
        if (isWaiting)
            return !isMessageError();

        return clickCursorID.Count > 0;
    }

    int getEffTagInput(bool isWaiting)
    {
        if (isWaiting)
            return readParseMessage();

        int id = clickCursorID[0];
        clickCursorID.RemoveAt(0);
        return id;
    }

    void setJupiter(CardScript sc)
    {
        if (sc.effectTargetID.Count >= 3 && sc.effectMotion[2] == (int)Motions.JupiterResona && sc.effectTargetID[1] >= 0)
        {
            string[] str = new string[2];
            for (int j = 0; j < 2; j++)
            {
                if (checkACG(sc.effectTargetID[j] % 50, sc.effectTargetID[j] / 50))
                    return;

                str[j] = getCardScr(sc.effectTargetID[j]).Name;
            }

            if (str[0] != str[1])
                return;

            CardScript jup = getCardScr(sc.effectTargetID[2]);
            jup.setEffect(0, jup.player, Motions.TopEnaCharge);
            jup.setEffect(0, jup.player, Motions.TopEnaCharge);
        }
    }

    void effectTargetSet(int playerNum, int cursorIndex, CardScript sc, bool isWaiting)
    {
        bool told = false;

        for (int i = cursorIndex; i < sc.effectTargetID.Count && checkEffTagInput(isWaiting); i++)
        {
            if (sc.effectTargetID[i] == -1)
            {
                int inputID = getEffTagInput(isWaiting);

                effectSelectIDbuf.Add(inputID);

                if (inputID >= 0)
                {
                    sc.effectTargetID[i] = inputID;

                    if (isWaiting && field[inputID / 50, inputID % 50] != Fields.HAND)
                        targetShowList.Add(inputID);

                    //Jupiterだけの特別
                    setJupiter(sc);
 
                    if (sc.TargetIDEnable)
                        sc.TargetID.Add(inputID);

                    if (sc.effectTargetID.Count >= i + 1 || sc.effectTargetID[i + 1] != -1)
                        sc.inputReturn = i + 1;
                }
                else
                {
                    if (!told)
                    {
                        told = true;
                        sc.inputReturn = i;
                    }

                    sc.effectMotion.RemoveAt(i);
                    sc.effectTargetID.RemoveAt(i);
                    i--;
                }
            }
        }

        if (sc.effectTargetID.Count == 0)
            return;

        movePhase[sc.effectTargetID[0] / 50 + playerNum] = 0;
        rotaPhase[sc.effectTargetID[0] / 50 + playerNum] = -1;
    }

    void endEffect(CardScript sc, int playerNum)
    {
        if(!usedList.Contains(sc.Name))
            usedList.Add(sc.Name);

        IgniEffID = -1;

        if (trueIgniSeted)
            trueIgniSeted = false;
        else
            trueIgnitionID = -1;

        sc.effectTargetID.Clear();
        sc.effectMotion.Clear();
        sc.effectFlag = false;
        sc.Targetable.Clear();

        effectExecuteList.Clear();

        for (int j = 0; j < chainMotion.Length; j++)
        {
            chainMotion[j] = chainMotionBuf[playerNum / 6, j];
            chainMotionBuf[playerNum / 6, j] = -1;
        }

        if (SpellCutInEffect)
        {
            SpellCutInEffect = false;
            return;
        }

        apllyGrid = false;

        //TopList
        TopGoTrashList.Clear();

        //lastMostions
        LastMotions.Clear();

        if (NewCardEffectFlag)
        {
            NewCardEffectFlag = false;
            NewCardEffecterCount = -1;
        }

        EffecterNowID = -1;

        effectFlag = false;
        effecter[0] = -1;

        effectSelectIDbuf.Clear();
        dialogSelectbuf.Clear();

        GUImoveID = GUImoveIDbuf;
        GUImoveIDbuf = -1;

        while (clickCursorIDbuf.Count > 0)
        {
            clickCursorID.Add(clickCursorIDbuf[0]);
            clickCursorIDbuf.RemoveAt(0);
        }
    }

    void guiCancelFunc(int GUIIndex, CardScript sc)
    {
        sc.inputReturn = GUIIndex;

        for (int j = GUIIndex; j < sc.effectTargetID.Count; j++)
        {
            if (sc.effectTargetID[j] == -2)
            {
                sc.effectMotion.RemoveAt(j);
                sc.effectTargetID.RemoveAt(j);
                j--;
            }
        }
    }

    int getGUIInputID(bool isWaiting)
    {
        if (isWaiting)
            return readParseMessage();

        int x = GUImoveID;
        GUImoveID = -1;
        return x;
    }

    void setGUITargetInput(int index,CardScript sc,bool isWaiting)
    {
        sc.effectTargetID[index] = getGUIInputID(isWaiting);
        if (sc.TargetIDEnable)
            sc.TargetID.Add(sc.effectTargetID[index]);
        effectSelectIDbuf.Add(sc.effectTargetID[index]);

        bool flag = false;
        for (int i = index+1; i < sc.effectTargetID.Count; i++)
            if(sc.effectTargetID[i]==-2)
                flag=true;

        if (!flag)
            sc.inputReturn = index + 1;
    }

    bool targetableIntoListCheck(int fusionID)
    {
        return fusionID >= 0 && !exitedList.Contains(fusionID);
    }

    bool effectCursorInput(CardScript sc, int playerNum)
    {
        int selecter = sc.effectSelecter;
        int cursorIndex = -1;
        

        for (int i = 0; i < sc.BeforeCutInNum && i < sc.effectTargetID.Count; i++)
        {
            if (sc.effectTargetID[i] == -1)
            {
                cursorIndex = i;
                break;
            }
        }

        if (cursorIndex >= 0)
        {
            //oneHandDeathではselecterはターゲットの所有者
            if (sc.effectMotion[cursorIndex] == (int)Motions.oneHandDeath)
                selecter = oneHandDeathSelecter;

            //通信時
            if (waitYou(selecter))
            {
                if (canRead())
                {
                    effectTargetSet(playerNum, cursorIndex, sc, true);

                    return false;
                }
                else
                    return false;
            }
            else if (clickCursorID.Count == 0)
            {
                //showZoneGoTop
                if (sc.effectMotion[cursorIndex] == (int)Motions.ShowZoneGoTop || sc.effectMotion[cursorIndex] == (int)Motions.ShowZoneGoBottom)
                {
                    Motions m = Motions.GoDeck;
                    if (sc.effectMotion[cursorIndex] == (int)Motions.ShowZoneGoBottom)
                        m = Motions.GoDeckBottom;

                    
                    sc.effectMotion.RemoveAt(cursorIndex);
                    sc.effectTargetID.RemoveAt(cursorIndex);
                    sc.Targetable.Clear();

                    for (int i = 0; true; i++)
                    {
                        int x = getShowZoneID(i);
                        if (x < 0)
                        {
                            if (i == 0)
                                return false;
                            break;
                        }

   
                        sc.Targetable.Add(x);
                        sc.effectMotion.Insert(cursorIndex, (int)m);
                        sc.effectTargetID.Insert(cursorIndex, -1);
                    }
                }

                for (int i = cursorIndex; i < sc.effectTargetID.Count; i++)
                {
                    if (sc.effectMotion[cursorIndex] == sc.effectMotion[i] && sc.effectTargetID[i] == -1)
                        selectNum++;
                    else
                        break;
                }

                if (selectNum > 0)
                {
                    switch (sc.effectMotion[cursorIndex])
                    {
                        case (int)Motions.LifeSetFromHand:
                            for (int i = 0; i < fieldAllNum(Fields.HAND, selecter); i++)
                            {
                                int id = fieldRankID(Fields.HAND, i, selecter);
                                if (id >= 0)
                                    sc.Targetable.Add(id + selecter * 50);
                            }
                            break;
                    }

                    //costBanish
                    if (sc.effectMotion[cursorIndex] == (int)Motions.CostBanish)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            int id = fieldRankID(Fields.SIGNIZONE, i, sc.player);
                            if (id >= 0)
                                sc.Targetable.Add(id + sc.player * 50);
                        }
                    }

/*                    List<int> list = sc.Targetable;
                    int length = list.Count;
                    for (int i = 0; i < length; i++)
                    {
                        setTargetCursorID(list[i] % 50, list[i] / 50);
                    }
*/
                    foreach (var id in sc.Targetable)
                        if (targetableIntoListCheck(id) || sc.effectMotion[cursorIndex]==(int)Motions.oneHandDeath)
                            setTargetCursorID(id % 50, id / 50);
                    sc.Targetable.Clear();

                    if (targetCursorList.Count > 0)
                    {
                        selectCursorFlag = true;
                        if (sc.cursorCancel)
                            cursorCancel = true;
                    }
                    else
                    {
                        sc.effectMotion.RemoveAt(cursorIndex);
                        sc.effectTargetID.RemoveAt(cursorIndex);
                    }

                    return false;
                }
            }
            else
            {
                cursorCancel = false;

                effectTargetSet(playerNum, cursorIndex, sc, false);

                //通信
                if (isTrans && selecter == 0)
                {
                    sendMessageBuf();
                }

                return false;
            }
        }

        return true;
    }

    bool effectGUIInput(CardScript sc,int playerNum)
    {
        int selecter = sc.effectSelecter;
        int GUIIndex = -1;
        for (int i = 0; i < sc.effectTargetID.Count; i++)
        {
            if (sc.effectTargetID[i] == -2 && sc.effectMotion[i] == sc.effectMotion[0])
            {
                GUIIndex = i;
                break;
            }
        }

        if (GUIIndex >= 0)
        {
            if (waitYou(selecter))
            {
                if (canRead())
                {
                    for (int i = GUIIndex; i < sc.effectTargetID.Count && !isMessageError(); i++)
                    {
                        if (sc.effectTargetID[i] == -2)
                        {
                            if (getNextMessage(selecter) == cancelStr)
                            {
                                guiCancelFunc(GUIIndex, sc);
                                break;
                            }
                            else
                            {
                                setGUITargetInput(i, sc, true);

                                //animation
                                int id = sc.effectTargetID[i];
                                if (sc.effectMotion[i] == (int)Motions.GoHand &&
                                    (field[id / 50, id % 50] == Fields.MAINDECK || field[id / 50, id % 50] == Fields.TRASH))
                                    setAnimation(id);
                            }
                        }
                    }
                    movePhase[sc.effectTargetID[0] / 50 + playerNum] = 0;
                    rotaPhase[sc.effectTargetID[0] / 50 + playerNum] = -1;
                    clickCursorID.Clear();
                    return false;
                }
                else
                    return false;
            }
            else if (GUImoveID != -1 || (sc.Targetable.Count == 1 && !sc.GUIcancelEnable))
            {
                if (sc.Targetable.Count == 1 && !sc.GUIcancelEnable)
                {
                    GUImoveID = sc.Targetable[0];
                    messagesBuf.Add("" + sc.Targetable[0]);
                }

                //通信
                if (isTrans && selecter == 0)
                {
                    sendMessageBuf();
                }

                GUIcancelEnabel = false;

                if (sc.targetableSameLevelRemove)
                {
                    for (int i = 0; i < sc.Targetable.Count; i++)
                    {
                        int x = sc.Targetable[i] % 50;
                        int target = sc.Targetable[i] / 50;
                        int lev = getCardLevel(x, target);

                        if (getCardLevel(GUImoveID % 50, GUImoveID / 50) == lev)
                        {
                            sc.Targetable.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else if (!sc.targetableDontRemove)
                    sc.Targetable.Remove(GUImoveID);

                setGUITargetInput(GUIIndex, sc, false);

                movePhase[sc.effectTargetID[0] / 50 + playerNum] = 0;
                rotaPhase[sc.effectTargetID[0] / 50 + playerNum] = -1;
                return false;
            }
            else
            {
                if (GUIcancel)
                {
                    GUIcancel = false;
                    GUIcancelEnabel = false;

                    if (isTrans)
                    {
                        messagesBuf.Add(cancelStr);
                        sendMessageBuf();
                    }

                    guiCancelFunc(GUIIndex, sc);
                    return false;
                }
                else
                {
                    if (sc.GUIcancelEnable)
                        GUIcancelEnabel = true;

/*                    List<int> list = sc.Targetable;
                    int length = list.Count;
                    for (int i = 0; i < length; i++)
                    {
                        selectCardList.Add(list[i]);
                    }*/

                    foreach (var id in sc.Targetable)
                        if (targetableIntoListCheck(id))
                            selectCardList.Add(id);

                    if (selectCardList.Count > 0)
                        selectCardFlag = true;
                    else
                    {
                        sc.effectMotion.RemoveAt(GUIIndex);
                        sc.effectTargetID.RemoveAt(GUIIndex);
                    }


                    return false;
                }
            }

        }

        return true;
    }

	void Effect ()
	{
		CardScript sc;
		int playerNum = 2;
		if (SpellCutInFlag)
			playerNum = 6;

		sc = card [effecter [playerNum / 6] / 50, effecter [playerNum / 6] % 50].GetComponent<CardScript> ();

		if(!SpellCutInFlag && NewCardEffectFlag){
			sc=NewCardList[NewCardEffecterCount].NewCardObj.GetComponent<CardScript>();
		}

		int selecter = sc.effectSelecter;
		
		if (sc.effectTargetID.Count == 0)
		{
            //効果が終わったときの処理
            endEffect(sc, playerNum);
		}
		else
		{
			//-1入力
            if (!effectCursorInput(sc, playerNum))
                return;
			
			//-2入力
            if (!effectGUIInput(sc, playerNum))
                return;

            //-3入力
            for (int i = 0; i < sc.effectTargetID.Count; i++)
            {
                if (sc.effectTargetID[i] == -3 && i > 0 && sc.effectTargetID[i - 1] >= 0)
                    sc.effectTargetID[i] = sc.effectTargetID[i - 1];
            }

			//スぺルカットインの有無
			if (AskSpellCutInFlag)
			{
				if (waitYou( SpellCutInPlayer ))
				{
					if (canRead ())
					{
						AskSpellCutInFlag = false;
						string r = readMessage ();
						if (r == YesStr)
						{
							SpellCutInFlag = true;
							GUImoveID = readParseMessage ();
							return;
						}						
					}
					else
						return;
				}
				else
				{
					SpellCutInSelectFlag = true;
					AskSpellCutInFlag = false;
					return;
				}
			}
			
			//実行部 _effect

			if(!effectInitialized){
				if (sc.effectTargetID.Count > 0 && sc.effectTargetID [0] / 50 + playerNum < (int)constance.MovingNum)
				{
					movePhase [sc.effectTargetID [0] / 50 + playerNum] = 0;
					rotaPhase [sc.effectTargetID [0] / 50 + playerNum] = -1;
					effectInitialized=true;

                    if (trueIgnitionID != -1 && sc.effectTargetID[0] == sc.ID + sc.player * 50 && sc.effectMotion[0] == (int)Motions.Down)
                        sc.effectTargetID[0] = trueIgnitionID;

                    TargetNowID = sc.effectTargetID[0];
                    EffecterNowID = sc.ID + sc.player * 50;

                    return;
				}
			}

			effectBody(sc,playerNum);
		}
	}

	bool isEffectBanishing(Motions m)
	{
		return m == Motions.EnaCharge || m == Motions.CostBanish || m== Motions.PowerSumBanish || m == Motions.WarmHole;
	}

    bool isEffectRemoving(Motions m)
    {
        return m == Motions.EnaCharge
            || m == Motions.CostBanish
            || m == Motions.GoHand
            || m == Motions.CostGoTrash
            || m == Motions.GoTrash
            || m == Motions.GoDeck
            || m == Motions.GoDeckBottom
            || m == Motions.WarmHole
            ;
    }

    void stopEffect(CardScript sc)
    {
        if (sc.effectTargetID.Count == 0)
            return;

        sc.effectMotion.RemoveAt(0);
        sc.effectTargetID.RemoveAt(0);
        effectInitialized = false;
    }

    bool effectNotMove(int effectPlayer)
    {
        return moveID[effectPlayer] == -1 && rotaID[effectPlayer] == -1;
    }

	bool ChoiceEffectBody(CardScript sc,int playerNum)
	{
		Motions m = (Motions)sc.effectMotion [0];
		int normalizeedEffectID = sc.effectTargetID [0] - sc.effectMotion [0] * 100;
		int selecter = sc.effectSelecter;
		int effectPlayer = sc.effectTargetID [0] / 50 + playerNum;
		int effectID = sc.effectTargetID [0] % 50;

        if (isEffectRemoving(m) && !toldRemoving)
        {
            toldRemoving = true;
            removingID = sc.effectTargetID[0];

            if (checkType(removingID % 50, removingID / 50, cardTypeInfo.レゾナ))
                Replace(sc, removingID, Motions.GoLrigDeck, null);

            return false;
        }

        if (m == Motions.Draw)
        {
            //cmrたん
            if (CMRFlag[effectPlayer % 2] && phase != Phases.DrawPhase && phase != Phases.GrowPhase && getTurnPlayer() == effectPlayer % 2)
            {
                stopEffect(sc);
                return false;
            }

            drawNum[effectPlayer % 2] = 0;

            for (int i = 0; i < sc.effectMotion.Count; i++)
            {
                if (sc.effectMotion[i] == (int)Motions.Draw && sc.effectTargetID[i] == sc.effectTargetID[0])
                    drawNum[effectPlayer % 2]++;
            }

            int drawBuf = drawNum[effectPlayer % 2];

            DrawCard(effectPlayer);

            if (drawBuf != drawNum[effectPlayer % 2] && drawBuf != 1)
            {
                sc.effectTargetID.RemoveAt(0);
                sc.effectMotion.RemoveAt(0);
            }
        }
        else if (m == Motions.SetDialog)
        {
            if (sc.messages.Count == 0)
            {
                sc.setDialogNum((DialogNumType)sc.getParameta(parametaKey.settingDialogNum));
                return false;
            }
        }
        else if (m == Motions.NotDamageThisTurn)
        {
            notDamagingFlag[effectPlayer % 2] = true;
        }
        else if (m == Motions.DontGrow)
        {
            NextDontGrowFlag[effectPlayer % 2] = true;
        }
        else if (m == Motions.NextMinusLimit)
        {
            nextMinusLimitCount[effectPlayer % 2]++;
        }
        else if (m == Motions.ClassNumBanish)
        {
            cardClassInfo info = (cardClassInfo)sc.getParameta(parametaKey.ClassNumBanishTarget);
            int n = getClassNum(effectPlayer % 2, Fields.SIGNIZONE, info);

            sc.Targetable.Clear();
            sc.funcTargetIn(1 - effectPlayer % 2, Fields.SIGNIZONE);
            for (int i = 0; i < n && i < sc.Targetable.Count; i++)
            {
                sc.effectTargetID.Insert(1, -1);
                sc.effectMotion.Insert(1, (int)Motions.EnaCharge);
            }
        }
        else if (m == Motions.Exclude)
        {
            ExitFunction(effectID, effectPlayer % 2);
            field[effectPlayer % 2, effectID] = Fields.Removed;
        }
        else if (m == Motions.MaxPowerBounce)
        {
            int max = sc.getMaxPower(effectPlayer % 2);

            for (int i = 0; i < 3; i++)
            {
                int x = fieldRankID(Fields.SIGNIZONE, i, effectPlayer % 2);
                if (x >= 0 && getCardPower(x, effectPlayer % 2) == max)
                    sc.setEffect(x, effectPlayer % 2, Motions.GoHand);
            }
        }
        else if (m == Motions.AllHandDeath)
        {
            for (int i = 0; true; i++)
            {
                int x = fieldRankID(Fields.HAND, i, effectPlayer % 2);
                if (x < 0)
                    break;
                sc.setEffect(x, effectPlayer % 2, Motions.HandDeath);

            }
        }
        else if (m == Motions.JerashiGeizu)
        {
            JerashiGeizuFlag[effectPlayer % 2] = true;
        }
        else if (m == Motions.SigniAttackSkip)
        {
            SigniAttackSkipFlag = true;
        }
        else if (m == Motions.LrigAttackSkip)
        {
            LrigAttackSkipFlag = true;
        }
        else if (m == Motions.TraIgniCostZeroEnd)
        {
            colorCostArry cca = new colorCostArry(true);
            IgniCostDecrease icd = new IgniCostDecrease(effectID, effectPlayer % 2, EffecterNowID % 50, EffecterNowID / 50, cca, true, Fields.TRASH);
            IgniCostDecList.Add(icd);
        }
        else if (m == Motions.GridCountUp)
        {
            GridCount[effectPlayer % 2]++;
        }
        else if (isEffectBanishing(m))
        {
            //パワーサムのはじめの代入
            if (m == Motions.PowerSumBanish && effectID == 0)
            {
                int maxVal = sc.getParameta(parametaKey.powerSumBanishValue);

                sc.Targetable.Clear();
                sc.maxPowerTargetIn(maxVal);

                if (sc.Targetable.Count > 0)
                {
                    sc.effectTargetID.Insert(1, -1);
                    sc.effectMotion.Insert(1, (int)m);
                }
                else
                    sc.getParameta(parametaKey.powerSumBanishValue);

                return true;
            }

            //バニッシュ耐性の確認
            if (getCardScr(effectID, effectPlayer % 2).ResiBanish
                || (getCardScr(effectID, effectPlayer % 2).ResiYourEffBanish && EffecterNowID != -1 && EffecterNowID / 50 == 1 - effectPlayer % 2)
                )
                return true;

            //これからバニッシュを行うという報告
            if (!toldBanishing)
            {
                toldBanishing = true;
                BanishingID = sc.effectTargetID[0];
                return false;
            }

            //場にいたころのパワー、ランクを取得
            int power = getCardPower(effectID, effectPlayer % 2);
            int rank = getRank(effectID, effectPlayer % 2);

            //バニッシュ
            EnaCharge(effectID, effectPlayer);

            //終了時
            if (moveID[effectPlayer] == -1 && rotaID[effectPlayer] == -1)
            {
                BanishedID = effectID + effectPlayer % 2 * 50;
                toldBanishing = false;

                //パワーサムの代入
                switch (m)
                {
                    case Motions.PowerSumBanish:
                        powerSum += power;
                        int maxVal = sc.getParameta(parametaKey.powerSumBanishValue);

                        sc.Targetable.Clear();
                        sc.maxPowerTargetIn(maxVal - powerSum);

                        if (sc.Targetable.Count > 0)
                        {
                            sc.effectTargetID.Insert(1, -1);
                            sc.effectMotion.Insert(1, (int)m);
                        }
                        else
                        {
                            sc.getParameta(parametaKey.powerSumBanishValue);
                            powerSum = 0;
                        }
                        break;

                    case Motions.WarmHole:
                        SigniNotSummonCount[effectPlayer % 2, rank] = 2;
                        break;
                }
            }
        }
        else if (m == Motions.GoEnaZone)
        {
            EnaCharge(effectID, effectPlayer);
        }
        else if (m == Motions.GoShowZone)
        {
            GoShowZone(effectID, effectPlayer);
        }
        else if (m == Motions.TopGoShowZone)
        {
            if (!apllyGrid)
            {
                apllyGrid = true;
                for (int j = 0; j < GridCount[effectPlayer % 2]; j++)
                    sc.setEffect(0, effectPlayer % 2, m);
            }

            int x = -1;
            int i = 0;
            while (true)
            {
                x = fieldRankID(Fields.MAINDECK, deckNum[effectPlayer % 2] - 1 - i, effectPlayer % 2);
                if (x >= 0 && !isShowZoneID(x, effectPlayer % 2))
                    break;

                i++;

                if (deckNum[effectPlayer % 2] - 1 - i < 0)
                    return true;
            }

            GoShowZone(x, effectPlayer);
        }
        else if (m == Motions.AntiSpell || m == Motions.CounterSpell)
        {
            antiFlag = true;

            if (m == Motions.CounterSpell)
                counterSpellFlag = true;
        }
        else if (m == Motions.NextTurnArtsLim)
        {
            NextTurnAtrsLimFlag[effectPlayer % 2] = true;
        }
        else if (m == Motions.ViolenceEffect)
        {
            ViolenceCount++;
            requipUpFlag = true;
        }
        else if (m == Motions.UpMirrorMirage)
        {
            MirrorMirageFlag[effectPlayer % 2] = true;
            requipUpFlag = true;
        }
        else if (m == Motions.TopEnaCharge)
        {
            EnaCharge(fieldRankID(Fields.MAINDECK, deckNum[effectPlayer % 2] - 1, effectPlayer % 2), effectPlayer);
        }
        else if (m == Motions.TopGoLifeCloth)
        {
            LifeClothSet(fieldRankID(Fields.MAINDECK, deckNum[effectPlayer % 2] - 1, effectPlayer % 2), effectPlayer);
        }
        else if (m == Motions.FREEZE)
        {
            if (!checkFreeze(effectID, effectPlayer % 2))
                setOneFrameID(OneFrameIDType.FreezedID, sc.effectTargetID[0]);

            getCardScr(effectID, effectPlayer % 2).Freeze = true;
            Singleton<SoundPlayer>.instance.playSE("freeze");
        }
        else if (m == Motions.AddIgniEnd)
        {
            IgniAdd igni = front[effectPlayer % 2, effectID].GetComponent<IgniAdd>();
            if (igni == null)
            {
                front[effectPlayer % 2, effectID].AddComponent<IgniAdd>();
                igni = front[effectPlayer % 2, effectID].GetComponent<IgniAdd>();
            }

            AddIgniList.Add(new powerChange(effectID + effectPlayer % 2 * 50, EffecterNowID, sc.IgniAddID));
        }
        else if (m == Motions.LostEffectEnd)
        {
            changeLostEffect(effectID, effectPlayer % 2, true, EffecterNowID % 50, EffecterNowID / 50, true);
        }
        else if (m == Motions.EnAbility)
        {
            int t = sc.getParameta(parametaKey.EnAbilityType);

            if (t != -1)
                enAbilityListAdd(sc.effectTargetID[0], EffecterNowID, (ability)t);

        }
        else if (m == Motions.EnDoubleCrashEnd)
        {
            /*            if (!getCardScr(effectID, effectPlayer % 2).DoubleCrash)
                        {
                            getCardScr(effectID, effectPlayer % 2).setAbility(ability.DoubleCrash, true);
                            EnDoubleCrashID.Add(effectID + effectPlayer % 2 * 50);
                        }*/

            enAbilityListAdd(sc.effectTargetID[0], EffecterNowID, ability.DoubleCrash);
        }
        else if (m == Motions.EnResiBanishEnd)
        {
            /*            if (!getCardScr(effectID, effectPlayer % 2).ResiBanish)
                        {
                            getCardScr(effectID, effectPlayer % 2).ResiBanish = true;
                            EnResiBanishID.Add(effectID + effectPlayer % 2 * 50);
                        }*/
            enAbilityListAdd(sc.effectTargetID[0], EffecterNowID, ability.resiBanish);
        }
        else if (m == Motions.EnLancerEnd)
        {
            /*            if (!getCardScr(effectID, effectPlayer % 2).lancer)
                        {
                            getCardScr(effectID, effectPlayer % 2).lancer = true;
                            EnLancerID.Add(effectID + effectPlayer % 2 * 50);
                        }*/
            enAbilityListAdd(sc.effectTargetID[0], EffecterNowID, ability.Lancer);
        }
        else if (m == Motions.DontAttack)
        {
            //            card[sc.effectTargetID[0] / 50, effectID].GetComponent<CardScript>().Attackable = false;
            //            dontAttackList.Add(sc.effectTargetID[0]);
            enAbilityListAdd(sc.effectTargetID[0], EffecterNowID, ability.DontAttack);
        }
        else if (m == Motions.UpIgnition)
        {
            UpIgnitionOther(effectID, effectPlayer % 2);
            trueIgnitionID = EffecterNowID;
            trueIgniSeted = true;

        }
        else if (m == Motions.GoHand)
        {
            //cmrたん
            if (CMRFlag[effectPlayer % 2] && phase != Phases.DrawPhase && phase != Phases.GrowPhase && getTurnPlayer() == effectPlayer % 2)
            {
                stopEffect(sc);
                return false;
            }

            GoHand(effectID, effectPlayer);

        }
        else if (m == Motions.GoLrigDeck)
        {
            GoLrigDeck(effectID, effectPlayer);
        }
        else if (m == Motions.DontShuffleGoHand)
        {
            DontShuffleGoHand(effectID, effectPlayer);
        }
        else if (m == Motions.CostDown)
        {
            int color = sc.getParameta(parametaKey.CostDownColor);
            int num = sc.getParameta(parametaKey.CostDownNum);
            int isArts = sc.getParameta(parametaKey.SpellOrArts);
            setSpellCostDown(color, num, effectPlayer % 2, isArts);
        }
        else if (m == Motions.GoDeck)
        {
            GoDeck(effectID, effectPlayer);
        }
        else if (m == Motions.GOLrigTrash || m == Motions.Exceed)
        {
            GoLrigTrash(effectID, effectPlayer);
        }
        else if (m == Motions.TopGoTrash)
        {
            GoTrash(fieldRankID(Fields.MAINDECK, deckNum[effectPlayer % 2] - 1, effectPlayer % 2), effectPlayer);

            if (exitID >= 0)
                TopGoTrashList.Add(new powerChange(exitID, EffecterNowID, 0));
        }
        else if (m == Motions.GoTrash)
        {
            GoTrash(effectID, effectPlayer);

            if (getExitID((int)Fields.ENAZONE, -1) != -1)
                sc.effectInsertOne(getExitID((int)Fields.ENAZONE, -1) / 50, Motions.EnaSort);
        }
        else if (m == Motions.CostGoTrash || m == Motions.LevelSumCostGoTrash)
        {
            //ACGによる無効
            if (checkACG(effectID, effectPlayer % 2) && field[effectPlayer % 2, effectID] == Fields.SIGNIZONE)
            {
                effectShortageID = EffecterNowID;

                if (EffecterNowID == -1)
                {
                    SystemCardScr.effectTargetID.Clear();
                    SystemCardScr.effectMotion.Clear();
                }
                return false;
            }

            int lev = getCardLevel(effectID, effectPlayer % 2);//処理前のレベルを参照

            int t = tellEffectID;
            tellEffectID = -1;

            //効果ではない
            GoTrash(effectID, effectPlayer);

            tellEffectID = t;

            if (notMoving(effectPlayer))
            {
                if (m == Motions.CostGoTrash)
                {
                    if (getExitID((int)Fields.ENAZONE, -1) != -1)
                        sc.effectInsertOne(getExitID((int)Fields.ENAZONE, -1) / 50, Motions.EnaSort);


                    //costGoTrashList
                    int client = EffecterNowID;
                    if (SystemEffectFlag)
                        client = SystemCardClientID;

                    CardScript clientScr = getCardScr(client);
                    if (clientScr != null && clientScr.costGoTrashIDenable)
                        costGoTrashIDList.Add(new powerChange(sc.effectTargetID[0], client, 0));
                }
                else if (m == Motions.LevelSumCostGoTrash)
                {
                    targetLevelSum -= lev;
                    if (targetLevelSum > 0)
                    {
                        sc.funcTargetIn(effectPlayer % 2, Fields.SIGNIZONE, targetLevelSumCheck, this);
                        sc.effectTargetID.Insert(1, -1);
                        sc.effectMotion.Insert(1, (int)m);
                    }
                }
            }
        }
        else if (m == Motions.GoDeckBottom)
        {
            GoDeckBottom(effectID, effectPlayer);
        }
        else if (m == Motions.SigniRise)
        {
            int rank = fieldRank[effectPlayer % 2, effectID];
            SigniRise(effectPlayer, rank);
        }
        else if (m == Motions.SigniFall)
        {
            int rank = fieldRank[effectPlayer % 2, effectID];

            SigniFall(effectPlayer, rank);

            if (moveID[effectPlayer] == -1 && rotaID[effectPlayer] == -1)
            {
                risingID = -1;
                if (sc.messages.Count > 0)
                    sc.messages.RemoveAt(0);
            }
        }
        else if (m == Motions.HandDeath || m == Motions.CostHandDeath)
        {
            HandDeath(effectID, effectPlayer);
        }
        else if (m == Motions.ChantArts)
        {
            if (routingChant(effectID, effectPlayer, 1))
                otherChant(effectID, effectPlayer, 5);
        }
        else if (m == Motions.ChantSpell)
        {
            if (routingChant(effectID, effectPlayer, 2))
                otherChant(effectID, effectPlayer, 4);
        }
        else if (m == Motions.ChantYourSpell)
        {
            ChantYourSpell(effectID, effectPlayer);
        }
        else if (m == Motions.oneHandDeath)
        {
            if (effectID == 0 || effectID == 50)
            {
                oneHandDeathSelecter = effectID / 50;

                sc.effectTargetID[0] = -1;
                sc.Targetable.Clear();
                for (int i = 0; i < handNum[effectPlayer % 2]; i++)
                {
                    int id = fieldRankID(Fields.HAND, i, effectPlayer % 2);
                    if (id > 0)
                        sc.Targetable.Add(id + effectPlayer % 2 * 50);
                }

                if (sc.Targetable.Count == 0)
                    return true;

                return false;
            }
            else
                HandDeath(effectID, effectPlayer);
        }
        else if (m == Motions.ColorHandDeath)
        {
            if (normalizeedEffectID >= 0)
            {
                int color = normalizeedEffectID % 50;
                int target = normalizeedEffectID / 50;

                sc.Targetable.Clear();
                for (int i = 0; i < handNum[target]; i++)
                {
                    int id = fieldRankID(Fields.HAND, i, target);
                    if (id >= 0 && getCardColor(id, target) == color && getCardType(id, target) == 2)
                        sc.Targetable.Add(id + target * 50);
                }

                if (sc.Targetable.Count > 0)
                {
                    sc.effectTargetID[0] = -1;
                }
                else
                {
                    sc.effectTargetID.RemoveAt(0);
                    sc.effectMotion.RemoveAt(0);
                }
                return false; ;
            }
            else
                HandDeath(effectID, effectPlayer);
        }
        else if (m == Motions.LrigSummon)
        {
            LrigSummon(effectID, effectPlayer);
        }
        else if (m == Motions.RandomHandDeath)
        {
            if (HandDeathID == -1)
            {
                if (fieldAllNum(Fields.HAND, effectPlayer % 2) == 0)
                    return true;

                moveID[effectPlayer] = -2;
                if (waitYou(selecter))
                {
                    if (canRead())
                    {
                        HandDeathID = readParseMessage() % 50;
                    }
                }
                else
                {
                    int rank = Random.Range(0, handNum[effectPlayer % 2] - 1);
                    HandDeathID = fieldRankID(Fields.HAND, rank, effectPlayer % 2);

                    //通信
                    if (isTrans && selecter == 0)
                    {
                        messagesBuf.Add("" + HandDeathID);
                        sendMessageBuf();
                    }
                }
            }
            else
            {
                if (moveID[effectPlayer] == -2)
                    moveID[effectPlayer] = -1;
                HandDeath(HandDeathID, effectPlayer);
                if (moveID[effectPlayer] == -1)
                    HandDeathID = -1;
            }
        }
        else if (m == Motions.OpenHand)
        {
            OpenHand(effectPlayer);
        }
        else if (m == Motions.CloseHand)
        {
            CloseHand(effectPlayer);
        }
        else if (m == Motions.Open)
        {
            Open(effectID, effectPlayer);
        }
        else if (m == Motions.onePlayerOpen)
        {
            if (!isTrans || selecter == 0)
            {
                Open(effectID, effectPlayer);
            }
        }
        else if (m == Motions.onePlayerLifeOpen)
        {
            if (!isTrans || selecter == 0)
            {
                int id = fieldRankID(Fields.LIFECLOTH, LifeClothNum[effectPlayer % 2] - effectID % 50 - 1, effectPlayer % 2);
                Open(id, effectPlayer);
            }
        }
        else if (m == Motions.onePlayerLifeClose)
        {
            if (!isTrans || selecter == 0)
            {
                int id = fieldRankID(Fields.LIFECLOTH, LifeClothNum[effectPlayer % 2] - effectID % 50 - 1, effectPlayer % 2);
                Close(id, effectPlayer);
            }
        }
        else if (m == Motions.Close)
        {
            Close(effectID, effectPlayer);
        }
        else if (m == Motions.Down || m == Motions.DownAndFreeze || m == Motions.EffectDown || m == Motions.DownAndCostDown)
        {
            if (getIDConditionInt(effectID, effectPlayer % 2) == (int)Conditions.Up)
            {
                Down(effectID, effectPlayer);

                if (moveID[effectPlayer] == -1 && rotaID[effectPlayer] == -1)
                {
                    downID = effectID + effectPlayer % 2 * 50;

                    if (m == Motions.EffectDown || m == Motions.DownAndFreeze)
                        effectDownedID = effectID + effectPlayer % 2 * 50;
                }
            }

            if (moveID[effectPlayer] == -1 && rotaID[effectPlayer] == -1)
            {
                if (m == Motions.DownAndFreeze)
                {
                    sc.effectTargetID.Add(effectID + effectPlayer % 2 * 50);
                    sc.effectMotion.Add((int)Motions.FREEZE);
                }

                if (m == Motions.DownAndCostDown)
                {
                    sc.effectTargetID.Add(effectPlayer % 2 * 50);
                    sc.effectMotion.Add((int)Motions.CostDown);
                }
            }
        }
        else if (m == Motions.Up)
        {
            Up(effectID, effectPlayer);
        }
        else if (m == Motions.Damaging)
        {
            if (notDamagingFlag[effectPlayer % 2])
                return true;

            if (fieldAllNum(Fields.LIFECLOTH, effectPlayer % 2) > 0)
            {
                int id = fieldRankID(Fields.LIFECLOTH, LifeClothNum[effectPlayer % 2] - 1, effectPlayer % 2);
                if (id >= 0)
                    Crash(id, effectPlayer);
            }
            else
                EndMessage(1 - effectPlayer % 2);
        }
        else if (m == Motions.Crash)
        {
            Crash(effectID, effectPlayer);
        }
        else if (m == Motions.TopLifeGohand)
        {
            int id = fieldRankID(Fields.LIFECLOTH, LifeClothNum[effectPlayer % 2] - 1, effectPlayer % 2);
            if (id >= 0)
                GoHand(id, effectPlayer);
        }
        else if (m == Motions.TopCrash)
        {
            int id = fieldRankID(Fields.LIFECLOTH, LifeClothNum[effectPlayer % 2] - 1, effectPlayer % 2);
            sc.effectTargetID[0] = id + 50 * (effectPlayer % 2);//移動対象がtargetに入るように工夫
            if (id >= 0)
                Crash(id, effectPlayer);
        }
        else if (m == Motions.NotBurstTopCrash)
        {
            //この瞬間だけtrue
            notBurst = true;

            int id = fieldRankID(Fields.LIFECLOTH, LifeClothNum[effectPlayer % 2] - 1, effectPlayer % 2);
            sc.effectTargetID[0] = id + 50 * (effectPlayer % 2);//移動対象がtargetに入るように工夫
            if (id >= 0)
                Crash(id, effectPlayer);

            notBurst = false;
        }
        else if (m == Motions.LifeClothSet || m == Motions.LifeSetFromHand)
        {
            LifeClothSet(effectID, effectPlayer);
        }
        else if (m == Motions.EnaSort)
        {
            EnaSort(effectPlayer);
        }
        else if (m == Motions.LifeClothSort)
        {
            ZoneSort(Fields.LIFECLOTH, effectPlayer);
        }
        else if (m == Motions.GoLrigBottom)
        {
            GoLrigBottom(effectID, effectPlayer);
        }
        else if (m == Motions.HandSort)
        {
            HandSort(effectPlayer);
        }
        else if (m == Motions.AntiCheck)
        {
            sc.AntiCheck = false;
        }
        else if (m == Motions.ExtraTrun)
        {
            NextExtraTurnCount++;
        }
        else if (m == Motions.GoNextTurn)
        {
            GoNextTrunFlag = true;

            for (int i = 0; i < 2; i++)
            {
                if (moveID[i] != -1)
                {
                    moveID[i] = -1;
                    rotaID[i] = -1;
                    selectClickID = -1;
                }
            }

        }
        else if (m == Motions.trashCardAkumaCharm)
        {
            sc.Targetable.Clear();
            sc.funcTargetIn(sc.player, Fields.TRASH);
            if (sc.Targetable.Count == 0)
                return true;

            sc.effectTargetID.Insert(1, -2);
            sc.effectMotion.Insert(1, (int)Motions.SetCharmizeID);

            sc.effectTargetID.Insert(2, sc.effectTargetID[0]);
            sc.effectMotion.Insert(2, (int)Motions.trashCardAkumaCharm_2);
        }
        else if (m == Motions.trashCardAkumaCharm_2)
        {
            sc.Targetable.Clear();
            sc.ClassTargetIn(sc.player, Fields.SIGNIZONE, cardClassInfo.精像_悪魔);
            if (sc.Targetable.Count == 0)
                return true;
            sc.effectTargetID.Insert(1, -1);
            sc.effectMotion.Insert(1, (int)Motions.SetCharm);
        }
        else if (m == Motions.SetCharmizeID)
        {
            sc.CharmizeID = sc.effectTargetID[0];
        }
        else if (m == Motions.SetCharm)
        {
            int x = fieldRank[effectPlayer % 2, effectID];
            SetCharm(sc.CharmizeID % 50, effectPlayer, x);
        }
        else if (m == Motions.TopSetCharm)
        {
            int x = fieldRank[effectPlayer % 2, effectID];
            sc.CharmizeID = fieldRankID(Fields.MAINDECK, deckNum[effectPlayer % 2] - 1, effectPlayer % 2) + effectPlayer % 2 * 50;
            SetCharm(sc.CharmizeID % 50, effectPlayer, x);
        }
        else if (m == Motions.GoUnderZone)
        {
            int rank = -1;

            if (sc.messages.Count > 0 && int.TryParse(sc.messages[0], out rank))
            {
                //risingIDのせってい
                if (risingID == -1)
                {
                    risingID = fieldRankID(Fields.SIGNIZONE, rank, effectPlayer % 2);

                    if (risingID >= 0)
                    {
                        risingID += effectPlayer % 2 * 50;

                        sc.effectTargetID.Insert(0, risingID);
                        sc.effectMotion.Insert(0, (int)Motions.SigniRise);

                        sc.effectTargetID.Add(risingID);
                        sc.effectMotion.Add((int)Motions.SigniFall);

                        effectInitialized = false;

                        return false;
                    }

                    return true;
                }
                //ほんたい
                GoUnderZone(effectID, effectPlayer, rank);
            }

        }
        else if (m == Motions.changeBaseEnd)
        {
            int b = sc.getParameta(parametaKey.changeBaseValue);
            changeBasePower(effectID, effectPlayer % 2, b);
            changeBasedList.Add(getFusionID(effectID, effectPlayer % 2));
        }
        else if (m == Motions.PowerUpEndPhase || m == Motions.PowerUpLevelEnd || m == Motions.CostPowerUpEnd)
        {
            if (!Rian_pow_Flag[1 - EffecterNowID / 50])
            {
                int x = card[effecter[playerNum / 6] / 50, effecter[playerNum / 6] % 50].GetComponent<CardScript>().powerUpValue;
                if (m == Motions.PowerUpLevelEnd)
                    x *= getCardLevel(effectID, effectPlayer % 2);

                powerUpEndPhase(effectID, effectPlayer, x, effecter[playerNum / 6]);
            }
        }
        else if (m == Motions.PowerUpAllEnd)
        {
            if (!Rian_pow_Flag[1 - EffecterNowID / 50])
            {
                int x = card[effecter[playerNum / 6] / 50, effecter[playerNum / 6] % 50].GetComponent<CardScript>().powerUpValue;

                for (int i = 0; i < 3; i++)
                {
                    int id = fieldRankID(Fields.SIGNIZONE, i, effectPlayer % 2);
                    if (id >= 0)
                        powerUpEndPhase(id, effectPlayer, x, effecter[playerNum / 6]);
                }
            }
        }
        else if (m == Motions.DoublePowerEnd)
        {
            int x = card[effectPlayer % 2, effectID].GetComponent<CardScript>().Power;
            powerUpEndPhase(effectID, effectPlayer, x, effecter[playerNum / 6]);
        }
        else if (m == Motions.MoveAttackFront)
        {
            MoveSigniPosition(effectID, effectPlayer, AttackFrontRank);
        }
        else if (m == Motions.ChangePosition)
        {
            int rank = fieldRank[EffecterNowID / 50, EffecterNowID % 50];
            MoveSigniPosition(effectID, effectPlayer, rank);
        }
        else if (m == Motions.RePosition)
        {
            if (!ReposiFlag)
                RePosition(effectPlayer, selecter);

            if (ReposiFlag)
            {
                if (selectSigniZone == -1)
                {
                    movePhase[effectPlayer] = 0;

                    if (waitYou(selecter))
                    {
                        if (canRead())
                        {
                            selectSigniZone = int.Parse(readMessage());
                        }
                        return false;
                    }
                    else
                    {
                        selectSigniZoneFlag = true;
                        RePosiZoneUp(effectPlayer % 2);
                        selectSigniPlayer = selecter;

                        return false; ;
                    }
                }
                else
                {
                    if (isTrans && messagesBuf.Count > 0 && selecter == 0)
                    {
                        sendMessageBuf();
                    }

                    MoveSigniPosition(ReposiID[0], effectPlayer, selectSigniZone);

                    if (moveID[effectPlayer] == -1 && rotaID[effectPlayer] == -1)
                    {
                        selectSigniZone = -1;

                        ReposiID.RemoveAt(0);

                        if (ReposiID.Count > 0)
                        {
                            return false;
                        }
                        else
                        {
                            ReposiFlag = false;

                            Repositioned[0] = false;
                            Repositioned[1] = false;
                            Repositioned[2] = false;
                        }
                    }
                }
            }
        }
        else if (m == Motions.SigniZonePowerUp)
        {
            if (selectSigniZone == -1)
            {
                if (waitYou(effectPlayer % 2))
                {
                    if (canRead())
                        selectSigniZone = int.Parse(readMessage());
                }
                else
                {
                    selectSigniZoneFlag = true;
                    SigniZoneUp(effectPlayer % 2, true);
                    selectSigniPlayer = selecter;
                }

                return false;
            }
            else
            {
                int turnCount = 2;
                zonePowerList.Add(new zonePowrUp(sc.powerUpValue, selectSigniZone, EffecterNowID, effectPlayer % 2, turnCount));
                selectSigniZone = -1;
            }
        }
        else if (m == Motions.Summon || m == Motions.RebornEndPhase || m == Motions.TempResonaSummon
            || m == Motions.JupiterResona || m == Motions.NotCipSummon || m == Motions.NotCipTempResona
            || (m == Motions.DownSummonFromTrash && field[effectPlayer % 2, effectID] == Fields.TRASH))
        {
            if (fieldAllNum(Fields.SIGNIZONE, effectPlayer % 2) < signiSumLimit[effectPlayer % 2])
            {
                if (selectSigniZone == -1)
                {
                    if (waitYou(effectPlayer % 2))
                    {
                        if (canRead())
                        {
                            selectSigniZone = int.Parse(readMessage());
                        }
                        return false;
                    }
                    else
                    {
                        selectSigniZoneFlag = true;
                        SigniZoneUp(effectPlayer % 2);
                        selectSigniPlayer = effectPlayer % 2;
                        return false;
                    }
                }
                else
                {
                    if (isTrans && messagesBuf.Count > 0 && effectPlayer % 2 == 0)
                        sendMessageBuf();

                    //本体
                    if ((m == Motions.DownSummonFromTrash && field[effectPlayer % 2, effectID] == Fields.TRASH))
                        DownSummon(effectID, effectPlayer);
                    else
                        Summon(effectID, effectPlayer);


                    //終了時
                    if (moveID[effectPlayer] == -1 && rotaID[effectPlayer] == -1)
                    {
                        int fID = effectID + effectPlayer % 2 * 50;

                        //rebornList
                        if (m == Motions.RebornEndPhase)
                            RebornList.Add(fID);

                        if (m == Motions.TempResonaSummon || m == Motions.NotCipTempResona)
                            TempResonaList.Add(fID);

                        //notCip
                        if (m == Motions.NotCipSummon || m == Motions.NotCipTempResona)
                            notCipID = fID;
                    }
                }
            }
        }
        else if (m == Motions.SameSummon)
        {
            int count = 0;
            for (int i = 0; i < sc.effectMotion.Count; i++)
            {
                if ((Motions)sc.effectMotion[i] == Motions.SameSummon)
                    count++;
                else
                    break;
            }
            int[] pID = new int[count];
            for (int i = 0; i < count; i++)
                pID[i] = sc.effectTargetID[i];
            SameSummon(pID, playerNum);
            if (moveID[effectPlayer] == -1 && rotaID[effectPlayer] == -1)
            {
                for (int i = 1; i < count; i++)
                {
                    sc.effectTargetID.RemoveAt(1);
                    sc.effectMotion.RemoveAt(1);
                }
            }
        }
        else if (m == Motions.PowerUp)
        {
            int x = card[effecter[playerNum / 6] / 50, effecter[playerNum / 6] % 50].GetComponent<CardScript>().powerUpValue;
            upCardPower(effectID, sc.effectTargetID[0] / 50, x);
        }
        else if (m == Motions.PayCost)
        {
            PayCostAndSort(effectID, effectPlayer);

            //前の動作で動いたやつを対象から除く
            if (exitID >= 0 && sc.targetablePayCostRemove)
            {
                for (int i = 0; i < sc.Targetable.Count; )
                {
                    if (sc.Targetable[i] == exitID)
                        sc.Targetable.RemoveAt(i);
                    else i++;
                }
            }

            if (shortageFlag)
            {
                shortageFlag = false;
                sc.effectTargetID.Clear();
                sc.effectMotion.Clear();
                return false;
            }
            else if (effectNotMove(effectPlayer))
                sc.PayedCostFlag = true;
        }
        else if (m == Motions.CheckBack)
        {
            CheckBack();

            if (!sc.effectMotion.Contains((int)Motions.CheckBack))
                effectInitialized = false;

            return false;
        }
        else if (m == Motions.checkBackLifeCloth)
        {
            CheckBackLifeCloth();

            if (!sc.effectMotion.Contains((int)Motions.checkBackLifeCloth))
                effectInitialized = false;

            return false;
        }
        else if (m == Motions.Shuffle)
        {
            if (waitYou(effectPlayer % 2))
            {
                if (canRead())
                {
                    while (getNextMessage(readTargetPlayer) != sprt)
                    {
                        shuffleBuf.Add(readMessage());
                    }
                    readMessage();

                    Shuffle(effectPlayer % 2);
                    moveID[effectPlayer] = -1;
                }
                else
                    moveID[effectPlayer] = -2;
            }
            else
            {
                Shuffle(effectPlayer % 2);

                //通信
                if (isTrans)
                {
                    messagesBuf.Add(sprt);
                    sendMessageBuf();
                }
            }
        }
        else if (m == Motions.stopAttack)
            StopAttackFlag = true;
        else if (m == Motions.SetAnimation)
            setAnimation(effectID + effectPlayer % 2 * 50);

        //toldRemoving
        if (effectNotMove(effectPlayer))
        {
            toldRemoving = false;
        }

		return true;
	}

	void effectBody(CardScript sc,int playerNum)
	{
		int effectPlayer = sc.effectTargetID [0] / 50 + playerNum;
        bool notResist = EffecterNowID < 0 ||
            sc.effectMotion[0] == (int)Motions.TopSetCharm ||
            sc.effectMotion[0] == (int)Motions.SetCharm ||
            ( !CheckEffectResist(sc.effectTargetID[0] % 50, sc.effectTargetID[0] / 50, EffecterNowID % 50, EffecterNowID / 50)
            && !GoNextTrunFlag);

        bool executed = notResist && sc.getCanUseFunc() && (!exitedList.Contains(sc.effectTargetID[0]) || !notMoving(effectPlayer));

        if (executed)
        {
            tellEffectID = sc.effectTargetID[0];

            bool flag = ChoiceEffectBody(sc, playerNum);

            tellEffectID = -1;

            if (!flag)
                return;
        }
 
		//moving
		Moving (effectPlayer);

		
		if (moveID [effectPlayer] == -1 && rotaID [effectPlayer] == -1)
		{
            //executed
            if (executed && EffecterNowID>=0)
                effectExecuteList.Add(new powerChange(sc.effectTargetID[0], EffecterNowID, (int)sc.effectMotion[0]));

            //costBanish
			if (sc.effectMotion [0] == (int)Motions.CostBanish)
				sc.costBanish = true;

            if (!SpellCutInEffect && EffecterNowID != -1 && notResist)
                LastMotions.Add((Motions)sc.effectMotion[0]);
  
			sc.effectTargetID.RemoveAt (0);
			sc.effectMotion.RemoveAt (0);

            if (EffecterNowID != -1)
                effectInitialized = false;
            else
                systemCardInitialized = false;

		}
	}

    bool CheckEffectResist(int ID, int player, int effecterID, int effecterPlayer)
    {
        if (effecterID < 0 || effecterPlayer < 0)//システムからいじるから耐性無視
            return false;

        return checkRian_eff(ID, player, effecterID, effecterPlayer)
            || checkMirrorMirage(ID, player, effecterID, effecterPlayer)
            || checkNickelFlag(ID, player, effecterID, effecterPlayer)
            || checkResiArts(ID, player, effecterID, effecterPlayer)
            || checkResiLrigEff(ID, player, effecterID, effecterPlayer)
            || checkResiYourWhiteCardEff(ID, player, effecterID, effecterPlayer)
            || (getCardScr(ID, player).resiEffect && effecterPlayer != player)
            ;
    }

    bool cardStatusBufColorCheck(int x, int target, cardColorInfo info)
    {
        return cardStatusBuf[target, x].cardColor == (int)info;
    }

    bool checkResiYourWhiteCardEff(int ID, int player, int effecter, int effectPlayer)
    {
        CardScript sc = getCardScr(ID, player);
        return checkAbility(ID, player, ability.resiYourWhiteCardEff) 
            && cardStatusBufColorCheck(effecter, effectPlayer, cardColorInfo.白)
            && effectPlayer != player
            && sc.isOnBattleField()
            ;
    }

    bool checkResiLrigEff(int ID, int player, int effecter, int effectPlayer)
    {
        CardScript sc = getCardScr(ID, player);
        return player != effectPlayer && sc.resiLrigEffect && getCardType(effecter, effectPlayer) == 0;
    }
    bool checkResiArts(int ID, int player, int effecter, int effectPlayer)
    {
        CardScript sc = getCardScr(ID, player);
        return player != effectPlayer && sc.checkAbility(ability.resiArts) && checkType(effecter, effectPlayer, cardTypeInfo.アーツ);
    }

    bool checkNickelFlag(int ID, int player, int effecterID, int effecterPlayer)
    {
        return nickelFlag[player]
            && field[player, ID] == Fields.SIGNIZONE
            && checkClass(ID, player, cardClassInfo.精羅_原子)
            && player != effecterPlayer
            && checkType(effecterID, effecterPlayer, cardTypeInfo.スペル);
    }

    bool checkRian_eff(int ID, int player, int effecterID, int effecterPlayer)
    {
        if (!Rian_eff_Flag[player] || ID < 0 || effecterID < 0 || player == effecterPlayer)
            return false;

        int yourType = getCardType(effecterID,effecterPlayer);
        Fields myField = field[player, ID];
        int myPower = getCardPower(ID, player);

        return myPower >= 15000 && (yourType == 2 || yourType == 3) && myField == Fields.SIGNIZONE;
    }

	bool checkMirrorMirage(int ID, int player, int effecterID,int effecterPlayer)
	{
		if(!MirrorMirageFlag[player] || ID<0 || effecterID<0)
			return false;

		CardScript sc = getCardScr(ID,player);

		int effecter = effecterID + effecterPlayer*50;
		CardScript effecterScr = getCardScr(effecter %50, effecter / 50);

		bool isBIKOU=sc.Class_1==4 && sc.Class_2 == 2 ;
		bool isYourSigniOrSpell = sc.player != effecter/50 && ( effecterScr.Type==2||effecterScr.Type==3);

		return isBIKOU && isYourSigniOrSpell;
	}
	
	void CheckBack ()
	{
		CardScript sc = card [effecter [0] / 50, effecter [0] % 50].GetComponent<CardScript> ();
		
		if (waitYou(sc.effectSelecter))
		{
			if (canRead ())
			{
				for (int i=0; i<sc.effectTargetID.Count; i++)
				{
					if (sc.effectMotion [i] == (int)Motions.CheckBack)
					{
						GUImoveID = readParseMessage ();

						//showZone
						removeShowZoneID(GUImoveID%50,GUImoveID/50);
						
						//decide ikisaki
						Vector3 ikisaki = MAINDECK;
						if (GUImoveID / 50 == 1)
							ikisaki = vec3Player2 (MAINDECK);
						
						//change tranform and fieldRank
						card [GUImoveID / 50, GUImoveID % 50].transform.position = vec3Addy (
							ikisaki, 0.025f * (deckNum [GUImoveID / 50] - sc.effectTargetID.Count));
						card [GUImoveID / 50, GUImoveID % 50].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 0, 1));
						fieldRank [GUImoveID / 50, GUImoveID % 50] = deckNum [GUImoveID / 50] - sc.effectTargetID.Count;
						
						//remove effect
						sc.effectTargetID.RemoveAt (i);
						sc.effectMotion.RemoveAt (i);
						i--;
						
						GUImoveID = -1;

						return;
					}
				}
			}
		}
		else if (GUImoveID == -1)
		{
			for (int i=0; i<sc.effectTargetID.Count; i++)
			{
				if (sc.effectMotion [i] == (int)Motions.CheckBack)
					selectCardList.Add (sc.effectTargetID [i]);
			}
			if (selectCardList.Count > 0)
				selectCardFlag = true;
			return;
		}
		else
		{
			for (int i=0; i<sc.effectTargetID.Count; i++)
			{
				if (sc.effectTargetID [i] == GUImoveID)
				{
					//showZone
					removeShowZoneID(GUImoveID%50,GUImoveID/50);

					Vector3 ikisaki = MAINDECK;

					if (GUImoveID / 50 == 1)
						ikisaki = vec3Player2 (MAINDECK);

					card [GUImoveID / 50, GUImoveID % 50].transform.position = vec3Addy (
						ikisaki, 0.025f * (deckNum [GUImoveID / 50] - sc.effectTargetID.Count));

					card [GUImoveID / 50, GUImoveID % 50].transform.rotation = Quaternion.AngleAxis (180f, new Vector3 (0, 0, 1));
					
					fieldRank [GUImoveID / 50, GUImoveID % 50] = deckNum [GUImoveID / 50] - sc.effectTargetID.Count;

					sc.effectTargetID.RemoveAt (i);
					sc.effectMotion.RemoveAt (i);

					GUImoveID = -1;
					
					//通信
					if (isTrans && messagesBuf.Count>0){
						for (int j = 0; j < messagesBuf.Count; j++) {
							Debug.Log(j+" : "+messagesBuf[j]);
						}

						sendMessageBuf ();
					}
					
					return;
				}
			}
		}
	}
	
	void CheckBackLifeCloth ()
	{
		CardScript sc = card [effecter [0] / 50, effecter [0] % 50].GetComponent<CardScript> ();
		
		if (waitYou( sc.effectSelecter ))
		{
			if (canRead ())
			{
				for (int i=0; i<sc.effectTargetID.Count; i++)
				{
					if (sc.effectMotion [i] == (int)Motions.checkBackLifeCloth)
					{
						GUImoveID = readParseMessage ();
						
						fieldRank [GUImoveID / 50, GUImoveID % 50] = LifeClothNum [GUImoveID / 50] - sc.effectTargetID.Count;
						//decide ikisaki
						Vector3 ikisaki = vec3Add (LIFECLOTH,
							new Vector3 (LifeClothWidth * fieldRank [GUImoveID / 50, GUImoveID % 50], 0.025f * fieldRank [GUImoveID / 50, GUImoveID % 50], 0f));
						if (GUImoveID / 50 == 1)
							ikisaki = vec3Player2 (ikisaki);
						
						//change tranform and fieldRank
						card [GUImoveID / 50, GUImoveID % 50].transform.position = ikisaki;
						
						//remove effect
						sc.effectTargetID.RemoveAt (i);
						sc.effectMotion.RemoveAt (i);
						i--;
						
						GUImoveID = -1;

						return;
					}
				}
			}
		}
		else if (GUImoveID == -1)
		{
			for (int i=0; i<sc.effectTargetID.Count; i++)
			{
				if (sc.effectMotion [i] == (int)Motions.checkBackLifeCloth)
					selectCardList.Add (sc.effectTargetID [i]);
			}
			if (selectCardList.Count > 0)
				selectCardFlag = true;
			return;
		}
		else
		{
			for (int i=0; i<sc.effectTargetID.Count; i++)
			{
				if (sc.effectTargetID [i] == GUImoveID)
				{
					fieldRank [GUImoveID / 50, GUImoveID % 50] = LifeClothNum [GUImoveID / 50] - sc.effectTargetID.Count;
					Vector3 ikisaki = vec3Add (LIFECLOTH,
						new Vector3 (LifeClothWidth * fieldRank [GUImoveID / 50, GUImoveID % 50], 0.025f * fieldRank [GUImoveID / 50, GUImoveID % 50], 0f));
					if (GUImoveID / 50 == 1)
						ikisaki = vec3Player2 (ikisaki);
					card [GUImoveID / 50, GUImoveID % 50].transform.position = ikisaki;
					sc.effectTargetID.RemoveAt (i);
					sc.effectMotion.RemoveAt (i);
					GUImoveID = -1;
					
					//通信
					if (isTrans)
						sendMessageBuf ();
					
					return;
				}
			}
		}
	}
	
	public int MultiEnaNum (int player)
	{
		int num = 0;
		for (int i=0; i<enaNum[player]; i++)
		{
			int id = fieldRankID (Fields.ENAZONE, i, player);
			if (card [player, id].GetComponent<CardScript> ().MultiEnaFlag)
				num++;
		}
		return num;
	}

    int BikouEnaNum(int player)
    {
        int num = 0;
        for (int i = 0; i < enaNum[player]; i++)
        {
            int id = fieldRankID(Fields.ENAZONE, i, player);
            if (checkClass(id,player, cardClassInfo.精像_美巧))
                num++;
        }
        return num;
    }

	int GuardNum (int player)
	{
		int num = 0;
		for (int i=0; i<handNum[player]; i++)
		{
			int id = fieldRankID (Fields.HAND, i, player);
			if (card [player, id].GetComponent<CardScript> ().GuardFlag 
				&& card [player, id].GetComponent<CardScript> ().Level > UnblockLevel)
				num++;
		}
		return num;
	}

	int GuardRankID (int rank, int player)
	{
		int num = 0;
		for (int i=0; i<handNum[player]; i++)
		{
			int id = fieldRankID (Fields.HAND, i, player);
			if (card [player, id].GetComponent<CardScript> ().GuardFlag 
				&& card [player, id].GetComponent<CardScript> ().Level > UnblockLevel)
			{
				if (num == rank)
					return id;
				num++;
			}
		}
		return -1;		
	}
	
	void Shuffle (int player)
	{
		int[] buf = new int[deckNum [player]];

		if(replayMode)
		{
			for (int i=0; i<shuffleBuf.Count; i++)
				buf [i] = int.Parse (shuffleBuf [i]);
			shuffleBuf.Clear ();
		}
		else if (isTrans)
		{	
			if (player == 0)
			{
				buf = RandDeckNum (deckNum [player]);
				for (int i=0; i<buf.Length; i++)
					messagesBuf.Add ("" + buf [i]);
			}
			else
			{
				for (int i=0; i<shuffleBuf.Count; i++)
				{
					if( !int.TryParse(shuffleBuf [i], out buf [i]))
						Debug.Log(shuffleBuf [i]);
//					buf [i] = int.Parse (shuffleBuf [i]);
				}
				shuffleBuf.Clear ();
			}
		}
		else
		{
			buf = RandDeckNum (deckNum [player]);
		}
		int[] deckBuf = new int[deckNum [player]];
		for (int i=0; i<deckNum[player]; i++)
		{
			deckBuf [i] = fieldRankID (Fields.MAINDECK, i, player);
		}
		for (int i=0; i<buf.Length; i++)
		{
			fieldRank [player, deckBuf [i]] = buf [i];
			Vector3 v = vec3Add (MAINDECK, new Vector3 (0f, 0.025f * buf [i], 0f));
			if (player == 1)
				v = vec3Player2 (v);
			card [player, deckBuf [i]].transform.position = v;
		}	
	}
	
	void GoHand (int ID, int player)
	{
		if (chainMotion [0] == -1 && movePhase [player] == 0 && rotaPhase [player] == -1)
			chainMotion [0] = 0;
		if (chainMotion [0] == 0)
		{
			drawNum [player % 2] = 1;
			handSortFlag [player % 2] = true;
			DrawCard (player);
			if (!handSortFlag [player % 2])
			{
				handMoveCount [player % 2] = 0;
				drawNum [player % 2] = 0;
				moveID [player] = -1;
				rotaID [player] = -1;
				movePhase [player] = 0;
				rotaPhase [player] = -1;
				chainMotion [0] = 1;
			}
		}
		if (chainMotion [0] == 1)
		{
            Fields f = field[player % 2, ID];

			NotSortGoHand (ID, player, true);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = -1;
                if (f == Fields.ENAZONE)
                    SetSystemCard(player % 2 * 50, Motions.EnaSort);
			}
		}
	}
	
	void DontShuffleGoHand (int ID, int player)
	{
		if (chainMotion [0] == -1 && movePhase [player] == 0 && rotaPhase [player] == -1)
			chainMotion [0] = 0;
		if (chainMotion [0] == 0)
		{
			drawNum [player % 2] = 1;
			handSortFlag [player % 2] = true;
			DrawCard (player);
			if (!handSortFlag [player % 2])
			{
				handMoveCount [player % 2] = 0;
				drawNum [player % 2] = 0;
				moveID [player] = -1;
				rotaID [player] = -1;
				movePhase [player] = 0;
				rotaPhase [player] = -1;
				chainMotion [0] = 1;
			}
		}
		if (chainMotion [0] == 1)
		{
			NotSortGoHand (ID, player, false);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = -1;
			}
		}		
	}
	
	void NotSortGoHand (int ID, int player, bool shuffleFlag)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			moveID [player] = ID;
			if (1 + handNum [player % 2] > 1)
			{
				float width = 9f - 14f / (1 + handNum [player % 2] + 1);
				destination [player] = vec3Add (HAND, new Vector3 (width, 0f, 0f));
			}
			else
				destination [player] = HAND;
			moveTime [player] = standartTime;
			rotaPhase [player] = 0;
		}
		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			bool flag = false;
			
			if (shuffleFlag && field [player % 2, moveID [player]] == Fields.MAINDECK && !isShowZoneID( moveID [player],player % 2))
				flag = true;

			ExitFunction (moveID [player], player % 2);

			field [player % 2, moveID [player]] = Fields.HAND;
			fieldRank [player % 2, moveID [player]] = handNum [player % 2];
			moveID [player] = -1;
			handNum [player % 2]++;
			
			//shuffle
			if (flag)
			{
				//通信
				if (isTrans)
				{
					if (player % 2 == 0)
					{
						Shuffle (player % 2);
						messagesBuf.Add (sprt);
						//送信
						sendMessageBuf ();
					}
					else
					{
						movePhase [player] = 2;
						moveID [player] = -2;
					}
				}
				else if(replayMode)
				{
					movePhase [player] = 2;
					moveID [player] = -2;

					readTargetPlayer=player%2;

				}
				else
				{
					Shuffle (player % 2);
				}
			}
		}

		//受信
		if (movePhase [player] == 2 && moveID [player] == -2)
		{
			if (canRead ())
			{
				moveID [player] = -1;

				while (getNextMessage( readTargetPlayer) != sprt)
				{
					string s=readMessage ();
					shuffleBuf.Add (s);
				}

				readMessage ();

				Shuffle (player % 2);
			}
		}
		
		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			setHandAngle (player);
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
	}
	
	void HandDeath (int ID, int player)
	{
		if (chainMotion [0] == -1 && movePhase [player] == 0 && rotaPhase [player] == -1)
			chainMotion [0] = 0;
		if (chainMotion [0] == 0)
		{
			GoTrash (ID, player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				movePhase [player] = 0;
				rotaPhase [player] = -1;
				chainMotion [0] = 1;
			}
		}
		if (chainMotion [0] == 1)
		{
			handSortFlag [player % 2] = true;
			DrawCard (player);
			if (!handSortFlag [player % 2])
			{
				handMoveCount [player % 2] = 0;
				moveID [player] = -1;
				rotaID [player] = -1;
				chainMotion [0] = -1;
			}
		}
	}
	
	void HandSort (int player)
	{
		handSortFlag [player % 2] = true;
		DrawCard (player);
		if (!handSortFlag [player % 2])
		{
			handMoveCount [player % 2] = 0;
			moveID [player] = -1;
			rotaID [player] = -1;
		}
	}
	
	void Muligan (int player, int muliganMax)
	{
		if (chainMotion [0] == -1 && movePhase [player] == 0 && rotaPhase [player] == -1)
		{
			if (clickCursorID.Count == 0)
			{
				setTargetCursorField (Fields.HAND, player);
				selectCursorFlag = true;
				selectNum = muliganMax;
				cursorCancel = true;
				moveID [player] = -2;
				return;
			}
			else
			{
				cursorCancel = false;
				for (int i=0; i<clickCursorID.Count; i++)
				{
					if (clickCursorID [i] == -1)
					{
						clickCursorID.RemoveAt (i);
						i--;
					}
				}
				if (clickCursorID.Count > 0)
				{
					chainMotion [0] = 0;
					muliganNum = clickCursorID.Count;
				}
				else
					moveID [player] = -1;
			}
		}
		if (chainMotion [0] == 0)
		{
			if (moveID [player] == -2)
				moveID [player] = -1;
			GoDeck (clickCursorID [0] % 50, player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				clickCursorID.RemoveAt (0);
				movePhase [player] = 0;
				rotaPhase [player] = -1;
				if (clickCursorID.Count == 0)
				{
					drawNum [player % 2] = muliganNum;
					muliganNum = -1;
					chainMotion [0] = 1;
					Shuffle (player % 2);
				}
				else
					moveID [player] = -2;
			}
		}
		if (chainMotion [0] == 1)
		{			
			DrawCard (player);
			if (moveID [player] == -1 && rotaID [player] == -1)
			{
				chainMotion [0] = -1;
			}
		}
	}

    void afterGoDeck(int player, int setRank)
    {
        ExitFunction(moveID[player], player % 2);

        field[player % 2, moveID[player]] = Fields.Non;

        if (setRank == 0)
            DeckRankUp(player % 2);

        field[player % 2, moveID[player]] = Fields.MAINDECK;

        if (setRank == 0)
            fieldRank[player % 2, moveID[player]] = setRank;
        else
            fieldRank[player % 2, moveID[player]] = deckNum[player % 2];

        deckNum[player % 2]++;
        moveID[player] = -1;
    }

	void GoDeck (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = vec3Add (MAINDECK, new Vector3 (0f, 0.025f * deckNum [player % 2], 0f));
			rotaPhase [player] = 0;
		}

		if (movePhase [player] == 1 && moveID [player] != -1)
		{
            afterGoDeck(player, deckNum[player % 2]);		
        }

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 180f;
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
		
	}
	
	void GoDeckBottom (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] == -1)
		{
			moveID [player] = ID;
			moveTime [player] = standartTime;
			destination [player] = vec3Add (MAINDECK, new Vector3 (0f, 0f, 0f));
			rotaPhase [player] = 0;
        }

		if (movePhase [player] == 1 && moveID [player] != -1)
		{
            /*			ExitFunction (moveID [player], player % 2);
                        field [player % 2, moveID [player]] = Fields.MAINDECK;
                        fieldRank [player % 2, moveID [player]] = 0;
                        deckNum [player % 2]++;
                        moveID [player] = -1;*/
            afterGoDeck(player, 0);
         }

		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			rotaTime [player] = moveTime [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 0f;
			angle [player, 2] = 180f;
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
	}

    void DeckRankUp(int player)
    {
        int num = deckNum[player];

        for (int i = num - 1; i >= 0; i--)
        {
            int id = fieldRankID(Fields.MAINDECK, i, player);
            fieldRank[player, id]++;
            card[player, id].transform.position = vec3Addy(card[player, id].transform.position, 0.025f);
        }
    }
	
	void LifeClothSet (int ID, int player)
	{
		if (movePhase [player] == 0 && moveID [player] < 0 && rotaID [player] == -1)
		{
			Singleton<SoundPlayer>.instance.playSE("draw");

			moveID [player] = ID;
			destination [player] = vec3Add (LIFECLOTH, new Vector3 (LifeClothWidth * LifeClothNum [player % 2], 0.025f * LifeClothNum [player % 2], 0f));
			rotaPhase [player] = 0;
			moveTime [player] = standartTime;
		}
		if (rotaPhase [player] == 0 && rotaID [player] == -1)
		{
			rotaID [player] = moveID [player];
			angle [player, 0] = 0f;
			angle [player, 1] = 90f;
			angle [player, 2] = 180f;
			rotaTime [player] = moveTime [player];
		}
		if (rotaPhase [player] == 1 && rotaID [player] != -1)
		{
			rotaID [player] = -1;
		}
		if (movePhase [player] == 1 && moveID [player] != -1)
		{
			ExitFunction (moveID [player], player % 2);
			field [player % 2, moveID [player]] = Fields.LIFECLOTH;
			fieldRank [player % 2, moveID [player]] = LifeClothNum [player % 2];
			LifeClothNum [player % 2]++;
			moveID [player] = -1;
			
			LifeAddFlag = true;
		}
	}
	
	void spellCutIn ()
	{
		CardScript scrpt;
		if(NewCardEffectFlag)
			scrpt=NewCardList[NewCardEffecterCount].NewCardObj.GetComponent<CardScript>();
		else 
			scrpt = card [effecter [0] / 50, effecter [0] % 50].GetComponent<CardScript> ();

		if (effectUsableFlag)
		{
/*				hakkenStr+="\neffectSelectIDbuf[0]="+effectSelectIDbuf[0]+"\n";
			for(int k=0;k<scrpt.Targetable.Count;k++){
				hakkenStr+=scrpt.Targetable[k]+" ";
			}*/
//			print("UsableCheck");
//			Debug.Log("scrpt.effectTargetID.Count = "+scrpt.effectTargetID.Count);

			for (int i=0; i<scrpt.effectTargetID.Count; i++)
			{
				if (scrpt.effectTargetID [i] < 0)
				{
					//costBanish
					if (scrpt.effectMotion [i] == (int)Motions.CostBanish)
					{
						for (int k=0; k<3; k++)
						{
							int id = fieldRankID (Fields.SIGNIZONE, k, scrpt.player);
							if (id >= 0)
								scrpt.Targetable.Add (id + scrpt.player * 50);
						}						
					}
					
					
					bool flag = false;
					for (int k=0; k<scrpt.Targetable.Count; k++)
					{
						if (scrpt.Targetable [k] == effectSelectIDbuf [0])
							flag = true;
					}
					if (flag)
					{
						scrpt.effectTargetID [i] = effectSelectIDbuf [0];
						if (scrpt.TargetIDEnable)
							scrpt.TargetID.Add (effectSelectIDbuf [0]);
						effectSelectIDbuf.RemoveAt (0);
						scrpt.Targetable.Clear ();
//						print("manager in\n");
						return;
					}
					else
					{
						scrpt.effectTargetID.RemoveAt (i);
						scrpt.effectMotion.RemoveAt (i);
						effectSelectIDbuf.RemoveAt (0);
//						print("manager remove\n");
//						print("nokori=");
//						Debug.Log(scrpt.effectTargetID.Count);
						return;
					}
				}
			}

 			SpellCutInFlag = false;
			effectUsableFlag = false;
			effecter [1] = -1;
			return;
		}
		else if (GUImoveID >= 0)
		{

			spellChangeFeild(Fields.Non);

			SpellCutInID = GUImoveID;
			movePhase [SpellCutInID / 50 + 4] = 0;
			rotaPhase [SpellCutInID / 50 + 4] = -1;
			GUImoveID = -1;
			
			if (isTrans && SpellCutInPlayer == 0)
				sendMessageBuf ();
		}

		if (SpellCutInID >= 0)
		{
			if(getCardType(SpellCutInID%50,SpellCutInID/50)==1){
				ChantArts (SpellCutInID % 50, SpellCutInID / 50 + 4);
				if (waitTime > 0)
					return;
				Moving (SpellCutInID / 50 + 4);
			}
			else if(!useSpellCutInUp){
				getCardScr(SpellCutInID%50,SpellCutInID/50).UseSpellCutIn=true;
				useSpellCutInUp=true;
				return;
			}
			else 
				useSpellCutInUp=false;


			if (moveID [SpellCutInID / 50 + 4] == -1 && rotaID [SpellCutInID / 50 + 4] == -1)
			{
				SpellCutInID = -1;
				if (shortageFlag)
					shortageFlag = false;
				
                if(!GoNextTrunFlag && !antiFlag)
					AskSpellCutInFlag = true;

				return;
			}
		}

		if (moveID [SpellCutInPlayer + 4] == -1 && rotaID [SpellCutInPlayer + 4] == -1)
		{
            if (AskSpellCutInFlag)
			{
				if (waitYou( SpellCutInPlayer ))
				{
					if (canRead ())
					{
						AskSpellCutInFlag = false;
						string r = readMessage ();
						if (r == YesStr)
							GUImoveID = readParseMessage ();

                        return;
					}
				}
				else
				{
					AskSpellCutInFlag = false;
					selectCardListSpellCutIn (SpellCutInPlayer);

                    if (selectCardList.Count > 0)
                        SpellCutInSelectFlag = true;
                    else if(isTrans)
                    {
                        messagesBuf.Add(NoStr);
                        sendMessageBuf();
                    }

 					selectCardList.Clear ();
				}
			}
			else if(spellFieldChanged)
			{
				if (antiFlag && !FafnirFlag)
				{

                    if (counterSpellFlag)
                    {
                        var scr = getCardScr(effecter[1] % 50, effecter[1] / 50);
                        if (NewCardEffectFlag)
                        {
                            int x = NewCardList[NewCardEffecterCount].TrueID;
                            int target = NewCardList[NewCardEffecterCount].TruePlayer;

                            if(target == effecter[1]/50)
                                scr.setEffect(x, target, Motions.ChantSpell);
                            else
                                scr.setEffect(x, target, Motions.ChantYourSpell);
                        }
                        else
                            scr.setEffect(effecter[0] % 50, effecter[0] / 50, Motions.ChantYourSpell);
                    }

                    drawNum[0] = 0;
                    drawNum[1] = 0;
                    SpellCutInFlag = false;
                    effecter[1] = -1;
				}
				else
				{             
                    effectUsableFlag = true;

					spellChangeFeild(Fields.CHECKZONE);

				}

                counterSpellFlag = false;
                antiFlag = false;
                FafnirFlag = false;

				scrpt.effectTargetID.Clear ();
				scrpt.effectMotion.Clear ();
				scrpt.TargetID.Clear ();
				scrpt.Targetable.Clear ();
			}
			else 
				SpellCutInFlag = false;
		}
			
	}

	void spellChangeFeild(Fields f)
	{
		if(NewCardEffectFlag)
			changeNewCardField(NewCardEffecterCount,f);
		else 
			field [effecter [0] / 50, effecter [0] % 50] = f;

		if(f==Fields.CHECKZONE)
			spellFieldChanged=false;
		else 
			spellFieldChanged=true;
	}

	void changeNewCardField(int count,Fields f){

		NewCard nc = NewCardList[count];
		NewCardList[count]=new NewCard(nc.NewCardObj,nc.TrueID,nc.TruePlayer,f);
	}
	
	void DeckReflesh (int player)
	{
		if (!deckRefleshFlag [player])
			return;
		if (refleshMotion [player] == 0)
		{
			int id = fieldRankID (Fields.TRASH, 0, player);
			GoDeck (id, player + 2);
			hakkenNum = -100;
			if (moveID [player + 2] == -1 && rotaID [player + 2] == -1)
			{
				movePhase [player + 2] = 0;
				rotaPhase [player + 2] = -1;
				if (trashNum [player] == 0)
				{
					if (LifeClothNum [player] > 0)
						refleshMotion [player]++;
					else
						refleshMotion [player] = 2;
					
					if (isTrans)
					{
						if (player == 0)
						{
							Shuffle (player);
							messagesBuf.Add (sprt);
							sendMessageBuf ();
						}
						else
							refleshMotion [player] = 3;
					}
					else if(replayMode)
					{
						refleshMotion [player] = 3;

						readTargetPlayer = player % 2;
					}
					else
						Shuffle (player);
				}
			}
		}
		if (refleshMotion [player] == 1)
		{
			int id = fieldRankID (Fields.LIFECLOTH, LifeClothNum [player] - 1, player);
			GoTrash (id, player + 2);
			if (moveID [player + 2] == -1 && rotaID [player + 2] == -1)
			{
				refleshMotion [player]++;
			}
		}
		if (refleshMotion [player] == 2)
		{
			deckRefleshFlag [player] = false;
			
			//2度目のリフレッシュ
			if (RefleshedFlag [player])
			{
				goEndPhaseFlag = true;
			}
			else
				RefleshedFlag [player] = true;
		}
		
		//受信
		if (refleshMotion [player] == 3)
		{
			if (canRead ())
			{
				while (getNextMessage(readTargetPlayer) != sprt)
				{
					shuffleBuf.Add (readMessage ());
				}
				readMessage ();
				Shuffle (player);
				if (LifeClothNum [player] > 0)
					refleshMotion [player] = 1;
				else
					refleshMotion [player] = 2;				
			}
		}
		
		Moving (player + 2);
	}
	
	void SameSummon (int[] pID, int player)
	{
		for (int i=summonCount; i<pID.Length; i++)
		{
			if (fieldAllNum (Fields.SIGNIZONE, pID [i] / 50) + summonCount >= signiSumLimit [ pID [i] / 50 ])
			{
				summonCount++;
			}
		}

		if (pID.Length > summonCount)
		{
			int effectPlayer = pID [summonCount] / 50 + player;
			if (selectSigniZone == -1)
			{
				if (waitYou( effectPlayer % 2 ))
				{
					if (canRead ())
						selectSigniZone = int.Parse (readMessage ());
				}
				else
				{
					selectSigniZoneFlag = true;
					SigniZoneUp (effectPlayer % 2);
					selectSigniPlayer = effectPlayer % 2;
				}
				moveID [pID [0] / 50 + player] = -2;
				return;
			}
			if (moveID [pID [0] / 50 + player] == -2)
				moveID [pID [0] / 50 + player] = -1;
			
			Summon (pID [summonCount] % 50, pID [summonCount] / 50 + player);
			
			if (moveID [pID [summonCount] / 50 + player] == -1 && rotaID [pID [summonCount] / 50 + player] == -1)
			{
				field [pID [summonCount] / 50, pID [summonCount] % 50] = Fields.Non;
				sameSummonList.Add (pID [summonCount]);
				summonCount++;
				if (pID.Length > summonCount)
				{
					movePhase [pID [summonCount] / 50 + player] = 0;
					rotaPhase [pID [summonCount] / 50 + player] = -1;
					moveID [pID [0] / 50 + player] = -2;
				}
			}
			else if (moveID [pID [0] / 50 + player] == -1)
				moveID [pID [0] / 50 + player] = -2;
		}
		else if (moveID [pID [0] / 50 + player] == -2)
			moveID [pID [0] / 50 + player] = -1;

		if (moveID [pID [0] / 50 + player] == -1 && rotaID [pID [0] / 50 + player] == -1)
		{
			for (int i=0; i<sameSummonList.Count; i++)
			{
				field [sameSummonList [i] / 50, sameSummonList [i] % 50] = Fields.SIGNIZONE;
			}
			sameSummonList.Clear ();
			summonCount = 0;
		}
	}
	
	bool FlagUpcheck ()
	{
		return(
			selectCardFlag 
			|| selectSigniZoneFlag 
			|| selectCursorFlag 
			|| selectEnaFlag 
			|| stopFlag 
			|| guardSelectflag 
			|| SpellCutInSelectFlag
			|| cipCheck
			|| DialogFlag
			|| selectAttackAtrs
			|| animationCount > 0
			|| targetShowCursor != null
			);
	}
		
	void searchAlldeck ()
	{

		DirectoryInfo di = new DirectoryInfo ("./deck/");
		ListOfDeck = di.GetFiles ("*.txt", System.IO.SearchOption.TopDirectoryOnly);
	}

	void searchReplay ()
	{
		if(Directory.Exists(@"replay")){
			DirectoryInfo[] array = new DirectoryInfo (@"replay").GetDirectories();

			replayList=new string[array.Length];

			for (int i = 0; i < array.Length; i++) {
				replayList[i] = array[i].Name;
			}
		}
	}

	//サーバーが初期化されたとき、サーバー側で呼び出されます。
	void OnServerInitialized ()
	{
		Debug.Log ("Server initialized and ready");
		connected = true;
	}
 
	//サーバーに接続したとき、クライアント側で呼び出されます。
	void OnConnectedToServer ()
	{
		wasServerAndClient=true;

		Debug.Log ("Connected to server");

		connected = true;
		isServer = false;

		messagesBuf.Add ("" + firstAttack);
		thinkingPlayer = firstAttack;

		DeckCreat (0);
//		selectCardListLrigIn (0);
/*		for (int i=0; i<50; i++)
		{
			messagesBuf.Add (SerialNumString [0, i]);
		}*/
//		Shuffle (0);
//		sendMessageBuf ();
	}
 
	// プレイヤーが接続されたとき、サーバー側で呼び出されます。
	void OnPlayerConnected (NetworkPlayer player)
	{
		wasServerAndClient=true;

		Debug.Log ("Connected from " + player.ipAddress + ":" + player.port);
		connected = true;
		isServer = true;
		DeckCreat (0);
//		selectCardListLrigIn (0);

/*		for (int i=0; i<50; i++)
		{
			messagesBuf.Add (SerialNumString [0, i]);
		}*/
//		Shuffle (0);
	}
 
	//プレイヤーが切断されたとき、サーバー側で呼び出されます。
	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Debug.Log ("Clean up after player " + player);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
		
		OnPhotonPlayerDisconnected ();
	}
 
	//サーバーから切断したとき、クライアント側で呼び出されます。
	void OnDisconnectedFromServer (NetworkDisconnection info)
	{
		connected = false;
		if (Network.isServer)
		{  
			Debug.Log ("Local server connection disconnected");
		}
		else
		{
			OnPhotonPlayerDisconnected ();
				
			if (info == NetworkDisconnection.LostConnection)
			{
				Debug.Log ("Lost connection to the server");
			}
			else
			{
				Debug.Log ("Successfully diconnected from the server");
			}
		}
		
	}
 
	//サーバーの接続に失敗したとき、クライアント側で呼び出されます。
	void OnFailedToConnect (NetworkConnectionError error)
	{
		Debug.Log ("Could not connect to server: " + error);
		connectButtun = true;
	}
	// 接続されているピアで認識されるRPC関数として指定するために[RPC]を記述する。
	[RPC]
	public void chatMessage (string msg, bool isSvr)
	{
		// 引数のメッセージをローカルの配列にセットする。
		messages.Add (msg);
		Sender = isSvr;
	}
	
	[RPC]
	public void MessageRemove ()
	{
		// ローカルの配列のメッセージをひとつ消す
		messages.RemoveAt (0);
	}

	[RPC]
	public void MessageClear ()
	{
		// ローカルの配列のメッセージをクリアする
		messages.Clear ();
	}
	
	int IdTrans (int id)
	{
        if (id < 0)
            return id;

		int player = id / 50;
		player = 1 - player;
		return id % 50 + player * 50;
	}
	
	void sendMessageBuf ()
	{
		messagesBuf.Add (endStr);
		
		while (messagesBuf.Count>0)
		{
			if(messagesBuf[0] != endStr)
				sentList.Add( messagesBuf[0] );

			if (NetScr.connected)
			{
				NetScr.NetChat (messagesBuf [0], isServer);
			}
			else
				GetComponent<NetworkView>().RPC ("chatMessage", RPCMode.All, new object[] {
										messagesBuf [0] ,
										isServer
								});

			messagesBuf.RemoveAt (0);
		}
	}
	
	string readMessage ()
	{
		if(replayMode)
		{
			string r=replayRead(readTargetPlayer);
            return r;
		}

		if (messages.Count == 0)
			return string.Empty;

		string s = messages [0];

		//received list
		if(!replayMode)
			receivedList.Add(s);
		
		if (NetScr.connected)
		{
			NetScr.NetRemove ();
		}
		else
			GetComponent<NetworkView>().RPC ("MessageRemove", RPCMode.All, new object[] {});
		
		if (messages.Count > 0 && messages [0] == endStr)
			readMessage ();

		return s;
	}
	
	//netManager　用
	public void AddMessege (string msg, bool isSvr)
	{
		messages.Add (msg);
		Sender = isSvr;
	}

	public void RemoveMessage ()
	{
		messages.RemoveAt (0);
	}
	
	int readParseMessage ()
	{
		if (isMessageError())
			return -1;

		int id = -1;
		string s=readMessage ();

		if(int.TryParse (s,out id) )
		{
			if(replayMode && readTargetPlayer==0)
				return id;

			return IdTrans (id);
		}
		else 
			Debug.Log(s);

		return -1;
	}

	bool isMessageError()
	{
		return getNextMessage(readTargetPlayer) == errorStr;
	}

	bool canRead ()
	{
		if(replayMode)
			return true;

		bool flag=messages.Count > 0 && messages [messages.Count - 1] == endStr && Sender != isServer;

		//thinkerChange
		if(phase != Phases.PrePhase && getTurnPlayer() == 0){
			if(flag)
			{
				if(thinkChangeFlag)
				{
					NetScr.rpcAll("thinkChange",new object[]{});
				}
			}
			else if(!thinkChangeFlag) 
			{
				NetScr.rpcAll("thinkChange",new object[]{});
			}
		}


		return flag;
	}

	[RPC]
	public void thinkChange()
	{
		thinkChangeFlag=!thinkChangeFlag;
		thinkingPlayer = 1 - thinkingPlayer;
	}

	[RPC]
	public void timeSynchro(int time)
	{
		myTime[1]=time;
		myFrameTime[1]=0;
	}

	bool notMoving ()
	{
		return moveID [0] == -1 && rotaID [0] == -1 && moveID [1] == -1 && rotaID [1] == -1;
	}
    bool notMoving(int player)
    {
        return moveID[player] == -1 && rotaID[player] == -1;
    }

/*	void debugMessageShow ()
	{
		if (isTrans)
		{
			Debug.Log ("debugdebugMessageShow start");
			for (int i=0; i<messages.Count; i++)
			{
				Debug.Log ("message " + i + " = " + messages [i]);
			}
			Debug.Log ("debugdebugMessageShow end");
		}
	}*/
	
	//部屋を作ったら呼ばれたい
	public void PhotonAfterCreatRoom ()
	{
		isServer = true;
	}
 
	//roomに入ったあと呼ばれたい
	 public void PhotonAfterJoined ()
	{
		Debug.Log ("creat deck");
		connected = true;

		firstAttack = Random.Range (0, 2);
		
		if (!isServer)
		{
			messagesBuf.Add ("" + firstAttack);

			thinkingPlayer = firstAttack;	
		}

		DeckCreat (0);
//		selectCardListLrigIn (0);
		
/*		for (int i=0; i<50; i++)
		{
			messagesBuf.Add (SerialNumString [0, i]);
		}
		Shuffle (0);

		if (!isServer)
			sendMessageBuf ();*/
	}
	
	void OnPhotonPlayerDisconnected ()
	{
		playerDissconnected=true;
	}

	void afterPlayerDisconnected(){
		if(NetScr.receivingReplay)
		{
			saveReplay();
			
			sentList.Clear();
			receivedList.Clear();
			
			NetScr.receivingReplay=false;
		}
		else if(!DuelEndFlag){
			messageShow ("disconnected");
			NetScr.resetManager ();
		}

		playerDissconnected=false;
	}

	void OnDisconnectedFromPhoton ()
	{
		OnPhotonPlayerDisconnected ();
	}
	
	int SigniLevelSum (int player)
	{
		int sum = 0;
		player = player % 2;
		for (int i=0; i<3; i++)
		{
			int id = fieldRankID (Fields.SIGNIZONE, i, player);
			if (id >= 0)
			{
				CardScript sc = getCardScr (id, player);
				sum += sc.Level;
			}
		}
		return sum;
	}
	
	bool checkLrigLim (int id, int player)
	{
		CardScript sc = getCardScr (id, player % 2);

		if(sc.LrigLimit_2 != 0 && (sc.LrigLimit_2 == LrigType [player % 2] || sc.LrigLimit_2 == LrigType2 [player % 2]))
		   return false;

		if (sc.LrigLimit != 0 && sc.LrigLimit != LrigType [player % 2] && sc.LrigLimit != LrigType2 [player % 2])
			return true;

		return false;
	}
	
	void setHandAngle (int player)
	{
		angle [player, 0] = -20f;
		angle [player, 1] = 0f;
		angle [player, 2] = 0f;
		if (! replayMode && !DebugFlag && player % 2 == 1)
			angle [player, 2] = 180f;
	}
	
	void UpdateLrigData (int player)
	{
		player = player % 2;
		int id = getLrigID (player);
		
		CardScript sc = card [player, id].GetComponent<CardScript> ();
		LrigType [player] = sc.LrigType;
		LrigType2 [player] = sc.LrigType_2;
		LrigLevel [player] = sc.Level;
//		LevelLimit [player] = sc.Limit;		
	}
	
	void setAnimation (int id)
	{
		animationID = id;
		animationCount = standartTime * 5;
	}
	
	void powerUpEndPhase (int ID, int player, int upValue, int effecter)
	{
		player = player % 2;
		if (field [player, ID] != Fields.SIGNIZONE)
			return;

		if(upValue<0)
		{
			for(int i=0;i<ViolenceCount;i++)
				upValue *= 2;		
		}

		SigniPowerUpValue [player, fieldRank [player, ID]] += upValue;
		upCardPower (ID, player, upValue);
	}
	
	void nomalizePower (int ID, int player)
	{
		player = player % 2;
		if (field [player, ID] != Fields.SIGNIZONE)
			return;
		
		int rank = fieldRank [player, ID];
		upCardPower (ID, player, -SigniPowerUpValue [player, rank]);
		SigniPowerUpValue [player, rank] = 0;
	}
	
	void setTargetShowCursor (int ID, int player)
	{
		player = player % 2;
		if (ID < 0)
			return;
		
		targetShowCursor = (GameObject)Instantiate (
			Resources.Load ("targetShowCursor"),
			vec3Addy (card [player, ID].transform.position, 0.03f),
			Quaternion.identity
		);
		targetShowCursor.transform.parent = this.transform;
		
		if (field [player, ID] == Fields.SIGNIZONE && signiCondition [player, fieldRank [player, ID]] == Conditions.Down)
		{
			targetShowCursor.transform.rotation = Quaternion.AngleAxis (90f, new Vector3 (0, 1, 0));
		}
	}
	
	void UpIgnition (int ID, int player)
	{
        IgniAdd igniAdd = front[player, ID].GetComponent<IgniAdd>();

        if (igniAdd == null)
            UpIgnitionOther(ID, player);
        else
        {
            for (int i = 0; i < AddIgniList.Count; i++)
            {
                if (AddIgniList[i].changedID == ID + player % 2 * 50)
                {
                    int x = AddIgniList[i].changeValue % 50;
                    int target = AddIgniList[i].changeValue / 50;
                    igniAdd.setIgniTarget(x,target);
                }
            }
            igniAdd.Ignition = true;
        }
	}

    void UpIgnitionOther(int ID, int player)
    {
        if (AlwysEffFlags.checkFlagUp(alwysEffs.Arachne, player) && havingCharm(ID, player))
            return;

        card[player, ID].GetComponent<CardScript>().Ignition = true;
        IgnitionUpID = ID + 50 * player;
    }
	
	//push button
	void pushSurrenderButton ()
	{
		if (NetScr.connected)
			NetScr.DisConnetServer ();
		else if (connected)
			Network.Disconnect ();
		
		Application.LoadLevel ("menusScene");
	}
	
	public void pushDuelStandby ()
	{
		replayMode=false;
		isTrans = false;
//		preDeckCreat = false;

		DeckCreat (0);
//		Shuffle (0);

		DeckCreat (1);
//		Shuffle (1);

//		selectCardListLrigIn (0);
	}
	
	public void messageShow (string s)
	{
/*		System.Windows.Forms.MessageBox.Show(s,
		                                     "AWS",
		                                     System.Windows.Forms.MessageBoxButtons.OK,
		                                     System.Windows.Forms.MessageBoxIcon.Information);
*/	
		MessageWindow = (GameObject)Instantiate (Resources.Load ("messageWindow"));
		messageScript ms = MessageWindow.GetComponent<messageScript> ();
		ms.messageShow (s);
	}
	
	void EndMessage (int player)
	{
		DuelEndFlag = true;

		if (player == 0)
		{
			messageShow ("YOU WIN");
		}
		else
			messageShow ("YOU LOSE");

		if(isTrans && Singleton<config>.instance.ReplaySaveFlag)//replaySaveFlag )
		{
			//replay
			saveReplay();
		}

		//通信終了
		if(isTrans && ! NetScr.connected && !isServer){
			Network.Disconnect();
		}


		if(/*canReplaySend && */isTrans){

			NetScr.enterMasterRoom();
		}
	}
	
	void saveReplay()
	{
		if(!Directory.Exists(@"replay"))
			Directory.CreateDirectory(@"replay");
		
		string[] subFolders = System.IO.Directory.GetDirectories(@"replay", "*");
		string path=@"";
		
		for(int i=0;true;i++)
		{
			path=@"replay/"+string.Format("{0:D4}",subFolders.Length + i);
			
			if(!Directory.Exists(path))
				break;
		}
		
		Directory.CreateDirectory(path);
		
		StreamWriter sentWriter=new StreamWriter(path+"/sentLog.txt");
		StreamWriter receivedWriter=new StreamWriter(path+"/receivedLog.txt");
		
		for (int i = 0; i < sentList.Count; i++)
		{
			sentWriter.WriteLine(sentList[i]);
		}
		
		for (int i = 0; i < receivedList.Count; i++)
		{
			receivedWriter.WriteLine(receivedList[i]);
		}
		
		sentWriter.Close();
		receivedWriter.Close();

	}
	
	public void preDeckCreatGUI ()
	{
		float h = Screen.height;
		float w = Screen.width;

		// 接続済み
		if (connected)
		{
			if (canRead())
			{
				//効果音鳴らす
				Singleton<SoundPlayer>.instance.playSE("enter");
				
				if (isServer)
				{
					string r = readMessage ();
					int seisuu = -1;

					if(int.TryParse(r , out seisuu))
					   firstAttack = 1 - seisuu;
					else 
						Debug.Log(r);

					thinkingPlayer=firstAttack;
				}

				DeckCreat(1);
				
/*				Shuffle (1);

				preDeckCreat[1] = false;
				
				if (isServer)
				{
					sendMessageBuf ();
				}*/
			}
		}
        //uGUI化しました
/*		else if (connectButtun)
		{
			bool buf = isTrans;

			isTrans = GUI.Toggle (new Rect (w / 5 + w / 50, 10 + w / 10 * 2 / 3 + 10, w / 10, w / 10 * 2 / 3), isTrans, "通信モード");

			if(isTrans != buf)
				replayMode=false;

			if (isTrans)
			{
				GUILayout.Label ("Sever and Client");

				connectionIP = GUILayout.TextField (connectionIP);

				// Clientになる場合
				if (GUILayout.Button ("Client"))
				{
                    pushClient();
				}
	 
				// Serverになる場合
				if (GUILayout.Button ("Server"))
				{
                    pushServer();
				}
				
				// Networkに接続する場合
				GUILayout.Label ("");
				GUILayout.Label ("Random Matching");
				if (GUILayout.Button ("connect"))
				{
                    pushConnect();
				}
			}
			else
			{
				float hei=(w / 5 - 20f);
					 
				if (GUI.Button (new Rect (10, h /50 , hei, hei / 3 * 1.5f), "デュエルスタンバイ！"))
				{
					pushDuelStandby ();
				}

				//replay
				string s="リプレイモード";

				if(replayMode)
					s = "ノーマルモード";

				if (GUI.Button (new Rect (10, h/50 + hei / 3*	1.5f + h/50, hei, hei / 3 * 1.5f), s))
				{
					replayMode = ! replayMode;
				}
			}
			firstAttackGUI ();
			scrollGUI ();
		}*/
	}

    void pushClient()
    {
        isTrans = true;
        Network.Connect(connectionIP, portNumber);
        connectButtun = false;
        notDebug();
    }

    void pushServer()
    {
        isTrans = true;
        Network.InitializeServer(1, portNumber, false);
        connectButtun = false;
        notDebug();
    }

    void pushConnect()
    {
        isTrans = true;
        connectButtun = false;
        NetScr.connetServer();
        DebugFlag = false;
        AttackPhaseSkip = true;

    }
	
	public void firstAttackGUI ()
	{
		float h = Screen.height;
		float w = Screen.width;
		
		if (GUI.Button (new Rect (10, h / 3, w / 5 - 20, h / 20), firstAttackStr))
		{
			firstAttack = 1 - firstAttack;
			if (firstAttackStr == "先行")
				firstAttackStr = "後攻";
			else
				firstAttackStr = "先行";
		}
	}

	void notDebug ()
	{
/*		if (connectionIP == roopIP)
			return;
        */
		DebugFlag = false;
		AttackPhaseSkip = true;
	}

    void pushRepButton(string s)
    {

        DebugFlag = false;
        AttackPhaseSkip = true;
        isTrans = false;
        replayMode = true;

        replayName = s;
        receivedIndex = 0;
        sentIndex = 0;

        loadReplay();

        if (sentList.Count > 0 && receivedList.Count > 0)
        {
            if (int.TryParse(sentList[0], out firstAttack))
                sentIndex++;
            else if (int.TryParse(receivedList[0], out firstAttack))
            {
                firstAttack = 1 - firstAttack;
                receivedIndex++;
            }
            else NetScr.resetManager();

            //p1
            DeckCreat(0);
            //p2
            DeckCreat(1);

        }
    }
	
	public void scrollGUI ()
	{
		float h = Screen.height;
		float w = Screen.width;
		//スクロールメニュー
		int y_pos = 0, dy = 40;

		int count=ListOfDeck.Length;

		if(replayMode)
			count=replayList.Length;
		
		scrollViewVector = GUI.BeginScrollView (new Rect (0, h * 2 / 3, w / 5, h / 3),
		                                        scrollViewVector, 
		                                        new Rect (0, 0, w / 50 - 10, count * dy));

		if(replayMode){
			foreach (string s in replayList)
			{
				if (GUI.Button (new Rect (0, y_pos, w / 5 - 10, dy), s))
				{
                    pushRepButton(s);
/*					AttackPhaseSkip = true;
					DebugFlag=false;

					replayName=s;
					receivedIndex=0;
					sentIndex=0;

					loadReplay();

					if(sentList.Count>0 && receivedList.Count>0){
						if(int.TryParse(sentList[0],out firstAttack))
							sentIndex++;
						else if(int.TryParse(receivedList[0],out firstAttack))
						{
							firstAttack = 1 - firstAttack;
							receivedIndex++;
						}
						else NetScr.resetManager();

						//p1
						DeckCreat(0);

/*						for (int i = 0; i < deckNum[0]; i++) {
							shuffleBuf.Add(replayRead(0));
						}

						Shuffle(0);

						//p2
						DeckCreat(1);

/*						for (int i = 0; i < deckNum[1]; i++) {
							shuffleBuf.Add(replayRead(1));
						}

						Shuffle(1);

						//ルリグの決定
						GUImoveID = int.Parse(replayRead(0));
						selectCardFlag=false;
                }*/
				}
				y_pos += dy;
			}

		}
		else{
			foreach (FileInfo sF in ListOfDeck)
			{
				if (GUI.Button (new Rect (0, y_pos, w / 5 - 10, dy), sF.Name.Split ('.') [0]))
				{
					if (DeckSelectplayer [0])
					{
						DeckString [0] = sF.Name.Split ('.') [0];
					}
					if (DeckSelectplayer [1])
					{
						DeckString [1] = sF.Name.Split ('.') [0];
					}
					
				}
				y_pos += dy;
			}
		}

		GUI.EndScrollView ();

		for (int i=0; i<2; i++)
		{
			DeckSelectplayer [i] = GUI.Toggle (new Rect (10, h / 2 + h / 20 * i, w / 10, h / 40), DeckSelectplayer [i], "player" + (i + 1));
			if (DeckSelectplayer [i])
				DeckSelectplayer [1 - i] = false;
			GUI.Label (new Rect (10, h / 2 + h / 20 * i + h / 40, w / 10, w / 10 * 2 / 3), DeckString [i]);
		}
	}

	string replayRead(int player)
	{
		player=player%2;
		string s=errorStr;

		if(player==0)
		{
			if(sentList.Count > sentIndex){
				s = sentList[sentIndex];
				sentIndex++;
			}
		}
		else
		{
			if(receivedList.Count > receivedIndex){
				s = receivedList[receivedIndex];
				receivedIndex++;
			}
		}

		if(replayNextReading(player)==endStr)
			replayRead(player);

		return s;
	}

	string replayNextReading(int player)
	{
		player=player%2;
		string s=errorStr;

		if(player==0)
		{
			if(sentList.Count > sentIndex){
				s = sentList[sentIndex];
			}
		}
		else
		{
			if(receivedList.Count > receivedIndex){
				s = receivedList[receivedIndex];
			}
		}

		return s;
	}

	string getNextMessage(int player)//replayモードならnextReading を、それ以外はmessage[0]を返す
	{
		if(replayMode)
			return replayNextReading(player);

		if(messages.Count==0)
			return errorStr;

		return messages[0];
	}

	bool waitYou(int player)
	{
		readTargetPlayer=player;

		return (isTrans && player==1) || replayMode;
	}

	bool isAttackEnd ()
	{
		if (phase == Phases.AttackPhase && ionaFlag [getTurnPlayer ()] && isSigniAttackable ())
			return false;
		
		return true;
	}
	
	bool isSigniAttackable ()
	{
		int player = getTurnPlayer ();
		for (int i=0; i<3; i++)
		{
			int id = fieldRankID (Fields.SIGNIZONE, i, player);
			if (id >= 0)
			{
				if (getCardScr (id, player).Attackable && signiCondition [player, i] == Conditions.Up)
					return true;
			}
		}
		return false;
	}
	
	public void targetableExceedIn (int player, CardScript sc)
	{
		for (int i=0; i<fieldAllNum(Fields.LRIGZONE,player); i++)
		{
			int id = fieldRankID (Fields.LRIGZONE, i, player);
			if (getCardType (id, player) == 0)
			{
				if (id != getLrigID (player))
				{
					sc.Targetable.Add (id + player * 50);
				}
			}
		}
	}

    public void targetableSameNameRemove(CardScript sc)
    {
        List<string> list = new List<string>();

        for (int i = 0; i < sc.Targetable.Count; i++)
        {
            string n = getCardScr(sc.Targetable[i] % 50, sc.Targetable[i] / 50).Name;

            if (list.Contains(n))
            {
                sc.Targetable.RemoveAt(i);
                i--;
            }
            else
                list.Add(n);
        }

        list.Clear();
    }

	public void targetableCharmIn (int player, CardScript sc)
	{
		int target = player;
		int f = 11;

		for (int i=0; i<3; i++)
		{
			int x = getFieldRankID (f, i, target);
			if (x >= 0)
				sc.Targetable.Add (x + 50 * target);		
		}
	}

    public void targetableUnderCardsIn(int ID,int player)
    {
        CardScript sc = getCardScr(ID, player);

        if (ID < 0 || sc == null || field[player,ID] != Fields.SIGNIZONE) 
            return;

        int rank = fieldRank[player, ID];

        for (int i = 0; i < underCards[rank].Count; i++)
        {
            int id = underCards[rank][i];
            sc.Targetable.Add(id);
        }
    }

	void LrigZoneSort (int player)
	{
		int max = fieldAllNum (Fields.LRIGZONE, player);
		for (int i = 0; i < max; i++)
		{
			int id = fieldRankID (Fields.LRIGZONE, i, player);
			Vector3 v = vec3Addy (LRIGZONE, 0.025f * i);
			if (player == 1)
				v = vec3Player2 (v);
			card [player, id].transform.position = v;
		}
	}
	
	void applySpellCostDown (int id, int player, int SpellOrArts )
	{
		if (spellCostDownFlag[SpellOrArts])
			return;
		
		CardScript sc = getCardScr (id, player);
		for (int i=0; i<sc.Cost.Length(); i++)
			sc.Cost.addCost(i, - spellCostDown [player, i, SpellOrArts]);

		spellCostDownFlag[SpellOrArts] = true;
	}
	
	void restrateSpellCostDown (int id, int player, int SpellOrArts)
	{
		CardScript sc = getCardScr (id, player);
		for (int i=0; i<sc.Cost.Length(); i++)
			sc.Cost.addCost(i,spellCostDown [player, i, SpellOrArts]);

		spellCostDownFlag[SpellOrArts] = false;
	}
	
	void resetSpellCostDown (int player, int SpellOrArts)
	{
		for (int i=0; i<spellCostDown.GetLength(1); i++)
		{
			spellCostDown [player, i, SpellOrArts] = 0;
		}

		costDownResetFlag[SpellOrArts]=true;
	}

	void saveDeckString ()
	{
		for (int i=0; i<DeckString.Length; i++)
		{
			Singleton<saveData>.instance.setData(DeckStringKey[i],DeckString[i]);
		}
	}

	void loadDeckString ()
	{
		for (int i=0; i<DeckString.Length; i++)
		{
			if (PlayerPrefs.HasKey (DeckStringKey [i]))
				DeckString [i] = Singleton<saveData>.instance.getData(DeckStringKey[i],"deck");

			if(!File.Exists(@"deck/"+DeckString [i]+".txt") && ListOfDeck.Length > 0)
				DeckString [i]=ListOfDeck[0].Name.Split('.')[0];
		}
	}

	bool useLimit(int ID,int player){
		if (!DebugFlag && !kyomuFlag[player] && checkLrigLim (ID, player))
			return true;

		if(getCardScr(ID,player).useLimit)
			return true;

		return false;
	}

	public void negate(int ID,int player)
	{
		if(ID<0)return;

        if (trueIgnitionID >= 0)
        {
            ID = trueIgnitionID % 50;
            player = trueIgnitionID / 50;
        }

		CardScript sc=getCardScr(ID,player);

		sc.effectTargetID.Clear();
		sc.effectMotion.Clear();
		sc.Targetable.Clear();
        sc.DialogFlag = false;
	}

	public bool isEffectFlagUp()
	{
		for (int i = 0; i < 100; i++)
		{
			if(getCardScr(i%50,i/50).effectFlag)
				return true;
		}

		return false;
	}

	bool isGrowPhaseSkip(int player)
	{
		return  GrowPhaseSkip[player] && !lrigSummonFlag[player];
	}

	public void hakkensitayo ()
	{
		hakken = true;
	}

	public int hakkenNumdayo ()
	{
		return hakkenNum;
	}

	void savePlayerPrefs()
	{
		PlayerPrefs.SetString(connectionKey, connectionIP);


		PlayerPrefs.SetFloat("volumeKey", Singleton<SoundPlayer>.instance.VOLUME);


//		PlayerPrefs.DeleteKey("canReplaySendKey");
//		if(!canReplaySend)
//			PlayerPrefs.SetInt("canReplaySendKey",0);

		PlayerPrefs.DeleteKey("replaySaveFlagKey");
		if(Singleton<config>.instance.ReplaySaveFlag)
			PlayerPrefs.SetInt("replaySaveFlagKey",1);


		//デッキストリングの保存
		saveDeckString();
	}

	void loadPlayerPrefs()
	{
		//デッキストリングの更新
		loadDeckString();
		
		//connectionIPの更新
		if(PlayerPrefs.HasKey(connectionKey))
			connectionIP=PlayerPrefs.GetString(connectionKey);

		//VOLUME の更新
//		if(PlayerPrefs.HasKey("volumeKey"))
//			Singleton<SoundPlayer>.instance.VOLUME=PlayerPrefs.GetFloat("volumeKey");

//		if(PlayerPrefs.HasKey("canReplaySendKey"))
//			canReplaySend = false;

//		if(PlayerPrefs.HasKey("replaySaveFlagKey"))
//			replaySaveFlag = false;
	}

	void OnDestroy(){
		savePlayerPrefs();

	}

	public void addSentList(string s){
		sentList.Add(s);
	}

	public void addReceivedList(string s){
		receivedList.Add(s);
	}

	void sendSentList()
	{
		for (int i = 0; i < sentList.Count; i++)
		{
			NetScr.rpcOthers("addSentList", new object[]{ sentList[i] } );
		}
	}

	void sendReceivedList()
	{
		for (int i = 0; i < receivedList.Count; i++)
		{
			NetScr.rpcOthers("addReceivedList", new object[]{ receivedList[i] } );
		}
	}

	public void sendReplay()
	{
		sendSentList();
		sendReceivedList();
	}

    void checkUgui()
    {
        if (beforGame == null)
            return;

        if (panelObj != null)
        {
            if (Singleton<config>.instance.configNow)
                panelObj.SetActive(false);
            else if(!panelObj.activeSelf)
                panelObj.SetActive(true);
        }

        bool flag = true;

        var com = beforGame.GetComponent<beforeGameManeger>();

        if (com.uiString == "start debug"
            || com.uiString == "server"
            || com.uiString == "client"
            || com.uiString == "connect")
        {
            DeckString[0] = com.getSelectedDeck(0);
            DeckString[1] = com.getSelectedDeck(1);

            if (com.getFirstAttack() == "先行")
                firstAttack = 0;
            else
                firstAttack = 1;

        }

        switch (com.uiString)
        {
            case "start replay":
                string s = com.getSelectedReplay();
                if (s!=string.Empty && Directory.Exists("./replay/" + s))
                    pushRepButton(s);
                else
                    flag = false;
                break;

            case "start debug":
                pushDuelStandby();
                break;

            case "server":
                pushServer();
                break;

            case "client":
                pushClient();
                break;

            case "connect":
                pushConnect();
                break;

            case "サレンダー":
                pushSurrenderButton();
                break;

            case "next phase":
                nextPhaseButton();
                flag = false;
                break;

            case "Yes":
                uiYes = true;
                flag = false;
                break;

            case "No":
                uiNo = true;
                flag = false;
                break;

            default:
                flag = false;
                break;
        }

        com.uiString = "";

        if (flag)
            beforGame.SetActive(false);
    }

    public void resetShowCard()
    {
        if (showCardText != null)
            showCardText.text = "";

        if (showCardImage != null)
            showCardImage.texture = Resources.Load("transTexture") as Texture;

        beforGame.SetActive(true);
    }

    void DoSystemInputReturn(int count)
    {
        if (SystemCardInputReturnFunc == null)
            return;

        SystemCardInputReturnFunc(count);
        SystemCardInputReturnFunc = null;
    }

    bool checkACG(int ID, int player)
    {
        return ACGFlag[player] || getCardScr(ID, player).checkAbility(ability.DontSelfGoTrash);
    }

    public void setSigniSummonLim(int levLim, int target, int myID, int myPlayer)
    {
        if (signiSumLimit[target] <= levLim)
            return;


        signiSumLimit[target] = levLim;
        signiSumLimitChangedID[target] = myID + myPlayer * 50;
    }

    public void normalizeSigniSummonLim(int target,int myID, int myPlayer)
    {
        if (!checkChangeSigniSummonLim(target, myID, myPlayer))
            return;

        signiSumLimit[target] = 3;
        signiSumLimitChangedID[target] = -1;
    }

    bool checkChangeSigniSummonLim(int target,int myID, int myPlayer)
    {
        return getFusionID(myID, myPlayer) == signiSumLimitChangedID[target];
    }

}