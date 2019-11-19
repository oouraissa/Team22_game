using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lockpick : MonoBehaviour
{
    public Gamemini gamemini;
    private float Lockpickposition;
    private bool keylock = false;
    private bool soundbool;
    private float soundtime;
    protected Vector2 pos; // 最初にクリックしたときの位置
    protected Quaternion rotation; // 最初にクリックしたときのBoxの角度
    protected Vector2 vecA; // Boxの中心からposへのベクトル
    protected Vector2 vecB;// Boxの中心から現在のマウス位置へのベクトル
    private Vector2 vecC;
    protected float angle; // vecAとvecBが成す角度
    public float angle2;
    float a;
    protected Vector3 AxB; // vecAとvecBの外積

    public AudioClip gatyagatya;

    private AudioSource audioSource;
    // PointerDownで呼び出す
    // クリック時にパラメータの初期値を求める

    private void Start()
    {
        soundtime = 0.0f;
        soundbool = true;
        vecC = new Vector2(-2, 0);
        //SetPos();
        keylock = false;
        audioSource = GetComponent<AudioSource>();
    }

    public bool Keyrock()
    {
        return keylock;
    }

    public void Checkpoint()
    {
        Lockpickposition = gamemini.Lockpickposition();
        
    }
    public virtual void SetPos()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);// マウス位置をワールド座標で取得
        rotation = transform.parent.rotation; // Boxの現在の角度を取得
        
    }

    private void Update()
    {
        Checkpoint();
        if(soundbool==false)
        {
            soundtime += Time.deltaTime;
            if(soundtime>5.0f)
            {
                soundtime = 0;
                soundbool = true;
            }
        }
        if (angle2 <= Lockpickposition+30 && angle2 >= Lockpickposition)
        {
            keylock = true;
        }
        else
        {
            keylock = false;
        }
    }

    // ハンドルをドラッグしている間に呼び出す
    public virtual void  Rotate()
    {       
        vecA = pos - (Vector2)transform.parent.position;//ある地点からのベクトルを求めるときはこう書くんだった
        vecB = Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.parent.position;
        if (vecB.y <= 0.0)
        {
            return;
        }
        // Vector2にしているのはz座標が悪さをしないようにするためです
        angle = Vector2.Angle(vecA, vecB); // vecAとvecBが成す角度を求める
        angle2 = Vector2.Angle(vecC, vecB);
        AxB = Vector3.Cross(vecA, vecB); // vecAとvecBの外積を求める
        
        if(angle2<0.0f)
        {
            angle2 = 0.0f;
        }
        else if(angle2>180.0f)
        {
            angle2 = 180.0f;
        }
      
        // 外積の z 成分の正負で回転方向を決める
        if (AxB.z >0)
        {
            transform.parent.localRotation = rotation * Quaternion.Euler(0, 0, angle);
            if(soundbool==true)
            {
                audioSource.PlayOneShot(gatyagatya);
                soundbool = false;
            }
           
        }
        else 
        {
            transform.parent.localRotation = rotation * Quaternion.Euler(0, 0, -angle); // 初期値との掛け算で相対的に回転させる
            if (soundbool == true)
            {
                audioSource.PlayOneShot(gatyagatya);
                soundbool = false;
            }
        }
    }

    
}

    

