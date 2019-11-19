using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemini : MonoBehaviour
{
    public Keyhole keyhole;
    public float[] lockpickposition;
    public float angle;//ロックピックの角度
    //public float[] Keyholeposition;
    // Start is called before the first frame update
    void Start()
    {
        Setpos();
    }

    public float Lockpickposition()//鍵穴の角度によってロックピックの角度が変化
    {
        float keyholeangle = keyhole.Angle();
        if(keyholeangle<=45)
        {
            angle=lockpickposition[0];
        }
        else if(keyholeangle <= 90&&keyholeangle >= 45)
        {
            angle = lockpickposition[1];
        }
        else if (keyholeangle <= 135&&keyholeangle >= 90)
        {
            angle = lockpickposition[2];
        }
        else if(keyholeangle >= 135)
        {
            angle = lockpickposition[3];
        }
        return angle;
    }

    public void Setpos()
    {
        int random = Random.Range(0, 3);
        switch(random)
        {
            case 0:
                lockpickposition = new float[] { 0, 90, 150 ,40};               
                break;
            case 1:
                lockpickposition = new float[] { 40,150,0,90  };
                break;
            case 2:
                lockpickposition = new float[] { 90,40, 150, 0 };
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
