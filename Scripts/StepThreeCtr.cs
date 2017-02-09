using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using RenderHeads.Media.AVProVideo;

public class StepThreeCtr : MonoBehaviour
{
    public GameObject Radar;
    public GameObject[] Modes;
    public GameObject[] PC;
    public GameObject[] Player;
    public Sprite[] S;
    public Text[] T;
    public GameObject Tiptext;
    public GameObject[] Scores;
    public Text[] ScoresTexts;
    public GameObject[] Games;
    public RectTransform[] R;
    public Text[] zongfen;
    public GameObject[] isvectory;
    public GameObject xuliezhen;
    public Sprite[] TextS;
    public Sprite[] SpriteS;

    public Color32 blue;
    public Color32 yellow;
    public GameObject[] PlPcInfo;
    public GameObject[] PlPcBg;
    public GameObject[] PlPcName;
    public GameObject[] PlInfoColor;
    public Image[] PlInfoImage;
    public Text[] PlInfoText;
    public GameObject[] PcInfoColor;
    public Image[] PcInfoImage;
    public Text[] PcInfoText;
    public GameObject FenshuChange;
    //public GameObject[] BGBD;


    public static List<tianfu> PlayerStep1Attack = new List<tianfu>();
    public static List<tianfu> PlayerStep2Attack = new List<tianfu>();
    public static List<tianfu> PlayerStep3Attack = new List<tianfu>();
    public static List<tianfu> PlayerStep1Defend = new List<tianfu>();
    public static List<tianfu> PlayerStep2Defend = new List<tianfu>();
    public static List<tianfu> PlayerStep3Defend = new List<tianfu>();

    public static tianfu guanliantianfu;

    MediaPlayer M;
    MediaPlayer.FileLocation _nextVideoLocation;
    MediaPlayer M2;
    MediaPlayer.FileLocation M2Location;

    RectTransform BG1;
    RectTransform BG2;

    Vector3 PlinfoP;
    Vector3 PcinfoP;

    float PlAx;
    float PlAy;
    float PcAx;
    float PcAy;

    int PClight;
    int PLlight;

    //控制流程相关
    int step = 0;
    public static float Timeer = -2;
    int[] num = { 0, 0, 0, 0, 0, 0 };
    int temp = -1;
    int a;
    //测试相关
    public string CorE = "E";
    public bool istest = false;
    //动画相关
    bool starttianfujieshaodonghua = false;
    bool startfenshudonghua = false;
    bool Anihelp = false;
    bool TimerCtr = false;
    float usingtimer;
    float huihefen = 0;
    float[] f = { 0, 0, 0, 0, 0, 0 };
    //分数
    public static Score PlayerScore = new Score();
    public static Score PCSocre = new Score();
    //播放序列帧动画相关
    public static int isplaying = 0;
    public static int xuliehao = 0;
    float xuelietimer = 0;
    //UDP相关
    public static string udp = string.Empty;
    public static bool havereciveudp = false;
    string sendstr = string.Empty;

    List<int> PCS = new List<int>();
    List<int> PLS = new List<int>();
    int PcPlSNum = 100;


    void Awake()
    {
        M = new MediaPlayer();
        M = GameObject.Find("AVPro Media Player").GetComponent<MediaPlayer>();
        M.Events.AddListener(OnMediaPlayerEvent);
        M2 = new MediaPlayer();
        M2 = GameObject.Find("BGVEDIO").GetComponent<MediaPlayer>();
        M2.Events.AddListener(M2ctr);

    }

    // Use this for initialization
    void Start()
    {
        BG1 = PlPcBg[0].GetComponent<RectTransform>();
        BG2 = PlPcBg[1].GetComponent<RectTransform>();
        PlinfoP = PlPcInfo[1].GetComponent<RectTransform>().position;
        PcinfoP = PlPcInfo[0].GetComponent<RectTransform>().position;


        if (istest)
        {
            HoleCtrl.IsStep3 = true;
            xmlreader.tianfus[0].Touched = true;
            xmlreader.tianfus[1].Touched = true;
            xmlreader.tianfus[2].Touched = true;
            xmlreader.tianfus[3].Touched = true;
            xmlreader.tianfus[4].Touched = true;
            xmlreader.tianfus[5].Touched = true;
            xmlreader.tianfus[6].Touched = true;
            xmlreader.tianfus[7].Touched = true;
            xmlreader.tianfus[8].Touched = true;
            xmlreader.tianfus[9].Touched = true;
            xmlreader.tianfus[10].Touched = true;
            xmlreader.tianfus[11].Touched = true;
            xmlreader.tianfus[12].Touched = true;
            xmlreader.tianfus[13].Touched = true;
            xmlreader.tianfus[14].Touched = true;
            xmlreader.tianfus[15].Touched = true;
            xmlreader.tianfus[16].Touched = true;
            xmlreader.tianfus[17].Touched = true;
            xmlreader.tianfus[18].Touched = true;
            xmlreader.tianfus[19].Touched = true;
            xmlreader.tianfus[20].Touched = true;
            xmlreader.tianfus[21].Touched = true;
            xmlreader.tianfus[22].Touched = true;
            xmlreader.tianfus[23].Touched = true;
            xmlreader.tianfus[24].Touched = true;
            xmlreader.tianfus[25].Touched = true;
            xmlreader.tianfus[26].Touched = true;
            xmlreader.tianfus[27].Touched = true;
            xmlreader.tianfus[28].Touched = true;
            xmlreader.tianfus[29].Touched = true;
            xmlreader.tianfus[30].Touched = true;
            xmlreader.tianfus[31].Touched = true;
            xmlreader.tianfus[32].Touched = true;
            xmlreader.tianfus[34].Touched = true;
            xmlreader.tianfus[35].Touched = true;
            xmlreader.tianfus[36].Touched = true;
        }
        else
        {
            HoleCtrl.IsStep2 = true;
            Games[0].SetActive(true);
        }
    }

