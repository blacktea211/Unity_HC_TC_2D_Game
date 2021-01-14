using UnityEngine;

public class APINoStatic : MonoBehaviour
{
    public Transform traA;
    public Transform traB;
    public GameObject mygameObject ;
    public Transform mytra;

    private void Start()
    {
        print("物件的座標" + traA.position);

        traB.position = new Vector3(2, 4, 6);

        print("物件圖曾為:" + mygameObject.layer);
        mygameObject.layer = 4;

        

    }

 
    void Update()
    {
        mytra.Rotate(1, 2, 3);

    }
}
