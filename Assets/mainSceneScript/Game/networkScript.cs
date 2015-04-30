using UnityEngine;
using System.Collections;

public class networkScript : Photon.MonoBehaviour {
	
	public GameObject GameManager;
	public bool connected=false;
	public bool receivingReplay=false;

	private DeckScript GameSrc;
	bool goRoomFlag=false;

	bool isMaster=false;

	const string masterName="master";

	bool goingMasterRoom=false;
	
	string[] option = {"isMaster"};
	ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable(){ {"isMaster" , 0 }};
	ExitGames.Client.Photon.Hashtable MasterHash = new ExitGames.Client.Photon.Hashtable(){ {"isMaster" , 1 }};

	// Use this for initialization
	void Start () {
		if(Application.platform==RuntimePlatform.WindowsEditor)
			isMaster=true;

		PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
		setManager();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void connetServer(){
		goRoomFlag=true;
		if(!connected)PhotonNetwork.ConnectUsingSettings("0.1");
		else OnJoinedLobby();
	}
	
	public void DisConnetServer(){
		PhotonNetwork.Disconnect();
		connected=false;
	}

	//コールバック
    void OnJoinedLobby() {
		Debug.Log("in Lobby");
		
		connected=true;
  
		//ランダムにルームへ参加
        if(goRoomFlag && !isMaster){
			enterRandomRoom();
		}
		else if(goingMasterRoom)
			PhotonNetwork.JoinRandomRoom(MasterHash,2);
    }
	
    //ルーム参加失敗時のコールバック
    void OnPhotonRandomJoinFailed() {
        Debug.Log("ルームへの参加に失敗しました");

		if(goingMasterRoom){
			goingMasterRoom = false;

			if(GameSrc.wasServerAndClient)
				DisConnetServer();
		}
		else{
			PhotonNetwork.CreateRoom (
				"", // ユニークな部屋名
				true, // リストに表示可能かどうか
				true, // 入場可能か
				2,
				hash,
				option); // 最大人数
			
			GameSrc.PhotonAfterCreatRoom();
		}
    }
 
    //ルーム参加成功時のコールバック
    void OnJoinedRoom() {
        Debug.Log("ルームへの参加に成功しました");

		if(PhotonNetwork.room.name != masterName )
			GameSrc.PhotonAfterJoined();

		else {
			goingMasterRoom = false;

			if(!isMaster)
				GameSrc.sendReplay();

			if(GameSrc.wasServerAndClient)
				DisConnetServer();
		}
    }

	
	public void NetChat(string msg, bool isSvr){
		photonView.RPC("chatMessage", PhotonTargets.All, new object[] { msg, isSvr } );
	}
	
	public void NetRemove(){
	    photonView.RPC("MessageRemove", PhotonTargets.All, new object[] {} );
	}
	

    [RPC]
	void chatMessage(string msg,bool isSvr)
    {
		GameSrc.AddMessege(msg,isSvr);
    }
	
    [RPC]
    void MessageRemove()
    {
        // ローカルの配列のメッセージをひとつ消す
		GameSrc.RemoveMessage();
    }

	[RPC]
	void thinkChange()
	{
		GameSrc.thinkChange();
	}

	[RPC]
	void timeSynchro(int time)
	{
		GameSrc.timeSynchro(time);
	}

	[RPC]
	void addSentList(string s)
	{
		GameSrc.addSentList(s);
		receivingReplay=true;
	}

	[RPC]
	void addReceivedList(string s)
	{
		GameSrc.addReceivedList(s);
		receivingReplay=true;
	}



	public void rpcAll(string funcNane, object[] arry)
	{
		if(connected)
			photonView.RPC(funcNane, PhotonTargets.All, arry );
		else
			GameSrc.GetComponent<NetworkView>().RPC(funcNane,RPCMode.All, arry);
	}

	public void rpcOthers(string funcNane, object[] arry)
	{
		if(connected)
			photonView.RPC(funcNane, PhotonTargets.Others, arry );
		else
			GameSrc.GetComponent<NetworkView>().RPC(funcNane,RPCMode.Others, arry);
	}

	void setManager(){
		GameManager=(GameObject)Instantiate(Resources.Load("manager"));
		GameSrc=GameManager.GetComponent<DeckScript>();
		GameSrc.NetworkManager=this.gameObject;		
	}
	
	public void resetManager(){
		if(goingMasterRoom)
			return;

        GameSrc.resetShowCard();
		Destroy(GameManager);
		GameSrc=null;
		
		if(connected){
			goRoomFlag=false;
			if(PhotonNetwork.inRoom)
				PhotonNetwork.LeaveRoom();
		}

		setManager();
	}

	public void enterMasterRoom(){
		goRoomFlag=false;
		goingMasterRoom=true;

		if(!connected)
			PhotonNetwork.ConnectUsingSettings("0.1");

		else if(PhotonNetwork.inRoom)
			PhotonNetwork.LeaveRoom();

	}
	
 
	void OnGUI() {
		//マスターだけ
		if(!isMaster)
			return;

        //サーバーとの接続状態をGUIへ表示
		string s=PhotonNetwork.connectionStateDetailed.ToString()+"\n";
		s += "count of players = "+PhotonNetwork.countOfPlayersInRooms + "\n";
		GUI.Label(new Rect(Screen.width/2,10,Screen.width/5,80),s);

		if(PhotonNetwork.insideLobby){

			Rect r=new Rect(Screen.width/2+Screen.width/5,10,Screen.width/11,Screen.width/11/2);
			if(GUI.Button(r,"Random Room"))
				enterRandomRoom();

			Rect r2=new Rect(Screen.width/2+Screen.width/5 + Screen.width/11 + 10,10,Screen.width/11,Screen.width/11/2);

			if(GUI.Button(r2,"Master Room")){
				PhotonNetwork.CreateRoom (masterName,true,true,2,MasterHash,option);
			}

		}

	}	

	void enterRandomRoom()
	{
		PhotonNetwork.JoinRandomRoom(hash,2);

	}
}