    void udpstrjiexi()
    {
        string[] str = udp.Split(';');
        //Debug.Log("Length," + str.Length);
        for (int number = 0; number < str.Length - 1; number++)
        {
            //Debug.Log(number + "," + str[number]);
            if (str[number] != "PC" && number < PcPlSNum)
            {
                PLS.Add(int.Parse(str[number]));
                xmlreader.tianfus[int.Parse(str[number])].Touched = true;
            }
            if (str[number] == "PC")
            {
                PcPlSNum = number;
            }
            if (number > PcPlSNum)
            {
                //Debug.Log(str[number]);
                PCS.Add(int.Parse(str[number]));
                //Debug.Log(PCS[number]);
            }
        }
        HoleCtrl.IsStep3 = true;
        HoleCtrl.IsStep2 = false;
        Games[0].SetActive(false);
        Games[1].SetActive(true);
        Games[2].SetActive(false);
        udp = String.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (HoleCtrl.IsStep2)
        {
            Games[0].SetActive(true);
            Games[1].SetActive(false);
            Games[2].SetActive(false);

            //GameObject.Find("ceshi").GetComponent<Text>().text = udp;
            if (udp != string.Empty && udp != "hello")
            {
                udpstrjiexi();
            }
        }
        if (HoleCtrl.IsStep3)
        //if (havereciveudp)
        {
            Timeer += Time.deltaTime * 1;

            Radar.GetComponent<RectTransform>().Rotate(0, 0, -200 * Time.deltaTime, Space.Self);

            if (step != temp)//每当step发生变化执行一次
            {
                //Debug.Log(step);
                Games[1].SetActive(true);
                SwitchDown();
                temp = step;
                //Debug.Log("S," + step);
                switch (step)
                {
                    case 0:
                        SelectedTianfuLoading();
                        if (istest) Pcloading();
                        else Pcloading2();
                        colorchange(true);
                        break;
                    case 1:
                        colorchange(true);
                        BGM2Change("BG1.mov");
                        TianfuObjLoading(PlayerStep1Attack, Player);
                        TianfuObjLoading(PCStep1Defend, PC);
                        break;
                    case 11:
                        for (int number = 0; number < xmlreader.tianfus.Length; number++)
                        {
                            xmlreader.tianfus[number].played = false;
                        }
                        colorchange(false);
                        TianfuObjLoading(PCStep1Attack, PC);
                        TianfuObjLoading(PlayerStep1Defend, Player);
                        break;
                    case 2:
                        for (int number = 0; number < xmlreader.tianfus.Length; number++)
                        {
                            xmlreader.tianfus[number].played = false;
                        }
                        colorchange(true);
                        BGM2Change("BG2.mov");
                        TianfuObjLoading(PlayerStep2Attack, Player);
                        TianfuObjLoading(PCStep2Defend, PC);
                        break;
                    case 22:
                        for (int number = 0; number < xmlreader.tianfus.Length; number++)
                        {
                            xmlreader.tianfus[number].played = false;
                        }
                        colorchange(false);
                        TianfuObjLoading(PCStep2Attack, PC);
                        TianfuObjLoading(PlayerStep2Defend, Player);
                        break;
                    case 3:
                        for (int number = 0; number < xmlreader.tianfus.Length; number++)
                        {
                            xmlreader.tianfus[number].played = false;
                        }
                        colorchange(true);
                        BGM2Change("BG3.mov");
                        TianfuObjLoading(PlayerStep3Attack, Player);
                        TianfuObjLoading(PCStep3Defend, PC);
                        break;
                    case 33:
                        for (int number = 0; number < xmlreader.tianfus.Length; number++)
                        {
                            xmlreader.tianfus[number].played = false;
                        }
                        colorchange(false);
                        TianfuObjLoading(PCStep3Attack, PC);
                        TianfuObjLoading(PlayerStep3Defend, Player);
                        break;
                    case 4:
                        //Debug.Log(step);
                        HoleCtrl.IsStep3 = false;
                        HoleCtrl.IsStep4 = true;
                        step = 0;
                        Timeer = -2;
                        temp = -1;

                        for (int i = 0; i < 6; i++)
                        {
                            num[i] = 0;
                        }

                        PlayerStep1Attack.Clear();
                        PlayerStep2Attack.Clear();
                        PlayerStep3Attack.Clear();
                        PlayerStep1Defend.Clear();
                        PlayerStep2Defend.Clear();
                        PlayerStep3Defend.Clear();

                        PCStep1Attack.Clear();
                        PCStep2Attack.Clear();
                        PCStep3Attack.Clear();
                        PCStep1Defend.Clear();
                        PCStep2Defend.Clear();
                        PCStep3Defend.Clear();

                        PCS.Clear();
                        PLS.Clear();

                        for (int i = 0; i < 9; i++)
                        {
                            PC[i].SetActive(false);
                            Player[i].SetActive(false);
                        }

                        PCSocre.Step4Math();
                        PlayerScore.Step4Math();

                        havereciveudp = false;

                        Games[1].SetActive(false);
                        Games[2].SetActive(true);


                        zongfen[0].text = Math.Round(PlayerScore.Step4, 0).ToString();
                        zongfen[1].text = Math.Round(PCSocre.Step4, 0).ToString();

                        sendstr = "Four;" + PlayerScore.Step4.ToString() + ";" + PCSocre.Step4.ToString();
                        udptest.SocketSend(sendstr);
                        sendstr = string.Empty;

                        if (PlayerScore.Step4 > PCSocre.Step4) isvectory[0].SetActive(true);
                        else isvectory[1].SetActive(true);

                        break;
                }
            }

            if (TimerCtr) Timeer = 0;

            Animeting();//第一优先级，改变step，只执行一次

            if (startfenshudonghua)
            {
                jifendonghua();
            }

            if (starttianfujieshaodonghua)
            {
                tianfujieshaodonghua();
            }

            //Debug.Log("isplaying:" + isplaying);

            if (isplaying != 1)
            {
                //Debug.Log(10);
                switch (step)
                {
                    case 1:
                        playingdonghua(PlayerStep1Attack, 2);
                        playingdonghua(PCStep1Defend, 1);
                        break;
                    case 11:
                        playingdonghua(PlayerStep1Defend, 2);
                        playingdonghua(PCStep1Attack, 1);
                        break;
                    case 2:
                        playingdonghua(PlayerStep2Attack, 2);
                        playingdonghua(PCStep2Defend, 1);
                        break;
                    case 22:
                        playingdonghua(PlayerStep2Defend, 2);
                        playingdonghua(PCStep2Attack, 1);
                        break;
                    case 3:
                        playingdonghua(PlayerStep3Attack, 2);
                        playingdonghua(PCStep3Defend, 1);
                        break;
                    case 33:
                        playingdonghua(PlayerStep3Defend, 2);
                        playingdonghua(PCStep3Attack, 1);
                        break;
                    default:
                        return;
                }
            }
        }

        if (HoleCtrl.IsStep4)
        {
            Timeer = Timeer + Time.deltaTime;
            if (Timeer > 3)
            {
                HoleCtrl.IsStep2 = true;
                HoleCtrl.IsStep4 = false;
                Games[0].SetActive(true);
                Games[2].SetActive(false);
                PlayerScore.ClearScore();
                PCSocre.ClearScore();
                //PlayerScore.Step1 = 1;
                //PlayerScore.Step2[0] = 0;
                //PlayerScore.Step2[1] = 1;
                //PlayerScore.Step3[0] = 0;
                //PlayerScore.Step3[1] = 1;
                //PlayerScore.Step4 = 0;
                //PCSocre.Step1 = 1;
                //PCSocre.Step2[0] = 0;
                //PCSocre.Step2[1] = 1;
                //PCSocre.Step3[0] = 0;
                //PCSocre.Step3[1] = 1;
                //PCSocre.Step4 = 0;
                PlayerScore.ClearScore();
                PCSocre.ClearScore();
                step = 0;
                for (int i = 0; i < ScoresTexts.Length; i++)
                {
                    ScoresTexts[i].text = "";
                    //Scores[i].SetActive(false);
                }
                for (int number = 0; number < xmlreader.tianfus.Length; number++)
                {
                    xmlreader.tianfus[number].Touched = false;
                    xmlreader.tianfus[number].played = false;
                }
                isvectory[0].SetActive(false);
                isvectory[1].SetActive(false);
                Timeer = -2;
                sendstr = string.Empty;
                udptest.SocketSend(sendstr);
                sendstr = string.Empty;
            }
        }
    }

