using UnityEngine;
using System.Collections;

public class getTextureScr : MonoBehaviour
{

    public string serial = string.Empty;
    bool setUp = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (serial != string.Empty && gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture == null && !setUp)
        {
            Texture t = Singleton<pics>.instance.getTexture(serial);
            if (t == null)
                return;

            MaterialPropertyBlock matb = new MaterialPropertyBlock();
            matb.AddTexture("_MainTex", t);

            gameObject.GetComponent<Renderer>().SetPropertyBlock(matb);

            setUp = true;
        }
    }
}
