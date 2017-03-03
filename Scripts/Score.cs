using UnityEngine;
using System.Collections;

public class Score
{
    public float Step1 = 1; //第一步分数核算
    public float[] Step2 = { 0, 1 }; //第二步分数，第一项为得分，第二项为影响第三项的系数
    public float[] Step3 = { 0, 1 }; //第三步分数核算
    public float Step4 = 0; //总分核算

    public void Step4Math()
    {
        Step4 = Step1 * (Step2[0] + (Step2[1] * (Step3[0] * Step3[1])));
        //Debug.Log(Step4);
    }

    public void Step3Math(int a)
    {
        Step3[0] = Step3[0] + a;
    }

    public void Step3Math(float a)
    {
        Step3[1] = Step3[1] * a;
    }

    public void Step2Math(int a)
    {
        Step2[0] = a + Step2[0];
    }

    public void Step2Math(float a, bool b)
    {
        if (b)
        {
            Step2[1] = a * Step2[1];
        }
        else
        {
            Step2[0] = a * Step2[0];
        }
    }

    public void Step1Math(float S)
    {
        //Step1 = Step1 * S;
        Step1 = Step1 + S;
    }

    public void ClearScore()
    {
        Step1 = 1;
        Step2[0] = 0;
        Step2[1] = 1;
        Step3[0] = 0;
        Step3[1] = 1;
        Step4 = 0;
    }
}
