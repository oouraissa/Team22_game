using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    enum Byouki
    {
        風邪,
        貧血,
        食中毒,
        糖尿病,
        不眠症,
        高血圧,
        栄養失調,
        ガン,
        肺炎,
        脳卒中
    }
    public float tairyoku;
    public GameMain gameMain;
    public int foodmoney;
    public Image byoukiImage;
    public Text Listtext;
    public bool byoukibool;
    public int deathday;
    private List<Byouki> currentByouki = new List<Byouki>();

    private void Start()
    {
        byoukibool = false;
    }
    /// <summary>
    /// 病気をテキストに記載
    /// </summary>
    public void CurrentByouki()
    {
        if (currentByouki.Count == 0)
        {
            return;
        }
        else
        {
            byoukibool = true;
            byoukiImage.gameObject.SetActive(true);
            foreach (var i in currentByouki)
            {
                Listtext.text = i.ToString() + "\n";
                if (i.ToString() == Byouki.肺炎.ToString())
                {
                   
                }                               
            }
        }       
    }

    /// <summary>
    /// 体力
    /// </summary>
    /// <returns></returns>
    public float Tairyoku()
    {
        foreach(Transform i in transform)
        {
            tairyoku += i.gameObject.GetComponent<Foodscript>().TairyokuCalcu();
        }
        return tairyoku;
    }

    public void ByoukiDeath()
    {
        if(deathday == 0&&byoukibool==true)
        {
            
        }
        foreach(var i in currentByouki)//特殊な病気の組み合わせ
        {
            int KHS = 0;//風邪、貧血、食中毒のカウント
            if(i.ToString()==Byouki.脳卒中.ToString())
            {
                gameMain.ScneSelect(GameMain.Gamestate.GameOver);//脳卒中は絶対死
            }
            else if(i.ToString()==Byouki.風邪.ToString()|| i.ToString() == Byouki.貧血.ToString()||i.ToString() == Byouki.食中毒.ToString())
            {
                KHS++;
                if(KHS==3)
                {
                    if(i.ToString()!= Byouki.風邪.ToString() ||i.ToString()!= Byouki.貧血.ToString()|| i.ToString() != Byouki.食中毒.ToString())
                    {
                        gameMain.ScneSelect(GameMain.Gamestate.GameOver);//風邪、貧血、食中毒＋何かで死
                    }
                }
            }
        }
    }



    /// <summary>
    /// 食べ物の値段
    /// </summary>
    /// <returns></returns>
    public int Moneys()
    {
        foreach (Transform i in transform)
        {
            foodmoney += i.gameObject.GetComponent<Foodscript>().SelectFood();
        }
        return foodmoney;
    }
    
    /// <summary>
    /// 体力によって病気になる
    /// </summary>
    public void SelectByouki()
    {
        float currenttairyoku = Tairyoku();
        if(currenttairyoku<=0)
        {
            currentByouki.Add(Byouki.脳卒中);
        }
        else if(currenttairyoku>0&&currenttairyoku<=20)
        {
            int random = Random.Range(1, 4);
            if(random==1)
            {
                currentByouki.Add(Byouki.肺炎);
            }
        }
        else if(currenttairyoku > 20 && currenttairyoku <= 50)
        {
            int random = Random.Range(1, 6);
            if(random==1)
            {
                currentByouki.Add(Byouki.風邪);
            }
            else if(random==2)
            {
                currentByouki.Add(Byouki.貧血);
            }
        }
        else if(currenttairyoku>50&&foodmoney!=0)
        {
            int random = Random.Range(1, 11);
            if (random == 1)
            {
                currentByouki.Add(Byouki.食中毒);
            }
        }
    }
    
}
