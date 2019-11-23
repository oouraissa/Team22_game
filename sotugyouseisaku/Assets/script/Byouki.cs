using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byouki : MonoBehaviour
{
    public enum Byoukistate
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

    Byoukistate currentstate;
    private int Deathday;
    public GameMain gameMain;
    public bool deathbool;//要らないかも

    public Byouki(Byoukistate state,int deathday)
    {
        int  daynow= gameMain.ReturnForDays();
        currentstate = state;
        Deathday = daynow + deathday;
        deathbool= true;
    }

    public Byouki(Byoukistate state)
    {
        currentstate = state;
        deathbool = false;
    }

    public string ByoukiText(Byoukistate byoukitext)
    {
        return byoukitext.ToString();
    }

    public void Death()
    {
        if(Deathday<=gameMain.ReturnForDays()&&deathbool)
        {
            gameMain.ScneSelect(GameMain.Gamestate.GameOver);
        }
        else
        {
            return;
        }
    }



    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
