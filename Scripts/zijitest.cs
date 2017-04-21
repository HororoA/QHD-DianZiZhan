using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class zijitest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    List<tianfu> ListRemove(List<tianfu> L, int i)
    {
        L.RemoveAt(i);
        return L;
    }

    List<tianfu> Zhong = new List<tianfu>();

    public void JJBQLWTMSB(int num)
    {
        List<tianfu> L = new List<tianfu>();
        switch (num)
        {
            case 5:
                for (int i = 0; i < 12; i++)
                {
                    L.Add(xmlreader.tianfus[i]);
                }
                break;
            case 6:
                for (int i = 12; i < 22; i++)
                {
                    L.Add(xmlreader.tianfus[i]);
                }
                break;
            case 9:
                for (int i = 22; i < 36; i++)
                {
                    L.Add(xmlreader.tianfus[i]);
                }
                break;
        }
        if (JJBQL(L, num))
        {
            string s = string.Empty;
            string s2 = string.Empty;
            for (int i = 0; i < Zhong.Count; i++)
            {
                s = s + Zhong[i].Info2.ToString() + ";";
                s2 = s2 + Zhong[i].Number + ";";
            }
            Debug.Log("消耗：" + s);
            Debug.Log("编号：" + s2);
        }
        else
        {
            Debug.Log("給得");
            Zhong.Clear();
            JJBQLWTMSB(num);
        }
        Zhong.Clear();
    }

    bool JJBQL(List<tianfu> LL, int Mix)
    {
        if (LL.Count > 0)
        {
            int now;
            int Ram = UnityEngine.Random.Range(0, LL.Count - 1);
            now = Mix - int.Parse(LL[Ram].Info2);
            if (now == 0)
            {
                Zhong.Add(LL[Ram]);
                return true;
            }
            else if (now < 0)
            {
                Debug.Log("错误记录");
                return JJBQL(ListRemove(LL, Ram), Mix);
            }
            else
            {
                Zhong.Add(LL[Ram]);
                return JJBQL(ListRemove(LL, Ram), now);
            }
        }
        else
        {
            return false;
        }
    }
}
