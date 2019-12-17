using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScript
{
    public enum Item
    {
        Stock,
        Fireinsurance,
        Earthquakeinsurance,
        Other,
        END
    }
    public enum Disaster
    {
        Fire,
        Earthquake,
        Fraud,
        End,
    }

    private Item currentItem;
    private string eventText;
   
    

    public EventScript(Item Itemname)
    {
        this.currentItem = Itemname;
    }

    public EventScript()
    {
        this.currentItem = Item.Other;
    }

    
    /// <summary>
    /// 取得したら表示するテキスト
    /// </summary>
    /// <returns></returns>
    public string Textselect()
    {       
        if(this.currentItem==Item.Stock)
        {
            int a = Random.Range(1, 4);
            if(a==1)
            {
                eventText = GameObject.Find("GameText").GetComponent<GameText>().EventText(17,1);
            }
            if(a==2||a==3)
            {
                eventText = GameObject.Find("GameText").GetComponent<GameText>().EventText(16, 1);
            }          
        }
        
        return "\n\n\n\n"+this.eventText;
    }

    public string ReturnText()
    {
        return this.eventText;
    }


    /// <summary>
    ///　アイテム取得したら出すやつ
    /// </summary>
    /// <param name="itemimage">イラスト表示</param>
    /// <param name="active">イラスト表示</param>
    public void EventSelect(Image itemimage,bool active)
    {
        if(active==true)
        {
            itemimage.gameObject.SetActive(true);
        }
        else
        {
            itemimage.gameObject.SetActive(false);
        }       
    }

    public Item Itemname()
    {
        return this.currentItem;
    }
   
    
}