    void colorchange(bool XB)
    {
        Color blue = new Color(95, 255, 231);
        Color yellow = new Color(244, 202, 31);
        if (XB)
        {
            GameObject.Find("TEXTBG").GetComponent<Image>().sprite = TextS[0];
            ScoresTexts[0].color = yellow;
            ScoresTexts[1].color = yellow;
            ScoresTexts[2].color = yellow;
            ScoresTexts[6].color = yellow;
            ScoresTexts[3].color = blue;
            ScoresTexts[4].color = blue;
            ScoresTexts[5].color = blue;
            ScoresTexts[7].color = blue;
        }
        else
        {
            GameObject.Find("TEXTBG").GetComponent<Image>().sprite = TextS[1];
            ScoresTexts[0].color = blue;
            ScoresTexts[1].color = blue;
            ScoresTexts[2].color = blue;
            ScoresTexts[6].color = blue;
            ScoresTexts[3].color = yellow;
            ScoresTexts[4].color = yellow;
            ScoresTexts[5].color = yellow;
            ScoresTexts[7].color = yellow;
        }
    }

    void jifenshu(tianfu T, int j)
    {
        startfenshudonghua = true;
        FenshuChange.SetActive(true);
        if (huihefen > 10)
        {
            FenshuChange.GetComponent<Text>().text = "+ " + huihefen;
        }
        else if (huihefen > 0)
        {
            FenshuChange.GetComponent<Text>().text = "X " + huihefen;
        }
        else
        {
            FenshuChange.GetComponent<Text>().text = "未得分";
        }
        f[0] = float.Parse(ScoresTexts[0].text);
        f[1] = float.Parse(ScoresTexts[1].text);
        f[2] = float.Parse(ScoresTexts[2].text);
        f[3] = float.Parse(ScoresTexts[3].text);
        f[4] = float.Parse(ScoresTexts[4].text);
        f[5] = float.Parse(ScoresTexts[5].text);
    }

    void jifendonghua()
    {
        if (Timeer < 2f)
        {
            switch (step)
            {
                case 1:
                    //Debug.Log(123);
                    ScoresTexts[0].text = (float.Parse(ScoresTexts[0].text) + ((PlayerScore.Step1 - f[0]) / 120)).ToString();
                    break;
                case 11:
                    ScoresTexts[3].text = (float.Parse(ScoresTexts[3].text) + ((PCSocre.Step1 - f[3]) / 120)).ToString();
                    break;
                case 2:
                    ScoresTexts[1].text = (float.Parse(ScoresTexts[1].text) + ((PlayerScore.Step2[0] - f[1]) / 120)).ToString();
                    break;
                case 22:
                    ScoresTexts[4].text = (float.Parse(ScoresTexts[4].text) + ((PCSocre.Step2[0] - f[4]) / 120)).ToString();
                    break;
                case 3:
                    ScoresTexts[2].text = (float.Parse(ScoresTexts[2].text) + ((PlayerScore.Step3[0] - f[2]) / 120)).ToString();
                    break;
                case 33:
                    ScoresTexts[5].text = (float.Parse(ScoresTexts[5].text) + ((PCSocre.Step3[0] - f[5]) / 120)).ToString();
                    break;
            }
        }
        if (Timeer > 2f)
        {
            startfenshudonghua = false;
            huihefen = 0;
            isplaying = 0;
            FenshuChange.SetActive(false);
            //阶段一
            ScoresTexts[3].text = PCSocre.Step1.ToString();
            ScoresTexts[0].text = PlayerScore.Step1.ToString();
            //阶段二
            ScoresTexts[4].text = PCSocre.Step2[0].ToString();
            ScoresTexts[1].text = PlayerScore.Step2[0].ToString();
            ScoresTexts[7].text = PCSocre.Step2[1].ToString();
            ScoresTexts[6].text = PlayerScore.Step2[1].ToString();
            //阶段三
            ScoresTexts[5].text = PCSocre.Step3[0].ToString();
            ScoresTexts[2].text = PlayerScore.Step3[0].ToString();
            sendstr = "end";
            udptest.SocketSend(sendstr);
            sendstr = string.Empty;
        }
    }

