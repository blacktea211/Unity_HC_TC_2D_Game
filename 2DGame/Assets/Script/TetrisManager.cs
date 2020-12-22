 using UnityEngine;

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

    [Header("畫布")]
    public Transform  traCanvas;



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
    }

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
                currentTetris.anchoredPosition -= new Vector2(0, 50);
            }

            //如果方塊的 X軸 < 260 ,不超過右邊邊界
            if (currentTetris.anchoredPosition.x < 220)
            {
                // 或者||
                // 按下 D 或 鍵盤右鍵 使方塊往右50
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentTetris.anchoredPosition += new Vector2(50, 0);
                }
            }

            if (currentTetris.anchoredPosition.x > -220)
            {
                // 按下 A 或 鍵盤左鍵 使方塊往左50
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentTetris.anchoredPosition -= new Vector2(50, 0);
                }
            }


            // 按下 W 或 鍵盤上鍵 使方塊逆時針旋轉90度
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                //屬性慢板上的 Rotation 必須用 eulerAngles 控制 , eulerAngles ( 0度~360度 )
                currentTetris.eulerAngles += new Vector3(0, 0, 90);
            }

            // 按下 空白建 或 鍵盤下鍵 就加速
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.DownArrow))
            {
                falltime = 0.2f ;            
            }
            //否則恢復速度
            else
            {
                falltime = 1.5f;
            }

            if (currentTetris .anchoredPosition .y == -300)
            {
                StartGame();
            }
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
        current.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 400);

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

}