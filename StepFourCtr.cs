﻿using UnityEngine;
using System.Collections;
using System;

public class StepFourCtr : MonoBehaviour
{

    public UnityEngine.UI.Text[] T;
    public GameObject[] wrl;


    double d1;
    double d2;
    double dd1;
    double dd2;
    double df1 = 0;
    double df2 = 0;

    float Timer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HoleCtrl.IsStep4)
        {
            Timer = Timer + Time.deltaTime;
            if (Timer < 0.5f)
            {
                d1 = Math.Round(NewStepThreeCtr.PlayerScore.Step4, 0);
                d2 = Math.Round(NewStepThreeCtr.PCSocre.Step4, 0);
                dd1 = d1 / 120;
                dd2 = d2 / 120;
            }
            if (Timer >= 0.5f && Timer < 2.5f)
            {
                jifenqi();
            }

            if (Timer >= 2.5f)
            {
                if (d1 < 1000)
                {
                    T[0].text = "00" + d1.ToString();
                }
                else if (d1 >= 1000 && d1 < 10000)
                {
                    T[0].text = "0" + d1.ToString();
                }
                else if (d1 >= 10000)
                {
                    T[0].text = d1.ToString();
                }
                if (d2 < 1000)
                {
                    T[1].text = "00" + d2.ToString();
                }
                else if (d1 >= 1000 && d1 < 10000)
                {
                    T[1].text = "0" + d2.ToString();
                }
                else if (d1 >= 10000)
                {
                    T[1].text = d2.ToString();
                }
                if (NewStepThreeCtr.PlayerScore.Step4 > NewStepThreeCtr.PCSocre.Step4)
                {
                    wrl[0].SetActive(true);
                }
                else
                {
                    wrl[1].SetActive(true);
                }
            }
        }
    }

    void jifenqi()
    {
        df1 = df1 + dd1;
        df2 = df2 + dd2;

        if (df1 < 10)
        {
            T[0].text = "0000" + df1.ToString();
        }
        else if (df1 >= 10 && df1 < 100)
        {
            T[0].text = "000" + df1.ToString();
        }
        else if (df1 >= 100 && df1 < 1000)
        {
            T[0].text = "00" + df1.ToString();
        }
        else if (df1 >= 1000 && df1 < 10000)
        {
            T[0].text = "0" + df1.ToString();
        }
        else if (df1 >= 10000)
        {
            T[0].text = df1.ToString();
        }

        if (df2 < 10)
        {
            T[1].text = "0000" + df2.ToString();
        }
        else if (df2 >= 10 && df2 < 100)
        {
            T[1].text = "000" + df2.ToString();
        }
        else if (df2 >= 100 && df2 < 1000)
        {
            T[1].text = "00" + df2.ToString();
        }
        else if (df2 >= 1000 && df2 < 10000)
        {
            T[1].text = "0" + df2.ToString();
        }
        else if (df2 >= 10000)
        {
            T[1].text = df2.ToString();
        }

    }

    public void BackToStart()
    {
        HoleCtrl.IsStep1 = true;
        HoleCtrl.IsStep4 = false;
        NewStepThreeCtr.PlayerScore.ClearScore();
        NewStepThreeCtr.PCSocre.ClearScore();
        for (int i = 0; i < xmlreader.tianfus.Length; i++)
        {
            xmlreader.tianfus[i].Touched = false;
        }
        Timer = 0;
        df1 = 0;
        df2 = 0;
        wrl[0].SetActive(false);
        wrl[1].SetActive(false);
    }
}
