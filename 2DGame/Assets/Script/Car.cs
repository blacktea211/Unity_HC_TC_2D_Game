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
    }
}
