 using UnityEngine;
using System.Collections;    //引用系統集合API - 協同程序

public class TetrisManager : MonoBehaviour
{
    [Header("掉落時間"), Range(0.1f, 3)]
    public float falltime = 1.5f;

    [Header("目前分數")]
    public int score;

    [Header("最高分數")]
    public int hightscore;

    [Header("等級")]
    public int level = 1;

    [Header("結束畫面")]
    public GameObject gmaeover;

    [Header("方塊掉落音效")]
    public AudioClip fallsound;

    [Header("方塊移動與旋轉音效")]
    public AudioClip movesound;

    [Header("方塊消除音效")]
    public AudioClip cleansound;

    [Header("方塊消除音效")]
    public AudioClip endsound;

    [Header("下一個俄羅斯方塊區域")]
    public Transform traNextArea;

    [Header("生成俄羅斯方塊的父物件")]
    public Transform traTetrisParent;


    [Header("各方塊起始位置")]
    public Vector2[] posSpawn =
    {
    new Vector2(0,360),
    new Vector2(15,360),
    new Vector2(-15,360),
    new Vector2(15,360),
    new Vector2(-15,360),
    new Vector2(15,375),
    new Vector2(15,345),
    new Vector2(-15,360),
    new Vector2(-15,360),
    new Vector2(0,360),
    new Vector2(0,360),
    new Vector2(15,345)
   };



    /// <summary>
    /// 下一個俄羅斯方塊編號
    /// </summary>
    public int indexnext;


    /// <summary>
    /// 目前俄羅斯方塊
    /// </summary>
    public RectTransform currentTetris;

    /// <summary>
    /// 計時器
    /// </summary>
    public float timer; 



    private void Start()
    {
        SpawnTetris();


    }


    private void Update()
    {
        ControlTertis();
        //Alpha1 鍵盤左上數字鍵1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            // 呼叫協同程序 +StartCoroutine
            StartCoroutine(ShakeEffect());
        }

    }


    #region  控制方塊
    private void ControlTertis()
    {
        if (currentTetris)
        {


            //  +=  累加 , 計時器 = 時間 + 每一幀(每秒的影格)的時間 =>累加時間
            timer += Time.deltaTime;

            if (timer >= falltime) //判斷式 > bool > 成立 > true 
                                   //              >不成立 > false 
            {
                timer = 0; //計時器歸0
                currentTetris.anchoredPosition -= new Vector2(0, 30);
            }

            //取得俄羅斯方塊Tetris腳本
            Tetris tetris = currentTetris.GetComponent<Tetris>();



            //如果 " 沒有 " 碰到 右邊牆壁，在執行以下動作 (讓方塊往右)
            if (!tetris.WallRight)
            {

                //如果方塊的 X軸 < 260 ,不超過右邊邊界
                //if (currentTetris.anchoredPosition.x < 220)


                // 或者||
                // 按下 D 或 鍵盤右鍵 使方塊往右30
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentTetris.anchoredPosition += new Vector2(30, 0);
                }
                           
            }


            //如果 " 沒有 " 碰到 左邊牆壁，在執行以下動作 (讓方塊往左)
            if (!tetris.WallLeft)
            {
                            
                //if (currentTetris.anchoredPosition.x > -220)
            
                // 按下 A 或 鍵盤左鍵 使方塊往左30
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentTetris.anchoredPosition -= new Vector2(30, 0);
                }

            }

          



            // 按下 W 或 鍵盤上鍵 使方塊逆時針旋轉90度
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                //屬性慢板上的 Rotation 必須用 eulerAngles 控制 , eulerAngles ( 0度~360度 )
                currentTetris.eulerAngles += new Vector3(0, 0, 90);

                tetris.Offset();
            }

            // 按下 空白建 或 鍵盤下鍵 就加速
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.DownArrow))
            {
                falltime = 0.1f ;            
            }
            //否則恢復速度
            else
            {
                falltime = 1.5f;
            }

          // if (currentTetris .anchoredPosition .y == -300)
            //{
              //  StartGame();
            //}


            //如果方塊碰到地板，StartGame (重新生成方塊)
            if (tetris.WallDown)
            {

                StartGame();

            }

        }

    }
    #endregion




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


        //測試
      indexnext = 6 ;




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
        GameObject current = Instantiate(tetris, traTetrisParent);

        //GetComponent<任何元件>()
        //<T>泛型 - 指所有類型
        //目前俄羅斯方塊 . 取得元件<介面變形>() . 座標 .方塊起始位置
        current.GetComponent<RectTransform>().anchoredPosition = posSpawn [indexnext];

        //2.上一次俄羅斯方塊隱藏
        tetris.SetActive(false);
        //3.隨機取得下一個
        SpawnTetris();

        //將生成的俄羅斯方塊 RectTransform 元件儲存,當開始遊戲，Unity自動抓取方塊元件
        currentTetris=current.GetComponent<RectTransform>();
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

    // 協同程序 [程式執行時，執行此程式 ( 在此程式中執行第一個指令，等待幾秒後在繼續下一個指令 ) 期間，其他程式持續執行不受影響]
    //可用於須等待時間(ex：塔防遊戲)
    // 傳回類型 - 時間
    // yeli 讓步、等待
    private IEnumerator ShakeEffect()
    {
        float interval = 0.5f;

        // RectTransform 介面變形
        RectTransform rect = traTetrisParent.GetComponent<RectTransform>();
        yield return new WaitForSeconds(1f);


        //Y軸向上20過0.5秒再向上15再過0.5秒...
        rect.anchoredPosition += Vector2.up * 20; ;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition += Vector2.up * 15; ;
        yield return new WaitForSeconds(interval);
    }
}