using UnityEngine;
using System.Collections;

public class saveData {
	public saveData(){}

	public void setData(string key, string s){
		PlayerPrefs.SetString( key, ToBase64(s) );
	}

	public void setData(string key, int x){
		PlayerPrefs.SetInt( key, x );
	}

	public void setData(string key, float f){
		PlayerPrefs.SetFloat( key, f );
	}

	public string getData(string key, string errorValue){
		if(!PlayerPrefs.HasKey(key))
		   return errorValue;

		return FromBase64(PlayerPrefs.GetString (key));
	}

	public int getData(string key, int errorValue){
		if(!PlayerPrefs.HasKey(key))
			return errorValue;

		return PlayerPrefs.GetInt(key);
	}

	public float getData(string key ,float errorValue){
		if(!PlayerPrefs.HasKey(key))
			return errorValue;

		return PlayerPrefs.GetFloat(key);
	}

	string FromBase64 (string s){
		return System.Text.UTF8Encoding.UTF8.GetString (System.Convert.FromBase64String (s));
	}
	
	string ToBase64 (string s){
		return System.Convert.ToBase64String (System.Text.UTF8Encoding.UTF8.GetBytes (s));
	}
	
}
