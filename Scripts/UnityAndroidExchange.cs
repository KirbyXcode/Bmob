using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityAndroidExchange : MonoBehaviour 
{
    public Text text;

    public void OnClick() 
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");//获取当前activity的对象 它就代表Android中的MainAcitivity
        int res = jo.Call<int>("test1", 56, 90);
        //print(res+"get result");
        text.text += res.ToString();
    }

    public void Test2(string str) 
    {
        text.text += str;
    }
}
