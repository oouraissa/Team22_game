using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Foodscript : MonoBehaviour
{    enum  Foods
    {
        Beer,
        Ryokutya,
        StrongZero,
        Been,
        Steak,
        Ramen,
        Bento,
        SmallDrug,
        LeargeDrug,
        BigDrug
    }
    public Button SmallDrug;
    public Button LeargeDrug;
    public Button BigDrug;
    public Button Beer;
    public Button Ryokutya;
    public Button StrongZero;
    public Button Been;
    public Button Steak;
    public Button Ramen;
    public Button Bento;

   
   
    private Foods currentfoods;
    private int foodsick;
    private int smallCount;
    private int leargeCount;
    private int bigCount;
    private int foodmoney;
    private float tairyoku;
    public void Onclick()
    {
        switch(gameObject.name)
        {
            case "Beer": SelectFood(Foods.Beer); break;
            case "Ryokutya": SelectFood(Foods.Ryokutya); break;
            case "StrongZero": SelectFood(Foods.StrongZero); break;
            case "Been": SelectFood(Foods.Been); break;
            case "Steak": SelectFood(Foods.Steak); break;
            case "Ramen": SelectFood(Foods.Ramen); break;
            case "Bento": SelectFood(Foods.Bento); break;
            case "SmallDrug": SelectFood(Foods.SmallDrug); break;
            case "LeargeDrug": SelectFood(Foods.LeargeDrug); break;
            case "BigDrug": SelectFood(Foods.BigDrug); break;
        }       
        Debug.Log(tairyoku + "+" + foodmoney);      
    }

    public float TairyokuCalcu()
    {
        float calcu = tairyoku;
        tairyoku = (int)((calcu / 200) * 100);
        return tairyoku;
    }

    public int SelectFood()
    {
        return foodmoney;
    }

    private void SelectFood(Foods oldfoods)
    {    
        switch (oldfoods)
        {
            case Foods.Beer:
                currentfoods=Foods.Beer;
                tairyoku += 20;
                foodmoney += 200;
                Beer.interactable = false;
                break;
            case Foods.Ryokutya:
                currentfoods=Foods.Ryokutya;
                tairyoku += 5;
                foodmoney += 120;
                Ryokutya.interactable = false;
                break;
            case Foods.StrongZero:
                currentfoods=Foods.StrongZero;
                tairyoku += 15;
                foodmoney += 110;
                StrongZero.interactable = false;
                break;
            case Foods.Been:
                currentfoods=Foods.Been;
                tairyoku += 2;
                foodmoney += 200;
                Been.interactable = false;
                break;
            case Foods.Steak:
                currentfoods=Foods.Steak;
                tairyoku += 40;
                foodmoney += 2000;
                Steak.interactable = false;
                break;
            case Foods.Ramen:
                currentfoods=Foods.Ramen;
                tairyoku += 20;
                foodmoney += 600;
                Ramen.interactable = false;
                break;            
            case Foods.Bento:
                currentfoods=Foods.Bento;
                tairyoku += 40;
                foodmoney += 0;
                Bento.interactable = false;
                break;
            case Foods.SmallDrug:
                this.smallCount++;
                foodmoney += 2000;
                tairyoku +=1;
                currentfoods=Foods.SmallDrug;
                SmallDrug.interactable = false;
                break;
            case Foods.LeargeDrug:
                this.leargeCount++;
                foodmoney += 5000;
                tairyoku += 1;
                currentfoods=Foods.LeargeDrug;
                LeargeDrug.interactable = false;
                break;
            case Foods.BigDrug:
                this.bigCount++;
                foodmoney += 10000;
                tairyoku += 1;
                currentfoods=Foods.BigDrug;
                BigDrug.interactable = false;
                break;
        }
        
    }

    public void Resetselect()
    {
        foodmoney = 0;
        tairyoku = 0;        
    }
    public void ResetCount()
    {
        smallCount = 0;
        leargeCount = 0;
        bigCount = 0;
    }

    /// <summary>
    /// フード名をstring型で返す
    /// </summary>
    /// <returns>フードの名前</returns>
    public string CurrentFoods()
    {
        return currentfoods.ToString();
    }

    public bool BigCount()
    {
        int a = Random.Range(10, 21);
        if (bigCount >= a)
        {
            return true;
        }
        else
            return false;
    }
    public bool LeargeCount()
    {
        int a = Random.Range(5, 9);
        if (leargeCount>= 1)
        {
            return true;
        }
        else
            return false;
    }

    public bool SmallCount()
    {
        //Debug.Log(smallCount);
        if (smallCount >= 3)
        {
            return true;
        }
        else
            return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
