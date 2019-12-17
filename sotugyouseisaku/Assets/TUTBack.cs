using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUTBack : MonoBehaviour
{
    [SerializeField]
    GameObject TUTField, TUTField2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        if (TUTField2.activeInHierarchy && TUTField.activeInHierarchy)
            TUTField2.SetActive(false);
        else if (!TUTField2.activeInHierarchy && TUTField.activeInHierarchy)
            TUTField.SetActive(false);
    }
}
