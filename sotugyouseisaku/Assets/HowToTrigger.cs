using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Assign Tutorial Field")]
    public GameObject TUTField, TUTField2,TUTBack;
    bool onClicked;
    void Start()
    {
        onClicked = false;
        TUTField.SetActive(false);
        TUTField2.SetActive(false);
        TUTBack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 pos = TUTField.transform.position;
        //if (onClicked)
        //{
        //    pos.x--;
        //    if (pos.x <= 0)
        //    { 
        //        pos.x = 0;
        //        onClicked = false;
        //    }
        //}
        if (TUTField.activeInHierarchy)
            TUTBack.SetActive(true);
        else
            TUTBack.SetActive(false);
    }

    public void OnClick()
    {
        //onClicked = true;
        TUTField.SetActive(true);
    }

}
