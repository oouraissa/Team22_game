using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
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
    private int days;//日数
    private int miniGameLeft;//ミニゲーム発生期間？
    private int moneys;//お金
    private int health;//健康度
    private float FadeoutImagealfa;//画面フェイドアルファ値
    private float FadeoutTextalfa;//テキストフェイドアルファ値
    private float fadetime;//Image
    
    private float tairyoku;//体力
    private int rndMiniGame;//ミニゲームフラッグランダマイザ

    public bool fadeIn;//フェードインの切り替え
    static bool Infidelity;//不倫end
    static bool Company;//皆勤end
    static bool Good;//Goodend
    static bool pickingFlag;

    public ButtonScript buttonScript;
    public GameText gameText;
    public Gamestate currentstate;
    public FoodManager foodManager;

    private string dialytext;
    private string eventtext;
    private string statustext;

    public Image FadeOut;

    public Text dayText;
    public Text moneyText;
    public Text Image1;
    public Text Image2;
    public Text Image3;
    public Text OpenDay;
    //public Text clearedLamp;
    [Header("ピッキングイベントを呼び出すための乱数の上限を指定")]//消してもOK
    public int rndMGLimit = 25;

    private void Start()
    {
        FadeoutImagealfa = 0.0f;
        fadetime = 0.01f;
       
        fadeIn = false;
       
        pickingFlag = false;//I dont get it how work
        days = 1;
        miniGameLeft = 0;
        rndMiniGame = 0;
        moneys = 30000;
        health = 50;
        tairyoku = 50;
        OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, 0);
        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 0);
        OpenDay.gameObject.SetActive(false);
        FadeOut.gameObject.SetActive(false);
        gameText.TextSelect();       
        dialytext = gameText.DialyText();
        eventtext = gameText.EventText();
        statustext = gameText.StatusText();
        OpenDay.text = days + "日目";
        moneyText.text = "所持金：\n" + moneys + "円";
        dayText.text = days + "日目";
        //Debug.Log("a");
        ScneSelect(Gamestate.GamePlay);//最初はOpningに行かない
    }
    
    /// <summary>
    /// シーンを設定
    /// </summary>
    /// <param name="state"></param>
    public void ScneSelect(Gamestate state)
    {
        currentstate = state;

        switch(state)
        {
            case Gamestate.Opning:
                
                break;
            case Gamestate.MiniGame:
                MiniGame();
                break;
            case Gamestate.GamePlay:
                
                break;
            case Gamestate.GameEnd:
                GameEnd();
                break;
            case Gamestate.Clear:
                Clear();
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
            case GameMain.Gamestate.Opning://フェードイン(画像のみ)
                if(fadeIn)
                {
                    FadeOut.gameObject.SetActive(true);
                    FadeoutImagealfa += fadetime;
                    FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                    if (FadeOut.color.a >= 1.0f)
                    {
                        FadeoutImagealfa = 1.00f;
                        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 1);
                        ScneSelect(Gamestate.GamePlay);
                        fadeIn = false;
                    }                    
                }
                break;
            case GameMain.Gamestate.GamePlay://フェードイン+日数表示                
                if (fadeIn)
                {
                    Debug.Log("GamePlayフェードイン");                  
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
                            if(foodManager.Deathbool()==true)
                            {
                                ScneSelect(Gamestate.GameOver);
                                fadeIn = false;
                            }
                            else
                            {
                                ScneSelect(Gamestate.Opning);
                                fadeIn = false;
                            }
                        }
                    }
                }                
             break;
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
            case GameMain.Gamestate.Opning:///シーンの始まりにフェードアウト+日数非表示
                if (FadeOut.gameObject.activeSelf == true && OpenDay.gameObject.activeSelf==true&&fadeIn == false)
                {
                    Debug.Log("Opning、 フェードアウト");
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
                            //ScneSelect(Gamestate.GamePlay);//最後にシーンを変える
                        }
                    }
                }                  
                break;

            case GameMain.Gamestate.GameOver:
                if (FadeOut.gameObject.activeSelf == true && OpenDay.gameObject.activeSelf == true && fadeIn == false)
                {
                    Debug.Log("GameOver、 フェードアウト");
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
                            //ScneSelect(Gamestate.GamePlay);//最後にシーンを変える
                        }
                    }
                }
                break;



            case GameMain.Gamestate.GamePlay:///シーンの始まりにフェードアウト
                if (FadeOut.gameObject.activeSelf==true&&fadeIn == false)
                {
                    Debug.Log("GamePlay フェードアウト");
                    FadeoutImagealfa -= fadetime;
                    FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                    if (FadeOut.color.a <= 0)
                    {
                        FadeoutImagealfa = 0.00f;
                        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 0);
                        FadeOut.gameObject.SetActive(false);
                       
                    }                  
                }
                break;
        }
    }

    public void Opning()
    {
        days++;
        OpenDay.gameObject.SetActive(true);
        FadeOut.gameObject.SetActive(true);
        moneyText.text = "所持金：\n" + moneys + "円";
        dayText.text = days + "日目";
        OpenDay.text = days + "日目";
        Debug.Log(moneys + "と" + tairyoku);
        foodManager.MoneyandTairyokuReset();       
        buttonScript.PositionReset();
        Debug.Log($"'Days' has increased: {days} days");
        Debug.Log("Loaded method Opening");
        //buttonScript.PositionReset();
        dialytext = gameText.DialyText(Random.Range(1, 10), 1);
        eventtext = gameText.EventText(Random.Range(0, 2), 1);
        statustext = gameText.StatusText(Random.Range(1, 4), 1);
        if (days <= (100 - 7)) 
        {
            if((days % 7) == 0)
            {
                pickingFlag = true;
                miniGameLeft = 5;
            }
            else if((days%7)==6){
                pickingFlag = false;
            }
        }
        if (pickingFlag == true)
        {
            Debug.Log($"[Test] Bool: PickingFlag is {pickingFlag.ToString()}");
            
        }
        Debug.Log("Opning終了");

    }

    public void GamePlay()
    {       
        Debug.Log("Loaded method GamePlay");
        tairyoku -= Random.Range(15, 31);
        Image1.text = dialytext + "\n\n" + eventtext + "\n\n" + statustext;              
    }

    public void GameEnd()
    {
        
    }

    public void Clear()
    {
        Debug.Log("Loaded 'Clear' method.");
    }

    public void GameOver()
    {  
        days++;
        OpenDay.gameObject.SetActive(true);
        FadeOut.gameObject.SetActive(true);
        moneyText.text = "所持金：\n" + moneys + "円";
        dayText.text = days + "日目";
        OpenDay.text = "死 ん だ";
        Debug.Log(moneys + "と" + tairyoku);
        foodManager.MoneyandTairyokuReset();
        buttonScript.PositionReset();
        Debug.Log("GameOver終了");
    }

    public void MiniGame()
    {
        SceneManager.LoadScene("minigame");
    }

    public Gamestate Gamestates()
    {
        return currentstate;
    }

    public void SelectStatusText()
    {
        if(tairyoku>=80)
        {
            statustext = gameText.StatusText(3, 1);
        }
        //else if(tairyoku <80&&)
    }

    public float Tairyoku()
    {
        return tairyoku;
    }

    public bool FadeIn(bool fadeIn)
    {
        this.fadeIn = fadeIn;
        return this.fadeIn;
    }

    void Update()
    {
        rndMiniGame = Random.Range(0, rndMGLimit);
        if(currentstate==Gamestate.Opning)
        {
            FadeOutImage(currentstate);
            FadeInImage(currentstate);
        }
        else if(currentstate==Gamestate.GamePlay)
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

    public void MoneyTairyokuCalcu()
    {
        moneys -= foodManager.Moneys();
        tairyoku += foodManager.Tairyoku();
        Debug.Log(moneys);
        Debug.Log(tairyoku);
    }

    public int NextDays()
    {
        days++;
        return days;
    }
    public int ReturnForDays()
    {
        return days;
    }

    public int ReturnForGMiniLeft()
    {
        return miniGameLeft;
    }

    public void GMiniSubtract()
    {
        miniGameLeft--;
    }

    public int ReturnForRndGMini()
    {
        return rndMiniGame;
    }
}
