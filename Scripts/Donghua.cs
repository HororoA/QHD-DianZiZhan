using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Donghua
{
    public tianfu T = new tianfu();

    public Vector3 qidian;
    public Vector3 zhongdian;
    public RectTransform R;
    //float timer = 0;

    public static Vector3 pingyi(Vector3 _qidian, Vector3 _zhongdian, Vector3 now)
    {
        float s1 = (_zhongdian.x - _qidian.x) / 180;
        float s2 = (_zhongdian.y - _qidian.y) / 180;
        now = new Vector3(now.x + s1, now.y + s2, now.z);
        return now;
    }

    public static Color32 gaoliang()
    {
        return new Color32();
    }

    public void playing(int i, float t)
    {
        //R.localPosition = pingyi(qidian, zhongdian, R.localPosition);
    }

    public static Sprite bofangxueliezhen(tianfu T, int j)
    {
        Sprite S = null;
        //Debug.Log(T.S.Count + "," + T.Name);
        List<Sprite> SB;
        if (j == 1)
        {
            SB = T.S;
        }
        else
        {
            SB = T.S2;
        }
        //Debug.Log(SB.Count);
        if (StepThreeCtr.xuliehao < SB.Count)
        {
            if (StepThreeCtr.guanliantianfu!=null)
            {
                for (int i = 0; i < xmlreader.BeiKezhi.Count; i++)
                {
                    if (xmlreader.BeiKezhi[i] == StepThreeCtr.guanliantianfu)
                    {
                        if (j == 1)
                        {
                            S = xmlreader.BeiKezhi[i].S[StepThreeCtr.xuliehao];
                        }
                        else
                        {
                            S = xmlreader.BeiKezhi[i].S2[StepThreeCtr.xuliehao];
                        }
                    }
                }
            }
            else
            {
                S = SB[StepThreeCtr.xuliehao];
            }
            StepThreeCtr.isplaying = 1;
        }
        else
        {
            T.playing = false;
            StepThreeCtr.isplaying = 0;
            StepThreeCtr.xuliehao = 0;
            //S = xmlreader.tianfus[26].S[0];
            S = xmlreader.S2;
        }
        return S;
    }

}
