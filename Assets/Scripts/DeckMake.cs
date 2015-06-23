using UnityEngine;
using System.Collections;


public class DeckMake : MonoBehaviour {
	private GameObject card,showcard;
	private string CardId,ChangeCardId;
	private bool isDeck,isLrig=false ;
	private bool changeflag,destroyflag,exchangeflag;
	private GameObject changeObject;
	private DeckMake deckmake;
//	private TextAsset infomations;
	private string CardInformationForShow;
	private bool isLifeBurst;
	private int cardtype = -1;
	
	//追加分
	private Texture cardTexture;
	
	void Update(){
		if(ismouseover)Debug.Log ("true desuyo-");
	

		if (Input.GetMouseButtonUp (0)) //カードが運ばれた後.全てのカードが同時に同様の分岐をすることに注意
			if(changeflag==true && destroyflag == true)
		{
			deckmake.SetCard ("death");
			exchangeflag= false;
			changeflag = false;
			changeObject = null;
		}
		
		else if(changeflag == true && exchangeflag == true){
			Debug.Log("change from " + deckmake.GetCard () + "to" + ChangeCardId);
			deckmake.SetCard (ChangeCardId);
			changeflag = false;
			exchangeflag = false;
			changeObject = null;
			
			
		}
		
		else if (changeflag == true&&changeObject!=null) {
			deckmake.SetCard (CardId);
			changeflag = false;
			exchangeflag = false;
			changeObject = null;
		}
		else
			
			
		{
			exchangeflag = false;
			destroyflag = false;
			changeObject = null;
		}

        if (cardTexture == null && CardId!= null)
            cardTexture = Singleton<pics>.instance.getTexture(CardId);

	}
	private bool ismouseover;

	void OnGUI(){
        if (ismouseover && /*これは代わり*/CardInformationForShow != ""/* && infomations != null*/)
        {
            GUI.Label(new Rect(0, Screen.height / 2, 250, 1000), CardInformationForShow);
            //			Destroy (showcard);
            //			showcard = (GameObject)Instantiate (Resources.Load ("Prefab/"+CardId), new Vector3(-8f,3f,-7f), transform.rotation);

            if (cardTexture != null)
            {
                Vector3 v = Camera.main.WorldToScreenPoint(new Vector3(-8f, 3f, -7f));
                float cardHeight = Screen.height * 0.35f;
                float cardWidth = cardHeight * 175 / 244;

                Rect rect = new Rect(v.x - cardWidth / 2, Screen.height - v.y - cardHeight / 2, cardWidth, cardHeight);
                GUI.DrawTexture(rect, cardTexture);
            }
        }
//		else Destroy (showcard);
		
		
		//追加
		if(cardTexture != null){
			Vector3 v = Camera.main.WorldToScreenPoint( transform.position );
			
			Vector3 v2 = new Vector3( transform.position.x + 3.5f, transform.position.y, transform.position.z);
			v2 = Camera.main.WorldToScreenPoint( v2 );
			
			float cardHeight= (v2.x - v.x);
			float cardWidth=cardHeight*175/244;
			
			Rect rect = new Rect (v.x - cardWidth / 2, Screen.height - v.y - cardHeight / 2, cardWidth, cardHeight);
			GUI.DrawTexture( rect, cardTexture); 
		}
	}
	
	void OnMouseEnter() {
		SetInformation ();
//		infomations=(TextAsset)Resources.Load(CardId.Split ('-')[0] + "/" +CardId + "data");
		ismouseover = true;
	}
	
	
	void OnMouseExit() {
//		infomations = null;
		CardInformationForShow = "";
		ismouseover = false;
		Debug.Log ("false ni narimasita");
	}

	
	public bool Getismouseover(){
		return ismouseover;
	}



	void SetInformation(){
        CardInformationForShow = Singleton<DataToString>.instance.SerialNumToString(CardId) ;
/*		cardstatus cds = new cardstatus(CardId);


		CardInformationForShow +=("カード名：" + cds.cardname + "\r\n");
		色 iro = (色)cds.cardColor;
		CardInformationForShow += ("色：" + iro + "\r\n");
		カードタイプ cdtp = (カードタイプ)cds.type;
		CardInformationForShow += ("カードタイプ：" + cdtp + "\r\n");
		if(cds.lrigLimit>=0){
			限定条件 gtjk = (限定条件)cds.lrigLimit;
			CardInformationForShow += ("限定条件：" + gtjk + "\r\n");
		}
		if (cds.growCost [0] >= 0) {
			CardInformationForShow += ("グロウコスト：（無）" + cds.growCost[0] +"（白）" + cds.growCost[1] + "（赤）" + cds.growCost[2] +"\r\n");
			CardInformationForShow += ("      　　　      （青）" + cds.growCost[3] +"（緑）" + cds.growCost[4]+"（黒）" + cds.growCost[5] +"\r\n");
		}
		if (cds.Cost [0] >= 0) {
			CardInformationForShow += ("コスト：（無）" + cds.Cost[0] +"（白）" + cds.Cost[1] + "（赤）" + cds.Cost[2] +"\r\n");
			CardInformationForShow += ("　　　　（青）" + cds.Cost[3] +"（緑）" + cds.Cost[4]+"（黒）" + cds.Cost[5] +"\r\n");
		}
		if(cds.cardClass[0]>=0){
			クラス cls = (クラス)(cds.cardClass[0]*10+cds.cardClass[1]);
			CardInformationForShow += ("クラス：" + cls + "\r\n");
		}
		if(cds.lrigType>=0){
			ルリグタイプ lgtp = (ルリグタイプ)cds.lrigType;
			CardInformationForShow += ("ルリグタイプ：" + lgtp + "\r\n");
		}
		if(cds.level>=0)CardInformationForShow+=("レベル：" +cds.level + "\r\n");
		if(cds.power>=0)CardInformationForShow +=("パワー：" +cds.power+ "\r\n");

		CardInformationForShow += cds.cardtext;*/
	}

