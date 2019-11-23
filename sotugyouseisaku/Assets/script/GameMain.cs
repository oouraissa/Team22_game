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
    private float fadeday;//Text
    private float tairyoku;//体力
    private int rndMiniGame;//ミニゲームフラッグランダマイザ
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
        fadetime = 0.01f;
        fadeday = 0.01f;
        Infidelity = false;
        Company = false;
        Good = false;
        pickingFlag = false;//I dont get it how work
        days = 1;
        miniGameLeft = 0;
        rndMiniGame = 0;
        moneys = 30000;
        health = 50;
        tairyoku = 50;
        gameText.TextSelect();//Text
        OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, 0);
        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 0);
        OpenDay.gameObject.SetActive(false);
        FadeOut.gameObject.SetActive(false);
        gameText.TextSelect();       
        dialytext = gameText.DialyText();
        eventtext = gameText.EventText();
        statustext = gameText.StatusText();
        moneyText.text = "所持金：\n" + moneys + "円";
        dayText.text = days + "日目";
        //Debug.Log("a");
        ScneSelect(Gamestate.GamePlay);//最初はOpningに行かない
    }
    
    public void ScneSelect(Gamestate state)
    {
        currentstate = state;

        switch(state)
        {
            case Gamestate.Opning:
                Opning();
                break;
            case Gamestate.MiniGame:
                MiniGame();
                break;
            case Gamestate.GamePlay:
                GamePlay();
                break;
            case Gamestate.GameEnd:
                GameEnd();
                break;
            case Gamestate.Clear:
                Clear();
                break;
            case Gamestate.GameOver:
                GameOver();
                break;
        }
    }

    public void Opning()
    {
        Debug.Log("Loaded method Opening");
        //buttonScript.PositionReset();
        dialytext = gameText.DialyText(Random.Range(1, 11), 1);
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
    }

    public void GamePlay()
    {
        Debug.Log("Loaded method GamePlay");
        
        Image1.text = dialytext + "\n\n" + eventtext + "\n\n" + statustext;       
        
        
    }

    public void GameEnd()
    {
        moneys -= foodManager.Moneys();
        tairyoku += foodManager.Tairyoku();
        foodManager.SelectByouki();
        foodManager.ByoukiText();
        days++;
        buttonScript.PositionReset();
        moneyText.text = "所持金：\n" + moneys + "円";
        OpenDay.text = days + "日目";
        dayText.text = days + "日目";
        Debug.Log($"'Days' has increased: {days} days");
    }

    public void Clear()
    {
        Debug.Log("Loaded 'Clear' method.");
    }

    public void GameOver()
    {
        OpenDay.text ="死 ん だ";
    }

    public void MiniGame()
    {
        SceneManager.LoadScene("minigame");
    }

    public Gamestate Gamestates()
    {
        return currentstate;
    }

    public float Tairyoku()
    {
        return tairyoku;
    }

    void Update()
    {
        rndMiniGame = Random.Range(0, rndMGLimit);

        //フェードアウト関連
        if(currentstate==Gamestate.Opning)
        {
            Debug.Log(currentstate);
            FadeoutTextalfa -= fadetime;           
            OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, FadeoutTextalfa);            
            if (OpenDay.color.a<=0.5f)
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
        if (currentstate == Gamestate.GameEnd)
        {
            OpenDay.gameObject.SetActive(true);
            FadeOut.gameObject.SetActive(true);
            FadeoutImagealfa+= fadetime;
            FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
            if (FadeOut.color.a >= 0.5f)
            {
                FadeoutTextalfa += fadetime;
                OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, FadeoutTextalfa);
                if (OpenDay.color.a >= 1)
                {
                    FadeoutImagealfa = 1.00f;
                    FadeoutTextalfa = 1.00f;
                    OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, FadeoutTextalfa);
                    FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                    foodManager.Scene();//最後にシーンを変える
                    
                }
            }

        }
        if (currentstate == Gamestate.Clear)
        {
            //clearedLamp.gameObject.SetActive(true);
            FadeOut.gameObject.SetActive(true);
            FadeoutImagealfa += fadetime;
            FadeOut.color = new Color(255, 255, 255, FadeoutImagealfa);
            if (FadeOut.color.a >= 1.0f)
            {
                //FadeoutTextalfa += fadetime;
                ////clearedLamp.color = new Color(0,0,0, FadeoutTextalfa);
                //if (clearedLamp.color.a >= 1)
                //{
                    FadeoutImagealfa = 1.00f;
                    FadeoutTextalfa = 1.00f;
                    //clearedLamp.color = new Color(0,0,0, FadeoutTextalfa);
                    FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeoutImagealfa);
                    //ScneSelect(Gamestate.Opning);//最後にシーンを変える
                //}
                
            }
        }
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
