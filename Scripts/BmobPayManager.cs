using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//支付类型
public enum PayType
{
    Zhifubao,
    Weixin
}
public class BmobPayManager : MonoBehaviour 
{
    public static BmobPayManager Instance { get; private set; }


    public GameObject payTypeGameObject;
    public Text messageText;
    private float price;
    private string foodName;
    private PayType payType=PayType.Zhifubao;
	// Use this for initialization
    void Awake() 
    {
        Instance = this;
    }
	void Start () 
    {
	    messageText.text = "";
	}

    //用来处理购买按钮的点击事件
    public void OnBuyButtonClick(float price,string foodName)
    {
        this.price = price;
        this.foodName = foodName;
        payTypeGameObject.SetActive(true);//显示支付类型的窗口让用户选择支付的渠道(支付宝，还是微信)
    }

    public void OnPaytypeZhifubaoChange(bool isSelected)
    {
        if (isSelected == true) 
        {
            payType=PayType.Zhifubao;
        }
    }

    public void OnPaytypeWeixinChange(bool isSelected) 
    {
        if (isSelected == true) 
        {
            payType=PayType.Weixin;
        }
    }

    public void OnCloseButtonClick() 
    {
        payTypeGameObject.SetActive(false);
    }

    public void OnSureButtonClick() 
    {
        //发起支付请求
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");//得到当前的主Activity

        if (payType == PayType.Zhifubao) 
        {
            jo.Call("payByZhifubao", price, foodName);
        }
        else
        {
            jo.Call("payByWeixin",price,foodName);
        }

    }

    //用来处理支付结果的
    public void OnPayResultReturn(string arg0)
    {
        string[] strArrray = arg0.Split('|');
        payTypeGameObject.SetActive(false);
        switch (strArrray[0]) {
            case "0":
                messageText.text = strArrray[1];
                break;
            case "1":
                messageText.text = "支付成功";
                break;
            case "2":
                messageText.text = "网络有问题";
                break;
        }
    }

}