	public int getpower(){
		cardstatus cds = new cardstatus (CardId);
		int popopower = cds.power;
		return popopower;
		}

	public int getlevel(){
		cardstatus cds = new cardstatus (CardId);
		int levelll = cds.level;
		return levelll;
	}

	public int getcolor(){
		cardstatus cds = new cardstatus (CardId);
		int levelll = cds.cardColor;
		return levelll;
	}


	public void DestroyCard(){
//		Destroy(card);
		cardtype = -1;
		cardTexture = null;
//		infomations = null;
		CardId = "";
	}
	
	public void SetCard(string cardId){//カードのIDを入れるとそのIDとカードを保持
		if (cardId == ""||cardId == "death"||cardId == "Destroy") {
			DestroyCard();
			CardId = "";
			return;
		}

		isLifeBurst = false;
		Destroy(card);
		CardId = cardId;
		//comment out
/*		if (Resources.Load ("Prefab/" + cardId) == null) {
			card = (GameObject)Instantiate (Resources.Load ("Prefab/UnKnown"), transform.position, transform.rotation);
		}
		else card = (GameObject)Instantiate (Resources.Load ("Prefab/"+cardId), transform.position, transform.rotation);
		card.transform.parent = transform;
 */
		//追加
/*		if (loadTexture(cardId) == null) {
			cardTexture = (Texture)Resources.Load ("BlackCard");
		}
		else*/
        cardTexture = loadTexture(cardId);		

		SetSomeCardData (cardId);
	}
	
	//追加
	Texture loadTexture(string Id){
        return Singleton<pics>.instance.getTexture(Id);
//      string[] s = Id.Split('-');
//      return (Texture)Resources.Load (s[0] + "/" + Id);
	}


    void SetSomeCardData(string cardId)
    {
        TextAsset textAsset = Singleton<DataToString>.instance.getResourceData(cardId);//(TextAsset)Resources.Load(cardId.Split('-')[0] + "/" + cardId + "data");
        if (textAsset == null)
            return;
 
        string[] s = textAsset.text.Split(' ', '\n');

        for (int ii = 0; ii < s.Length; ii++)
        {
            if (s[ii].IndexOf("#BurstIcon") >= 0 && s[ii + 1].IndexOf("True") >= 0)
                isLifeBurst = true;

            if (s[ii].IndexOf("#Type") >= 0)
            {
                if (s[ii + 1].IndexOf("0") >= 0 || s[ii + 1].IndexOf("1") >= 0 || s[ii + 1].IndexOf("4") >= 0) isLrig = true;
                else isLrig = false;

                try
                {
                    cardtype = int.Parse(s[ii + 1]);
                }
                catch { cardtype = -1; }
            }
        }
    }
	
	public void SetIsBurst(bool b){
		isLifeBurst = b;
	}
	public bool IsDeck(){
		return isDeck;
	}
	public bool IsLifeBurst(){
		return isLifeBurst;
	}
	public bool IsLrig(){
		return isLrig;
	}
	public void SetIsDeck(bool IS){
		isDeck = IS;
	}
	public void SetIsLrig(bool IS){
		isLrig = IS;
	}
	public int GetType(){
		return cardtype;
	}
	

	void OnTriggerStay2D (Collider2D c)
	{

		deckmake = c.gameObject.GetComponent<DeckMake> ();
		
		if (deckmake.IsDeck () && !isDeck) {//相手をかえるもの目線.今のところデッキ外のカードがデッキのカードをかえるばあいのみ
			if (transform.position.z != 0.0 && (!(isLrig ^ deckmake.IsLrig ())) && (!(isLifeBurst ^ deckmake.IsLifeBurst ()))) {
				changeObject = c.gameObject;
				changeflag = true;
				return;
			} else if (transform.position.z == 0.0) {
				changeflag = true;
				destroyflag = true;
				return;
			}
		}
		
		if (deckmake.IsDeck () && isDeck) {//相手をかえるもの目線.今のところデッキ外のカードがデッキのカードをかえるばあいのみ
			if (exchangeflag == false&& !(isLrig ^ deckmake.IsLrig ()) && !(isLifeBurst ^ deckmake.IsLifeBurst ()) ) {
				changeObject = c.gameObject;
				changeflag = true;
				exchangeflag = true;
				ChangeCardId = CardId;
				Debug.Log ("changecardid is changed to " + CardId);
				
			} 
			return;
		}
		
		exchangeflag = false;
		changeflag = false;
		destroyflag = false;
		changeObject = null;



	}
	
	
	
	void OnTriggerExit2D (Collider2D c)
	{
		changeflag = false;
		changeObject = null;
		destroyflag = false;
		exchangeflag = false;
	}
	
	
	
	public string GetCard(){//保持しているカードのIDを見る
		return CardId;
	}

}

//DataToStringにまとめた
//struct cardstatusもそっち
/* 
enum 色{無色,白,赤,青,緑,黒}
enum カードタイプ{ルリグ,アーツ,シグニ,スペル}
enum 限定条件{なし,タマ限定,花代限定,ピルルク限定,緑子限定,ウリス限定}
enum ルリグタイプ{タマ = 1,花代,ピルルク,緑子,ウリス}
enum クラス
{
    精元 = -1,
    精武_アーム = 10,
    精武_ウェポン = 11,
    精羅_鉱石 = 20,
    精羅_宝石 = 21,
    精羅_植物 = 22,
    精械_電機 = 30,
    精像_天使 = 40,
    精生_水獣 = 50,
    精生_地獣 = 51,
    精生_空獣 = 52
}*/