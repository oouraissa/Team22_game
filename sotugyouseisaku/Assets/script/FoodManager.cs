using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    //enum Byouki
    //{
    //    風邪,
    //    貧血,
    //    食中毒,
    //    糖尿病,
    //    不眠症,
    //    高血圧,
    //    栄養失調,
    //    ガン,
    //    肺炎,
    //    脳卒中
    //}
    
    public float tairyoku;
    public GameMain gameMain;
    public int foodmoney;
    public Image byoukiImage;
    public Text Listtext;
    public int deathday;
    private bool deathbool=false;
    private List<Byouki> currentByouki = new List<Byouki>();

    /// <summary>
    /// 病気をテキストに記載
    /// </summary>
    public void ByoukiText()
    {
        if (currentByouki.Count == 0)
        {
            return;
        }
        else
        {
            byoukiImage.gameObject.SetActive(true);
            foreach (var i in currentByouki)
            {
                Listtext.text = i.ToString() + "\n";                                             
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
        foreach(var i in currentByouki)//特殊な病気の組み合わせ
        {
            i.Death();
            int KHS = 0;//風邪、貧血、食中毒のカウント
            if(i.ToString()== Byouki.Byoukistate.脳卒中.ToString())
            {
                deathbool=true;//脳卒中は絶対死
            }
       else if(i.ToString()== Byouki.Byoukistate.風邪.ToString() || i.ToString() == Byouki.Byoukistate.貧血.ToString()
               || i.ToString() == Byouki.Byoukistate.食中毒.ToString())
            {
                KHS++;
                if(KHS==3)
                {
                    if(i.ToString()!= Byouki.Byoukistate.風邪.ToString() || i.ToString()!= Byouki.Byoukistate.貧血.ToString() 
                        || i.ToString()!= Byouki.Byoukistate.食中毒.ToString())
                    {
                        deathbool=true;//風邪、貧血、食中毒＋何かで死
                    }
                }
            }
        }
    }

    public void Scene()
    {
        if(deathbool)
            gameMain.ScneSelect(GameMain.Gamestate.GameOver);
        else
        gameMain.ScneSelect(GameMain.Gamestate.Opning); //死なないのならばOpningシーンへ
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
        float currenttairyoku = gameMain.Tairyoku();
        if(currenttairyoku<=0)
        {
            
            foreach(var i in currentByouki)
            {
                if(i.ToString()==Byouki.Byoukistate.脳卒中.ToString())//病気のリストに同じ名前があったらスルー
                {
                    return;
                }
                else
                {
                    Byouki nousottyuu = new Byouki(Byouki.Byoukistate.脳卒中, 0);
                    currentByouki.Add(nousottyuu);
                }              
            }          
        }
        else if(currenttairyoku>0&&currenttairyoku<=20)
        {
            int random = Random.Range(1, 4);
            if(random==1)
            {
                Byouki haien = new Byouki(Byouki.Byoukistate.肺炎, 10);
                foreach (var i in currentByouki)
                {
                    if (i.ToString() == Byouki.Byoukistate.肺炎.ToString())
                    {
                        return;
                    }
                    else
                    {
                        currentByouki.Add(haien);
                    }
                   
                }
            }
        }
        else if(currenttairyoku > 20 && currenttairyoku <= 50)
        {
            int random = Random.Range(1, 6);
            if(random==1)
            {
                
                foreach (var i in currentByouki)
                {
                    if (i.ToString() == Byouki.Byoukistate.風邪.ToString())
                    {
                        return;
                    }
                    else
                    {
                        Byouki kaze = new Byouki(Byouki.Byoukistate.風邪);
                        currentByouki.Add(kaze);
                    }
                       
                }
            }
            else if(random==2)
            {
                
                foreach (var i in currentByouki)
                {
                    if (i.ToString() == Byouki.Byoukistate.貧血.ToString())
                    {
                        return;
                    }
                    else
                    {
                        Byouki hinketu = new Byouki(Byouki.Byoukistate.貧血);
                        currentByouki.Add(hinketu);
                    }
                }
            }
        }
        else if(currenttairyoku>50&&foodmoney!=0)
        {
            int random = Random.Range(1, 11);
            if (random == 1)
            {
                Byouki byouki = new Byouki(Byouki.Byoukistate.食中毒);
                foreach (var i in currentByouki)
                {
                    if (i.ToString() == Byouki.Byoukistate.食中毒.ToString())
                    {
                        return;
                    }
                    else
                    {
                        Byouki shokutyudoku = new Byouki(Byouki.Byoukistate.食中毒);
                        currentByouki.Add(shokutyudoku);
                    }
                }
            }
        }
    }
    
}
