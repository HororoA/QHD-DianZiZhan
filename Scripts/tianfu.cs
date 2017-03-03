using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tianfu
{
    public bool Touched = false;                    //是否选择
    public string AorD;                             //进攻或防守
    public string Name;                             //天赋名字
    public string Info;                             //天赋信息
    public string Info2;                            //天赋消耗点数
    public string Info3;                            //天赋威力伤害
    public string Info4;                            //天赋克制关系（一定为其他天赋名称）
    public string Info5;                            //天赋特殊效果
    public int step;                                //天赋属于阶段（共3各阶段）
    public string Case;                             //天赋威力系数
    public string Locate;                           //天赋在游戏内对应的的物体名称
    public string CombainNum = string.Empty;        //激活此天赋需要的天赋名称
    public int Number;                              //天赋在xml表格里的序号，一般也为读取后存入的序号
    public bool played = false;                     //是否对应时被提前播放动画
    public bool playing = false;                    //是否正在播放动画
    public List<Sprite> S = new List<Sprite>();     //序列帧（左至右）
    public List<Sprite> S2= new List<Sprite>();     //序列帧（右至左）
    public int Slength;                             //序列帧长度
    public string NewDownStr;                       //下表
}
