using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameText : MonoBehaviour
{
    public string[] dialyMessage; //テキストの加工前の一行を入れる変数
    public string[,] dialyWords; //テキストの複数列を入れる2次元配列 

    public string[] byoukiMessage;
    public string[,] byoukiWords;

    public string[] eventMessage;
    public string[,] eventWords;

    public string[] statusMessage;
    public string[,] statusWords;

    private int dialyRowLength; //テキスト内の行数を取得する変数
    private int eventRowLength;
    private int statusRowLength;
    private int byoukiRowLength;

    private int dialyColumn; //テキスト内の列数を取得する変数
    private int eventColumn;
    private int statusColumn;
    private int byoukiColumn;

    private string[] textLines = new string[4];//テキストの数
    // Start is called before the first frame update
    public void TextSelect()
    {

        //image1 = GetComponent<Text>();
        TextAsset dialyAsset = new TextAsset();
        TextAsset eventAsset = new TextAsset();
        TextAsset statusAsset = new TextAsset();
        TextAsset byoukiAsset = new TextAsset();

        dialyAsset = Resources.Load("Dialy", typeof(TextAsset)) as TextAsset;
        eventAsset = Resources.Load("Event", typeof(TextAsset)) as TextAsset;
        statusAsset = Resources.Load("Status", typeof(TextAsset)) as TextAsset;
        byoukiAsset = Resources.Load("Byouki", typeof(TextAsset)) as TextAsset;

        textLines[0] = dialyAsset.text;
        textLines[1] = eventAsset.text;
        textLines[2] = statusAsset.text;
        textLines[3] = byoukiAsset.text;

       
        dialyMessage = textLines[0].Split('\n');
        eventMessage = textLines[1].Split('\n');
        statusMessage = textLines[2].Split('\n');
        byoukiMessage = textLines[3].Split('\n');

        dialyColumn = dialyMessage[0].Split('\t').Length;
        eventColumn = eventMessage[0].Split('\t').Length;
        statusColumn = statusMessage[0].Split('\t').Length;
        byoukiColumn = byoukiMessage[0].Split('\t').Length;

        dialyRowLength = dialyMessage.Length;
        eventRowLength = eventMessage.Length;
        statusRowLength = statusMessage.Length;
        byoukiRowLength = byoukiMessage.Length;

        dialyWords = new string[dialyRowLength, dialyColumn];
        eventWords = new string[eventRowLength, eventColumn];
        statusWords = new string[statusRowLength, statusColumn];
        byoukiWords = new string[byoukiRowLength, byoukiColumn];

        //Debug.Log(dialyWords[dialyRowLength, dialyColumn]);
        //dialyを登録
        for (int i = 0; i < dialyRowLength; i++)
        {
            string[] tempWords = dialyMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入
            //Debug.Log(tempWords[0]);
            for (int n = 0; n < dialyColumn; n++)
            {              
                dialyWords[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                //Debug.Log(dialyWords[i, n]);                              //Debug.Log(i);
            }
        }

        //event
        for (int i = 0; i < eventRowLength; i++)
        {
            string[] evtempWords = eventMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < eventColumn; n++)
            {
                eventWords[i, n] = evtempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                //Debug.Log(eventWords[i, n]);
            }
        }
        //status
        for (int i = 0; i < statusRowLength; i++)
        {
            string[] sttempWords = statusMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < statusColumn; n++)
            {
                statusWords[i, n] = sttempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                //Debug.Log(i);
            }
        }

        //byouki
        for (int i = 0; i < byoukiRowLength; i++)
        {
            string[] sttempWords = byoukiMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < byoukiColumn; n++)
            {
                byoukiWords[i, n] = sttempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                //Debug.Log(byoukiWords);
            }
        }
    }

    public string DialyText(int rowLength,int columnLength)
    {
        string text = dialyWords[rowLength, columnLength];
        return text;
    }

    public string DialyText()
    {
        string text = dialyWords[0,1];
        return text;
    }

    public string EventText(int rowLength, int columnLength)
    {
        string text = eventWords[rowLength, columnLength];
        return text;
    }

    public string EventText()
    {
        string text = eventWords[0,1];
        return text;
    }

    public string StatusText(int rowLength, int columnLength)
    {
        string text = statusWords[rowLength, columnLength];
        return text;
    }

    public string StatusText()
    {
        string text = statusWords[2,1];
        return text;
    }

    public string ByoukiText(int rowLength, int columnLength)
    {
        string text = byoukiWords[rowLength, columnLength];
        return text;
    }
    
    public string ByoukiText()
    {
        string text = byoukiWords[0, 1];
        return text;
    }

    // Update is called once per frame
    private void Start()
    {
        //Debug.Log(byoukiWords);
    }
}
