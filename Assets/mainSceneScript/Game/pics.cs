using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pics{

	Dictionary<string, draw> textures = new Dictionary<string, draw>();

	MonoBehaviour mono;
	GameObject monoObj=null;

    const int TEXTURES_MAX = 200;

	class draw
	{
		public Texture tex = null;
		public int count=0;


		public draw(){}

		public void setMaterial(Texture t)
		{
			tex = t;
			count=0;
		}
	}

	public pics(){
		monoObj = new GameObject("pics");
		monoObj.AddComponent<MonoBehaviour>();

		mono = monoObj.GetComponent<MonoBehaviour>();
	}

	public Texture getTexture(string key)
	{
		if(textures.ContainsKey(key))
		{
			Texture tex = textures[key].tex;

			return tex;
		}
		else 
		{
			if(mono == null)
			{
				monoObj = new GameObject("pics");
				monoObj.AddComponent<MonoBehaviour>();
			
				mono = monoObj.GetComponent<MonoBehaviour>();
			}
		
			mono.StartCoroutine(setTexture(key));
		}

		return null;
	}

	IEnumerator setTexture(string s)
	{
		textures.Add(s, new draw() );


		string path = creatPath(s);

		string[] dataPath=Application.dataPath.Split('/');
		string newDataPath = string.Empty;

		for (int i = 0; i < dataPath.Length - 1; i++)
			newDataPath +=dataPath[i]+"/";

		WWW loader = new WWW("file://"+newDataPath+"pics/"+path+".jpg");
		yield return loader;

        if (loader.error == null)
            textures[s].setMaterial(loader.texture);
        else
            textures.Remove(s);
	}

	void sippai(string s)
	{
		System.Windows.Forms.MessageBox.Show(s,
		                                     "AWS",
		                                     System.Windows.Forms.MessageBoxButtons.OK,
		                                     System.Windows.Forms.MessageBoxIcon.Information);

	}

	public string creatPath(string s)
	{
		if(s.Contains("-"))
		{
			string[] array = s.Split('-');

			return array[0]+"/"+s;
		}

		return "others/"+s;
	}
}
