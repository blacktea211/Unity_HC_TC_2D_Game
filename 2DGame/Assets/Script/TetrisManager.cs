using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    public float falltime = 1.5f;
    [Header("掉落時間"), Range(0.1f, 3)]

    public int score;
    [Header("目前分數")]

    public int hightscore;
    [Header("最高分數")]

    public int level = 1;
    [Header("等級")]

    public GameObject gmaeover;
    [Header("結束畫面")]

    public AudioClip fallsound;
    [Header("方塊掉落音效")]

    public AudioClip movesound;
    [Header("方塊移動與旋轉音效")]

    public AudioClip cleansound;
    [Header("方塊消除音效")]

    [Header("方塊消除音效")]
    public AudioClip endsound;

    [Header("下一個俄羅斯方塊區域")]
    public Transform traNextArea;

    [Header("畫布")]
    public Transform  traCanvas;


    /// <summary>
    /// 下一個俄羅斯方塊編號
    /// </summary>
    public int indexnext;

    private void Start()
    {
        SpawnTetris();


    }

    //方法
    /// <summary>
    /// 生成俄羅斯方塊
    /// 隨機顯示下一個方塊(隨機顯示0~10的方塊)
    /// </summary>
    private void SpawnTetris()
    {
        //下一個編號 = 隨機的範圍(最小,最大)
        //Random .Range不會顯示最大值 , Random .Range (0,11) => 隨機顯示0~10的方塊
        indexnext = Random.Range(0, 11);

        //下一個俄羅斯方塊區域 . 取得子物件(子物件編號) . 轉為遊戲物件 . 啟動設定(顯示)
        traNextArea.GetChild(indexnext).gameObject.SetActive(true);

    }

    /// <summary>
    /// 開始遊戲
    /// 1.生成俄羅斯方塊放在正確位置
    /// 2.上一次俄羅斯方塊隱藏
    /// 3.隨機取得下一個方塊
    /// </summary>
    public void StartGame()
    {
        //1.生成俄羅斯方塊要放的位置
        //保存上一次的俄羅斯方塊
        GameObject tetris = traNextArea.GetChild(indexnext).gameObject;

        //目前俄羅斯方塊=生成物件(物件，父物件)
        GameObject current = Instantiate(tetris, traCanvas);

        //GetComponent<任何元件>()
        //<T>泛型 - 指所有類型
        //目前俄羅斯方塊 . 取得元件<介面變形>() . 座標 .二維向量
        current.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, 400);

        //2.上一次俄羅斯方塊隱藏
        tetris.SetActive(false);
        //3.隨機取得下一個
        SpawnTetris();
    }
   
    public  void Addscore()
    {
         
    }

    private void Gametime()
    {
    }

    private void End()
    {
    }

    public void Again()
    {
    }

    public void QuitGame()
    {
    }

}