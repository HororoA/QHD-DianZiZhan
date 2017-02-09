using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class NewStepThreeCtr : MonoBehaviour {

    public GameObject[] Player;
    public GameObject[] PC;
    public Sprite[] S;
    public Text[] DiWoText;

    public static List<tianfu> PlayerStep = new List<tianfu>();
    public static List<tianfu> PCStep = new List<tianfu>();

    public Vector3[] VPL;
    public Vector3[] VPC;
    public Vector3[] VPL2;
    public Vector3[] VPC2;

    float Timeer = -2;

    List<int> PLC = new List<int>(); //玩家切换点位
    List<int> PCC = new List<int>(); //机器切换点位
    int PLCcount; //玩家第X个切换点
    int PCCcount; //机器第X个切换点

    public static Score PlayerScore = new Score();
    public static Score PCSocre = new Score();

    bool isstart = false;
    bool haveloaded = false;

    public static int xuliehao = 0;

    public static string TianfuNumber = string.Empty;

	// Use this for initialization
    void Start()
    {
        for (int i = 0; i < VPL.Length; i++)
        {
            VPL[i] = Player[i].GetComponent<RectTransform>().position;
            VPC[i] = PC[i].GetComponent<RectTransform>().position;

            VPL2[i] = VPL[i] - new Vector3(0, 0, 0);
            VPC2[i] = VPC[i] - new Vector3(0, 0, 0);
            VPL2[i] = new Vector3(VPL[i].x / 60, VPL[i].y / 60, VPL[i].z / 60);
            VPC2[i] = new Vector3(VPC[i].x / 60, VPC[i].y / 60, VPC[i].z / 60);
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        if (HoleCtrl.IsStep3)
        {
            if (!isstart)
            {
                prepareanime();
            }
            
            if (isstart)
            {
                guan();
                string[] S = StringS(TianfuNumber);
                for (int i = 0; i < S.Length; i++)
                {
                    switch (S[i])
                    {
                        case "PL":
                            kaiPL(int.Parse(S[i + 1]));
                            break;
                        case "PC":
                            kaiPC(int.Parse(S[i + 1]));
                            break;
                        case "end":
                            guan();
                            break;
                        case "Four":
                            isstart = false;
                            haveloaded = false;
                            PLC.Clear();
                            PCC.Clear();
                            PLCcount = 0;
                            PCCcount = 0;
                            Timeer = -2;
                            DiWoText[0].color = new Color(DiWoText[0].color.r, DiWoText[0].color.g, DiWoText[0].color.b, 0);
                            DiWoText[1].color = new Color(DiWoText[1].color.r, DiWoText[1].color.g, DiWoText[1].color.b, 0);
                            for (int j = 0; j < PC.Length; j++)
                            {
                                PC[j].SetActive(false);
                                Player[j].SetActive(false);
                            }
                            PlayerStep.Clear();
                            PCStep.Clear();

                            PlayerScore.Step4 = float.Parse(S[1]);
                            PCSocre.Step4 = float.Parse(S[2]);
                            HoleCtrl.IsStep3 = false;
                            HoleCtrl.IsStep4 = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    void guan()
    {
        for (int j = 0; j < PlayerStep.Count; j++)
        {
            if (PlayerStep[j].AorD == "A")
            {
                Player[j].GetComponent<Image>().sprite = S[2];
            }
            else
            {
                Player[j].GetComponent<Image>().sprite = S[3];
            }
            //PlayerStep[j].playing = false;
        }
        for (int j = 0; j < PCStep.Count; j++)
        {
            if (PCStep[j].AorD == "A")
            {
                PC[j].GetComponent<Image>().sprite = S[2];
            }
            else
            {
                PC[j].GetComponent<Image>().sprite = S[3];
            }
            //PCStep[j].playing = false;
        }
    }

    void kaiPL(int number)
    {
        for (int i = 0; i < PlayerStep.Count; i++)
        {
            Debug.Log("PL:," + number);
            if (PlayerStep[i].Number == number)
            {
                if (PlayerStep[i].AorD == "A")
                {
                    Player[i].GetComponent<Image>().sprite = S[0];
                }
                else
                {
                    Player[i].GetComponent<Image>().sprite = S[1];
                }
            }
        }
    }

    void kaiPC(int number)
    {
        for (int i = 0; i < PCStep.Count; i++)
        {
            Debug.Log("PC:," + number);
            if (PCStep[i].Number == number)
            {
                if (PCStep[i].AorD == "A")
                {
                    PC[i].GetComponent<Image>().sprite = S[0];
                }
                else
                {
                    PC[i].GetComponent<Image>().sprite = S[1];
                }
            }
        }
    }

    void prepareanime()
    {
        Timeer = Timeer + Time.deltaTime;
        if (Timeer < 2)
        {
            if (!haveloaded)
            {
                //GameObject.Find("ceshi").GetComponent<Text>().text = IP;
                SelectedTianfuLoading();
                //Pcloading();
                Pcloading2();
                for (int i = 0; i < PlayerStep.Count; i++)
                {
                    Player[i].SetActive(true);
                    Player[i].GetComponentInChildren<Text>().text = PlayerStep[i].Name;
                    Player[i].GetComponentInChildren<Text>().color = new Color(255, 255, 255, 0);
                    Player[i].GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0);
                    Player[i].GetComponentInChildren<RectTransform>().position = new Vector3(0, 0, 0);
                    if (PlayerStep[i].AorD == "A")
                    {
                        Player[i].GetComponent<Image>().sprite = S[2];
                    }
                    else
                    {
                        Player[i].GetComponent<Image>().sprite = S[3];
                    }
                    if (i > 0 && ((PlayerStep[i].AorD != PlayerStep[i - 1].AorD && PlayerStep[i].step == PlayerStep[i - 1].step) || PlayerStep[i].AorD == PlayerStep[i - 1].AorD && PlayerStep[i].step != PlayerStep[i - 1].step))
                    {
                        PLC.Add(i);
                    }
                }
                PLC.Add(PlayerStep.Count);
                for (int i = 0; i < PCStep.Count; i++)
                {
                    PC[i].SetActive(true);
                    PC[i].GetComponentInChildren<Text>().text = PCStep[i].Name;
                    PC[i].GetComponentInChildren<Text>().color = new Color(255, 255, 255, 0);
                    PC[i].GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0);
                    PC[i].GetComponentInChildren<RectTransform>().position = new Vector3(0, 0, 0);
                    if (PCStep[i].AorD == "A")
                    {
                        PC[i].GetComponent<Image>().sprite = S[2];
                    }
                    else
                    {
                        PC[i].GetComponent<Image>().sprite = S[3];
                    }
                    if (i > 0 && PCStep[i].step != PCStep[i - 1].step)
                    {
                        PCC.Add(i);
                    }
                }
                PCC.Add(PCStep.Count);
                haveloaded = true;
            }

            DiWoText[0].color = new Color(DiWoText[0].color.r, DiWoText[0].color.g, DiWoText[0].color.b, DiWoText[0].color.a + 0.1f);
            DiWoText[1].color = new Color(DiWoText[1].color.r, DiWoText[1].color.g, DiWoText[1].color.b, DiWoText[1].color.a + 0.1f);

            if (Timeer < -1)
            {
                donghua(0,true);
                donghua(1, true);
                donghua(2, true);
                donghua(3, true);
                donghua(4, true);
                donghua(5, true);
                donghua(6, true);
            }
            if (Timeer < 0 && Timeer > -1)
            {
                donghua(7, true);
                donghua(8, true);
                donghua(9, true);
                donghua(10, true);
                donghua(11, true);
                donghua(12, true);
            }
            if (Timeer < 1 && Timeer > 0)
            {
                donghua(0, false);
                donghua(1, false);
                donghua(2, false);
                donghua(3, false);
                donghua(4, false);
                donghua(5, false);
                donghua(6, false);
            }
            if (Timeer < 2 && Timeer > 1)
            {
                donghua(7, false);
                donghua(8, false);
                donghua(9, false);
                donghua(10, false);
                donghua(11, false);
                donghua(12, false);
            }

            GameObject.Find("Starttext").GetComponent<Text>().text = "VS";
            GameObject.Find("Starttext").GetComponent<Text>().fontSize = 270;
        }
        else
        {
            //Debug.Log(HoleCtrl.sendstr + "PC;" + HoleCtrl.sendstr2);
            udpouter.SocketSend(HoleCtrl.sendstr + "PC;" + HoleCtrl.sendstr2);
            Timeer = -2;
            haveloaded = true;
            isstart = true;
            HoleCtrl.sendstr = string.Empty;
            HoleCtrl.sendstr2 = string.Empty;
        }
    }

    void donghua(int i, bool XB)
    {
        if (XB)
        {
            Vector3 v1 = Player[i].GetComponent<RectTransform>().position;
            Player[i].GetComponent<RectTransform>().position = new Vector3(v1.x + VPL2[i].x, v1.y + VPL2[i].y, v1.z + VPL2[i].z);
            Player[i].GetComponentInChildren<Text>().color = new Color(255, 255, 255, Player[i].GetComponentInChildren<Text>().color.a + 0.1f);
            Player[i].GetComponentInChildren<Image>().color = new Color(255, 255, 255, Player[i].GetComponentInChildren<Image>().color.a + 0.1f);
        }
        else
        {
            Vector3 v2 = PC[i].GetComponent<RectTransform>().position;
            PC[i].GetComponent<RectTransform>().position = new Vector3(v2.x + VPC2[i].x, v2.y + VPC2[i].y, v2.z + VPC2[i].z);
            PC[i].GetComponentInChildren<Text>().color = new Color(255, 255, 255, PC[i].GetComponentInChildren<Text>().color.a + 0.1f);
            PC[i].GetComponentInChildren<Image>().color = new Color(255, 255, 255, PC[i].GetComponentInChildren<Image>().color.a + 0.1f);
        }
    }

    void Pcloading2()
    {
        //Debug.Log(123);
        int a = PlayerStep.Count;
        for (int ii = 0; ii <= 2; ii++)
        {
            float f = 0;
            switch (ii)
            {
                case 0:
                    f = UnityEngine.Random.Range(0, 8);
                    break;
                case 1:
                    f = UnityEngine.Random.Range(12, 17);
                    break;
                case 2:
                    f = UnityEngine.Random.Range(22, 29);
                    break;
            }
            double b = Math.Round(f, 0);
            string s = b.ToString();
            xmlreader.tianfus[int.Parse(s)].Touched = true;
            tianfu T = xmlreader.tianfus[int.Parse(s)];
            PCStep.Add(T);
        }
        for (int i = 3; i < a; i++)
        {
            float f = UnityEngine.Random.Range(0, 36);
            double b = Math.Round(f, 0);
            string s = b.ToString();
            if (!xmlreader.tianfus[int.Parse(s)].Touched)
            {
                xmlreader.tianfus[int.Parse(s)].Touched = true;
                tianfu T = xmlreader.tianfus[int.Parse(s)];
                PCStep.Add(T);
            }
            else if (i != 0)
            {
                i = i - 1;
            }
        }
        PCMAOPAO();
        List<tianfu> A1 = new List<tianfu>();
        List<tianfu> D1 = new List<tianfu>();
        List<tianfu> A2 = new List<tianfu>();
        List<tianfu> D2 = new List<tianfu>();
        List<tianfu> A3 = new List<tianfu>();
        List<tianfu> D3 = new List<tianfu>();
        for (int i = 0; i < PCStep.Count; i++)
        {
            switch (PCStep[i].step)
            {
                case 1:
                    if (PCStep[i].AorD == "A") A1.Add(PCStep[i]);
                    else D1.Add(PCStep[i]);
                    break;
                case 2:
                    if (PCStep[i].AorD == "A") A2.Add(PCStep[i]);
                    else D2.Add(PCStep[i]);
                    break;
                case 3:
                    if (PCStep[i].AorD == "A") A3.Add(PCStep[i]);
                    else D3.Add(PCStep[i]);
                    break;
            }
        }
        PCStep.Clear();
        List<tianfu> S1 = new List<tianfu>();
        List<tianfu> S2 = new List<tianfu>();
        List<tianfu> S3 = new List<tianfu>();
        S1 = ADchange(A1, D1);
        S2 = ADchange(A2, D2);
        S3 = ADchange(A3, D3);
        ADplus(S1);
        ADplus(S2);
        ADplus(S3);
        for (int i = 0; i < PCStep.Count; i++)
        {
            HoleCtrl.sendstr2 = HoleCtrl.sendstr2 + PCStep[i].Number + ";";
        }
    }

    void PCMAOPAO()
    {
        tianfu temp = new tianfu();
        for (int i = 0; i < PCStep.Count - 1; i++)
        {
            for (int j = 0; j < PCStep.Count - 1 - i; j++)
            {
                if (PCStep[j].Number > PCStep[j + 1].Number)
                {
                    temp = PCStep[j + 1];
                    PCStep[j + 1] = PCStep[j];
                    PCStep[j] = temp;
                }
            }
        }
    }

    void Pcloading()
    {
        for (int i = 0; i < 35; i++)
        {
            if (xmlreader.tianfus[i].Touched)
            {
                PCStep.Add(xmlreader.tianfus[i + 1]);
            }
        }
        List<tianfu> A1 = new List<tianfu>();
        List<tianfu> D1 = new List<tianfu>();
        List<tianfu> A2 = new List<tianfu>();
        List<tianfu> D2 = new List<tianfu>();
        List<tianfu> A3 = new List<tianfu>();
        List<tianfu> D3 = new List<tianfu>();
        for (int i = 0; i < PCStep.Count; i++)
        {
            switch (PCStep[i].step)
            {
                case 1:
                    if (PCStep[i].AorD == "A") A1.Add(PCStep[i]);
                    else D1.Add(PCStep[i]);
                    break;
                case 2:
                    if (PCStep[i].AorD == "A") A2.Add(PCStep[i]);
                    else D2.Add(PCStep[i]);
                    break;
                case 3:
                    if (PCStep[i].AorD == "A") A3.Add(PCStep[i]);
                    else D3.Add(PCStep[i]);
                    break;
            }
        }
        PCStep.Clear();
        List<tianfu> S1 = new List<tianfu>();
        List<tianfu> S2 = new List<tianfu>();
        List<tianfu> S3 = new List<tianfu>();
        S1 = ADchange(A1, D1);
        S2 = ADchange(A2, D2);
        S3 = ADchange(A3, D3);
        ADplus(S1);
        ADplus(S2);
        ADplus(S3);
        //xulieloadinghelper(PCStep, 1);
    }

    List<tianfu> ADchange(List<tianfu> A, List<tianfu> D)
    {
        List<tianfu> T=new List<tianfu>();
        for (int i = 0; i < D.Count; i++) T.Add(D[i]);
        for (int i = 0; i < A.Count; i++) T.Add(A[i]);
        return T;
    }

    void ADplus(List<tianfu> L)
    {
        for (int i = 0; i < L.Count; i++)
        {
            PCStep.Add(L[i]);
        }
    }

    void SelectedTianfuLoading()
    {
        for (int i = 0; i < xmlreader.tianfus.Length; i++)
        {
            if (xmlreader.tianfus[i].Touched)
            {
                PlayerStep.Add(xmlreader.tianfus[i]);
                HoleCtrl.sendstr = HoleCtrl.sendstr + i.ToString() + ";";
                xmlreader.tianfus[i].Touched = false;
            }
        }
        //step = 1;
        //Debug.Log(HoleCtrl.sendstr);
    }
    
    void SwitchDown()
    {
        for (int i = 0; i < 9; i++)
        {
            //PC[i].SetActive(false);
            //PC[i].GetComponent<Image>().sprite = S[2];
            //Player[i].SetActive(false);
            //Player[i].GetComponent<Image>().sprite = S[3];
            //T[0].text = "";
            //T[1].text = "";
        }
    }

    string[] StringS(string s)
    {
        string[] str = s.Split(';');
        //Debug.Log(str[0]);
        //Debug.Log(str[1]);
        return str;
    }
}