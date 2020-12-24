using UnityEngine;

public class Tetris : MonoBehaviour
{
    [Header("角度為 0 , 線條的長度")]
    public float length0;
    [Header("角度為 90 , 線條的長度")]
    public float length90;

    /// <summary>
    /// 紀錄目前角度
    /// </summary>
    public float length;


    /// <summary>
    /// 判斷是否碰到牆壁 => 碰到牆壁時打勾
    /// </summary>
    public bool WallRight;
    public bool WallLeft;

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
        {
            length = length0 ;
            Gizmos.DrawRay(transform.position, Vector3.right * length0 );
        }

        else if(z == 90 || z== 270)
        {
            length = length90 ;
            Gizmos.DrawRay(transform.position, Vector3.right * length90);
        }

    }


    private void Start()
    {
        //儲存遊戲開始的角度
        length = length0;
    }


    void Update()
    {
        Checkwall();
    }


    private void Checkwall()
    {   
        //2D物理碰撞資訊 區域變數名稱 = 2D物理射線碰撞(位置,方向,長度,圖層)

        //Physics2D.Raycast起始點, 方向, 長度 , 判斷是否撞到圖層8);
        //RaycastHit2D  將方塊撞到牆壁的資料傳回 hit 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, length , 1 << 8);
        print(hit.transform.name);

        //並且 &&
        //如果碰到 並且 名稱為  右邊牆壁
        if (hit &&hit .transform .name == "右邊牆壁")
        {
            WallRight = true;
        }
        else 
        {
            WallRight = false;
        }

        if (hit && hit.transform.name == "左邊牆壁")
        {
            WallLeft = true;
        }
        else
        {
            WallLeft = false;
        }
    }

    
    
}
