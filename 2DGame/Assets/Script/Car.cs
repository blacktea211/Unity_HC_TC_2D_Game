 using UnityEngine;

public class Car : MonoBehaviour
{
    //單行註解
    // 備註=數值
    // 尺寸3m
    // 重量
    // 顏色

    //指定值
    //私人 private 不會顯示在屬性面板
    //公開 public  會顯示在屬性面板


    #region 欄位
    //欄位屬性
    //標題Header
    //提示Tooltip
    [Header("車子尺寸"), Range(0, 500)]
    public int size = 3;
    [Header("車子重量"), Tooltip("車子重量單位:噸")]
    public float weight = 300.7f;
    public string brand = "toyota";
    public bool skyWindow = true;

    //其他常用類型
    //顏色 Color
    public Color colorA = Color.red;//內建顏色
    public Color colorB = new Color(0, 0, 1);//內建顏色 (R,G,B)
    public Color colorC = new Color(0,0, 1, 0.5f);//內建顏色 (R,G,B,A透明度)

    //向量 Vector 2-4 (2維~4維)
    public Vector2 v2A = Vector2.zero; //2維
    public Vector2 v2B = Vector2.one;
    public Vector2 v2C = new Vector2 (0, 1);

    public Vector3 v3A = new Vector3(0, 1, 1); //3維
    public Vector4 v4A = new Vector4(0, 1, 1, 1); //4維
    
    //音效
    public AudioClip soundExplosion;

    //圖片
    public Sprite spriteA;

    //遊戲物件與預置物 GameObjet
    public GameObject gameObject1;

    //元件:屬性面板上的"粗體字"
    public Transform tra;

    public Sprite Render;
    #endregion

    #region 事件

    //事件-開始事件 (訊息)
    private void Start()
    {
        print("helloworld");

        //取得欄位(抓出"屬性面板上"的資料)
        print(size);
        print(weight);

        //設定欄位(修改資料)
        weight = 100.5f;
        skyWindow = false;


        //呼叫自訂方法
        //呼叫方法語法:自訂方法名稱();
        MethodA();
        MethodB();
        MethodC();

        //區域變數
        //類型 區域變數名稱 指定 值
        //僅限於此區域使用 {大括號}
        int intA = MethodB();
        print(intA);

       

        float b = BMI(60, 1.7f);
        print ("我的BMI");

        Drive(50);
        Drive(100, "後方");

    }


    #endregion


    #region 方法
    //一個程式包含欄位,事件,方法

    //欄位語法:
    //修飾詞,類型,欄位名稱,指定 值

    //方法語法:
    //修飾詞(public,private),傳回類型,方法名稱,(),{}
    //無傳回類型 void : 沒有任何回傳資料

    private void MethodA()
    {
        print("我是方法");
    }

    private int MethodB ()
    {
        return 123;
    }

    private float PI ()
    {
     return  3.14f;
    }

   private Vector3 v3 ()
    {
        return new Vector3(1, 2, 3);
    }

    private void MethodC(int nimber)
    {
        number += 10;   //累加後的數字+10 ex number=1 ,  Method =>   number += 10  =11
        print("累加後的數字+10");
    }

    private float BMI (float w,float h)
    {
        float bmi = w / (h * h);
        return bmi;
    }

    /// <summary>
    /// 車子移動速度及方向
    /// </summary>
    /// <param name="speed">"時速"</param>
    /// <param name="direction">"方向"</param>
    private void Drive(int speed, string direction = "前方")
    {
        print("時速:" + speed);
        print("方向:" + direction);
    }
        #endregion


    }