    void playingdonghua(List<tianfu> T, int j)
    {
        for (int i = 0; i < T.Count; i++)
        {
            //Debug.Log(T[i].playing + "," + T[i].Name);
            if (T[i].playing)
            {
                //Debug.Log(T[i].Name + "," + T[i].S.Count);
                //Sprite S = Donghua.bofangxueliezhen(T[i], j);
                //xuliezhen.GetComponent<Image>().sprite = S;

                if (isplaying == 4)
                {
                    isplaying = 1;
                    Timeer = 0;
                    jifenshu(T[i], j);
                }

                if (isplaying == 3)
                {
                    tianfujieshao(T[i], j - 1);
                    if (guanliantianfu != null)
                    {
                        if (j == 1) tianfujieshao(guanliantianfu, 1);
                        else tianfujieshao(guanliantianfu, 0);
                        if (j == 2) sendstr = sendstr + ";PC;" + guanliantianfu.Number.ToString();
                        else sendstr = sendstr + ";PL;" + guanliantianfu.Number.ToString();
                    }
                    //Debug.Log("1," + isplaying);
                    isplaying = 1;
                    //Debug.Log("2," + isplaying);
                    Timeer = 0;
                    //Debug.Log(isplaying);
                    udptest.SocketSend(sendstr);
                    sendstr = string.Empty;
                }
                //Debug.Log("1," + isplaying);
                if (isplaying == 2)
                {
                    
                    isplaying = 1;
                    if (guanliantianfu == null)
                    {
                        string s = T[i].Locate;
                        s = s + "\\";
                        s = s + j.ToString();
                        s = s + ".mov";
                        AVCtr(s);
                    }
                    else
                    {
                        string s = "Spe\\";
                        s = s + T[i].Locate;
                        s = s + "\\";
                        s = s + guanliantianfu.Locate + "\\";
                        s = s + j.ToString();
                        s = s + ".mov";
                        AVCtr(s);
                    }
                    //Debug.Log(sendstr);

                }
            }
        }
    }

    string zifuchuli(string s)
    {
        string b = string.Empty;
        b = s.Replace("\n", "");
        //Debug.Log(b);
        return b;
    }

    void infoimaekai(int i)
    {
        PcInfoColor[i].GetComponent<Image>().color = new Color(PcInfoColor[i].GetComponent<Image>().color.r, PcInfoColor[i].GetComponent<Image>().color.g, PcInfoColor[i].GetComponent<Image>().color.b, PcInfoColor[i].GetComponent<Image>().color.a + 0.1f);
        PlInfoColor[i].GetComponent<Image>().color = new Color(PlInfoColor[i].GetComponent<Image>().color.r, PlInfoColor[i].GetComponent<Image>().color.g, PlInfoColor[i].GetComponent<Image>().color.b, PlInfoColor[i].GetComponent<Image>().color.a + 0.1f);
    }

    void infoimageguan(int i)
    {
        
    }

    void infotextkai(int i)
    {
        PcInfoColor[i].GetComponent<Text>().color = new Color(PcInfoColor[i].GetComponent<Text>().color.r, PcInfoColor[i].GetComponent<Text>().color.g, PcInfoColor[i].GetComponent<Text>().color.b, PcInfoColor[i].GetComponent<Text>().color.a + 0.1f);
        PlInfoColor[i].GetComponent<Text>().color = new Color(PlInfoColor[i].GetComponent<Text>().color.r, PlInfoColor[i].GetComponent<Text>().color.g, PlInfoColor[i].GetComponent<Text>().color.b, PlInfoColor[i].GetComponent<Text>().color.a + 0.1f);
    }

    void infotextguan(int i)
    {
        PcInfoColor[i].GetComponent<Text>().color = new Color(PcInfoColor[i].GetComponent<Text>().color.r, PcInfoColor[i].GetComponent<Text>().color.g, PcInfoColor[i].GetComponent<Text>().color.b, PcInfoColor[i].GetComponent<Text>().color.a - 0.1f);
        PlInfoColor[i].GetComponent<Text>().color = new Color(PlInfoColor[i].GetComponent<Text>().color.r, PlInfoColor[i].GetComponent<Text>().color.g, PlInfoColor[i].GetComponent<Text>().color.b, PlInfoColor[i].GetComponent<Text>().color.a - 0.1f);
    }

    void tianfujieshao(tianfu T, int j)
    {
        PlPcInfo[j].SetActive(true);
        if (j == 1)
        {
            PlPcBg[0].SetActive(true);
            PlInfoText[1].text = T.Info;
            PlInfoText[0].text = T.Name;
            PlInfoText[2].text = zifuchuli(T.Name);
            if (T.AorD == "A")
            {
                PlInfoImage[0].sprite = S[2];
                PlInfoImage[1].sprite = SpriteS[1];
            }
            else
            {
                PlInfoImage[0].sprite = S[3];
                PlInfoImage[1].sprite = SpriteS[3];
            }
        }
        else
        {
            PlPcBg[1].SetActive(true);
            PcInfoText[1].text = T.Info;
            PcInfoText[0].text = T.Name;
            PcInfoText[2].text = zifuchuli(T.Name);
            if (T.AorD == "A")
            {
                PcInfoImage[0].sprite = S[2];
                PcInfoImage[1].sprite = SpriteS[0];
            }
            else
            {
                PcInfoImage[0].sprite = S[3];
                PcInfoImage[1].sprite = SpriteS[2];
            }
        }
        PlPcInfo[1].GetComponent<RectTransform>().position = Player[PLlight].GetComponent<RectTransform>().position;
        PlPcInfo[0].GetComponent<RectTransform>().position = PC[PClight].GetComponent<RectTransform>().position;
        PlPcInfo[1].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        PlPcInfo[0].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        int intnumber = 60;
        PlAx = (PlinfoP.x - PlPcInfo[1].GetComponent<RectTransform>().position.x) / intnumber;
        PlAy = (PlinfoP.y - PlPcInfo[1].GetComponent<RectTransform>().position.y) / intnumber;
        PcAx = (PcinfoP.x - PlPcInfo[0].GetComponent<RectTransform>().position.x) / intnumber;
        PcAy = (PcinfoP.y - PlPcInfo[0].GetComponent<RectTransform>().position.y) / intnumber;
        starttianfujieshaodonghua = true;
    }

