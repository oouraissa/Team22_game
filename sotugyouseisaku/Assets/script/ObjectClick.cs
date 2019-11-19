using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectClick : MonoBehaviour
{
    private List<string> messageList = new List<string>();
    public Text text;
    int messgeindex;
    int messagecount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Novel());
        messagecount = 0;
        messgeindex = 0;
    }

    public void AddList(string massage)
    {
        messageList.Add(massage);
    }

    public IEnumerator Novel()
    {
        string a = text.text;
        a = "";
        while (messageList[messgeindex].Length > messagecount)//文字をすべて表示していない場合ループ
        {
            text.text += messageList[messgeindex][messagecount];//一文字追加
            messagecount++;//現在の文字数
            yield return new WaitForSeconds(2);//任意の時間待つ
        }

        if (messgeindex < messageList.Count)//全ての会話を表示したか
        {
            StartCoroutine(Novel());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
