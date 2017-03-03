using UnityEngine;
using System.Collections;

public class StepOneCtr : MonoBehaviour {

	// Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame() 
    {
        HoleCtrl.IsStep1 = false;
        HoleCtrl.IsStep2 = true;
        udpouter.SocketSend("Step1");
    }
}
