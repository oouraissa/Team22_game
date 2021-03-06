﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byouki /*: MonoBehaviour*/
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
        脳卒中,
        End
    }

    Byoukistate currentstate;
    private int Deathday;
    public GameMain gameMain;
    public bool deathbool;//要らないかも
    private int ID;

    public Byouki(Byoukistate state,int deathday,int daynow)
    {
        this.currentstate = state;
        //this.ID=Byoukistate.
        this.Deathday = daynow+ deathday;//現在の日にち+deathdayで死亡
        this.deathbool = true;
    }

    /// <summary>
    /// 死なない病気
    /// </summary>
    /// <param name="state"></param>
    public Byouki(Byoukistate state)
    {
        currentstate = state;
        deathbool = false;
    }

    /// <summary>
    /// この日にちが経過したとき死亡
    /// </summary>
    /// <returns></returns>
    public int ReturnDeathday()
    {
        return Deathday;
    }

    public bool Returndeathbool()
    {
        return deathbool;
    }

    public Byoukistate Currentstate()
    {
        return currentstate;
    }

    public string ByoukiText()
    {
        return "\n"+this.currentstate.ToString()+"\n";
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    gameMain=GetComponent<GameMain>();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
