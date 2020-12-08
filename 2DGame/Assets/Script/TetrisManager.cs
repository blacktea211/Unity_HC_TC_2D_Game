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

    public AudioClip endsound;
    [Header("方塊消除音效")]

}