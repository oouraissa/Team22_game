using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public int dayincIdence = 0;
    private int downMoney = 0;//払うお金の合計
    private int firstMoney = 0;//加入時に払うためのお金の数値
    private int continued = 0;//継続的に払うお金
    private int upMoney = 0;
    private float downTairyoku = 0;
    public Text eventText;
    public Text douguText;
    public GameText gameText;
    public GameMain gameMain;
    private bool eventbool = false;
    private bool disaster = false;
    private bool stocksell = false;
    private bool eventlimit = false;
    public Button maru;
    public Button batu;
    public Button dougu;
    public Button stockButton;
    public Image stock;
    public Image earthquakeinsurance;
    public Image fireinsurance;
    public EventScript.Item currentevent;//発生したイベント
    public EventScript.Disaster currentdisaster;//発生したハプニング
    private EventScript[] eventScripts = new EventScript[(int)EventScript.Item.END];

    /// <summary>
    /// 〇
    /// </summary>
    public void Onclick1()
    {
        eventbool = true;
        //downMoney = 1;
        Debug.Log(eventbool);
        batu.image.color = batu.image.color;
        //EventSelect();
    }

    /// <summary>
    /// ×
    /// </summary>
    public void Onclick2()
    {
        eventbool = false;
        Debug.Log(eventbool);
        maru.image.color = maru.image.color;
    }

    /// <summary>
    /// 災害回避
    /// </summary>
    public void Onclick3()
    {
        disaster = false;
        Debug.Log("災害を回避");
    }

    /// <summary>
    /// 株を売る
    /// </summary>
    public void Onclick4()
    {
        stocksell = true;
        Debug.Log("株を売る");
    }
    /// <summary>
    /// 災害発生イベント
    /// </summary>
    /// <param name="disaster">災害の名前</param>
    public void Disasterinsidence(EventScript.Disaster disaster)
    {
        if(gameMain.ReturnForDays()>=8)
        {
            this.currentdisaster = disaster;
            switch (disaster)
            {
                case EventScript.Disaster.Fire:
                    this.disaster = true;
                    Debug.Log("火災イベント");
                    eventText.text = GameObject.Find("GameText").GetComponent<GameText>().EventText(8, 1);
                    DouguCheck(EventScript.Item.Fireinsurance);
                    break;//火災イベント
                case EventScript.Disaster.Earthquake:
                    this.disaster = true;
                    Debug.Log("地震イベント");
                    eventText.text = GameObject.Find("GameText").GetComponent<GameText>().EventText(10, 1);
                    DouguCheck(EventScript.Item.Earthquakeinsurance);
                    break;//火災イベント

            }
        }
        else
        {
            this.currentdisaster = EventScript.Disaster.End;
            eventText.text = GameObject.Find("GameText").GetComponent<GameText>().EventText(20, 1);
        }
    }

    /// <summary>
    /// イベント発生テキスト
    /// </summary>
    /// <param name="ItemText">テキストの種類</param>
    public void Eventinsidence(EventScript.Item ItemText)
    {
        if(eventScripts[(int)ItemText]==null&& ItemText != EventScript.Item.Other)
        {
            switch (ItemText)
            {
                case EventScript.Item.Stock:
                    eventText.text = GameObject.Find("GameText").GetComponent<GameText>().EventText(4, 1);
                    currentevent = ItemText;
                    maru.gameObject.SetActive(true);
                    batu.gameObject.SetActive(true);
                    break;//株イベント
                case EventScript.Item.Fireinsurance:
                    eventText.text = GameObject.Find("GameText").GetComponent<GameText>().EventText(2, 1);
                    currentevent = ItemText;
                    maru.gameObject.SetActive(true);
                    batu.gameObject.SetActive(true);
                    break;//火災保険イベント
                case EventScript.Item.Earthquakeinsurance:
                    eventText.text = GameObject.Find("GameText").GetComponent<GameText>().EventText(3, 1);
                    currentevent = ItemText;
                    maru.gameObject.SetActive(true);
                    batu.gameObject.SetActive(true);
                    break;//地震保険イベント
            }
        }
        if(ItemText==EventScript.Item.Other|| eventScripts[(int)ItemText] != null)
        {
            eventText.text = GameObject.Find("GameText").GetComponent<GameText>().EventText(20, 1);
            currentevent = ItemText;
        }
        //case EventScript.Item.Fraud:
    }
    /// <summary>
    /// 道具textとImageを変える
    /// </summary>
    /// <param name="currentImage">変えるimage</param>
    /// <param name="text">変えるtext</param>
    public void Changeimage(Button dougu,Image currentImage, string text)
    {
        dougu.image.sprite = currentImage.sprite;
        douguText.text = text;
    }

    public void DouguCheck(EventScript.Item item)
    {
       
       if (eventScripts[(int)item] != null)
       {
            switch (item)
            {
                case EventScript.Item.Fireinsurance:
                    Changeimage(dougu,fireinsurance, "火災保険");
                    dougu.gameObject.SetActive(true); break;
                case EventScript.Item.Earthquakeinsurance:
                    Changeimage(dougu,earthquakeinsurance, "地震保険");
                    dougu.gameObject.SetActive(true); break;
               
            }
       }               
       else
       {
            switch (item)
            {
                case EventScript.Item.Fireinsurance:
                    Debug.Log("火災道具なし");
                    dougu.gameObject.SetActive(true);
                    Changeimage(dougu,fireinsurance, "火災保険");
                    dougu.image.color = new Color(dougu.image.color.r, dougu.image.color.g, dougu.image.color.g, 0.5f);
                    dougu.interactable = false;
                    break;
                case EventScript.Item.Earthquakeinsurance:
                    Changeimage(dougu,earthquakeinsurance, "地震保険");
                    dougu.gameObject.SetActive(true);
                    dougu.image.color = new Color(dougu.image.color.r, dougu.image.color.g, dougu.image.color.g, 0.5f);
                    dougu.interactable = false;
                    break;
            }
        }
    }

    /// <summary>
    /// アイテムを取得
    /// </summary>
    public void GetItem()
    {
        if(disaster==true)//災害の回避に失敗したらここにしこたま書く
        {
            switch(currentdisaster)
            {
                case EventScript.Disaster.Fire:
                    downMoney += 15000;
                    downTairyoku += 40;
                    break;
                case EventScript.Disaster.Earthquake:
                    downMoney += 30000;
                    downTairyoku += 20;
                    break;
            }
        }
        else
        {
            switch (currentdisaster)
            {
                case EventScript.Disaster.Fire:
                    downMoney += 0;
                    downTairyoku += 5;
                    break;
                case EventScript.Disaster.Earthquake:
                    downMoney += 0;
                    downTairyoku += 5;
                    break;
            }
        }
        if(eventbool==true)
        {         
            if(eventScripts[(int)currentevent]==null)
            {
                Debug.Log(currentevent);
                eventScripts[(int)currentevent] = new EventScript(currentevent);//ここでアイテムを取得 
                switch(currentevent)
                {
                    case EventScript.Item.Stock:firstMoney += 15000;break;
                    case EventScript.Item.Earthquakeinsurance:firstMoney += 10000;break;
                    case EventScript.Item.Fireinsurance:firstMoney += 10000;break;
                }
            }            
        }
    }



    /// <summary>
    /// 株を持っているとき状態を表示
    /// </summary>
    public void StockImage()
    {
        if(eventScripts[(int)EventScript.Item.Stock] != null)
        {
            eventText.text += eventScripts[(int)EventScript.Item.Stock].Textselect();
            Changeimage(stockButton,stock, "株を売る("+upMoney+")円");
            stockButton.gameObject.SetActive(true);
            Debug.Log("株を買いました");
        }
    }
    /// <summary>
    /// 株を売った時表示
    /// </summary>
    public void StockBotton()
    {
        if (stocksell == true&& eventScripts[(int)EventScript.Item.Stock] != null)
        {
            if (eventScripts[(int)EventScript.Item.Stock].ReturnText() == GameObject.Find("GameText").GetComponent<GameText>().EventText(17, 1))
            {
                Debug.Log("普通に売りました");
                upMoney += 11000;
                eventScripts[(int)EventScript.Item.Stock].EventSelect(stock, false);
                eventScripts[(int)EventScript.Item.Stock] = null;
            }
            else if (eventScripts[(int)EventScript.Item.Stock].ReturnText() == GameObject.Find("GameText").GetComponent<GameText>().EventText(16, 1))
            {
                Debug.Log("安く売りました");
                upMoney += 1000;
                eventScripts[(int)EventScript.Item.Stock].EventSelect(stock, false);
                eventScripts[(int)EventScript.Item.Stock] = null;

            }
        }
    }

    /// <summary>
    /// お金が減る
    /// </summary>
    /// <returns></returns>
    public int MainMoney()
    {
        downMoney += firstMoney;
        firstMoney = 0;
        Debug.Log("払うお金"+downMoney+"+"+"貰うお金"+upMoney);
        return downMoney -upMoney;
    }

    public string StutusText()
    {
        string stutusText = "";
        if(firstMoney!=0)
        {
            for(int i=0;i<(int) EventScript.Item.END;i++)
            {
                if(eventScripts[i]!=null)
                {
                    Debug.Log(eventScripts[i]);
                    switch (eventScripts[i].Itemname())
                    {
                        case EventScript.Item.Stock: stutusText += gameText.StatusText(6, 1) + firstMoney + gameText.StatusText(7, 1) + "\n\n"; break;
                        case EventScript.Item.Fireinsurance: stutusText += gameText.StatusText(5, 1) + firstMoney + gameText.StatusText(7, 1) + "\n\n"; break;
                        case EventScript.Item.Earthquakeinsurance: stutusText += gameText.StatusText(4, 1) + firstMoney + gameText.StatusText(7, 1) + "\n\n"; break;
                    }
                }
            }
                   
                        
        }

        if(upMoney!=0)
        {
            stutusText += gameText.StatusText(10, 1) + upMoney + gameText.StatusText(9, 1) + "\n\n";
        }
       
        if(downMoney!=0)
        {
            stutusText += gameText.StatusText(11, 1) + downMoney + gameText.StatusText(7, 1) + "\n\n";
        }
        //foreach (var i in eventScripts)
        //{

        //    if (downMoney != 0)
        //    {
        //        switch (i.Itemname())
        //        {
        //            case EventScript.Item.Stock: stutusText += gameText.StatusText(6, 1) + "15000" + gameText.StatusText(7, 1) + "\n\n"; break;
        //            case EventScript.Item.Fireinsurance: stutusText += gameText.StatusText(5, 1) + "10000" + gameText.StatusText(7, 1) + "\n\n"; break;
        //            case EventScript.Item.Earthquakeinsurance: stutusText += gameText.StatusText(4, 1) + "10000" + gameText.StatusText(7, 1) + "\n\n"; break;
        //        }
        //    }
        //    else if (upMoney != 0)
        //    {
        //        switch (i.Itemname())
        //        {
        //            case EventScript.Item.Stock: stutusText += gameText.StatusText(10, 1) + upMoney + gameText.StatusText(9, 1) + "\n\n"; break;
        //            case EventScript.Item.Fireinsurance: stutusText += gameText.StatusText(5, 1) + "10000" + gameText.StatusText(7, 1) + "\n\n"; break;
        //            case EventScript.Item.Earthquakeinsurance: stutusText += gameText.StatusText(4, 1) + "10000" + gameText.StatusText(7, 1) + "\n\n"; break;
        //        }
        //    }
        //}
        return stutusText;
    }
    /// <summary>
    /// 体力が減る
    /// </summary>
    /// <returns></returns>
    public float MainTairyokuDown()
    {
        return downTairyoku;
    }
    /// <summary>
    /// GamePlayで実行
    /// </summary>
    public void EventGamePlay()
    {
        dayincIdence++;
        downMoney = 0;
        downTairyoku = 0;
        upMoney = 0;
        Debug.Log("イベントカウント"+dayincIdence);
        if (dayincIdence<3&&eventlimit==false)
        {
            int a = Random.Range(4, 5);
            switch (a)
            {
                case 1: Eventinsidence(EventScript.Item.Stock); eventlimit = true; break;
                case 2: Eventinsidence(EventScript.Item.Fireinsurance); eventlimit = true; break;
                case 3: Eventinsidence(EventScript.Item.Earthquakeinsurance); eventlimit = true; break;
                case 4: Disasterinsidence(EventScript.Disaster.Fire); eventlimit = true; break;
                case 5: Disasterinsidence(EventScript.Disaster.Earthquake); eventlimit = true; break;
            }
            if(a>=6)
            {
                Eventinsidence(EventScript.Item.Other); 
            }
        }
        else if (dayincIdence >= 3)
        {
            int a = Random.Range(1, 4);
            if (a == 1 && eventlimit == false)
            {
                Eventinsidence(EventScript.Item.Stock);
                dayincIdence = 0;
            }
            else if (a == 2 && eventlimit == false)
            {
                Eventinsidence(EventScript.Item.Fireinsurance);
                dayincIdence = 0;
            }
            else if (a == 3 && eventlimit == false)
            {
                Eventinsidence(EventScript.Item.Fireinsurance);
                dayincIdence = 0;
            }
            dayincIdence = 0;
            eventlimit = false;
        }
        else if (eventlimit == true)
        {
            Debug.Log("other");
            Eventinsidence(EventScript.Item.Other);
        }
        
        StockImage();
        
    }

    public void EventOpning()
    {              
        //GetItem();
        StockBotton();
        WriteEvent();
        // WriteEvent();
        eventbool = false;
        disaster = true;
        stocksell = false;
        maru.gameObject.SetActive(false);
        batu.gameObject.SetActive(false);
        dougu.gameObject.SetActive(false);
        currentdisaster = EventScript.Disaster.End;
        currentevent = EventScript.Item.END;
    }

    /// <summary>
    /// アイテムのテキストなど表示
    /// </summary>
    public void WriteEvent()
    {       
        foreach(var i in eventScripts)
        {
            if(i!=null)
            {
                Debug.Log("イベント"+i.Itemname());
                switch(i.Itemname())
                {                   
                    case EventScript.Item.Stock: i.EventSelect(stock, true); break;//株の状態を表示
                    case EventScript.Item.Fireinsurance: i.EventSelect(fireinsurance,true); break;
                    case EventScript.Item.Earthquakeinsurance: i.EventSelect(earthquakeinsurance,true); break;//株の状態を表示
                }
            }
        }
    }
}
