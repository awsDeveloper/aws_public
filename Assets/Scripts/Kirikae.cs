using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;




public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
{
	
	
	public TrustAllCertificatePolicy() { }
	public bool CheckValidationResult(System.Net.ServicePoint sp,
	                                  System.Security.Cryptography.X509Certificates.X509Certificate cert,
	                                  System.Net.WebRequest req,
	                                  int problem)
	{
		return true;
	}
}

public class Kirikae : MonoBehaviour {
	
	public const string awsversion = "1.9.2";
	public const string versioninfotxt = "https://www.dropbox.com/s/sg9x2zj77po2eyc/version.txt?dl=1";




	void OnGUI(){
		
		
		
		
		
		if(GUI.Button (new Rect(0,0,100,100),"Deck Making"))
			Application.LoadLevel("DeckMakeScene");
		
		if(GUI.Button (new Rect(100,0,100,100),"Duel Mode"))
			Application.LoadLevel("mainScene");
		GUI.Label (new Rect(0,200,100,30),"ver. "+awsversion);
		
		if (updateflag) {//アップデートができる場合に表示されるアレコレ
			GUI.Label (new Rect(0,300,200,50),"アプリケーションをアップデートできます");
			GUI.Label (new Rect(0,350,200,50),"アップデート中、アプリケーションが再起動されます");
			if(GUI.Button (new Rect(0,400,100,50),"アップデート"))
				appupdate ();
			
		}
		else if(existFlag)
			GUI.Label (new Rect(0,300,200,50),"アプリケーションは最新です");

		else //デバック用に自分のバージョンがなかったとき表示するようにした
			GUI.Label (new Rect(0,300,200,50),"存在しないバージョンです");
			
		
	}//アップデート用ＧＵＩここまで

	static bool updateflag = false;//確認を一回だけにしたのでstatic化
	string aaa = "a";

	static bool updateCheck=false;//アップデートを確認するのは起動ごとに一回にした
	static bool existFlag=false;//自分のバージョンの存在が確認できたかのフラグ
	
	
	void appupdate(){
		
		
		
		UnityEngine.Debug.Log ("114");
		string st = awsversion;
		
		
		string[] reader;
		string[] splitwords = {"\r\n"};
		WebClient client = new WebClient ();
		client.Credentials = CredentialCache.DefaultCredentials;
		client.Headers.Add (HttpRequestHeader.UserAgent, "anything");
		System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy ();
		

		try {
			UnityEngine.Debug.Log ("114");
			
			string versionst = client.DownloadString (versioninfotxt);
			aaa = "aaa";
			UnityEngine.Debug.Log ("114");
			
			reader = versionst.Split (splitwords, StringSplitOptions.None);
			UnityEngine.Debug.Log ("114");
			
		} catch (Exception e) {
			UnityEngine.Debug.Log (e.ToString ());
			
			return;
		}
		//StreamReader reader = new StreamReader("./version.txt");
		UnityEngine.Debug.Log ("114");
		
		int readerindex = 0;
		for (;; readerindex++) {
			
			if (readerindex >= reader.Length || reader [readerindex].IndexOf (st) >= 0)
				break;
		}

		readerindex++;
		
		if (readerindex < reader.Length) {
			string[] url = reader [readerindex].Split (' ');
			
			try {
				if (url.Length != 4)
					throw new ArgumentException ();
			} catch (ArgumentException) {
				return;
			}
			
			
			
			
			try {
				client.DownloadFile (url [3], "./AWSkousin.exe");
				
				
			} catch (Exception e) {
				UnityEngine.Debug.Log (e.ToString());
				return;
			}
			//							File.Move(System.Windows.Forms.Application.ExecutablePath, "./AWS.exe");
			Process.Start ("AWSkousin.exe",url [1] + " " + url [2] + " \"" +System.Windows.Forms.Application.ExecutablePath+"\"" );
			
			
			
			Application.Quit ();
			
			
		} else {
			
			try{
				File.Delete("./u.gbg");
				
			}
			catch(Exception){
			}
		}
		
		
		
		
		
		
		
		
		
		
		
	}
	
	
	
	// Use this for initialization
	void Start () {

		//アップデートチェック済なら何もしない unity editor でも何もしない
		if(updateCheck || Application.platform == RuntimePlatform.WindowsEditor)
			return;

		
		UnityEngine.Debug.Log ("114");
		string st = awsversion;
		
		
		string[] reader;
		string[] splitwords = {"\r\n"};
		WebClient client = new WebClient ();
		client.Credentials = CredentialCache.DefaultCredentials;
		client.Headers.Add (HttpRequestHeader.UserAgent, "anything");
		System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy ();
		
		
		try {
			UnityEngine.Debug.Log ("114");
			
			string versionst = client.DownloadString (versioninfotxt);
			aaa = "aaa";
			UnityEngine.Debug.Log ("114");
			
			reader = versionst.Split (splitwords, StringSplitOptions.None);
			UnityEngine.Debug.Log ("114");
			
		} catch (Exception e) {
			UnityEngine.Debug.Log (e.ToString ());
			
			return;
		}
		//StreamReader reader = new StreamReader("./version.txt");
		UnityEngine.Debug.Log ("114");
		
		int readerindex = 0;
		for (;; readerindex++) {
			
			if (readerindex >= reader.Length || reader [readerindex].IndexOf (st) >= 0)
			{
				//自分のバージョンがみつかった判定
				if( !(readerindex >= reader.Length) )
					existFlag=true;

				break;
			}
		}
		
		readerindex++;

		//ここに来たらアップデートのチェックが済んでいるとする
		updateCheck=true;

		if (readerindex < reader.Length) {
			string[] url = reader [readerindex].Split (' ');
			
			try {
				if (url.Length != 4)
					throw new ArgumentException ();
			} catch (ArgumentException) {
				return;
			}
			
			
			updateflag = true;
			return;
			
			
		} else {
			
			try{
				File.Delete("./u.gbg");
				
			}
			catch(Exception){
			}
		}
		
		
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
