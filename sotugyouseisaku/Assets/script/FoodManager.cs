using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{  
    public float tairyoku;
    public GameMain gameMain;
    public Foodscript smalldrug;
    public Foodscript leargedrug;
    public Foodscript BigDrug;
    public int foodmoney;
    public Image byoukiImage;
    public Text Listtext;
    public int deathday;
    private bool deathbool=false;
    public List<Button> buttos;
    //public List<Byouki> currentByouki = new List<Byouki>();
    public Byouki[] currentByouki = new Byouki[(int)Byouki.Byoukistate.End];
    private void Start()
    {
        currentByouki = new Byouki[(int)Byouki.Byoukistate.End];
        //Debug.Log(currentByouki.Length);
        smalldrug.gameObject.SetActive(false);
        leargedrug.gameObject.SetActive(false);
        BigDrug.gameObject.SetActive(false);
    }
    /// <summary>
    /// 病気を細分化して薬によって処理
    /// </summary>
    public void Heal()
    {       
        List<int> smallByouki = new List<int>();
        List<int> leargeByouki = new List<int>();
        List<int> BigByouki = new List<int>();
        //Debug.Log(smalldrug.SmallCount());
        //Debug.Log(leargedrug.SmallCount());
        //Debug.Log(BigDrug.SmallCount());
        foreach (var i in currentByouki)
        {
            if(i!=null)
            {
                switch (i.Currentstate())
                {
                    case Byouki.Byoukistate.風邪: smallByouki.Add((int)Byouki.Byoukistate.風邪);break;
                    case Byouki.Byoukistate.貧血: smallByouki.Add((int)Byouki.Byoukistate.貧血); break;
                    case Byouki.Byoukistate.食中毒: smallByouki.Add((int)Byouki.Byoukistate.食中毒); break;
                    case Byouki.Byoukistate.肺炎: leargeByouki.Add((int)Byouki.Byoukistate.肺炎); break;
                }               
            }           
        }
        //Debug.Log(leargeByouki.Count);
        if(smalldrug.SmallCount()==true)
        {         
            int small = smallByouki[Random.Range(0, smallByouki.Count+1)];
            currentByouki[small] = null;
            smalldrug.ResetCount();
            //Debug.Log(smallByouki.Count);
        }

        if (leargedrug.LeargeCount() == true)
        {
            int learge = Random.Range(0, leargeByouki.Count+1);
            //currentByouki.Remove(leargeByouki[learge]);
            leargedrug.ResetCount();
        }
        if(BigDrug.BigCount()==true)
        {
            int big = Random.Range(0, BigByouki.Count + 1);
            //currentByouki.Remove(BigByouki[big]);
            BigDrug.ResetCount();
        }

        if(smallByouki.Count==0&&leargeByouki.Count==0&&BigByouki.Count==0)
        {
            byoukiImage.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 病気をテキストに記載
    /// </summary>
    public void ByoukiText()
    {
        Listtext.text = "";
        //string a;
        foreach(var i in currentByouki)
        {
            
            if(i!=null)
            {
                byoukiImage.gameObject.SetActive(true);
                Listtext.text += i.ByoukiText();
            }
        }

        

    }
    /// <summary>
    /// 特殊な病気の組み合わせ+死亡処理
    /// </summary>
    public void SpecialDeath()
    {
        int KHS = 0;//風邪、貧血、食中毒のカウント
        List<Byouki> oldByouki = new List<Byouki>();
        for(int i=0;i<currentByouki.Length;i++)
        {
            if(currentByouki[i]!=null)
            {
                oldByouki.Add(currentByouki[i]);
            }
            
        }
        foreach (var i in oldByouki)
        {
            if (i.ReturnDeathday() <= gameMain.ReturnForDays() && i.Returndeathbool() == true)
            {
                deathbool = true;
            }
            if (i.Currentstate() == Byouki.Byoukistate.脳卒中)
            {
                deathbool = true;
                //gameMain.ScneSelect(GameMain.Gamestate.GameOver);//脳卒中は絶対死
            }
       else if (i.Currentstate() == Byouki.Byoukistate.風邪 || i.Currentstate() == Byouki.Byoukistate.貧血
                    || i.Currentstate() == Byouki.Byoukistate.食中毒)
            {
                KHS++;
                //Debug.Log("KHS" + KHS);
                if (KHS == 3)
                {
                    if (i.Currentstate() != Byouki.Byoukistate.風邪 || i.Currentstate() != Byouki.Byoukistate.貧血
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

    /// <summary>
    /// お金と体力をリセット　
    /// </summary>
    public void MoneyandTairyokuReset()
    {
        foreach(Transform i in transform)
        {
            i.gameObject.GetComponent<Foodscript>().Resetselect();
        }
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

    public void ResetFoodselect()
    {
        tairyoku = 0;
        foodmoney = 0;
        foreach (var b in buttos)
        {           
             b.interactable = true;           
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
        float currenttairyoku = gameMain.Tairyoku();
        //List<Byouki> reset = currentByouki;      
        if (currenttairyoku<= 0)
        {
            Debug.Log("脳卒中");
           // Byouki nousottyuu = new Byouki(Byouki.Byoukistate.脳卒中, 0, gameMain.ReturnForDays());
            currentByouki[(int)Byouki.Byoukistate.脳卒中] = new Byouki(Byouki.Byoukistate.脳卒中, 0, gameMain.ReturnForDays());
        }
        else if (currenttairyoku > 0 && currenttairyoku <= 20)
        {
            int random = Random.Range(1, 3);       
            if (random == 1)
            {                
                Byouki haien = new Byouki(Byouki.Byoukistate.肺炎, 10, gameMain.ReturnForDays());
                currentByouki[(int)Byouki.Byoukistate.肺炎] = haien;
                leargedrug.gameObject.SetActive(true);
                //reset.Add(haien);
            }
        }
        else if (currenttairyoku > 20 && currenttairyoku <= 50)
        {
            int random = Random.Range(1, 5);
            if (random == 1)
            {
                //RemoveByouki(Byouki.Byoukistate.風邪);
               // Byouki kaze = new Byouki(Byouki.Byoukistate.風邪);
                currentByouki[(int)Byouki.Byoukistate.風邪] = new Byouki(Byouki.Byoukistate.風邪);
                smalldrug.gameObject.SetActive(true);

            }
            else if (random == 2)
            {
                //RemoveByouki(Byouki.Byoukistate.貧血);
               // Byouki hinketu = new Byouki(Byouki.Byoukistate.貧血);
                currentByouki[(int)Byouki.Byoukistate.貧血] = new Byouki(Byouki.Byoukistate.貧血);
                smalldrug.gameObject.SetActive(true);
            }
        }
        else if (currenttairyoku > 50 && foodmoney != 0)
        {
            int random = Random.Range(1, 11);
            if (random == 1)
            {
                //RemoveByouki(Byouki.Byoukistate.食中毒);
                //Byouki byouki = new Byouki(Byouki.Byoukistate.食中毒);
                currentByouki[(int)Byouki.Byoukistate.食中毒] = new Byouki(Byouki.Byoukistate.食中毒);
                smalldrug.gameObject.SetActive(true);

            }
        }

        Heal();
    }

   

}
