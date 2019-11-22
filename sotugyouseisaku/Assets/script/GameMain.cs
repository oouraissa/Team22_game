using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    public enum Gamestate
    {
        Opning,
        GamePlay,
        GameEnd,
        Clear,
        GameOver
    }
    private int days;//日数
    private int moneys;//お金
    private int health;//健康度
    private float FadeoutImagealfa;//画面フェイドアルファ値
    private float FadeoutTextalfa;//テキストフェイドアルファ値
    private float fadetime;//Image
    private float fadeday;//Text
    private float Tairyoku;//体力
    static bool Infidelity;//不倫end
    static bool Company;//皆勤end
    static bool Good;//Goodend

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

    private void Start()
    {
        fadetime = 0.01f;
        fadeday = 0.01f;
        Infidelity = false;
        Company = false;
        Good = false;
        days = 99;
        moneys = 30000;
        health = 50;
        Tairyoku = 50;
        OpenDay.color = new Color(OpenDay.color.r, OpenDay.color.g, OpenDay.color.b, 0);
        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, 0);
        OpenDay.gameObject.SetActive(false);
        FadeOut.gameObject.SetActive(false);
        gameText.TextSelect();       
        dialytext = gameText.DialyText(1, 1);
        eventtext = gameText.EventText();
        statustext = gameText.StatusText(2,1);
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
        moneys -= foodManager.Moneys();
        Tairyoku += foodManager.Tairyoku();
        //buttonScript.PositionReset();
        dialytext = gameText.DialyText(Random.Range(1, 4), 1);
        eventtext = gameText.EventText(Random.Range(1, 3), 1);
        statustext = gameText.StatusText(Random.Range(1, 4), 1);

        
    }

    public void GamePlay()
    {
        gameText.TextSelect();
        Image1.text = dialytext + "\n\n" + eventtext + "\n\n" + statustext;       
        
        
    }

    public void GameEnd()
    {
        days++;
        buttonScript.PositionReset();
        moneyText.text = "所持金：" + moneys + "円";
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

    }

    public Gamestate Gamestates()
    {
        return currentstate;
    }

    void Update()
    {
        //フェードアウト関連
        if(currentstate==Gamestate.Opning)
        {
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
                    ScneSelect(Gamestate.Opning);//最後にシーンを変える
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
}
