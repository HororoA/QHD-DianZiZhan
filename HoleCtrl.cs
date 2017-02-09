using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HoleCtrl : MonoBehaviour
{
    public GameObject[] Steps;
    public GameObject[] Step2s;
    public Sprite[] IsTouched;
    //public string[] Infos;
    public int points;
    private Color Red = new Color(255, 0, 0, 255);
    private Color White = new Color(255, 255, 255, 255);
    private Color Green = new Color(0, 255, 0, 255);
    string TwiceTouch = string.Empty;
    GameObject Gtemp;
    bool IsStart = false;
    List<GameObject> LG = new List<GameObject>();
    Dictionary<GameObject, tianfu> DG = new Dictionary<GameObject, tianfu>();
    public Vector3[] V;
    Vector3 V0 = new Vector3(0, 0, 0);
    public Vector3[] V2;
    float StartTime = 60f;
    float Timing = 0;
    public static bool IsStep1 = false;
    public static bool IsStep2 = false;
    public static bool IsStep3 = false;
    public static bool IsStep4 = false;

    Vector3 VR;
    Vector3 VS;

    bool isloaded = false;

    public Text[] Texts;

    public static string sendstr = string.Empty;
    public static string sendstr2 = string.Empty;
    //bool BeginState = false;
    static int EnabledButton = -1;
    // Use this for initialization
    void Start()
    {
        //VS = GameObject.Find("Left").GetComponent<RectTransform>().position;
        //VR = GameObject.Find("Right").GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStep1)
        {
            Steps[3].SetActive(false);
            Steps[0].SetActive(true);
        }
        if (IsStep2)
        {
            Steps[0].SetActive(false);
            Steps[1].SetActive(true);
            PointsExpoler();
            if (!isloaded)
            {
                for (int i = 0; i < xmlreader.tianfus.Length; i++)
                {
                    V[i] = GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<RectTransform>().position;
                    V2[i] = V[i] - V0;
                    V2[i] = new Vector3(V2[i].x / StartTime, V2[i].y / StartTime, V2[i].z / StartTime);
                }
                PointsExpoler();
                TianfuSpriteLoading(true);
                isloaded = true;
            }
            if (IsStart)
            {
                Timing += Time.deltaTime * 1;
                //Debug.Log(Timing);
                if (Timing < 1.03f)
                {
                    for (int i = 0; i < xmlreader.tianfus.Length; i++)
                    {
                        //if (!xmlreader.tianfus[i].Touched)
                        {
                            Vector3 VN = GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<RectTransform>().position;
                            GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<RectTransform>().position = new Vector3(VN.x - V2[i].x, VN.y - V2[i].y, VN.z);

                            Color CN = GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<Image>().color;
                            GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<Image>().color = new Color(CN.r, CN.g, CN.b, CN.a - 0.05f);

                            Color CO = GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<RawImage>().color;
                            GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<RawImage>().color = new Color(CO.r, CO.g, CO.b, CO.a - 0.05f);

                            Color CM = GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<Text>().color;
                            GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<Text>().color = new Color(CM.r, CM.g, CM.b, CM.a - 0.1f);
                            GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<Text>().enabled = false;
                        }
                    }

                    for (int i = 0; i < Texts.Length; i++)
                    {
                        //Texts[i].enabled = false;
                        Color TC = Texts[i].color;
                        Texts[i].color = new Color(TC.r, TC.g, TC.b, TC.a - 0.1f);
                    }

                    Color CP = GameObject.Find("Point").GetComponent<Image>().color;
                    GameObject.Find("Point").GetComponent<Image>().color = new Color(CP.r, CP.g, CP.b, CP.a - 1f);

                    Color CQ = GameObject.Find("Point").GetComponentInChildren<Text>().color;
                    GameObject.Find("Point").GetComponentInChildren<Text>().color = new Color(CQ.r, CQ.g, CQ.b, CQ.a - 1f);
                    GameObject.Find("Point").GetComponentInChildren<Text>().enabled = false;

                    Color CR = GameObject.Find("Right").GetComponent<Image>().color;
                    GameObject.Find("Right").GetComponentInChildren<Text>().enabled = false;
                    GameObject.Find("Right").GetComponent<Image>().color = new Color(CR.r, CR.g, CR.b, CR.a - 1f);
                    
                    //GameObject.Find("Right").GetComponent<RectTransform>().position = new Vector3(VR.x - 10f, VR.y, VR.z);

                    Color CS = GameObject.Find("Left").GetComponent<Image>().color;
                    GameObject.Find("Left").GetComponentInChildren<Text>().enabled = false;
                    GameObject.Find("Left").GetComponent<Image>().color = new Color(CS.r, CS.g, CS.b, CS.a - 1f);

                    //GameObject.Find("Left").GetComponent<RectTransform>().position = new Vector3(VS.x + 10f, VS.y, VS.z);

                }
                if (Timing > 1.5f)
                {
                    Color C = new Color(255, 255, 255, 1);

                    for (int i = 0; i < xmlreader.tianfus.Length; i++)
                    {
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<RectTransform>().position = V[i];
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<RawImage>().enabled = false;
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<Image>().color = C;
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<RawImage>().color = C;
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<Text>().enabled = true;
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<Text>().color = C;
                    }

                    for (int i = 0; i < Texts.Length; i++)
                    {
                        //Texts[i].enabled = false;
                        Texts[i].color = C;
                    }

                    GameObject.Find("Point").GetComponent<Image>().color = C;
                    GameObject.Find("Point").GetComponentInChildren<Text>().color = C;
                    GameObject.Find("Point").GetComponentInChildren<Text>().enabled = true;
                    GameObject.Find("Right").GetComponentInChildren<Text>().enabled = true;
                    GameObject.Find("Right").GetComponent<Image>().color = C;
                    //GameObject.Find("Right").GetComponent<RectTransform>().position = VR;
                    GameObject.Find("Left").GetComponentInChildren<Text>().enabled = true;
                    GameObject.Find("Left").GetComponent<Image>().color = C;
                    //GameObject.Find("Left").GetComponent<RectTransform>().position = VS;

                    TianfuSpriteLoading(false);
                    IsStep2 = false;
                    IsStep3 = true;
                    IsStart = false;
                    isloaded = false;
                    Timing = 0;
                    points = 20;
                    TwiceTouch = string.Empty;
                    Gtemp = null;                 
                }
            }
        }
        if (IsStep3)
        {
            Steps[1].SetActive(false);
            Steps[2].SetActive(true);
        }
        if (IsStep4)
        {
            Steps[2].SetActive(false);
            Steps[3].SetActive(true);
        }
    }

    /// <summary>
    /// 剩余点数显示
    /// </summary>
    private void PointsExpoler()
    {
        GameObject.Find("Points").GetComponent<Text>().text = points.ToString();
        if (points < 0)
            GameObject.Find("Points").GetComponent<Text>().color = Red;
        else if(points>0)
            GameObject.Find("Points").GetComponent<Text>().color = White;
        else
            GameObject.Find("Points").GetComponent<Text>().color = Green;
    }

    /// <summary>
    /// 点击事件控制
    /// </summary>  
    public void Chossed(GameObject G)
    {
        tianfu x = xmlreader.tianfus[xmlreader.LoNuDict[G.name]];
        GameObject.Find("Info").GetComponent<Text>().text = x.Info;
        GameObject.Find("Info2").GetComponent<Text>().text = x.Name;
        GameObject.Find("Info3").GetComponent<Text>().text = x.Info2;
        GameObject.Find("Info4").GetComponent<Text>().text = x.Info4;
        GameObject.Find("Info5").GetComponent<Text>().text = x.Info5;
        GameObject.Find("Info6").GetComponent<Text>().text = x.Info3;
        if (G.name == "8" || G.name == "9" || G.name == "21" || G.name == "22" || G.name == "34" || G.name == "35") return;
        if (points <= 0 && !x.Touched) return;
        //G.GetComponent<Image>().sprite = IsTouched[1];
        //if (Gtemp != null && Gtemp != G) Gtemp.GetComponent<Image>().sprite = IsTouched[0];
        Gtemp = G;
        if (G.name == TwiceTouch)
        {
            if (x.CombainNum != "")
            {
                if (xmlreader.tianfus[xmlreader.CoNuDict[x.CombainNum]].Touched)
                {
                    if (Touching(x, G))
                    {
                        bool XB = false;
                        for (int i = 0; i < LG.Count; i++)
                        {
                            if (LG[i] == G)
                            {
                                break;
                            }
                            XB = true;
                        }
                        if (LG.Count == 0) XB = true;
                        if (XB)
                        {
                            LG.Add(G);
                            DG.Add(G, x);
                        }
                    }
                    else return;
                }
                else
                {
                    return;
                }
            }
            else if (!Touching(x, G)) return;
        }
        TwiceTouch = G.name;
    }

    private bool Touching(tianfu x, GameObject G)
    {
        if (!xmlreader.tianfus[xmlreader.LoNuDict[G.name]].Touched && ((points - int.Parse(x.Info2)) < 0)) return false;
        xmlreader.tianfus[xmlreader.LoNuDict[G.name]].Touched = !xmlreader.tianfus[xmlreader.LoNuDict[G.name]].Touched;
        G.GetComponentInChildren<RawImage>().enabled = xmlreader.tianfus[xmlreader.LoNuDict[G.name]].Touched;
        if (xmlreader.tianfus[xmlreader.LoNuDict[G.name]].Touched)
        {
            points = points - int.Parse(x.Info2);
            if (xmlreader.Combian.ContainsKey(x.Name))
            {
                if (xmlreader.Combian[x.Name].AorD == "A")
                {
                    //Debug.Log(xmlreader.Combian[x.Name].Locate);
                    GameObject.Find(xmlreader.Combian[x.Name].Locate).GetComponent<Image>().sprite = IsTouched[1];
                }
                else if (xmlreader.Combian[x.Name].AorD == "D")
                {
                    GameObject.Find(xmlreader.Combian[x.Name].Locate).GetComponent<Image>().sprite = IsTouched[3];
                }
            }
        }
        else
        {
            points = points + int.Parse(x.Info2);
            if (xmlreader.Combian.ContainsKey(x.Name)) GameObject.Find(xmlreader.Combian[x.Name].Locate).GetComponent<Image>().sprite = IsTouched[0];
            for (int i = 0; i < LG.Count; i++)
            {
                if (x.Name == DG[LG[i]].CombainNum && DG[LG[i]].Touched)
                {
                    Touching(DG[LG[i]], LG[i]);
                }
            }
        }
        PointsExpoler();
        return true;
    }

    public void Step2sCtrl(int G)
    {
        //for (int i = 0; i <= 5; i++) Step2s[i].SetActive(false);
        if (G != EnabledButton)
        {
            Step2s[G].SetActive(true);
            //GameObject.Find("Info").GetComponent<Text>().text = Infos[G];
            EnabledButton = G;
        }
        else
        {
            EnabledButton = -1;
            GameObject.Find("Info").GetComponent<Text>().text = "请选择天赋";
        }
        TwiceTouch = string.Empty;
    }

    public void StartButton()
    {
        tianfu[] T = xmlreader.tianfus;
        bool XB1 = true;
        bool XB2 = true;
        bool XB3 = true;
        for (int i = 0; i < T.Length; i++)
        {
            if (T[i].AorD == "A")
            {
                switch (T[i].step)
                {
                    case 1:
                        if (T[i].Touched) XB1 = false;
                        break;
                    case 2:
                        if (T[i].Touched) XB2 = false;
                        break;
                    case 3:
                        if (T[i].Touched) XB3 = false;
                        break;
                }
            }
        }
        if (XB1 || XB2 || XB3) return;
        if (points < 0) return;
        IsStart = true;
    }

    public void TianfuSpriteLoading(bool XB)
    {
        if (XB)
        {
            for (int i = 0; i < xmlreader.tianfus.Length; i++)
            {
                switch (xmlreader.tianfus[i].AorD)
                {
                    case "A":
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<Image>().sprite = IsTouched[1];
                        break;
                    case "D":
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<Image>().sprite = IsTouched[3];
                        break;
                    case "CA":
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<Image>().sprite = IsTouched[5];
                        break;
                    case "CD":
                        GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<Image>().sprite = IsTouched[6];
                        break;
                }
                if (xmlreader.tianfus[i].CombainNum != "")
                {
                    GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<Image>().sprite = IsTouched[0];
                    GameObject.Find(xmlreader.tianfus[i].Locate).GetComponentInChildren<Text>().color = new Color(147, 147, 147, 255);
                }
            }
        }
        else
        {
            for (int i = 0; i < xmlreader.tianfus.Length; i++)
            {
                GameObject.Find(xmlreader.tianfus[i].Locate).GetComponent<Image>().sprite = IsTouched[0];
            }
        }
    }
}