using UnityEngine;

public class Tetris : MonoBehaviour
{
    [Header("角度為 0 , 線條的長度")]
    public float length0;
    [Header("角度為 90 , 線條的長度")]
    public float length90;
    [Header("旋轉位移左右")]
    public float offsetX;
    [Header("旋轉位移上下")]
    public float offsetY;
    [Header("測是否能旋轉")]
    public float lenghtRotate0R;
    public float lenghtRotate0L;
    public float lenghtRotate90R;
    public float lenghtRotate90L;


    /// <summary>
    /// 紀錄目前角度
    /// </summary>
    private float length;
    private float lenghtDown;
    private float lenghtRotateR;
    private float lenghtRotateL;


    /// <summary>
    /// 判斷是否碰到牆壁 => 碰到牆壁時打勾
    /// </summary>
    
    //右
    public bool WallRight;
    //左
    public bool WallLeft;
    //下
    public bool WallDown;

    /// <summary>
    /// 是否能 旋轉
    /// </summary>
    public bool canRotate = true;
    private RectTransform rect;

    /// <summary>
    /// 繪製圖示
    /// </summary>
    private void OnDrawGizmos()
    {
        //圖示顏色
        Gizmos.color = Color.red;

        //將浮點數轉為整數 => 去小數點 => 轉換的元件前+(int)
        int z = (int)transform.eulerAngles.z;


         if (z == 0 || z == 180)
        {   //儲存目前長度
            length = length0 ;
            //右邊射線
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length0 );
            //左邊射線
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, -Vector3.right * length0 );
            //向下射線
            lenghtDown = length90;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -Vector3.up * length90 );
            //繪製旋轉射線
            lenghtRotateR = lenghtRotate0R;
            lenghtRotateL = lenghtRotate0L;
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, Vector3.right * lenghtRotate0R);
            Gizmos.DrawRay(transform.position, -Vector3.right * lenghtRotate0L);
        }

        else if(z == 90 || z== 270)
        {
            length = length90 ;
            //右邊射線
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length90);
            //左邊射線
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, -Vector3.right * length90);
            //向下射線
            lenghtDown = length0;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -Vector3.up * length0);
            //繪製旋轉射線
            lenghtRotateR = lenghtRotate90R;
            lenghtRotateL = lenghtRotate90L;
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, Vector3.right * lenghtRotate90R);
            Gizmos.DrawRay(transform.position, -Vector3.right * lenghtRotate90L);
        }

    }


    private void Start()
    {
        //儲存遊戲開始的角度
        length = length0;

        rect = GetComponent<RectTransform>();

    }


    void Update()
    {
        Checkwall();
    }

    /// <summary>
    /// 檢查射線是否碰到牆壁
    /// </summary>
    private void Checkwall()
    {   
        //2D物理碰撞資訊 區域變數名稱 = 2D物理射線碰撞(位置,方向,長度,圖層)

        //Physics2D.Raycast起始點, 方向, 長度 , 判斷是否撞到圖層8)
        //RaycastHit2D  將方塊撞到牆壁的資料傳回 hit 
        RaycastHit2D hitR = Physics2D.Raycast(transform.position, Vector3.right, length , 1 << 8);
       // print(hitR.transform.name);

        //並且 &&
        //如果碰到 並且 名稱為  右邊牆壁
        if (hitR && hitR.transform.name == "右邊牆壁")
        {
            WallRight = true;
        }
        else 
        {
            WallRight = false;
        }

        //Physics2D.Raycast起始點, 方向, 長度 , 判斷是否撞到圖層8)
        //如果碰到 並且 名稱為  左邊牆壁
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector3.right, length, 1 << 8);
      //  print(hitLeft.transform.name);

        if (hitLeft && hitLeft.transform.name == "左邊牆壁")
        {
            WallLeft = true;
        }
        else
        {
            WallLeft = false;
        }

        //旋轉射線
        RaycastHit2D hitRotateR = Physics2D.Raycast(transform.position, Vector3.right, lenghtRotateR, 1 << 8);
        RaycastHit2D hitRotateL = Physics2D.Raycast(transform.position, Vector3.right, lenghtRotateL, 1 << 8);

        //碰到 左 右 邊牆壁不能旋轉
        if ((hitRotateR && hitRotateR.transform.name == "右邊牆壁" || hitRotateL && hitRotateL.transform.name == "左邊牆壁"))
        {
            canRotate = false;
        }
        else
        {
            canRotate = true;
        }

        //Physics2D.Raycast起始點, 方向, 長度 , 判斷是否撞到圖層9)
        //如果碰到 並且 名稱為  地板
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -Vector3.up, lenghtDown, 1 << 9);
       // print(hitDown.transform.name);

        if (hitDown && hitDown.transform.name == "地板")
        {
            WallDown = true;
        }
        else
        {
            WallDown = false;
        }
              
    }


    public void Offset()
    {

        //將浮點數轉為整數 => 去小數點 => 轉換的元件前+(int)
        int z = (int)transform.eulerAngles.z;


        if (z == 0 || z == 180)
        {
           
            rect.anchoredPosition += new Vector2(offsetX, offsetY);
        }




        else if (z == 90 || z == 270)
        {
          
            rect.anchoredPosition -= new Vector2(offsetX, offsetY);
        }
    }


}
