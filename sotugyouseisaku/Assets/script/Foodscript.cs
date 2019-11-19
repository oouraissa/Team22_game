using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foodscript : MonoBehaviour
{
    enum Foods
    {

        Beer,
        Ryokutya,
        StrongZero,
        Been,
        Karaage,
        Ramen
    }

    private List<Foods> currentfoods = new List<Foods>();
    private int foodhelth;
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
            case "Karaage": SelectFood(Foods.Karaage); break;
            case "Ramen": SelectFood(Foods.Ramen); break;
        }       
        Debug.Log(tairyoku + "+" + foodmoney);
       
    }

    public float TairyokuCalcu()
    {
        float calcu = tairyoku;
        tairyoku = (int)((calcu / 200)*100)+0.5f;
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
                currentfoods.Add(Foods.Beer);
                tairyoku += 15;
                foodmoney += 300;break;
            case Foods.Ryokutya:
                currentfoods.Add(Foods.Ryokutya);
                tairyoku += 5;
                foodmoney += 160; break;
            case Foods.StrongZero:
                currentfoods.Add(Foods.StrongZero);
                tairyoku += 30;
                foodmoney += 150; break;
            case Foods.Been:
                currentfoods.Add(Foods.Been);
                tairyoku += 2;
                foodmoney += 100; break;
            case Foods.Karaage:
                currentfoods.Add(Foods.Karaage);
                tairyoku += 20;
                foodmoney += 500; break;
            case Foods.Ramen:
                currentfoods.Add(Foods.Ramen);
                tairyoku += 40;
                foodmoney += 650; break;             
        }
        
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
