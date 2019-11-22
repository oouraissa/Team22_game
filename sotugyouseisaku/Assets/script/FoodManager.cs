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
        拒食症,
        心筋症,
        ガン,
        肺炎,
        脳卒中
    }
    public float tairyoku;
    public int foodmoney;
    public Image byoukiImage;
    public Text Listtext;
    private List<Byouki> currentByouki = new List<Byouki>();

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
            byoukiImage.gameObject.SetActive(true);
        }

        foreach(var i in currentByouki)
        {
            Listtext.text = i.ToString() + "\n";
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
        else if(currenttairyoku>50)
        {
            int random = Random.Range(1, 11);
            if (random == 1)
            {
                currentByouki.Add(Byouki.食中毒);
            }
        }
    }
    
}
