using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialyScript : MonoBehaviour
{
    public GameText gameText;
    public Text Image1;
    public Text FamilyButtontext;
    public GameMain gameMain;
    public EventManager eventManager;
    public Fade fade;   
    public Button familyMoneyButton;
    public string dialytext;
    private bool familybool = true;//家族イベントフラグ
    private bool FamilyMoneybool = false;//お金を払うかどうか
    private int familycount = 1;
    private int familymoneyPay;

    public void Onclick()
    {
        if (FamilyMoneybool == false)
        {
            FamilyMoneybool = true;
            familyMoneyButton.image.color = new Color(familyMoneyButton.image.color.r, familyMoneyButton.image.color.g, familyMoneyButton.image.color.b, 0.5f);
        }
        else
        {
            FamilyMoneybool = false;
            familyMoneyButton.image.color = new Color(familyMoneyButton.image.color.r, familyMoneyButton.image.color.g, familyMoneyButton.image.color.b, 1.0f);
        }
        
    }



    public void FamilyEventText()
    {      
        if(familybool&&gameMain.ReturnForDays()%7==0)
        {
            familyMoneyButton.gameObject.SetActive(true);
            switch (familycount)
            {
                case 1: dialytext = gameText.DialyText(10, 1)+"\n\n\n\n\n";FamilyButtontext.text = "5000円"; break;
                case 2: dialytext = gameText.DialyText(11, 1)+"\n\n\n\n\n"; FamilyButtontext.text = "10000円"; break;
                case 3: dialytext = gameText.DialyText(12, 1)+"\n\n\n\n\n"; FamilyButtontext.text = "7000円";break;
                case 4: dialytext = gameText.DialyText(13, 1) + "\n\n\n\n\n"; FamilyButtontext.text = "30000円"; break;
            }
        }
        else if(gameMain.ReturnForDays()==1)//初日
        {
            familyMoneyButton.gameObject.SetActive(false);
            dialytext = gameText.DialyText();
        }
        else
        {
            familyMoneyButton.gameObject.SetActive(false);
            dialytext = gameText.DialyText(Random.Range(4, 10), 1);
        }
        Image1.text =eventManager.StutusText() + gameMain.Kyuuryou() + dialytext+"\n\n"+gameMain.SelectStatusText();
        
    }

    public int FamilyEventPay()
    {
        if(FamilyMoneybool)
        {
            switch (familycount)
            {
                case 1:familymoneyPay = 5000;break;
                case 2: familymoneyPay = 10000; break;
                case 3:familymoneyPay = 7000;break;
                case 4:familymoneyPay = 30000;break;
            }
            familycount++;
        }
        return familymoneyPay;
    }

    public bool FamilyClear()
    {
        if (familycount >= 5)
        {
            return true;
        }
        else
            return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        familyMoneyButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade.Gamestates() == Fade.Gamestate.Opning)
        {
            familymoneyPay = 0;
        }
    }
}
