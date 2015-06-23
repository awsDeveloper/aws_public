using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class DeckManager : MonoBehaviour {
	//各種宣言
	private GameObject[] card = new GameObject[60];//カードの姿かたちを保有
	private string[] tok=new string[50];//デッキ構成の情報
	private string[] searchCard=new string [101]; //検索結果を保有
	private int searchIndex;//検索結果の数を保有
	private Drag dragme;
	private string DeckName="Deck";
	private string NowDeckName = "Deck";
	private string searchName="";
	private DeckMake[] deckmake = new DeckMake[60];
	private bool changeFlag;
	private string fileSaveMassage = "You can save deck here.";
	private int numofsigni = 0;
	private int numofspell = 0;
	// Use this for initialization
	
	void Start () {//身その一（はじめの処理）
		Resources.LoadAll ("");
		dragme = GetComponent<Drag> ();

		searchAlldeck ();

		//deckNameを読みだす
		string loadName = Singleton<saveData>.instance.getData("DeckKey1","Deck");
		if(!File.Exists(@"deck/"+loadName+".txt") && ListOfDeck.Length > 0)
			loadName = ListOfDeck[0].Name.Split('.')[0];

		DeckName = loadName;

		LoadDeck (loadName);

		ShowDeck ();
		CheckDeckComponent ();
		SearchCard ("タマ",isinitialization : true);
	}

	//オブジェクトが破棄されたときの処理
	void OnDestroy(){
		//deckNameを保存する
        string oldDeck=Singleton<saveData>.instance.getData("DeckKey1", "Deck");
        if (oldDeck != DeckName)
        {
            Singleton<saveData>.instance.setData("DeckKey2", oldDeck);
            Singleton<saveData>.instance.setData("DeckKey1", DeckName);
        }
	}

	private int checkdeckcount = -1;

	void Update(){//身その1.5（毎回やる処理）
		if (checkdeckcount >= 3)
		{
			checkdeckcount = -1;
			CheckDeckComponent ();
		}
		else if (checkdeckcount >= 0)
			checkdeckcount++;
		if (Input.GetMouseButtonDown (1)) {
			InsertCardIntoDeck ();
			CheckDeckComponent();
		}
		if (Input.GetMouseButtonUp (0))
		{
			CheckDeckComponent();
			checkdeckcount++;
		}


	}
	
	
	private string[] NumSearchOption = new string[4] {"指定なし","ぴったり","以上","以下"};
	private string[] TypeSelection = new string[5] {"指定なし","ルリグ","アーツ","シグニ","スペル"};
	private int levelselect = 0;
	private int whichtypewatch = 0;
	private int searchColor = 0;
	private string searchPowoerInput = "0";
	private int PowerSelection = 0;
	private string[] ColorSelection = new string[7] {"指定なし","無色","白","赤","青","緑","黒"};
	private string searchLevel = "0";
	private string[] LifeBurstSelection = new string[3] {"指定なし","ライフバースト","それ以外"};
	private int searchBurstInput = 0;
	private FileInfo[] ListOfDeck;
	
	private float slidervalue = 0;
	
	private Vector2 scrollViewVector = new Vector2 (0,0);
	
	
	void searchAlldeck(){

		DirectoryInfo di = new DirectoryInfo("./deck/");
		ListOfDeck = di.GetFiles("*.txt",System.IO.SearchOption.TopDirectoryOnly);
	}

	public GUIStyle customButton;
	
	void OnGUI()//身その二（ＵＩ）
	{
		if(Event.current.type == EventType.keyDown  &&Event.current.character == '\n'){
			Debug.Log ("エンターが押された");
			if( GUI.GetNameOfFocusedControl().IndexOf("kensaku") >= 0 ){

				SearchCard(searchName,int.Parse (searchLevel),levelselect,whichtypewatch,searchColor,int.Parse (searchPowoerInput),PowerSelection,searchBurstInput,false);
			}
		}



		GUI.Window (810, new Rect (Screen.width-1010, 10, 500, 140), drawwindow, "デッキ");

		GUI.Window (1919, new Rect (Screen.width-500, 10, 490, 140), searchwindow, "検索");

		GUI.Label (new Rect (Screen.width-530, 155, 100, 20), "検索結果：" + searchIndex.ToString () + "件");
		GUI.Label (new Rect (0,Screen.height-40,100,20), "シグニ：" + numofsigni);
		GUI.Label (new Rect (0,Screen.height-70,100,20), "スペル：" + numofspell);
		GUI.Label (new Rect (0,Screen.height-100,100,20), "デッキ内構成");


		if ( GUI.Button( new Rect(Screen.width-420, 155, 100, 20), "前へ" ) )//buttonがぶっとんだｗｗｗｗｗｗｗｗｗ
		{
			if(ShowCardIndex>0)ShowCardIndex--;
			ShowCard (false);
		}
		
		if ( GUI.Button( new Rect(Screen.width-210, 155, 100, 20), "次へ" ) )//buttonがぶっとんだｗｗｗｗｗｗｗｗｗ
		{
			if(ShowCardIndex<(searchIndex-1)/10)ShowCardIndex++;
			ShowCard(false);
		}
	}
	
	private int ShowCardIndex = 0;

	private void NoneSearchGUI(){
		GUI.SetNextControlName ("kensaku0");
		searchName = GUI.TextField (new Rect (80, 100, 100, 20),searchName);
		GUI.Label(new Rect(0,100,80,20), "カード名：");
		ToggleOfType ();
		ToggleOfColor ();
		PowerSelection = 0;
		searchBurstInput = 0;
		levelselect = 0;
		searchLevel = "0";

	}
	
	private void LrigSearchGUI(){
		GUI.SetNextControlName ("kensaku1");
		searchName = GUI.TextField (new Rect (80, 100, 100, 20),searchName);
		GUI.Label(new Rect(0,100,80,20), "カード名：");
		ToggleOfType ();
		ToggleOfColor ();
		ToggleOfLevel ();
		PowerSelection = 0;
		searchBurstInput = 0;
	}
	private void ArtsSearchGUI(){
		GUI.SetNextControlName ("kensaku2");
		searchName = GUI.TextField (new Rect (80, 100, 100, 20),searchName);
		GUI.Label(new Rect(0,100,80,20), "カード名：");
		ToggleOfType ();
		ToggleOfColor ();
		levelselect = 0;
		searchLevel = "0";
		PowerSelection = 0;
		searchBurstInput = 0;
	}
	private void SigniSearchGUI(){
		GUI.SetNextControlName ("kensaku3");
		searchName = GUI.TextField (new Rect (80, 100, 100, 20),searchName);
		GUI.Label(new Rect(0,100,80,20), "カード名：");
		
		ToggleOfType ();
		ToggleOfColor ();
		
		ToggleOfLevel ();
		ToggleOfPower ();
		ToggleOfBurst ();
	}
	private void SpellSearchGUI(){
		GUI.SetNextControlName ("kensaku4");
		searchName = GUI.TextField (new Rect (80, 100, 100, 20),searchName);
		GUI.Label(new Rect(0,100,80,20), "カード名：");
		ToggleOfType ();
		ToggleOfColor ();
		ToggleOfBurst ();
		levelselect = 0;
		searchLevel = "0";
		PowerSelection = 0;
	}
	
	private void ToggleOfType(){
		whichtypewatch = GUI.SelectionGrid(new Rect(0,0,450,20),whichtypewatch, TypeSelection, 5, "Toggle");
	}
	
	private void ToggleOfColor(){
		searchColor = GUI.SelectionGrid (new Rect(0,20,480,20),searchColor, ColorSelection, 7, "Toggle");
	}
	
	private void ToggleOfLevel(){
		GUI.Label(new Rect(0,40,60,20), "レベル：");
		levelselect = GUI.SelectionGrid (new Rect(160,40,280,20),levelselect, NumSearchOption, 4, "Toggle");
		searchLevel = GUI.TextField (new Rect (60, 40, 100, 20), searchLevel);
	}
	
	private void ToggleOfPower(){
		GUI.Label(new Rect(0,60,60,20), "パワー：");
		PowerSelection = GUI.SelectionGrid (new Rect(160,60,280,20),PowerSelection, NumSearchOption, 4, "Toggle");
		searchPowoerInput = GUI.TextField (new Rect (60, 60, 100, 20), searchPowoerInput);
	}
	
	private void ToggleOfBurst(){
		searchBurstInput = GUI.SelectionGrid (new Rect(0,80,330,20),searchBurstInput, LifeBurstSelection, 3, "Toggle");
	}

	void searchwindow(int id){
		GUILayout.BeginArea(new Rect(10, 15, 500, 200));

		switch (whichtypewatch-1) {
		case -1:NoneSearchGUI();break;
		case 0:LrigSearchGUI();break;
		case 1:ArtsSearchGUI ();break;
		case 2:SigniSearchGUI();break;
		case 3:SpellSearchGUI ();break;
		default:NoneSearchGUI ();break;
		}

		GUILayout.EndArea();


		if ( GUI.Button( new Rect(270, 115, 100, 20), "検索ぼたん" ) ||false)//buttonがぶっとんだｗｗｗｗｗｗｗｗｗ
		{
			SearchCard(searchName,int.Parse (searchLevel),levelselect,whichtypewatch,searchColor,int.Parse (searchPowoerInput),PowerSelection,searchBurstInput,false);
		}


	}

	void drawwindow(int id){
		if ( GUI.Button( new Rect(140, 30, 80, 20), "保存" ,customButton) )//buttonがぶっとんだｗｗｗｗｗｗｗｗｗ
		{
			WriteIntoDeckFile();

			Singleton<SoundPlayer>.instance.playSE("decision");
		}

		if ( GUI.Button( new Rect(260, 30, 80, 20), "編集終了" ,customButton) )//buttonがぶっとんだｗｗｗｗｗｗｗｗｗ
		{
			Application.LoadLevel("menusScene");
		}		
	
		if ( GUI.Button( new Rect(260, 70, 100, 20), "別名保存" ,customButton) )//buttonがぶっとんだｗｗｗｗｗｗｗｗｗ
		{
			Singleton<SoundPlayer>.instance.playSE("decision");

			WriteIntoDeckFile(DeckName);
			searchAlldeck ();
		}

//		if ( GUI.Button( new Rect(260, 100, 100, 20), "新規作成",customButton ) )//buttonがぶっとbんだｗｗｗｗｗｗｗｗｗ
//		{
//			makeNewDeck (DeckName);
//			ShowDeck();
//		}

		if ( GUI.Button( new Rect(260, 100, 100, 20), "カード全削除",customButton ) )//buttonがぶっとbんだｗｗｗｗｗｗｗｗｗ
		{
			DeleteDeck ();
		}

		if ( GUI.Button( new Rect(140, 100, 100, 20), "ＩＤで整列",customButton ) )//buttonがぶっとbんだｗｗｗｗｗｗｗｗｗ
		{
			SortDeck ();
		}
		//		if ( GUI.Button( new Rect(120, 120, 100, 20), "ひらく" ,customButton) )//buttonがぶっとんだｗｗｗｗｗｗｗｗｗ
//		{
//			LoadDeck (DeckName);
//			ShowDeck();
//		}

		if ( GUI.Button( new Rect(380, 30, 100, 20), "パワーで整列",customButton ) )//buttonがぶっとbんだｗｗｗｗｗｗｗｗｗ
		{
			sortCardByPower ();
		}

		if ( GUI.Button( new Rect(380, 70, 100, 20), "レベルで整列",customButton ) )//buttonがぶっとbんだｗｗｗｗｗｗｗｗｗ
		{
			sortCardByLevel ();
            sortCardByType();
		}

		if ( GUI.Button( new Rect(380, 100, 100, 20), "色で整列",customButton ) )//buttonがぶっとbんだｗｗｗｗｗｗｗｗｗ
		{
			sortCardByColor ();
		}


		DeckName = GUI.TextField (new Rect (140, 70, 100, 20),DeckName);

		scrollViewVector = GUI.BeginScrollView(new Rect(10, 30, 120,100) , scrollViewVector, new Rect (0, 0, 99, ListOfDeck.Length*40));
		
		int y = 0, dy = 40;
		foreach (FileInfo sF in ListOfDeck) {
			if(GUI.Button (new Rect (0,y,99,dy), sF.Name.Split('.')[0])){
				LoadDeck (sF.Name.Split('.')[0]);
				CheckDeckComponent();
				ShowDeck ();

				DeckName = NowDeckName;
			}
			y+=dy;
		}
		
		GUI.EndScrollView();



	}
	
	void LoadDeck(string deckname){//デッキ情報が入ったテキストファイルを読み込んで内容を保存.ない場合新規作成
		
		
		NowDeckName = deckname;
		System.Text.Encoding.GetEncoding("utf-16");
		FileInfo fi = new FileInfo("./deck/"+NowDeckName+".txt");//ここもなおすよ
		StreamReader sw = fi.OpenText();
		string readstring = sw.ReadToEnd ();
		sw.Close ();
		tok=readstring.Split('\n');
		for (int i = 0; i < tok.Length; i+= 1)
			tok[i] = tok[i].Trim();
	}
	
	void makeNewDeck(string deckname){
		NowDeckName = deckname;
		TextAsset txt=(TextAsset)Resources.Load(deckname);//かぶってたら新規作成しない
		if (txt == null) {
			File.Create ("deck/"+ deckname +".txt").Close ();//なおすよ
			tok = null;
			searchAlldeck ();
			return;
		}

	}

	void DeleteDeck(){
	
		for (int i =0; i<50; i++)
		{
			deckmake[i].SetCard ("death");
		}
	
	
	}
	
	bool isStart = true;
	void ShowDeck(){//自分の保持しているデータからカードを並べる
		int imasagasiteirucard = 0;
		const float dx = 3.5f;
		float x = -10f;
		for (int retu=0; retu<3; retu++) {
			x = -10f;
			for (int deckNum=0; deckNum<10; deckNum++) {
				if(!isStart)deckmake [deckNum+10*retu].SetCard ("death");
				else{
					card [deckNum+10*retu] = (GameObject)Instantiate (
						Resources.Load ("Prefab/CardHolder"),
						new Vector3 (x, -(float)retu*4f, 0f),
						Quaternion.identity
						);
					deckmake [deckNum+10*retu] = card [deckNum+10*retu].GetComponent<DeckMake> ();
				}
				if(tok == null)continue;
				
				x += dx;
				
				deckmake [deckNum+10*retu].SetIsDeck (true);
				if(retu==0){
					
					deckmake [deckNum+10*retu].SetIsLrig (true);
					deckmake [deckNum+10*retu].SetCard (tok[imasagasiteirucard]);
					Debug.Log ("imasagasiteirucard in lrig = " +imasagasiteirucard.ToString ());
					Debug.Log ("decknum in lrig = " +(deckNum+10*retu).ToString ());

					imasagasiteirucard++;
				}
				else
				do{
					if(imasagasiteirucard>=50)break;
					deckmake [deckNum+10*retu].SetCard (tok[imasagasiteirucard]);
					Debug.Log (imasagasiteirucard.ToString() + "aaaaa");
					if(tok[imasagasiteirucard]=="")deckmake[deckNum+10*retu].SetIsBurst(true);
					imasagasiteirucard++;
				}while(!deckmake [deckNum+10*retu].IsLifeBurst()&&imasagasiteirucard <50);
				card [deckNum+10*retu].transform.parent = transform;
			}
		}

		imasagasiteirucard = 10;
		for (int retu=3; retu<5; retu++) {
			x = -10f;
			for (int deckNum=0; deckNum<10; deckNum++) {
				if(!isStart)deckmake [deckNum+10*retu].SetCard ("death");
				else{
					card [deckNum+10*retu] = (GameObject)Instantiate (
						Resources.Load ("Prefab/CardHolder"),
						new Vector3 (x, -(float)retu*4f, 0f),
						Quaternion.identity
						);
					deckmake [deckNum+10*retu] = card [deckNum+10*retu].GetComponent<DeckMake> ();
				}
				if(tok == null)continue;
				
				x += dx;
				
				deckmake [deckNum+10*retu].SetIsDeck (true);
				deckmake [deckNum+10*retu].SetCard (tok[deckNum+10*retu]);
				do{
					if(imasagasiteirucard>=50)break;
					deckmake [deckNum+10*retu].SetCard (tok[imasagasiteirucard]);
					if(tok[imasagasiteirucard]=="")
						deckmake[deckNum+10*retu].SetIsBurst(false);

					imasagasiteirucard++;
				}while(deckmake [deckNum+10*retu].IsLifeBurst()&&imasagasiteirucard<50);
				
				card [deckNum+10*retu].transform.parent = transform;
			}
		}
		
		isStart = false;		
	}
	
	void ShowCard(bool isInishalization){
		const float dx = 3.5f;
		float x = -10f;
		for (int deckNum=0; deckNum<10; deckNum++) {
			if(isInishalization){
				card [deckNum+50] = (GameObject)Instantiate (
					Resources.Load ("Prefab/CardHolder"),
					new Vector3 (x, 5f, 0f),
					Quaternion.identity
					);
				x += dx;
				deckmake [deckNum+50] = card [deckNum+50].GetComponent<DeckMake> ();
			}
			deckmake [deckNum+50].SetIsDeck(false);
			
			deckmake [deckNum+50].SetCard (searchCard [deckNum+ShowCardIndex*10]);
			card [deckNum+50].transform.parent = transform;
			
		}	
		
	}
	
	void SearchCard(string searchname,int searchlevel = 0,int searchlevelselect = -1,int searchtypeselect = -1,int searchcolor = 0,int argSearchPower=0,int argPowerSelection = 0,int argSearchBurst = 0,bool isinitialization = false){//検索のアレコレをするメソッド.
		searchIndex = 0;
		ShowCardIndex = 0;
		Resources.LoadAll ("");
		TextAsset[] unko = (TextAsset[])Resources.FindObjectsOfTypeAll(typeof(TextAsset));
        System.Globalization.CompareInfo ci =
            System.Globalization.CultureInfo.CurrentCulture.CompareInfo;
		Debug.Log ("*data.txt of length = " + unko.Length);


//		DirectoryInfo di = new DirectoryInfo(Application.dataPath);
//		FileInfo[] searchFiles = di.GetFiles("*data.txt",System.IO.SearchOption.AllDirectories);
//		foreach (FileInfo sF in searchFiles) {
//			bool iscorrectcard = (searchlevelselect==-1)?true:false;
//			TextAsset textAsset=(TextAsset)Resources.Load(sF.Name.Split ('-')[0]+"/"+sF.Name.Split ('.')[0]);//その場しのぎ率114514％,なおすよ
//			string[] s =textAsset.text.Split(' ','\n');


		foreach(TextAsset sF in unko){
			bool iscorrectcard = (searchlevelselect==-1)?true:false;
			TextAsset textAsset = sF;
			string[] s = textAsset.text.Split(' ','\n');



		for(int i=0;i<s.Length;i++){//ここまででファイルの読み込み完了,以下に実際の検索をかく
				if(s[i]=="#Type"){
                     if (searchtypeselect==0||int.Parse (s[i+1]) == searchtypeselect-1) {
						iscorrectcard = true;
						
					}
					break;
				}
			}
			
			
			for(int i=0;i<s.Length;i++){//ここまででファイルの読み込み完了,以下に実際の検索をかく

                if (searchname != "" && s[i] == "#Name") if (ci.IndexOf(s[i + 1], searchname, System.Globalization.CompareOptions.IgnoreWidth |
            System.Globalization.CompareOptions.IgnoreKanaType | System.Globalization.CompareOptions.IgnoreCase) < 0 /*s[i+1].IndexOf (searchname) < 0*/)
                    {
					iscorrectcard = false;
				}
				
				if(argSearchBurst!=0&&s[i]=="#BurstIcon")if ((s[i+1].IndexOf ("True") < 0&&argSearchBurst==1)||(s[i+1].IndexOf ("False") < 0&&argSearchBurst==2)) {
					iscorrectcard = false;
				}
				
				if(searchlevelselect!=0&&s[i]=="#Level"){
					if (searchlevelselect == 1 &&s[i+1].IndexOf (searchlevel.ToString()) < 0) {//レベル１０以上のカードがある場合この実装では不十分
						iscorrectcard = false;
					}
					if (searchlevelselect == 2 &&int.Parse (s[i+1]) < searchlevel) {
						iscorrectcard = false;
					}
					if (searchlevelselect == 3 &&int.Parse (s[i+1]) > searchlevel) {
						iscorrectcard = false;
					}
				}
				
				if(argPowerSelection!=0&&s[i]=="#Power"){
					if (argPowerSelection == 1 &&int.Parse (s[i+1]) != argSearchPower) {
						iscorrectcard = false;
					}
					if (argPowerSelection == 2 &&int.Parse (s[i+1]) < argSearchPower) {
						iscorrectcard = false;
					}
					if (argPowerSelection == 3 &&int.Parse (s[i+1]) > argSearchPower) {
						iscorrectcard = false;
					}
				}
				
				if(searchcolor!=0&&s[i]=="#Color"){
 					if (int.Parse (s[i+1])!=searchcolor-1||searchIndex>=100) {
						iscorrectcard = false;
					}
				}
				
			}		
			
			if(searchIndex>=100)iscorrectcard = false;
			if(sF.name.IndexOf ('-')<0)iscorrectcard = false;
			if(sF.name.IndexOf ('/')>=0)iscorrectcard = false;
			if(sF.text.IndexOf ("#Name")<0)iscorrectcard = false;

			if(iscorrectcard){
				searchCard[searchIndex] = sF.name.Split ('.')[0].Split ('d')[0];
				Debug.Log (sF.name+"aaaaaaaaamd"+sF.name.Split ('.')[0].Split ('d')[0]);
				searchIndex++;
			}
			
		}
		
		for (int iiiii = searchIndex; iiiii<100; iiiii++) {
			 
			searchCard[iiiii] = "";
			
		}
		
		ShowCard (isinitialization);
	}
	
	void WriteIntoDeckFile(string deckname = ""){
		if (CheckDeck() != 1)
			return;
		if (deckname != "")
			NowDeckName = DeckName;
		File.Create ("./deck/" + NowDeckName + ".txt").Close ();//なおすよ
		FileInfo fi = new FileInfo("./deck/"+NowDeckName+".txt");//ここもなおすよ
		//write
		StreamWriter sw = fi.AppendText();
		for(int i=0;i<10;i++)
		{
            if (deckmake[i].GetCard() != null && deckmake[i].GetCard() != "")
                sw.Write(deckmake[i].GetCard() + "\n");
            else sw.Write("\n");
		}
		for(int i=10;i<50;i++)
		{
			if(deckmake[i].GetCard()!=null&&deckmake[i].GetCard()!="")
				if(deckmake[i].IsLifeBurst())
					sw.Write(deckmake[i].GetCard()+"\n");
		}
		for(int i=10;i<50;i++)
		{
			if(deckmake[i].GetCard()!=null&&deckmake[i].GetCard()!="")
				if(!deckmake[i].IsLifeBurst())
					sw.Write(deckmake[i].GetCard()+"\n");
		}
		
		
		sw.Flush();
		
		sw.Close(); 		
		fileSaveMassage = "Completed";
	}

	void sortCardByPower(){
		int i, j;
		string temp;
		
		for (i = 0; i < 20 - 1; i++) {
			for (j = 20 - 1; j > i; j--) {
				if (deckmake[j + 10 - 1].getpower () < deckmake[j+10].getpower ()) {  
					temp = deckmake[j+10].GetCard();        /* 交換する */
					deckmake[j+10].SetCard (deckmake[j+10-1].GetCard ());
					deckmake[j+10 - 1].SetCard (temp);
				}
			}	
		}

		for (i = 0; i < 20 - 1; i++) {
			for (j = 20 - 1; j > i; j--) {
				if (deckmake[j + 30 - 1].getpower () < deckmake[j+30].getpower ()) {  
					temp = deckmake[j+30].GetCard();        /* 交換する */
					deckmake[j+30].SetCard (deckmake[j+30-1].GetCard ());
					deckmake[j+30 - 1].SetCard (temp);
				}
			}	
		}

		}

	void sortCardByLevel(){
		int i, j;
		string temp;

		for (i = 0; i < 10 - 1; i++) {
			for (j = 10 - 1; j > i; j--) {
				if (deckmake[j  - 1].getlevel () < deckmake[j].getlevel ()) {  
					temp = deckmake[j].GetCard();        /* 交換する */
					deckmake[j].SetCard (deckmake[j-1].GetCard ());
					deckmake[j - 1].SetCard (temp);
				}
			}	
		}
		
		for (i = 0; i < 20 - 1; i++) {
			for (j = 20 - 1; j > i; j--) {
				if (deckmake[j + 10 - 1].getlevel () < deckmake[j+10].getlevel ()) {  
					temp = deckmake[j+10].GetCard();        /* 交換する */
					deckmake[j+10].SetCard (deckmake[j+10-1].GetCard ());
					deckmake[j+10 - 1].SetCard (temp);
				}
			}	
		}
		
		for (i = 0; i < 20 - 1; i++) {
			for (j = 20 - 1; j > i; j--) {
				if (deckmake[j + 30 - 1].getlevel () < deckmake[j+30].getlevel()) {  
					temp = deckmake[j+30].GetCard();        /* 交換する */
					deckmake[j+30].SetCard (deckmake[j+30-1].GetCard ());
					deckmake[j+30 - 1].SetCard (temp);
				}
			}	
		}
		
	}

    void sortCardByType()
    {
        int i, j;
        string temp;

        for (i = 0; i < 10 - 1; i++)
        {
            for (j = 10 - 1; j > i; j--)
            {
                if (deckmake[j - 1].GetType() > deckmake[j].GetType())
                {
                    temp = deckmake[j].GetCard();        /* 交換する */
                    deckmake[j].SetCard(deckmake[j - 1].GetCard());
                    deckmake[j - 1].SetCard(temp);
                }
            }
        }

        for (i = 0; i < 20 - 1; i++)
        {
            for (j = 20 - 1; j > i; j--)
            {
                if (deckmake[j + 10 - 1].GetType() > deckmake[j + 10].GetType())
                {
                    temp = deckmake[j + 10].GetCard();        /* 交換する */
                    deckmake[j + 10].SetCard(deckmake[j + 10 - 1].GetCard());
                    deckmake[j + 10 - 1].SetCard(temp);
                }
            }
        }

        for (i = 0; i < 20 - 1; i++)
        {
            for (j = 20 - 1; j > i; j--)
            {
                if (deckmake[j + 30 - 1].GetType() > deckmake[j + 30].GetType())
                {
                    temp = deckmake[j + 30].GetCard();        /* 交換する */
                    deckmake[j + 30].SetCard(deckmake[j + 30 - 1].GetCard());
                    deckmake[j + 30 - 1].SetCard(temp);
                }
            }
        }

    }

	void sortCardByColor(){
		int i, j;
		string temp;
		
		for (i = 0; i < 10 - 1; i++) {
			for (j = 10 - 1; j > i; j--) {
				if (deckmake[j  - 1].getcolor () < deckmake[j].getcolor ()) {  
					temp = deckmake[j].GetCard();        /* 交換する */
					deckmake[j].SetCard (deckmake[j-1].GetCard ());
					deckmake[j - 1].SetCard (temp);
				}
			}	
		}
		
		for (i = 0; i < 20 - 1; i++) {
			for (j = 20 - 1; j > i; j--) {
				if (deckmake[j + 10 - 1].getcolor () < deckmake[j+10].getcolor ()) {  
					temp = deckmake[j+10].GetCard();        /* 交換する */
					deckmake[j+10].SetCard (deckmake[j+10-1].GetCard ());
					deckmake[j+10 - 1].SetCard (temp);
				}
			}	
		}
		
		for (i = 0; i < 20 - 1; i++) {
			for (j = 20 - 1; j > i; j--) {
				if (deckmake[j + 30 - 1].getcolor () < deckmake[j+30].getcolor()) {  
					temp = deckmake[j+30].GetCard();        /* 交換する */
					deckmake[j+30].SetCard (deckmake[j+30-1].GetCard ());
					deckmake[j+30 - 1].SetCard (temp);
				}
			}	
		}
		
	}





    void InsertCardIntoDeck()
    {
        string cardIdtoInsert = "1145141919";
        bool lrigFlag = false;
        bool lifeburstflag = false;

        for (int i = 0; i < 60; i++)//いまマウスの上にあるカードが何かをチェック
        {
            if (deckmake[i].Getismouseover() && !deckmake[i].IsDeck())
            {
                cardIdtoInsert = deckmake[i].GetCard();
                lrigFlag = deckmake[i].IsLrig();
                lifeburstflag = deckmake[i].IsLifeBurst();
            }
            if (deckmake[i].Getismouseover() && deckmake[i].IsDeck())
            {
                deckmake[i].DestroyCard();
                return;
            }
        }

        if (cardIdtoInsert == "1145141919")
        {//ない場合ここでボッシュート
            Debug.Log("nothing to insert");
            return;
        }

        for (int i = 0; i < 50; i++)
        {//実際の挿入
            if ((deckmake[i].GetCard() == null || deckmake[i].GetCard() == "") && deckmake[i].IsLrig() == lrigFlag && deckmake[i].IsLifeBurst() == lifeburstflag)
            {

                deckmake[i].SetCard(cardIdtoInsert);
                return;
            }
        }

    }



	void SortDeck(string deckname = ""){
		//File.Create ("./deck/SoRt_SoRt_AnD_SoRt.txt").Close ();//なおすよ
		//FileInfo fi = new FileInfo("./deck/SoRt_SoRt_AnD_SoRt.txt");//ここもなおすよ
		//write
		//StreamWriter sw = fi.AppendText();
		string[] Lrigsort = new string[10];
		string[] lifeburstsort = new string[20];
		string[] nlbs = new string[20];



		for(int i=0;i<10;i++)
		{
			if(deckmake[i].GetCard()!=null&&deckmake[i].GetCard()!="")
				Lrigsort[i] = deckmake[i].GetCard()+"";
			else Lrigsort[i] = "ZZZZZ";

			Debug.Log ("lrigsort[i] = " + Lrigsort[i]);
		}

		StringComparer cmp = StringComparer.OrdinalIgnoreCase;
		Array.Sort(Lrigsort, cmp);

		for(int i=0;i<10;i++)
		{
			if(Lrigsort[i]!="ZZZZZ")
			deckmake[i].SetCard (Lrigsort[i]);
			else deckmake[i].SetCard ("");
		}


		for(int i=10;i<30;i++)
		{
			if(deckmake[i].GetCard()!=null&&deckmake[i].GetCard()!="")
				lifeburstsort[i-10] = deckmake[i].GetCard()+"";
			else lifeburstsort[i-10] = "ZZZZZ";
		}
				
		Array.Sort(lifeburstsort, cmp);
		
		for(int i=0;i<20;i++)
		{
			if(lifeburstsort[i]!="ZZZZZ")
			deckmake[i+10].SetCard (lifeburstsort[i]);
			else deckmake[i+10].SetCard("");
		}

		for(int i=30;i<50;i++)
		{
			if(deckmake[i].GetCard()!=null&&deckmake[i].GetCard()!="")
				nlbs[i-30] = deckmake[i].GetCard()+"";
			else nlbs[i-30] = "ZZZZZ";
		}
		
		
		Array.Sort(nlbs, cmp);
		
		for(int i=0;i<20;i++)
		{
			if(nlbs[i]!="ZZZZZ")
			deckmake[i+30].SetCard (nlbs[i]);
			else deckmake[i+30].SetCard ("");
		}

		fileSaveMassage = "Completed";

	

	}

	void SortDeckbyLevel(){





	}

	void CheckDeckComponent(){
		numofsigni = 0;
		numofspell = 0;

		int ii = 0;
		for (int i =10; i<50; i++)
		{
			ii = deckmake[i]. GetType();
			if(ii==2)numofsigni++;
			else if(ii==3)numofspell++;
		}
	
	
	}

	int CheckDeck(){
		
		int NumOfLifeBurst=0;
		numsearchsystem NSS = new numsearchsystem();
		for (int i=1; i<50; i++) {//デッキの走査はdeckmake[i]でやろう
            if (deckmake[i].GetCard() == null || deckmake[i].GetCard() == "")
            {

                if (i >= 10)
                {
                    Debug.Log("d"); fileSaveMassage = "デッキが完成していません";
                    return -1;
                }
                else { Debug.Log("a"); continue; }
            }
            else
            {
                TextAsset textAsset = Singleton<DataToString>.instance.getResourceData(deckmake[i].GetCard());
                    //(TextAsset)Resources.Load(deckmake[i].GetCard().Split('-')[0] + "/" + deckmake[i].GetCard() + "data");
                NSS.SearchAndAdd(deckmake[i].GetCard());

                string[] s = textAsset.text.Split(' ', '\n');
                for (int ii = 0; ii < s.Length; ii++)
                {
                    if (i >= 10 && deckmake[i].IsLifeBurst())
 //                       && (s[ii].IndexOf("☆") >= 0 || ( s[ii].IndexOf("#BurstIcon") >= 0 && s[ii+1].IndexOf("True")>=0)) )
                    {
                        NumOfLifeBurst++;
                        break;
                    }
                    if (i < 10 && s[ii].IndexOf("#Type") >= 0 && s[ii + 1].IndexOf("0") < 0 && s[ii + 1].IndexOf("1") < 0 && s[ii + 1].IndexOf("4")<0)
                    {
                        fileSaveMassage = "Lrig Deck Error!!!" + s[ii + 1];//ルリグデッキにルリグでもアーツでもレゾナでもないやつがある場合
                        return -2;
                    }
                    if (i >= 10 && s[ii].IndexOf("#Type") >= 0 && (s[ii + 1].IndexOf("0") >= 0 || s[ii + 1].IndexOf("1") >= 0 || s[ii + 1].IndexOf("4") >= 0))
                    {
                        fileSaveMassage = "Lrig Deck Error!!";//メインデッキにルリグやアーツ、レゾナがある場合
                        return -3;
                    }
                }
            }
		}
		if (NSS.isNumOver()) {//4枚制限が守られていない
			fileSaveMassage = "Card number error!!!";
			return -4; 
		}
		if(NumOfLifeBurst!=20){
			
			fileSaveMassage = "Error!!!! LifeBurstNumError!!!" + NumOfLifeBurst.ToString ();
			return -1; //-1：ライフバーストの数がおかしい
		}
		fileSaveMassage = "Saving Deck...";
		
		return 1;
	}		
	
}


class numsearchsystem{
	private string[] cardid = new string[50];
	private int[] cardnum = new int[50];
	private int index = 0;
	
	public void SearchAndAdd(string s){
		for (int i=0; i<index; i++) {
			if(cardid[i]==s){
				cardnum[i]++;
				return;
			}	
		}
		cardid [index] = s;
		cardnum [index] = 1;
		index++;
	}
	public bool isNumOver(){
		for (int i=0; i<index; i++) {
			if(cardnum[i]>4){
				return true;
			}
			
		}
		return false;
		
	}
};
