using UnityEngine;
using System.Collections;

public class DataToString{

    string CardInformationForShow = "";
    string MainSerialNum = "";

    char kugiri='：';

    public DataToString() { }


    public string SerialNumToString(string serialNum)
    {
        if (serialNum == MainSerialNum || serialNum == "" || serialNum == string.Empty)
            return CardInformationForShow;

        MainSerialNum = serialNum;

        CardInformationForShow = "";
        cardstatus cds = new cardstatus(serialNum);

        CardInformationForShow += ("<<" + cds.cardname + ">>\r\n");

        cardColorInfo iro = (cardColorInfo)cds.cardColor;
        CardInformationForShow += ("[" + iro + "] ");

        cardTypeInfo cdtp = (cardTypeInfo)cds.type;
        CardInformationForShow += ("" + cdtp + "\r\n");

        if (cds.lrigLimit >= 1)
        {
            LrigTypeInfo gtjk = (LrigTypeInfo)cds.lrigLimit;
            CardInformationForShow += (gtjk + "限定\r\n");
        }

        if (cds.type == (int)cardTypeInfo.ルリグ)
        {
            CardInformationForShow += "グロウコスト" + CostToString(cds.growCost) + "\r\n";
//            CardInformationForShow += ("グロウコスト：（無）" + cds.growCost[0] + "（白）" + cds.growCost[1] + "（赤）" + cds.growCost[2] + "\r\n");
//            CardInformationForShow += ("      　　　      （青）" + cds.growCost[3] + "（緑）" + cds.growCost[4] + "（黒）" + cds.growCost[5] + "\r\n");
        }

        if (cds.type == (int)cardTypeInfo.アーツ || cds.type == (int)cardTypeInfo.スペル)
        {
            CardInformationForShow += "コスト" + CostToString(cds.Cost) + "\r\n";
//            CardInformationForShow += ("コスト：（無）" + cds.Cost[0] + "（白）" + cds.Cost[1] + "（赤）" + cds.Cost[2] + "\r\n");
//            CardInformationForShow += ("　　　　（青）" + cds.Cost[3] + "（緑）" + cds.Cost[4] + "（黒）" + cds.Cost[5] + "\r\n");
        }

        if (cds.cardClass[0] >= 0)
        {
            CardInformationForShow += ("クラス " + classToString(cds.cardClass[0], cds.cardClass[1]));

            if (cds.secondClass[0] >= 0)
                CardInformationForShow += " / " + classToString(cds.secondClass[0], cds.secondClass[1]).Split(kugiri)[1];

            CardInformationForShow += "\r\n";
        }

        if (cds.lrigType >= 0)
        {
            LrigTypeInfo lgtp = (LrigTypeInfo)cds.lrigType;
            CardInformationForShow += ("ルリグタイプ " + lgtp + "\r\n");
        }

        if (cds.level >= 0) 
            CardInformationForShow += ("レベル " + cds.level + "\r\n");

        if (cds.Limit >= 0)
            CardInformationForShow += ("リミット " + cds.Limit + "\r\n");

        if (cds.power >= 0)
            CardInformationForShow += ("パワー " + cds.power + "\r\n");

        CardInformationForShow += cds.cardtext;


        return CardInformationForShow;
    }

    string classToString(int c0, int c1)
    {
        cardClassInfo cls = (cardClassInfo)(c0 * 10 + c1);
        return cls.ToString().Replace('_', kugiri);
    }

    string CostToString(colorCostArry x)
    {
        int num = 0;
        string reString = "【";//コスト: "

        for (int i = 0; i < x.Length(); i++)
        {
            if (x[i] > 0)
            {
                num++;
                reString += (cardColorInfo)i + "×" + x[i] + " ";
            }
        }

        if (num == 0)
            reString += "×0";

        reString += "】\n";

        return reString;
    }

    public TextAsset getResourceData(string serialNum)
    {
        return (TextAsset)Resources.Load("CardData/" + serialNum.Split('-')[0] + "/" + serialNum + "data");
    }
}

public enum cardColorInfo 
{
    無色,
    白,
    赤,
    青,
    緑,
    黒,
}

public enum cardTypeInfo
{
    ルリグ,
    アーツ,
    シグニ,
    スペル,
    レゾナ,
}

public enum LrigTypeInfo 
{
    なし,
    タマ,
    花代,
    ピルルク, 
    緑子,
    ウリス,
    エルドラ,
    ユヅキ,
    ウムル,
    イオナ,
    リメンバ,
    ミルルン,
    アン,
    タウィル,
    サシェ,
    ミュウ
}

public enum cardClassInfo
{
    精元 = -1,

    精武_アーム = 10,
    精武_ウェポン = 11,
    精武_毒牙 = 12,

    精羅_鉱石 = 20,
    精羅_宝石 = 21,
    精羅_植物 = 22,
    精羅_原子 = 23,
    精羅_宇宙 = 24,

    精械_電機 = 30,
    精械_古代兵器 = 31,
    精械_迷宮 = 32,

    精像_天使 = 40,
    精像_悪魔 = 41,
    精像_美巧 = 42,

