using System;
using UnityEngine;

public class APIStatic : MonoBehaviour
{
    private void Start()
    {
        print(Mathf.PI);
        print(Mathf.NegativeInfinity);
        print(Mathf.Infinity);
        print(Camera.allCamerasCount);
        print(Input.anyKey);
        print(Time.time);
        int number = Mathf.Abs(-999);
        print("隨機範圍1-35" + UnityEngine.Random.Range(1, 35));
        print("隨機範圍0.1-55.5+" +UnityEngine. Random.Range(0.1f, 55.5f));
       // print("打開UnityAPIMathf網頁"+Application.OpenURL ("https://docs.unity3d.com/2019.2/Documentation/ScriptReference/Mathf.html"));
        print("10.37去小數點後的值"+Mathf.Floor(10.37f));


    Time.timeScale=0.5f;
    Cursor.visible=false;

    }

    private void print(Func<float, int> floorToInt)
    {
        throw new NotImplementedException();
    }


    private void Update()
    {
        
        //Input.GetKeyDown = true; 
    }

}

