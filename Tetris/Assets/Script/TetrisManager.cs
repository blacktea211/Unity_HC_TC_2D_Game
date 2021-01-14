using UnityEngine;
using UnityEngine.UI;         //引用系統引擎 - 介面
using UnityEngine.SceneManagement;
using System.Collections;     //引用系統集合API - 協同程序
using System.Linq;            // 引用 系統.查詢語言 API - 偵測陣列、清單內的資料

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
    public GameObject gameover;

    [Header("方塊掉落音效")]
    public AudioClip fallsound;

    [Header("方塊移動音效")]
    public AudioClip movesound;

    [Header("方塊旋轉音效")]
    public AudioClip rotationsound;

    [Header("方塊消除音效")]
    public AudioClip cleansound;

    [Header("遊戲結束音效")]
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

    private AudioSource aud;

    // 開始事件：開始後執行一次
    private void Start()
    {
        aud = GetComponent<AudioSource>();
        SpawnTetris();      
    }

    /// <summary>
    /// 是否遊戲結束
    /// </summary>
    private bool gameOver;


    // 更新事件：一秒執行約60次
    private void Update()
    {
        if (gameOver) return;    // 如果 遊戲結束 就跳出
        {

        }
        ControlTertis();

        FastDown();

        //Alpha1 鍵盤左上數字鍵1
       // if (Input.GetKeyDown(KeyCode.Alpha1))
        //{

            
            // 呼叫協同程序 +StartCoroutine
         //   StartCoroutine(ShakeEffect());
      //  }

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



            //如果 " 沒有 " 碰到 右邊牆壁 及 右邊小方塊，在執行以下動作 (讓方塊往右)
            if (!tetris.WallRight && !tetris.smallRight)
            {

                //如果方塊的 X軸 < 260 ,不超過右邊邊界
                //if (currentTetris.anchoredPosition.x < 220)

               

                // 或者||
                // 按下 D 或 鍵盤右鍵 使方塊往右30
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    aud.PlayOneShot(movesound, Random.Range(0.5f, 1f));
                    currentTetris.anchoredPosition += new Vector2(30, 0);
                }
                           
            }


            //如果 " 沒有 " 碰到 左邊牆壁 及 左邊小方塊，在執行以下動作 (讓方塊往左)
            if (!tetris.WallLeft && !tetris.smallLeft)
            {                
                //if (currentTetris.anchoredPosition.x > -220)

                // 按下 A 或 鍵盤左鍵 使方塊往左30
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    aud.PlayOneShot(movesound, Random.Range(0.5f, 1f));
                    currentTetris.anchoredPosition -= new Vector2(30, 0);
                }

            }

          



            // 按下 W 或 鍵盤上鍵 使方塊逆時針旋轉90度
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                aud.PlayOneShot(rotationsound, Random.Range(0.5f, 1f));

                //屬性慢板上的 Rotation 必須用 eulerAngles 控制 , eulerAngles ( 0度~360度 )
                currentTetris.eulerAngles += new Vector3(0, 0, 90);

                tetris.Offset();
            }


            if (!fastDown)  //如果沒有 快速落下  才能 加速
            {


                // 按下 S 或 鍵盤下鍵 就加速
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    falltime = 0.1f;
                }
                //否則恢復速度
                else
                {
                    falltime = falltimeMax;
                }

                // if (currentTetris .anchoredPosition .y == -300)
                //{
                //  StartGame();
                //}

            }

            //如果方塊碰到地板  或  小方塊底部碰撞
            if (tetris.WallDown || tetris.smallBottom)
            {
                //注意順序

                SetGround();                        //方塊遍地板後
                StartCoroutine(CheckTetris());      //檢查方塊是否連成一線
                StartGame();                        //重生一顆方塊
                StartCoroutine(ShakeEffect());      //啟動偕同程序(協同方法())  ,  晃動效果
                              
            }

        }

    }
    #endregion

    /// <summary>
    /// 設定方塊掉落後為方塊
    /// </summary>
    private void SetGround()
    {
        aud.PlayOneShot(fallsound, Random.Range(0.5f, 1f));

        /**迴圈：for (for + tab*2下)
        // 迴圈 =>  1.初始值  2.條件判斷  3.敘述 print()  4.迭代器 (運算遞增/遞減)
        // 初始值 " 只跑第一次 " => 迴圈 ：1234 => 234 => 234 ...
         */

        int count = currentTetris.childCount;                 //取得 目前方塊 的子物件
        for (int i = 0; i < count; i++)                       //迴圈執行子物件數量次數  (抓每顆方塊裡的方塊數量)
        {
            currentTetris.GetChild(i).name = "方塊";           //小方塊名稱改為方塊
            currentTetris.GetChild(i).gameObject.layer =10;   //小方塊圖層改為方塊
        }


        // 將俄羅斯 小方塊 搬到 分數區域
        for (int i = 0; i < count; i++)
        {
            currentTetris.GetChild(0).SetParent(traScoreArea);
        }
        // 刪除 父物件
        Destroy(currentTetris.gameObject);
    }

    [Header("分數判定區域")]
    public Transform traScoreArea;
    /// <summary>
    /// 記錄所有分數在判定區域的小方塊
    /// </summary>
    public RectTransform[] restsmall;
    /// <summary>
    /// 要刪除的列數
    /// </summary>
    public bool[] destroyRow = new bool[23];
    /// <summary>
    /// 剩下的方塊要掉落的高度
    /// </summary>
    public float[] downHight;

    /// <summary>
    /// 檢查方塊是否連成一線
    /// </summary>
    private IEnumerator CheckTetris()
    {
        restsmall = new RectTransform[traScoreArea.childCount];     //指定數量跟子物件相同

        for (int i = 0; i < traScoreArea.childCount; i++)            //利用迴圈將子物件儲存
        {
            restsmall[i] = traScoreArea.GetChild(i).GetComponent<RectTransform>();

            float y = restsmall[i].anchoredPosition.y;              //取得 方塊y軸 的位置，檢查是否失敗
            if (y >= 360 - 15 && y <= 360 + 15) GameOver();         //檢查 y軸 是否介於 345 ~ 375 之間  => 是 => 遊戲結束
           
        }

        int row = 23;    //總共 23 列
        for (int i = 0; i <row; i++)
        {
            float bottom = -315;   //最底層的位置
            float step = 30;       //每一層的間隔


            // 檢查陣列內 等於 true 的資料
            // 陣列.哪裡 ( 代名詞 => 條件 )
            // var 無類型

            // 檢查有幾顆小方塊的位置 在 y 軸 -300 正負15 避免誤差值
            var small = restsmall.Where(x => x.anchoredPosition.y >= bottom + step * i - 15 && x.anchoredPosition.y <= bottom + step * i + 15);

            //消除
            //如果一排有17顆小方塊 (放滿一排)
            if (small.ToArray().Length == 17)
            {
                aud.PlayOneShot(cleansound, Random.Range(0.5f, 1f));
                yield return StartCoroutine(Shine(small.ToArray()));       //開始閃爍
                destroyRow[i] = true;                                      //紀錄要刪除的列數
                Addscore(1000);                                            //增加分數

            }
        }

        downHight = new float[traScoreArea.childCount];               //紀錄有幾顆 刪除後的方塊


        for (int i = 0; i < downHight.Length; i++) downHight[i] = 0;   //先將掉落高度調為0

        //記錄每顆方塊掉下的高度
        for (int i = 0; i < destroyRow.Length;i++)
        {
            if (!destroyRow[i]) continue;                                       //如果 此列 沒有要刪除 就跳過 繼續下一列
            {
                for (int j = 0; j < restsmall.Length; j++)                      //迴圈 執行 每一顆剩下的方塊
                {
                    if (restsmall[j].anchoredPosition.y > -300 + 30*i -15)      //如果 此方塊 Y軸 大於 要刪除的列
                    {
                        downHight[j] -= 30;                                     //座標 遞減 30
                    }
                }
            }
            destroyRow[i] = false;                                              //恢復為不刪除
        }
        
        //更新小方塊的高度：往下掉
        for (int i = 0; i < restsmall.Length;i++)
        {
            restsmall[i].anchoredPosition += Vector2.up * downHight[i];
        }
    }

    /// <summary>
    /// 方塊連線時閃爍，閃爍後刪除方塊
    /// </summary>
    /// <param name="smalls"></param>
    /// <returns></returns>
    private IEnumerator Shine(RectTransform[] smalls)
    {
        float interval = 0.1f;
        for (int i = 0; i < 17; i++) smalls[i].GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 17; i++) smalls[i].GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 17; i++) smalls[i].GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 17; i++) smalls[i].GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(interval);

        // 刪除
        yield return new WaitForSeconds(interval);  //延遲
        for (int i = 0; i < 17; i++) Destroy(smalls[i].gameObject);  //用迴圈判斷 集滿一排時刪除小方塊

        // 重新取得小方塊： 避免 陣列中 方塊 Missing 導致錯誤
        yield return new WaitForSeconds(interval);
        restsmall = new RectTransform[traScoreArea.childCount];     //指定數量跟子物件相同

        for (int i = 0; i < traScoreArea.childCount; i++)           //利用迴圈將子物件儲存
        {
            restsmall[i] = traScoreArea.GetChild(i).GetComponent<RectTransform>();
        }
        
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


        //測試
    //  indexnext = 11;




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
        fastDown = false ;    //  碰到地板後，沒有快速落下

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





    /// <summary>
    /// 協同程序
    /// </summary>
    /// <returns></returns>
   
    // 協同程序 [程式執行時，執行此程式 ( 在此程式中執行第一個指令，等待幾秒後在繼續下一個指令 ) 期間，其他程式持續執行不受影響]
    // 可用於須等待時間(ex：塔防遊戲)
    // 傳回類型 - 時間
    // yeli 讓步、等待

    //畫面震動
    private IEnumerator ShakeEffect()
    {
        float interval = 0.05f;

        // RectTransform 介面變形
        RectTransform rect = traTetrisParent.GetComponent<RectTransform>();
      //  yield return new WaitForSeconds(1f);


        //Y軸向上20過0.5秒再向上15再過0.5秒...
        rect.anchoredPosition += Vector2.up * 20; ;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition += Vector2.up * 15; ;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(interval);
    }


    /// <summary>
    /// 是否快速落下
    /// </summary>
    private bool fastDown;
   
    /// <summary>
    /// 快速掉落功能
    /// </summary>
    private void FastDown()
    {
        if (currentTetris && !fastDown)             //如果有 目前的方塊 且 沒有快速落下
        {

            
            if (Input.GetKeyDown(KeyCode.Space))    //如果 按下 空白鍵 
            {              
                fastDown = true;                    //快速掉落
                falltime = 0.02f;                   //掉落時間              
            }            

        }
    }

    [Header("分數文字")]
    public Text textScore;
    [Header("等級文字")]
    public Text TextLv;
    /// <summary>
    /// 掉落時間最大值
    /// </summary>
    private float falltimeMax = 1.5f;
    /// <summary>
    /// 添加分數 等級
    /// </summary>
    public void Addscore(int add)
    {
        score += add;                                           // 分數增加
        textScore.text = "分數：" + score;                      // 更新介面

        level = 1 + score / 1000;                              // 等級公式  lv 1 =>  score = 1 + 100 / 1000
        TextLv.text = "等級：" + level;                        // 更新介面

        falltimeMax = 1.5f - level / 2;                        //速度提升公式
        falltimeMax = Mathf.Clamp(falltimeMax, 0.1f, 99f);     // Clamp ( 夾住 ) ， 將數值限制在此範圍內
        falltime = falltimeMax;
    }

    private void Gametime()
    {
    }

    [Header("目前分數")]
    public Text textCurrent;
    [Header("最高分數")]
    public Text textHight;

   

    /// <summary>
    /// 遊戲結束
    /// </summary>
    private void GameOver()
    {
        if (!gameOver)      
        {
            aud.PlayOneShot(endsound, Random.Range(0.5f, 1f));
            gameOver = true;            //遊戲結束
            StopAllCoroutines();        //停止偕同程序
            gameover.SetActive(true);    //顯示遊戲結束介面

            textCurrent.text = "目標分數：" + score;

            // 如果 分數 > 本機端紀錄的 最高分數
            // PlayerPrefs   API
            if (score > PlayerPrefs.GetInt("最高分數"))
            {
                // 更新 本機端紀錄的 最高分數 與 介面
                PlayerPrefs.SetInt("最高分數", score);
                textHight.text = "最高分數：" + score;
            }

            // 否則 更新 最高分數為 本機端紀錄的 最高分數
            else textHight.text = "最高分數：" + PlayerPrefs.GetInt("最高分數");
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("遊戲場景");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}