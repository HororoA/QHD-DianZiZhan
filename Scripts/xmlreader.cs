using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System;


public class xmlreader : MonoBehaviour
{
    //天赋类
    public static tianfu[] tianfus = new tianfu[42];
    public static Donghua[] donghuas = new Donghua[42];
    //查找用字典
    public static Dictionary<string, int> LoNuDict = new Dictionary<string, int>();
    public static Dictionary<string, int> CoNuDict = new Dictionary<string, int>();
    public static Dictionary<string, tianfu> Combian = new Dictionary<string, tianfu>();
    public static Dictionary<tianfu, string> KeZhi = new Dictionary<tianfu, string>();



    public Sprite S1;
    public static Sprite S2;

    public static List<tianfu> BeiKezhi = new List<tianfu>();

    void Awake()
    {
        for (int i = 0; i < tianfus.Length; i++)
        {
            tianfus[i] = new tianfu();
            donghuas[i] = new Donghua();
            donghuas[i].T = tianfus[i];
        }
        LoadXml();
        S2 = S1;

        Dictionary<tianfu, string>.KeyCollection k = KeZhi.Keys;
        foreach (tianfu t in k)
        {
            if (KeZhi[t] != "无")
            {
                if (KeZhi[t] != "-")
                {
                    BeiKezhi.Add(t);
                }
            }
        }
        for (int i = 0; i < BeiKezhi.Count; i++)
        {
            //LoadingxulieB(BeiKezhi[i], 1);
        }

        //Loadingxulie();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (HoleCtrl.IsStep2)
        {
            Loadtianfu();
        }
    }

    void LoadXml()
    {
        //创建xml文档
        XmlDocument xml = new XmlDocument();
        XmlReaderSettings set = new XmlReaderSettings();
        set.IgnoreComments = true;//这个设置是忽略xml注释文档的影响。有时候注释会影响到xml的读取
        //xml.Load(XmlReader.Create((Application.persistentDataPath + "/data.xml"), set));
        xml.Load(XmlReader.Create(("C:/data.xml"), set));
        //得到objects节点下的所有子节点
        XmlNodeList xmlNodeList = xml.SelectSingleNode("root").ChildNodes;
        //遍历所有子节点
        int i = 0;
        foreach (XmlElement xl1 in xmlNodeList)
        {
            tianfus[i].Name = Huanhang(xl1.GetAttribute("Name"));
            //Debug.Log(tianfus[i].Name);
            foreach (XmlElement xl2 in xl1.ChildNodes)
            {
                switch (xl2.Name)
                {
                    case "Info1":
                        tianfus[i].Info = xl2.InnerText;
                        break;
                    case "Info2":
                        tianfus[i].Info2 = xl2.InnerText;
                        break;
                    case "Info3":
                        tianfus[i].Info3 = xl2.InnerText;
                        break;
                    case "Info4":
                        //tianfus[i].Info4 = xl2.InnerText;
                        tianfus[i].Info4 = Huanhang(xl2.InnerText);
                        break;
                    case "Info5":
                        tianfus[i].Info5 = xl2.InnerText;
                        break;
                    case "Case":
                        tianfus[i].Case = xl2.InnerText;
                        break;
                    case "LocateNumber":
                        tianfus[i].Locate = xl2.InnerText;
                        break;
                    case "CombainNum":
                        //tianfus[i].CombainNum = xl2.InnerText;
                        tianfus[i].CombainNum = Huanhang(xl2.InnerText);
                        break;
                    case "Step":
                        tianfus[i].step = int.Parse(xl2.InnerText);
                        break;
                    case "AorD":
                        tianfus[i].AorD = xl2.InnerText;
                        break;
                    case "Length":
                        tianfus[i].Slength = int.Parse(xl2.InnerText);
                        break;
                }
            }
            tianfus[i].Number = i;
            LoNuDict.Add(tianfus[i].Locate, i);
            CoNuDict.Add(tianfus[i].Name, i);
            if (tianfus[i].CombainNum != "") Combian.Add(tianfus[i].CombainNum, tianfus[i]);
            if (tianfus[i].Info4 != "") KeZhi.Add(tianfus[i], tianfus[i].Info4);
            i++;
        }
    }

    /// <summary>
    /// 天赋树读取
    /// </summary>
    void Loadtianfu()
    {
        for (int i = 0; i < tianfus.Length; i++)
        {
            try
            {
                GameObject.Find((tianfus[i].Locate)).GetComponentInChildren<UnityEngine.UI.Text>().text = tianfus[i].Name;
            }
            catch (Exception e)
            {
                //Debug.Log(e);
            }
            //Debug.Log("Name:" + tianfus[i].Name + ",Case;" + tianfus[i].Case + ",Info:" + tianfus[i].Info + ",Number:" + tianfus[i].Number + ",Locate:" + tianfus[i].Locate);
        }
    }

    string Huanhang(string s)
    {
        string b = string.Empty;
        if (s.Length == 6)
        {
            //Debug.Log(b);
            b = s.Insert(4, "\n");
            return b;
        }
        if (s.Length == 8)
        {
            //Debug.Log(b);
            b = s.Insert(4, "\n");
            return b;
        }
        else return s;
    }

    public static void Loadingxulie(tianfu T, int i)
    {
        //Debug.Log(T.Name + "," + T.Slength + "," + T.Number);
        for (int j = 0; j < T.Slength; j++)
        {
            if (i == 1)
            {
                T.S.Add(Resources.Load(T.Locate + "/" + i + "/" + j, typeof(Sprite)) as Sprite);
            }
            else
            {
                T.S2.Add(Resources.Load(T.Locate + "/" + i + "/" + j, typeof(Sprite)) as Sprite);
            }
        }
    }

    public static void LoadingxulieB(tianfu T, int i)
    {
        //Debug.Log(T.Name + "," + T.Slength + "," + T.Number);
        for (int j = 0; j < T.Slength; j++)
        {
            if (i == 1)
            {
                T.S.Add(Resources.Load("Spe/" + T.Locate + "/" + i + "/" + j, typeof(Sprite)) as Sprite);
                //Debug.Log("Spe/" + T.Locate + "/" + i + "/" + j);
            }
            else
            {
                T.S2.Add(Resources.Load("Spe/" + T.Locate + "/" + i + "/" + j, typeof(Sprite)) as Sprite);
            }
        }
    }

    public static void jiazhuangLoadingxulie(tianfu T, int i)
    {
        //Debug.Log(T.Name + "," + T.Slength + "," + T.Number);
        for (int j = 0; j < T.Slength; j++)
        {
            if (i == 1)
            {
                T.S.Add(S2);
            }
            else
            {
                T.S2.Add(S2);
            }
        }
    }

}