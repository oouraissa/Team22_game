using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public enum Gamestate
    {
        Opning,
        MiniGame,
        GamePlay,
        GameEnd,
        Clear,
        GameOver
    }
    public bool fadeIn;//フェードインの切り替え
    public AudioClip closedoor;
    AudioSource audioSource;
    public GameMain gameMain;
    public FoodManager foodManager;
    public Gamestate currentstate;//現在のシーン
    public Image FadeOut;
    private float FadeoutImagealfa;//画面フェイドアルファ値
    private float FadeoutTextalfa;//テキストフェイドアルファ値
    private float fadetime;//Image

    public Text OpenDay;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        FadeoutImagealfa = 0.0f;
        fadetime = 0.01f;
        OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, 0);
        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 0);
        OpenDay.gameObject.SetActive(false);
        FadeOut.gameObject.SetActive(false);
        //OpenDay.text = days + "日目";
    }

    public void FadeOpnig()
    {
        OpenDay.gameObject.SetActive(true);
        FadeOut.gameObject.SetActive(true);
        OpenDay.text = gameMain.ReturnForDays() + "日目";
    }

    public void FadeGameOver()
    {
        OpenDay.gameObject.SetActive(true);
        FadeOut.gameObject.SetActive(true);
        if(gameMain.Money()<=0)
        {
            OpenDay.text = "お金がなくなった";
        }
        else if(foodManager.Deathbool() == true)
        {
            OpenDay.text = "死 ん だ";
        }
        
    }

    public void FadeGameClear()
    {
        OpenDay.gameObject.SetActive(true);
        FadeOut.gameObject.SetActive(true);
        OpenDay.text = "生き延びた！";
    }
    /// <summary>
    /// シーンを設定
    /// </summary>
    /// <param name="state"></param>
    public void ScneSelect(Gamestate state)
    {
        currentstate = state;

        switch (state)
        {
            case Gamestate.Opning:

                break;
            case Gamestate.MiniGame:

                break;
            case Gamestate.GamePlay:

                break;
            case Gamestate.GameEnd:

                break;
            case Gamestate.Clear:

                break;
            case Gamestate.GameOver:

                break;
        }
    }
    /// <summary>
    /// フェードイン、基本シーンの最後に実行
    /// </summary>
    /// <param name="game"></param>
    public void FadeInImage(Gamestate game)
    {
        switch (game)
        {
            case Gamestate.Opning://フェードイン(画像のみ)
                if (fadeIn)
                {
                   
                    FadeOut.gameObject.SetActive(true);
                    FadeoutImagealfa += fadetime;
                    FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                    if (FadeOut.color.a >= 1.0f)
                    {
                        FadeoutImagealfa = 1.00f;
                        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 1);
                        ScneSelect(Gamestate.GamePlay);
                        audioSource.PlayOneShot(closedoor);
                        fadeIn = false;
                        Debug.Log("Opningフェードイン");
                    }
                }
                break;
            case Gamestate.GamePlay://フェードイン+日数表示                
                if (fadeIn)
                {
                    
                    FadeoutImagealfa += fadetime;
                    FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                    if (FadeOut.color.a >= 0.5f)
                    {
                        FadeoutTextalfa += fadetime;
                        OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, FadeoutTextalfa);
                        if (OpenDay.color.a >= 1)
                        {
                            FadeoutImagealfa = 1.00f;
                            FadeoutTextalfa = 1.00f;
                            OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, 1);
                            FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 1);
                            
                            if (foodManager.Deathbool() == true||gameMain.Money()<=0)
                            {
                                
                                ScneSelect(Fade.Gamestate.GameOver);
                            }
                            else
                            {                                 
                                ScneSelect(Fade.Gamestate.Opning);
                            }
                            Debug.Log("GamePlayフェードイン");
                            fadeIn = false;

                        }
                    }
                }
                break;
            case Gamestate.GameOver:
                if (fadeIn)
                {
                    FadeOut.gameObject.SetActive(true);
                    FadeoutImagealfa += fadetime;
                    FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                    if (FadeOut.color.a >= 1.0f)
                    {
                        FadeoutImagealfa = 1.00f;
                        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 1);
                        SceneManager.LoadScene("Title");
                        fadeIn = false;
                        Debug.Log("ゲームオーバーフェードイン");
                    }
                }
                break;
            //case Gamestate.Clear:
            //    if (fadeIn)
            //    {

            //    }
        }

    }

    /// <summary>
    /// フェードアウト、次のシーンの最初に実行
    /// </summary>
    /// <param name="game"></param>
    public void FadeOutImage(Gamestate game)
    {
        switch (game)
        {
            case Gamestate.Opning:///シーンの始まりにフェードアウト+日数非表示
                if (FadeOut.gameObject.activeSelf == true && OpenDay.gameObject.activeSelf == true && fadeIn == false)
                {
                   
                    FadeoutTextalfa -= fadetime;
                    OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, FadeoutTextalfa);
                    if (OpenDay.color.a <= 0.5f)
                    {
                        FadeoutImagealfa -= fadetime;
                        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                        if (FadeOut.color.a <= 0)
                        {
                            FadeoutTextalfa = 0.00f;
                            FadeoutImagealfa = 0.00f;
                            OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, 0);
                            FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 0);
                            OpenDay.gameObject.SetActive(false);
                            FadeOut.gameObject.SetActive(false);
                            Debug.Log("Opning、 フェードアウト");
                            //ScneSelect(Gamestate.GamePlay);//最後にシーンを変える
                        }
                    }
                }
                break;

            case Gamestate.GameOver:
                if (FadeOut.gameObject.activeSelf == true && OpenDay.gameObject.activeSelf == true && fadeIn == false)
                {
                   
                    FadeoutTextalfa -= fadetime;
                    OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, FadeoutTextalfa);
                    if (OpenDay.color.a <= 0.5f)
                    {
                        FadeoutImagealfa -= fadetime;
                        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                        if (FadeOut.color.a <= 0)
                        {
                            FadeoutTextalfa = 0.00f;
                            FadeoutImagealfa = 0.00f;
                            OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, 0);
                            FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 0);
                            OpenDay.gameObject.SetActive(false);
                            FadeOut.gameObject.SetActive(false);
                            Debug.Log("GameOver、 フェードアウト");
                            //ScneSelect(Gamestate.GamePlay);//最後にシーンを変える
                        }
                    }
                }
                break;



            case Gamestate.GamePlay:///シーンの始まりにフェードアウト
                if (FadeOut.gameObject.activeSelf == true && fadeIn == false)
                {
                   
                    FadeoutImagealfa -= fadetime;
                    FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                    if (FadeOut.color.a <= 0)
                    {
                        FadeoutImagealfa = 0.00f;
                        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 0);
                        FadeOut.gameObject.SetActive(false);
                        Debug.Log("GamePlay フェードアウト");
                        //ScneSelect(Gamestate.GamePlay);
                    }
                }
                break;
        }
    }

    public void  SceneTransition()
    {
        if (currentstate == Gamestate.Opning)
        {
            FadeOutImage(currentstate);
            FadeInImage(currentstate);
        }
        else if (currentstate == Gamestate.GamePlay)
        {
            FadeOutImage(currentstate);
            FadeInImage(currentstate);
        }
        else if (currentstate == Gamestate.GameOver)
        {
            FadeOutImage(currentstate);
            FadeInImage(currentstate);
        }
    }

    public bool FadeIn(bool fadeIn)
    {
        this.fadeIn = fadeIn;
        return this.fadeIn;
    }

    public Gamestate Gamestates()
    {

        return currentstate;
    }
    // Start is called before the first frame update
    

    
}
