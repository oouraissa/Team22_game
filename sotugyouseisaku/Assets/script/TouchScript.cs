using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScript : MonoBehaviour
{
    public Image byoukiImage;//病気UI
    public Image byoukiList;//病気リスト
    private void OnMouseEnter()
    {
        if(gameObject.name==byoukiImage.name)
        {
            byoukiList.gameObject.SetActive(true);
        }

    }

    private void OnMouseExit()
    {
        if (gameObject.name == byoukiList.name)
        {
            byoukiList.gameObject.SetActive(false);
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
