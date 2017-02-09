using UnityEngine;
using System.Collections;

public class fps : MonoBehaviour {

    public int targetFrameRate = 60;

    //当程序唤醒时
    void Awake()
    {
        //修改当前的FPS
        Application.targetFrameRate = targetFrameRate;
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
