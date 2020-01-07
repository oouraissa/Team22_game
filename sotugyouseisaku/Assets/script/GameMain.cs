﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{  
    private int days;//日数
    private int miniGameLeft;//ミニゲーム発生期間？
    private int moneys;//お金
    private int health;//健康度
     
    private float tairyoku;//体力
    private int rndMiniGame;//ミニゲームフラッグランダマイザ

    public bool fadeIn;//フェードインの切り替え
    static bool Infidelity;//不倫end
    static bool Company;//皆勤end
    static bool Good;//Goodend
    static bool pickingFlag;

    public ButtonScript buttonScript;
    public GameText gameText;
    public Fade fade;
    public FoodManager foodManager;
    public EventManager eventManager;
    
    private string dialytext;
   
    private string statustext;

    public Text dayText;
    public Text moneyText;
    public Text Image1;
    public Text Image2;
   
    
    //public Text clearedLamp;
    [Header("ピッキングイベントを呼び出すための乱数の上限を指定")]//消してもOK
    public int rndMGLimit = 25;

    private void Start()
    {
        
        fadeIn = false;
       
        pickingFlag = false;//I dont get it how work
        days = 1;
        miniGameLeft = 0;
        rndMiniGame = 0;
        moneys = 30000;
        health = 50;
        tairyoku = 64;
        
        gameText.TextSelect();       
        dialytext = gameText.DialyText();
        
        statustext = gameText.StatusText();
        
        moneyText.text = "所持金：\n" + moneys + "円";
        dayText.text = days + "日目";
        GamePlay();
        fade.ScneSelect(Fade.Gamestate.GamePlay);//最初はOpningに行かない      
    }
    
    public void Opning()
    {
        days++;
        eventManager.EventOpning();
        dialytext = eventManager.StutusText() + gameText.DialyText(Random.Range(4, 10), 1);
        //MoneyTairyokuCalcu();
        fade.FadeOpnig();
        moneyText.text = "所持金：\n" + moneys + "円";
        dayText.text = days + "日目";      
        Debug.Log(moneys + "と" + tairyoku);
        foodManager.MoneyandTairyokuReset();       
        buttonScript.PositionReset();
        Debug.Log($"'Days' has increased: {days} days");
        Debug.Log("Loaded method Opening");
        //buttonScript.PositionReset();
        

        Kyuuryou();


        statustext = SelectStatusText();
        
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
        Debug.Log(dialytext + "+" + statustext);
        Image1.text = dialytext+"\n\n"+statustext;
        eventManager.EventGamePlay();
        tairyoku -= Random.Range(15, 31);
        
        //Image1.text = dialytext + "\n\n" + eventtext + "\n\n" + statustext;              
    }

    public void GameEnd()
    {
        
    }

    public void Clear()
    {
        Debug.Log("Loaded 'Clear' method.");
        
        
        SceneManager.LoadScene("GamePlay");
    }

    public void GameOver()
    {  
        days++;
        fade.FadeGameOver();
        moneyText.text = "所持金：\n" + moneys + "円";
        dayText.text = days + "日目";
        
        Debug.Log(moneys + "と" + tairyoku);
        foodManager.MoneyandTairyokuReset();
        buttonScript.PositionReset();
        //SceneManager.LoadScene("GamePlay");
        Debug.Log("GameOver終了");
    }

    public void MiniGame()
    {
        SceneManager.LoadScene("minigame");
    }

   

    public string SelectStatusText()
    {
        
        if(tairyoku>=80)
        {
            statustext = gameText.StatusText(1, 1);
        }
        else if(tairyoku<=79&&tairyoku>=30)
        {
            statustext = gameText.StatusText(2, 1);
        }
        else if (tairyoku <=29)
        {
            statustext = gameText.StatusText(3, 1);
        }

        

        return statustext;
    }

    public float Tairyoku()
    {
        return tairyoku;
    }

    public int Money()
    {
        return moneys;
    }

    public void Kyuuryou()
    {
        if(days%7==1)
        {
            moneys += 15000;
            dialytext+= gameText.DialyText(3, 1);
        }
    }

    void Update()
    {
        rndMiniGame = Random.Range(0, rndMGLimit);
        fade.SceneTransition();

    }

    public void MoneyTairyokuCalcu()
    {
        moneys -= foodManager.Moneys()+eventManager.MainMoney();
        tairyoku += foodManager.Tairyoku()-eventManager.MainTairyokuDown();      
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
