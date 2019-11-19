using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    private Vector2 VecA;//最初にボタンを押したときの角度   
    private Vector2 VecB;//ボタンを押しつづけてる時の角度
    private double x;//rcos
    private double y;//rsos
    private float theta;//回したときの角度(シータ)
    private float angle;// vecAとvecBが成す角度
    private double r = 2;//半径
    protected Vector3 AxB; // vecAとvecBの外積
    private Quaternion rotation;
    private bool keyhole;
    private bool audiobool;
    public Lockpick Lockpick;

    public AudioClip katikati;

    private AudioSource audioSource;

    private void Start()
    {
        angle = 0.0f;
        theta = 0.0f;
        audiobool = true;
        rotation= transform.parent.rotation;
        audioSource = GetComponent<AudioSource>();
    }

    

    //ボタンを押したとき  
    private void Controler()
    {
        if(theta<=-180.0f)
        {
            theta = -180.0f;
        }
        else if(theta>=0.0f)
        {
            theta = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            VecA = new Vector2((float)r+0, 0) - (Vector2)transform.parent.position;     
        }

        //if (keyhole == true)
        {
            theta+=0.05f;
            x = r * (Math.Cos(theta * Math.PI / 180));//rcosを求める
            y = r * (Math.Sin(theta * Math.PI / 180));//rsinを求める
            VecB = new Vector2((float)x, (float)y) - (Vector2)transform.parent.position; //VecBの座標          
        }
      
        if (Input.GetKey(KeyCode.A) && keyhole == true)
        {
            theta --;       
            x = r * (Math.Cos(theta * Math.PI / 180));//rcosを求める
            y = r * (Math.Sin(theta * Math.PI / 180));//rsinを求める
            VecB = new Vector2((float)x, (float)y)-(Vector2)transform.parent.position; //VecBの座標         
        }
        angle = Vector2.Angle(VecA, VecB);//vecAとvecBが成す角度を求める
        AxB = Vector3.Cross(VecA, VecB);//vecAとvecBの外積を求める
        if (AxB.z > 0.0f)
        {
            transform.parent.localRotation =rotation* Quaternion.Euler(0, 0, angle); // 初期値との掛け算で相対的に回転させる
        }
        else
        {
            transform.parent.localRotation =rotation* Quaternion.Euler(0, 0, -angle); // 初期値との掛け算で相対的に回転させる
        }
        Debug.Log(angle);
        if(angle>=90.0f&&audiobool==true)
        {
            audioSource.PlayOneShot(katikati);
            audiobool = false;
        }
    }

    public float Angle()
    {
        return angle;
    }

    private void Update()
    {
        keyhole = Lockpick.Keyrock();
        Controler();
    }



    // Start is called before the first frame update

}
