﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject Imagesposition;
    public GameObject Image;
    public GameObject Image2;
    public GameObject Image3;
    public Text OpenDay;
    public Image fadeImage;
    public Text button2;
    public GameMain gameMain;
    public Fade fade;
    public FoodManager foodManager;
    public float novelspeed;
    private bool firstClick;
    private bool lastClick;
    private bool changetime;
    private bool firstImages;
    
    private int Imagenumber;
    private float Imagesspeed;
    private float FadeoutImagealfa;
    
    private GameObject[]Images = new GameObject[3];
    public void OnClick()
    {        
        if(changetime==false)
        {
            Pageselect(1);
        }
    }

    public void OncClick2()
    {
        Pageselect(2);
        
    }

    public void OnClick3()
    {
        //Debug.Log("i");
        Imagesspeed = -2;
        firstImages = false;
        //changetime = true;
    }

    public void OnClick4()
    {
        if(fade.Gamestates()==Fade.Gamestate.Opning)
        {
            gameMain.GamePlay();
            Imagesposition.SetActive(true);
            fade.FadeIn(true);           
            //fade.ScneSelect(Fade.Gamestate.GamePlay);

            firstImages = true;

            if (gameMain.ReturnForGMiniLeft() > 0)
            {
                gameMain.GMiniSubtract();
                Debug.Log($"int.miniGameLeft has decreased: {gameMain.ReturnForGMiniLeft().ToString()}");
                Debug.Log($"int.rndMiniGame set: {gameMain.ReturnForRndGMini().ToString()}");
                if(gameMain.ReturnForRndGMini()==0)
                    gameMain.MiniGame();
            }

            if (gameMain.ReturnForDays() == 100)
            {
                fade.currentstate = Fade.Gamestate.Clear;
            }
        }
           
    }

    public void Pageselect(int page)
    {
        //Image1ならファーストクリックをtrueに
        if (Imagenumber == 0)
        {
            firstClick = true;
        }
        else
        {
            firstClick = false;
        }

       
        switch (page)
        {
            case 1:               
                if(lastClick&&fade.Gamestates()==Fade.Gamestate.GamePlay)
                {
                    Debug.Log("うんこ");                   
                    foodManager.SelectByouki();
                    foodManager.ByoukiText();
                    foodManager.SpecialDeath();
                    fade.FadeIn(true);
                    if (foodManager.Deathbool() == true)
                    {
                        //foodManager.ByoukiText();
                        gameMain.GameOver();
                        //fade.FadeIn(true);                        
                    }
                    else
                    {
                        //foodManager.Heal();                       
                        gameMain.Opning();
                        Imagesposition.SetActive(false);
                        foodManager.ResetFoodselect();
                        //fade.FadeIn(true);                       
                    }
                    lastClick = false;
                    changetime = true;
                }               
                else
               novelspeed = -1; /*Debug.Log("Burton1終了");*/break;
            case 2: 
                if(firstClick)
                {
                    firstImages = true;
                    Imagesspeed = 2;
                }
                else
                Imagenumber--; novelspeed = 1;break;             
        }

    }

    IEnumerator ButtonStop()
    {
        yield return new WaitForSeconds(60);
        
    }

    public void PositionReset()
    {
        for (int a = 0; a < Images.Length; a++)
        {
            Images[a].transform.position = new Vector2(15, Imagesposition.transform.position.y);
        }
        Imagesposition.transform.position= new Vector2(15, Imagesposition.transform.position.y);
        //StartCoroutine("ButtonStop");
        Imagenumber = 0;
    }


    

    // Start is called before the first frame update
    void Start()
    {
        Imagenumber = 0;
        novelspeed = 0;
        
        changetime = false;
        firstImages = true;
        firstClick = true;
        lastClick = false;
        Images[0]=Image;//0
        Images[1]=Image2;//1
        Images[2]=Image3;//2
    }

    

    // Update is called once per frame
    void Update()
    {
       //Debug.Log(Imagenumber);
        if (changetime)
        {
            StartCoroutine("ButtonStop");
            changetime = false;
            //Debug.Log("i");
        }

        //if(fade==true)
        //{
        //    FadeOut(gameMain.currentstate);
           
        //}

        if(fade.currentstate==Fade.Gamestate.GamePlay&&Imagenumber==2)
        {
            button2.text = "晩酌してねる";
        }
        else if(fade.currentstate == Fade.Gamestate.Opning && Imagenumber == 2)
        {
            button2.text = "会社に行かねば";
        }
        else
        {
            button2.text = "つぎへ";
        }

        if(firstImages!=true)
        {
            if (Images[Imagenumber].transform.position.x <= -15.1)
            {
                Images[Imagenumber].transform.position = new Vector2(-15, Images[Imagenumber].transform.position.y);
                novelspeed = 0;
                Imagenumber++;
                if (Imagenumber > 2)
                {
                    Imagenumber = 2;
                }
            }
            else if (Images[Imagenumber].transform.position.x >= 0.1)
            {
                //Debug.Log("a");
                Imagesposition.transform.position = new Vector2(0, Imagesposition.transform.position.y);
                Images[Imagenumber].transform.position = new Vector2(0, Images[Imagenumber].transform.position.y);
                Imagesspeed = 0;
                novelspeed = 0;
            }
            Images[Imagenumber].transform.position =
                new Vector2(Images[Imagenumber].transform.position.x + novelspeed, Images[Imagenumber].transform.position.y);
        }

        //Image３ならラストクリックをtrueに
        if (Imagenumber == 2)
        {
            lastClick = true;
        }
        else
        {
            lastClick = false;
        }
        //例外
        if (Imagenumber < 0)
        {
            Imagenumber = 0;
        }
        

        //OnClick3
        Imagesposition.transform.position = new Vector2(Imagesposition.transform.position.x + Imagesspeed,
            Imagesposition.transform.position.y);

        if (Imagesposition.transform.position.x <= -0.1)
        {
            //Debug.Log("a");
            Imagesposition.transform.position = new Vector2(0f, Imagesposition.transform.position.y);
            Imagesspeed = 0;
        }
        if (Imagesposition.transform.position.x >= 12.1f)
        {
            Imagesposition.transform.position = new Vector2(12f, Imagesposition.transform.position.y);
            Imagesspeed = 0;
        }

    }
}
