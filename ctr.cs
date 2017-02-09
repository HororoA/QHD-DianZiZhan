using UnityEngine;
using System.Collections;


public class ctr : MonoBehaviour {

    public Transform T;
    bool nextbegin = false;
    float now = 0;
    float last = 0;
    float len = 0;
	// Use this for initialization
	void Start () {
        InvokeRepeating("length", 0, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        while (true)
        {
            last = now;
            now = float.Parse(Input.mousePosition.x.ToString());
            len = Mathf.Abs(now - last) + len;
            if (nextbegin)
            {
                Debug.Log(len % 10);
                T.position = new Vector3(T.position.x + len % 10, T.position.y, T.position.z);
                len = 0; 
                break;
            }
        }
	}

    void length()
    {
        nextbegin = true;
    }
}
