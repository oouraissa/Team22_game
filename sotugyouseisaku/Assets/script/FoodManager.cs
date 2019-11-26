using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{  
    public float tairyoku;
    public GameMain gameMain;
    public int foodmoney;
    public Image byoukiImage;
    public Text Listtext;
    public int deathday;
    private bool deathbool=false;
    public List<Byouki> currentByouki = new List<Byouki>();

    private void Start()
    {
        currentByouki = new List<Byouki>();
    }
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
                Listtext.text = i.ByoukiText() + "\n";                                             
            }
        }       
    }
    /// <summary>
    /// 特殊な病気の組み合わせ+死亡処理
    /// </summary>
    public void SpecialDeath()
    {
        foreach(var i in currentByouki)
        {
            if(i.ReturnDeathday()<=gameMain.ReturnForDays())
            {
                deathbool = true;
            }
            int KHS = 0;//風邪、貧血、食中毒のカウント
            if(i.Currentstate()== Byouki.Byoukistate.脳卒中)
            {
                deathbool = true;
                //gameMain.ScneSelect(GameMain.Gamestate.GameOver);//脳卒中は絶対死
            }
       else if(i.Currentstate() == Byouki.Byoukistate.風邪|| i.Currentstate() == Byouki.Byoukistate.貧血
               || i.Currentstate() == Byouki.Byoukistate.食中毒)
            {
                KHS++;
                if(KHS==3)
                {
                    if(i.Currentstate() != Byouki.Byoukistate.風邪|| i.Currentstate() != Byouki.Byoukistate.貧血 
                        || i.Currentstate() != Byouki.Byoukistate.食中毒)
                    {
                        deathbool = true;
                        //gameMain.ScneSelect(GameMain.Gamestate.GameOver);//風邪、貧血、食中毒＋何かで死
                    }
                }
            }
        }
       
    }

    /// <summary>
    /// 死亡フラグ
    /// </summary>
    /// <returns></returns>
    public bool Deathbool()
    {
        return deathbool;
    }

    
    public void MoneyandTairyokuReset()
    {
        foodmoney = 0;
        tairyoku = 0;
    }

    /// <summary>
    /// 体力の計算
    /// </summary>
    /// <returns></returns>
    public float Tairyoku()
    {
        foreach (Transform i in transform)
        {
            tairyoku += i.gameObject.GetComponent<Foodscript>().TairyokuCalcu();
        }
        return tairyoku;
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
        if (currenttairyoku<= 0)
        {
            Debug.Log("脳卒中");
            RemoveByouki(Byouki.Byoukistate.脳卒中);
            Byouki nousottyuu = new Byouki(Byouki.Byoukistate.脳卒中, 0, gameMain.ReturnForDays());
            currentByouki.Add(nousottyuu);
            //Byouki nousottyuu = GameObject.Find("Byouki").GetComponent<Byouki>();
            //foreach (var i in currentByouki)
            //{
            //    Debug.Log(i);
            //    if (i.Currentstate()==Byouki.Byoukistate.脳卒中)//病気のリストに同じ名前があったらスルー
            //    {
                    
            //        return;
                    
            //    }
            //    else
            //    {
            //        currentByouki.Add(nousottyuu);
            //    }              
            //}          
        }
        else if(currenttairyoku>0&&currenttairyoku<=20)
        {
            int random = Random.Range(1, 4);
            if(random==1)
            {
                RemoveByouki(Byouki.Byoukistate.肺炎);
                Byouki haien = new Byouki(Byouki.Byoukistate.肺炎, 10,gameMain.ReturnForDays());
                currentByouki.Add(haien);
                //foreach (var i in currentByouki)
                //{
                //    if (i.Currentstate() == Byouki.Byoukistate.肺炎)
                //    {
                //        return;
                //    }
                //    else
                //    {
                //        currentByouki.Add(haien);
                //    }
                   
                //}
            }
        }
        else if(currenttairyoku > 20 && currenttairyoku <= 50)
        {
            int random = Random.Range(1, 6);
            if(random==1)
            {
                RemoveByouki(Byouki.Byoukistate.風邪);
                Byouki kaze = new Byouki(Byouki.Byoukistate.風邪);
                currentByouki.Add(kaze);
                //foreach (var i in currentByouki)
                //{
                //    if (i.Currentstate() == Byouki.Byoukistate.風邪)
                //    {
                //        return;
                //    }
                //    else
                //    {
                //        Byouki kaze = new Byouki(Byouki.Byoukistate.風邪);
                //        currentByouki.Add(kaze);
                //    }
                       
                //}
            }
            else if(random==2)
            {
                RemoveByouki(Byouki.Byoukistate.貧血);
                Byouki hinketu = new Byouki(Byouki.Byoukistate.貧血);
                currentByouki.Add(hinketu);
                //foreach (var i in currentByouki)
                //{
                //    if (i.Currentstate() == Byouki.Byoukistate.貧血)
                //    {
                //        return;
                //    }
                //    else
                //    {
                //        Byouki hinketu = new Byouki(Byouki.Byoukistate.貧血);
                //        currentByouki.Add(hinketu);
                //    }
                //}
            }
        }
        else if(currenttairyoku>50&&foodmoney!=0)
        {
            int random = Random.Range(1, 11);
            if (random == 1)
            {
                RemoveByouki(Byouki.Byoukistate.食中毒);
                Byouki byouki = new Byouki(Byouki.Byoukistate.食中毒);
                currentByouki.Add(byouki);
                //foreach (var i in currentByouki)
                //{
                //    if (i.Currentstate() == Byouki.Byoukistate.食中毒)
                //    {
                //        return;
                //    }
                //    else
                //    {
                //        Byouki shokutyudoku = new Byouki(Byouki.Byoukistate.食中毒);
                //        currentByouki.Add(shokutyudoku);
                //    }
                //}
            }
        }
    }

    public void RemoveByouki(Byouki.Byoukistate removestate)
    {
        foreach(var i in currentByouki)
        {
            if(i.Currentstate()==removestate)
            {
                currentByouki.Remove(i);
            }
        }
    }

}