    精生_水獣 = 50,
    精生_地獣 = 51,
    精生_空獣 = 52,
    精生_龍獣 = 53,
    精生_凶蟲 = 54,

    地獣または空獣 = 5152,
    鉱石または宝石 = 2021,
}

public class cardstatus
{//カードｉｄとともにインスタンス化するとそのカードの性質が一通りどうにかなる
    public int type = -1;//ルリグ・スペル・シグニ・アーツの見分け
    public int level=-1;
    public colorCostArry growCost = new colorCostArry(cardColorInfo.無色, 0);
    public int cardColor=-1;

    public int lrigType = -1;
    public int lrigType_2 = -1;

    public colorCostArry Cost = new colorCostArry(cardColorInfo.無色, 0);
    public int Limit = -1;
    public int lrigLimit = -1;
    public int lrigLimit_2 = 0;
    public int power=-1;
    public bool BurstIcon=false;
    public int[] cardClass = new int[2] { -1, -1 };
    public int[] secondClass = new int[2] { -1, -1 };
    public string cardtext="";
    public string cardname="";

    public string crossRightName = "";
    public string crossLeftName = "";

    public cardstatus()
    {
    }
    public cardstatus(string cardId)
    {
        TextAsset textAsset = Singleton<DataToString>.instance.getResourceData(cardId);

       if (textAsset == null)
           return;
        
        string[] s = textAsset.text.Replace("\r","").Split(' ', '\n');
        cardname = "";
        int textindex = 0;

        for (int ii = 0; ii < s.Length; ii++)
        {
            if (s[ii].IndexOf("BurstIcon") >= 0 && s[ii + 1].IndexOf("True") >= 0) BurstIcon = true;
            else if (s[ii].IndexOf("Name") >= 0) cardname = s[ii + 1];
            else if (s[ii].IndexOf("LrigType2") >= 0) lrigType_2 = int.Parse(s[ii + 1]);
            else if (s[ii].IndexOf("LrigType") >= 0) lrigType = int.Parse(s[ii + 1]);
            else if (s[ii].IndexOf("Type") >= 0) type = int.Parse(s[ii + 1]);
            else if (s[ii].IndexOf("Level") >= 0) level = int.Parse(s[ii + 1]);
            else if (s[ii].IndexOf("GrowCost") >= 0)
            {
                string[] gcs = s[ii + 1].Split('/');
//                Debug.Log(gcs.Length.ToString() + "    " + growCost.Length.ToString());
                for (int iii = 0; iii < growCost.Length() && iii < gcs.Length; iii++)
                {
                    growCost[iii] = int.Parse(gcs[iii]);
//                    Debug.Log(iii.ToString() + "      " + gcs[iii]);
                }
            }
            else if (s[ii].IndexOf("Color") >= 0) cardColor = int.Parse(s[ii + 1]);
            else if (s[ii].IndexOf("Cost") >= 0)
            {
                string[] gcs = s[ii + 1].Split(' ', '/');
                for (int iii = 0; iii < growCost.Length() && iii < gcs.Length; iii++)
                {
                    Cost[iii] = int.Parse(gcs[iii]);
                }
            }
            else if (s[ii].IndexOf("LrigLimit2") >= 0) lrigLimit_2 = int.Parse(s[ii + 1]);
            else if (s[ii].IndexOf("LrigLimit") >= 0) lrigLimit = int.Parse(s[ii + 1]);
            else if (s[ii].IndexOf("Limit") >= 0) Limit = int.Parse(s[ii + 1]);
            else if (s[ii].IndexOf("Power") >= 0) power = int.Parse(s[ii + 1]);
            else if (s[ii].IndexOf("secondClass") >= 0)
            {
                string[] gcs = s[ii + 1].Split(' ', '/', ':');
                for (int iii = 0; iii < secondClass.Length && iii < gcs.Length; iii++)
                {
                    secondClass[iii] = int.Parse(gcs[iii]);
                }

            }
            else if (s[ii].IndexOf("Class") >= 0)
            {
                string[] gcs = s[ii + 1].Split(' ', '/', ':');
                for (int iii = 0; iii < cardClass.Length && iii < gcs.Length; iii++)
                {
                    cardClass[iii] = int.Parse(gcs[iii]);
                }

            }
            else if (s[ii].IndexOf("Text") >= 0) textindex = ii;
        }
        if (textindex != 0)
        {
            for (int ii = textindex + 1; ii < s.Length; ii++)
            {
                if (s[ii].Contains("【クロス】"))
                {
                    int start = s[ii].IndexOf("《");
                    int goal = s[ii].IndexOf("》");

                    if (start + 1 >= 0 && goal - start - 1 > 0)
                    {
                        string cName = s[ii].Substring(start + 1, goal - start - 1);
                        if (s[ii].Replace(cName,"").Contains("左"))
                            crossRightName = cName;
                        else
                            crossLeftName = cName;

                    }
                }

                cardtext += s[ii].Replace("☆", "【ライフバースト】");
                cardtext += "\r\n";
            }
        }

    }
};