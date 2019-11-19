using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public float tairyoku;
    public int foodmoney;

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
    
    
}