    void tianfujieshaodonghua()
    {
        //Debug.Log(Timeer);  
        if (Timeer < 1)
        {
            float f = 0.016f;
            PlPcInfo[1].GetComponent<RectTransform>().position = new Vector3(PlPcInfo[1].GetComponent<RectTransform>().position.x + PlAx, PlPcInfo[1].GetComponent<RectTransform>().position.y + PlAy, PlPcInfo[1].GetComponent<RectTransform>().position.z);
            PlPcInfo[0].GetComponent<RectTransform>().position = new Vector3(PlPcInfo[0].GetComponent<RectTransform>().position.x + PcAx, PlPcInfo[0].GetComponent<RectTransform>().position.y + PcAy, PlPcInfo[0].GetComponent<RectTransform>().position.z);
            if (PlPcInfo[1].GetComponent<RectTransform>().localScale.x < 1)
            {
                PlPcInfo[1].GetComponent<RectTransform>().localScale = new Vector3(PlPcInfo[1].GetComponent<RectTransform>().localScale.x + f, PlPcInfo[1].GetComponent<RectTransform>().localScale.y + f, PlPcInfo[1].GetComponent<RectTransform>().localScale.z + f);
                PlPcInfo[0].GetComponent<RectTransform>().localScale = new Vector3(PlPcInfo[0].GetComponent<RectTransform>().localScale.x + f, PlPcInfo[0].GetComponent<RectTransform>().localScale.y + f, PlPcInfo[0].GetComponent<RectTransform>().localScale.z + f);
            }
        }
        if (Timeer > 1 && Timeer < 2)
        {
            PlPcInfo[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            PlPcInfo[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            PlPcInfo[1].GetComponent<RectTransform>().position = PlinfoP;
            PlPcInfo[0].GetComponent<RectTransform>().position = PcinfoP;
            PlPcName[0].SetActive(true);
            PlPcName[1].SetActive(true);
            infotextkai(0);
        }
        if (Timeer > 2 && Timeer < 3)
        {
            infotextkai(1);
            infotextkai(2);
            infoimaekai(3);
        }
        if (Timeer > 3 && Timeer < 4)
        {

        }
        if (Timeer > 5f && Timeer < 6f)
        {
            infotextguan(0);
            infotextguan(1);
            infotextguan(2);
        }
        if (Timeer > 5.5f && Timeer < 6f)
        {
            PlPcInfo[0].SetActive(false);
            PlPcInfo[1].SetActive(false);
            PlPcBg[0].SetActive(false);
            PlPcBg[1].SetActive(false);
            PlPcName[0].SetActive(false);
            PlPcName[1].SetActive(false);
            Color C = new Color(255, 255, 255, 0);
            PcInfoColor[0].GetComponent<Text>().color = C;
            PlInfoColor[0].GetComponent<Text>().color = C;
            PcInfoColor[1].GetComponent<Text>().color = C;
            PlInfoColor[1].GetComponent<Text>().color = C;
            PcInfoColor[2].GetComponent<Text>().color = C;
            PlInfoColor[2].GetComponent<Text>().color = C;
            PcInfoColor[3].GetComponent<Image>().color = C;
            PlInfoColor[3].GetComponent<Image>().color = C;
        }
        if (Timeer > 6f)
        {
            starttianfujieshaodonghua = false;
            isplaying = 2;
            Timeer = 0;
        }
    }

    void Pcloading2()
    {
        //Debug.Log(PCS.Count);
        for (int i = 0; i < PCS.Count; i++)
        {
            //xmlreader.tianfus[PCS[i]].Touched = true;
            tianfu T = xmlreader.tianfus[PCS[i]];
            //Debug.Log(T.Name);
            if (T.AorD == "A")
            {
                switch (T.step)
                {
                    case 1:
                        PCStep1Attack.Add(T);
                        break;
                    case 2:
                        PCStep2Attack.Add(T);
                        break;
                    case 3:
                        PCStep3Attack.Add(T);
                        break;
                }
            }
            else
            {
                switch (T.step)
                {
                    case 1:
                        PCStep1Defend.Add(T);
                        break;
                    case 2:
                        PCStep2Defend.Add(T);
                        break;
                    case 3:
                        PCStep3Defend.Add(T);
                        break;
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
                if (xmlreader.tianfus[i + 1].AorD == "A")
                {
                    switch (xmlreader.tianfus[i + 1].step)
                    {
                        case 1:
                            PCStep1Attack.Add(xmlreader.tianfus[i + 1]);
                            break;
                        case 2:
                            PCStep2Attack.Add(xmlreader.tianfus[i + 1]);
                            break;
                        case 3:
                            PCStep3Attack.Add(xmlreader.tianfus[i + 1]);
                            break;
                    }
                }
                else
                {
                    switch (xmlreader.tianfus[i + 1].step)
                    {
                        case 1:
                            PCStep1Defend.Add(xmlreader.tianfus[i + 1]);
                            break;
                        case 2:
                            PCStep2Defend.Add(xmlreader.tianfus[i + 1]);
                            break;
                        case 3:
                            PCStep3Defend.Add(xmlreader.tianfus[i + 1]);
                            break;
                    }
                }
            }
        }
        //xulieloadinghelper(PCStep1Attack, 2);
        //xulieloadinghelper(PCStep2Attack, 2);
        //xulieloadinghelper(PCStep3Attack, 2);
        //xulieloadinghelper(PCStep1Defend, 2);
        //xulieloadinghelper(PCStep2Defend, 2);
        //xulieloadinghelper(PCStep3Defend, 2);
    }

    void SelectedTianfuLoading()
    {
        for (int i = 0; i < xmlreader.tianfus.Length; i++)
        {
            if (xmlreader.tianfus[i].Touched)
            {
                if (xmlreader.tianfus[i].AorD == "A")
                {
                    switch (xmlreader.tianfus[i].step)
                    {
                        case 1:
                            PlayerStep1Attack.Add(xmlreader.tianfus[i]);
                            break;
                        case 2:
                            PlayerStep2Attack.Add(xmlreader.tianfus[i]);
                            break;
                        case 3:
                            PlayerStep3Attack.Add(xmlreader.tianfus[i]);
                            break;
                    }
                }
                else
                {
                    switch (xmlreader.tianfus[i].step)
                    {
                        case 1:
                            PlayerStep1Defend.Add(xmlreader.tianfus[i]);
                            break;
                        case 2:
                            PlayerStep2Defend.Add(xmlreader.tianfus[i]);
                            break;
                        case 3:
                            PlayerStep3Defend.Add(xmlreader.tianfus[i]);
                            break;
                    }
                }
                //xmlreader.tianfus[i].Touched = false;
            }
        }
        //xulieloadinghelper(PlayerStep1Attack, 1);
        //xulieloadinghelper(PlayerStep2Attack, 1);
        //xulieloadinghelper(PlayerStep3Attack, 1);
        //xulieloadinghelper(PlayerStep1Defend, 1);
        //xulieloadinghelper(PlayerStep2Defend, 1);
        //xulieloadinghelper(PlayerStep2Defend, 1);
        step = 1;

    }

    void xulieloadinghelper(List<tianfu> L, int ii)
    {
        //Debug.Log(1);
        for (int i = 0; i < L.Count; i++)
        {
            //xmlreader.Loadingxulie(L[i], ii);
        }
    }

    void TianfuObjLoading(List<tianfu> L, GameObject[] G)
    {
        for (int i = 0; i < L.Count; i++)
        {
            G[i].SetActive(true);
            G[i].GetComponentInChildren<Text>().text = L[i].Name;
            if (L[i].AorD == "A") G[i].GetComponent<Image>().sprite = S[2];
            else if (L[i].AorD == "D") G[i].GetComponent<Image>().sprite = S[3];
        }
        //Debug.Log(L.ToString());
    }

    void Animeting()
    {
        //if (Timeer > 2 && !isplaying)
        if (isplaying == 0)
        {
            if (num[0] < (PlayerStep1Attack.Count + PCStep1Defend.Count))
            {
                if (Anihelp)
                {
                    a = animehelper(1, 0, PlayerStep1Attack, PCStep1Defend, true);
                    if (a != -1)
                    {
                        PCStep1Defend[a].played = true;
                    }
                }
                else animehelper(1, "侦查阶段，由我方先攻");
            }
            else if (num[1] < (PlayerStep1Defend.Count + PCStep1Attack.Count))
            {
                if (Anihelp)
                {
                    a = animehelper(11, 1, PCStep1Attack, PlayerStep1Defend, false);
                    if (a != -1)
                    {
                        PlayerStep1Defend[a].played = true;
                    }
                }
                else animehelper(11, "攻防转换");
            }
            else if (num[2] < (PlayerStep2Attack.Count + PCStep2Defend.Count))
            {
                if (Anihelp)
                {
                    a = animehelper(2, 2, PlayerStep2Attack, PCStep2Defend, true);
                    if (a != -1)
                    {
                        PCStep2Defend[a].played = true;
                    }
                }
                else animehelper(2, "干扰阶段开始");
            }
            else if (num[3] < (PlayerStep2Defend.Count + PCStep2Attack.Count))
            {
                if (Anihelp)
                {
                    a = animehelper(22, 3, PCStep2Attack, PlayerStep2Defend, false);
                    if (a != -1)
                    {
                        //Debug.Log(guanliantianfu.Name);
                        PlayerStep2Defend[a].played = true;
                    }
                }
                else
                {
                    //for (int ii = 0; ii < PlayerStep2Defend.Count; ii++)
                    //{
                    //    Debug.Log(PlayerStep2Defend[ii].played + "," + PlayerStep2Defend[ii].Name);
                    //}
                    animehelper(22, "攻防转换");
                }
            }
            else if (num[4] < (PlayerStep3Attack.Count + PCStep3Defend.Count))
            {
                if (Anihelp)
                {
                    a = animehelper(3, 4, PlayerStep3Attack, PCStep3Defend, true);
                    if (a != -1)
                    {
                        PCStep3Defend[a].played = true;
                    }
                }
                else animehelper(3, "摧毁阶段开始");
            }
            else if (num[5] < (PlayerStep3Defend.Count + PCStep3Attack.Count))
            {
                if (Anihelp)
                {
                    a = animehelper(33, 5, PCStep3Attack, PlayerStep3Defend, false);
                    if (a != -1)
                    {
                        PlayerStep3Defend[a].played = true;
                    }
                }
                else animehelper(33, "攻防转换");
            }
            else
            {
                step = 4;
                Timeer = 0;
            }
        }
    }

    /// <summary>
    /// 极其混沌，不要打开
    /// </summary>
    /// <param name="_step"></param>
    /// <param name="i"></param>
    /// <param name="_Player"></param>
    /// <param name="_PC"></param>
    /// <param name="XB"></param>
    int animehelper(int _step, int i, List<tianfu> _Player, List<tianfu> _PC, bool XB)
    {
        int retint = -1;
        //--------------------------------------关----------------------------------------------------
        xuliehao = 0;
        T[1].text = "";
        T[0].text = "";
        guanliantianfu = null;
        for (int k = 0; k < _PC.Count; k++)
        {
            _PC[k].playing = false;
        }
        for (int k = 0; k < _Player.Count; k++)
        {
            _Player[k].playing = false;
        }
        for (int k = 0; k < PC.Length; k++)
        {
            if (XB)
            {
                PC[k].GetComponent<Image>().sprite = S[3];
            }
            else
            {
                PC[k].GetComponent<Image>().sprite = S[2];
            }
        }
        for (int k = 0; k < Player.Length; k++)
        {
            if (XB)
            {
                Player[k].GetComponent<Image>().sprite = S[2];
            }
            else
            {
                Player[k].GetComponent<Image>().sprite = S[3];
            }
        }
        //--------------------------------------开----------------------------------------------------
        if (num[i] < _Player.Count)
        {
            if (!_Player[num[i]].played)
            {
                if (_Player[num[i]].AorD == "A")
                {
                    if (XB) Player[num[i]].GetComponent<Image>().sprite = S[0];
                    else PC[num[i]].GetComponent<Image>().sprite = S[0];
                }
                else
                {
                    if (XB) Player[num[i]].GetComponent<Image>().sprite = S[1];
                    else PC[num[i]].GetComponent<Image>().sprite = S[1];
                }
                if (XB) PLlight = num[i];
                else PClight = num[i];
                if (XB) T[0].text = _Player[num[i]].Info5;
                else T[1].text = _Player[num[i]].Info5;
                //Debug.Log(xmlreader.KeZhi.ContainsValue(_Player[num[i]].Name) + "," + _Player[num[i]].Name);
                if (xmlreader.KeZhi.ContainsValue(_Player[num[i]].Name))
                {
                    float Sou = 1;
                    for (int j = 0; j < _PC.Count; j++)
                    {
                        if (xmlreader.KeZhi[_PC[j]] == _Player[num[i]].Name)
                        {
                            Sou = float.Parse(StringS(_PC[j].Case)[1]) * Sou;
                            if (XB)
                            {
                                for (int n = 0; n < PC.Length; n++)
                                {
                                    PC[n].GetComponent<Image>().sprite = S[3];
                                    if (_PC[j].Name == PC[n].GetComponentInChildren<Text>().text)
                                    {
                                        PC[n].GetComponent<Image>().sprite = S[1];
                                        T[1].text = _PC[j].Info5;
                                        guanliantianfu = _PC[j];
                                        if (XB) PLlight = n;
                                        else PClight = n;
                                        retint = j;
                                    }
                                }
                            }
                            else
                            {
                                for (int n = 0; n < Player.Length; n++)
                                {
                                    Player[n].GetComponent<Image>().sprite = S[3];
                                    //    Debug.Log(_PC[j].Name + "," + Player[n].GetComponentInChildren<Text>().text);
                                    if (_PC[j].Name == Player[n].GetComponentInChildren<Text>().text)
                                    {
                                        Player[n].GetComponent<Image>().sprite = S[1];
                                        T[1].text = _PC[j].Info5;
                                        guanliantianfu = _PC[j];
                                        if (XB) PLlight = n;
                                        else PClight = n;
                                        retint = j;
                                    }
                                }
                            }
                        }
                    }
                    if (retint == -1)
                    {
                        if (XB) Socoring(PlayerScore, _Player[num[i]]);
                        else Socoring(PCSocre, _Player[num[i]]);
                    }
                    else
                    {
                        if (XB) Socoring(PlayerScore, _Player[num[i]], Sou);
                        else Socoring(PCSocre, _Player[num[i]], Sou);
                    }
                }
                else if (XB) Socoring(PlayerScore, _Player[num[i]]);
                else Socoring(PCSocre, _Player[num[i]]);
                _Player[num[i]].playing = true;
                sendstr = _Player[num[i]].Number.ToString();
                if (XB) sendstr = "PL;" + sendstr;
                else sendstr = "PC;" + sendstr;
                usingtimer = Time.time;
            }
        }
        else
        {
            if (!_PC[num[i] - _Player.Count].played)
            {
                T[0].text = "";
                if (_PC.Count <= 0) return -1;
                if (_PC[num[i] - _Player.Count].AorD == "D")
                {
                    if (XB) PC[num[i] - _Player.Count].GetComponent<Image>().sprite = S[1];
                    else Player[num[i] - _Player.Count].GetComponent<Image>().sprite = S[1];
                }
                else
                {
                    if (XB) PC[num[i] - _Player.Count].GetComponent<Image>().sprite = S[0];
                    else Player[num[i] - _Player.Count].GetComponent<Image>().sprite = S[0];
                }
                if (XB) PLlight = num[i] - _Player.Count;
                else PClight = num[i] - _Player.Count;
                if (XB) T[1].text = _PC[num[i] - _Player.Count].Info5;
                else T[0].text = _PC[num[i] - _Player.Count].Info5;
                if (xmlreader.KeZhi.ContainsValue(_PC[num[i] - _Player.Count].Name))
                {
                    float Sou = 1;
                    for (int j = 0; j < _Player.Count; j++)
                    {
                        if (xmlreader.KeZhi[_Player[j]] == _PC[num[i] - _Player.Count].Name)
                        {
                            Sou = float.Parse(StringS(_Player[j].Case)[1]) * Sou;
                            for (int n = 0; n < Player.Length; n++)
                            {
                                Player[n].GetComponent<Image>().sprite = S[3];
                                if (_Player[j].Name == Player[n].GetComponentInChildren<Text>().text)
                                {
                                    Player[n].GetComponent<Image>().sprite = S[1];
                                    T[1].text = _Player[j].Info5;
                                    guanliantianfu = _Player[j];
                                    if (XB) PLlight = n;
                                    else PClight = n;
                                    retint = j;
                                }
                            }
                        }
                    }
                    if (retint == -1)
                    {
                        if (XB) Socoring(PlayerScore, _PC[num[i] - _Player.Count]);
                        else Socoring(PCSocre, _PC[num[i] - _Player.Count]);
                    }
                    else
                    {
                        if (XB) Socoring(PlayerScore, _PC[num[i] - _Player.Count], Sou);
                        else Socoring(PCSocre, _PC[num[i] - _Player.Count], Sou);
                    }
                }
                else if (XB) Socoring(PlayerScore, _PC[num[i] - _Player.Count]);
                else Socoring(PCSocre, _PC[num[i] - _Player.Count]);
                _PC[num[i] - _Player.Count].playing = true;
                sendstr = _PC[num[i] - _Player.Count].Number.ToString();
                if (!XB) sendstr = "PL;" + sendstr;
                else sendstr = "PC;" + sendstr;
                usingtimer = Time.time;
            }
        }
        //-----------------------------------------通用------------------------------------------
        num[i]++;
        if (num[i] >= _Player.Count)
        {
            if (num[i] == _Player.Count + _PC.Count)
            {

            }
            else
            {
                while (_PC[num[i] - _Player.Count].played)
                {
                    num[i]++;
                }
            }
        }
        
        if (num[i] >= (_PC.Count + _Player.Count)) Anihelp = false;
        Timeer = 0;
        step = _step;
        isplaying = 3;
        return retint;
    }

    void animehelper(int _step, string fenshu)
    {
        TimerCtr = true;
        Tiptext.SetActive(true);
        Tiptext.GetComponent<Text>().text = fenshu;
        Invoke("Invoker", 3f);
        Timeer = 0;
        step = _step;
    }

    void Invoker()
    {
        TimerCtr = false;
        Tiptext.SetActive(false);
        Anihelp = true;
    }

    void SwitchDown()
    {
        for (int i = 0; i < 9; i++)
        {
            PC[i].SetActive(false);
            PC[i].GetComponent<Image>().sprite = S[2];
            Player[i].SetActive(false);
            Player[i].GetComponent<Image>().sprite = S[3];
            T[0].text = "";
            T[1].text = "";
        }
    }

    string[] StringS(string s)
    {
        string[] str = s.Split(';');
        //Debug.Log(str[0]);
        //Debug.Log(str[1]);
        return str;
    }

    void Socoring(Score S, tianfu T)
    {
        switch (T.step)
        {
            case 1:
                S.Step1Math(float.Parse(StringS(T.Case)[1]));
                huihefen = float.Parse(StringS(T.Case)[1]);
                break;
            case 2:
                switch (StringS(T.Case)[0])
                {
                    case "D":
                        S.Step2Math(int.Parse(StringS(T.Case)[1]));
                        huihefen = int.Parse(StringS(T.Case)[1]);
                        break;
                    case "DA":
                        S.Step2Math(float.Parse(StringS(T.Case)[1]), true);
                        huihefen = float.Parse(StringS(T.Case)[1]);
                        if (StringS(T.Case).Length > 2)
                        {
                            //Debug.Log("double,S2" + S.Step2[0]);
                            S.Step2Math(int.Parse(StringS(T.Case)[3]));
                            huihefen = int.Parse(StringS(T.Case)[3]);
                            //Debug.Log("step2," + S.Step2[0]);
                        }
                        break;
                    case "DB":
                        S.Step2Math(float.Parse(StringS(T.Case)[1]), false);
                        huihefen = float.Parse(StringS(T.Case)[1]);
                        break;
                }
                break;
            case 3:
                switch (StringS(T.Case)[0])
                {
                    case "DM":
                        //Debug.Log("case," + float.Parse(StringS(T.Case)[1]));
                        S.Step3Math(int.Parse(StringS(T.Case)[1]));
                        huihefen = int.Parse(StringS(T.Case)[1]);
                        break;
                    case "DC":
                        //Debug.Log("case," + float.Parse(StringS(T.Case)[1]));
                        S.Step3Math(float.Parse(StringS(T.Case)[1]));
                        if (StringS(T.Case).Length > 2)
                        {
                            //Debug.Log("double,S3" + S.Step3[0]);
                            S.Step3Math(int.Parse(StringS(T.Case)[3]));
                            huihefen = int.Parse(StringS(T.Case)[3]);
                            //Debug.Log("step3," + S.Step3[0]);
                        }
                        break;
                }
                break;
        }
    }

    void Socoring(Score S, tianfu T, float f)
    {
        switch (T.step)
        {
            case 2:
                //Debug.Log("f," + f);
                //Debug.Log("case," + float.Parse(StringS(T.Case)[1]));
                int b = Convert.ToInt32(float.Parse(StringS(T.Case)[1]) * f);
                //Debug.Log("b," + b);
                S.Step2Math(b);
                huihefen = b;
                break;
            case 3:
                //Debug.Log("f," + f);
                //Debug.Log("case," + float.Parse(StringS(T.Case)[1]));
                int c = Convert.ToInt32(float.Parse(StringS(T.Case)[1]) * f);
                //Debug.Log("c," + c);
                S.Step3Math(c);
                huihefen = c;
                break;
        }
    }

    public void AVCtr(string path)
    {
        //BGBD[1].SetActive(true);
        //_nextVideoLocation = MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
        path = CorE + ":\\DDZVEDIO\\" + path;
        _nextVideoLocation = MediaPlayer.FileLocation.AbsolutePathOrURL;
        if (!M.OpenVideoFromFile(_nextVideoLocation, path, false))
        {
            Debug.LogError("Failed to open video!");
            isplaying = 0;
        }
    }

    public void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et)
    {
        switch (et)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                //Debug.Log("play");
                M.Play();
                break;
            case MediaPlayerEvent.EventType.Started:
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
                //GatherProperties();
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                isplaying = 4;
                M.CloseVideo();
                break;
        }
        //AddEvent(et);
    }

    public void BGM2Change(string path)
    {
        path = CorE + ":\\DDZVEDIO\\" + path;
        M2Location = MediaPlayer.FileLocation.AbsolutePathOrURL;
        if (!M2.OpenVideoFromFile(M2Location, path, false))
        {
            Debug.LogError("Failed to open video!");
        }
    }

    public void M2ctr(MediaPlayer mp, MediaPlayerEvent.EventType et)
    {
        switch (et)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                break;
            case MediaPlayerEvent.EventType.Started:
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
                //GatherProperties();
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                break;
        }
        //AddEvent(et);
    }
}
