using System.Collections;
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
    public Image fadeImage;
    public Text button2;
    public GameMain gameMain;
    public float novelspeed;
    private bool firstClick;
    private bool lastClick;
    private bool changetime;
    private bool firstImages;
    private bool fade;
    private int Imagenumber;
    private float Imagesspeed;
    private float FadeoutImagealfa;
    private float alfa;
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
        changetime = true;
    }

    public void OnClick4()
    {
        if(gameMain.Gamestates()==GameMain.Gamestate.Opning)
        {
            fade = true;
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
                gameMain.currentstate = GameMain.Gamestate.Clear;
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
                if(lastClick&&gameMain.Gamestates()==GameMain.Gamestate.GamePlay)
                {
                    gameMain.ScneSelect(GameMain.Gamestate.GameEnd);
                    //firstClick = true;
                    lastClick = false;
                    changetime = true;
                }
                else
                novelspeed = -1;  break;
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
            Images[a].transform.position = new Vector2(12, Imagesposition.transform.position.y);
        }
        Imagesposition.transform.position= new Vector2(12, Imagesposition.transform.position.y);
        //StartCoroutine("ButtonStop");
        Imagenumber = 0;
    }

    private void FadeOut(GameMain.Gamestate game)
    {
        switch(game)
        {
             case GameMain.Gamestate.Opning:
             fadeImage.gameObject.SetActive(true);        
             FadeoutImagealfa +=alfa;
             fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, FadeoutImagealfa);
             if (fadeImage.color.a >= 1.00f)
             {
                    Debug.Log("a");
                    FadeoutImagealfa = 1.00f;
                    fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, FadeoutImagealfa);
                    gameMain.ScneSelect(GameMain.Gamestate.GamePlay);
             }
             break;

            case GameMain.Gamestate.GamePlay:
                FadeoutImagealfa -= alfa;
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, FadeoutImagealfa);
                if (fadeImage.color.a < 0.0f)
                {
                    Debug.Log(fadeImage.color.a);
                    FadeoutImagealfa = 0.00f;
                    fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, FadeoutImagealfa);
                    fadeImage.gameObject.SetActive(false);
                    fade = false;
                }
                break;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Imagenumber = 0;
        novelspeed = 0;
        alfa = 0.01f;
        fade = false;
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

        if(fade==true)
        {
            FadeOut(gameMain.currentstate);
           
        }

        if(gameMain.currentstate==GameMain.Gamestate.GamePlay&&Imagenumber==2)
        {
            button2.text = "晩酌してねる";
        }
        else if(gameMain.currentstate == GameMain.Gamestate.Opning && Imagenumber == 2)
        {
            button2.text = "会社に行かねば";
        }
        else
        {
            button2.text = "つぎへ";
        }

        if(firstImages!=true)
        {
            if (Images[Imagenumber].transform.position.x <= -11)
            {
                Images[Imagenumber].transform.position = new Vector2(-10, 0);
                novelspeed = 0;
                Imagenumber++;
                if (Imagenumber > 2)
                {
                    Imagenumber = 2;
                }
            }
            else if (Images[Imagenumber].transform.position.x >= 1)
            {
                //Debug.Log("a");
                Imagesposition.transform.position = new Vector2(0, Imagesposition.transform.position.y);
                Images[Imagenumber].transform.position = new Vector2(0, 0);
                Imagesspeed = 0;
                novelspeed = 0;
            }
            Images[Imagenumber].transform.position =
                new Vector2(Images[Imagenumber].transform.position.x + novelspeed, 0);
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

        if (Imagesposition.transform.position.x <= -1)
        {
            //Debug.Log("a");
            Imagesposition.transform.position = new Vector2(0, Imagesposition.transform.position.y);
            Imagesspeed = 0;
        }
        if (Imagesposition.transform.position.x >= 11)
        {
            Imagesposition.transform.position = new Vector2(10, Imagesposition.transform.position.y);
            Imagesspeed = 0;
        }

    }
}
