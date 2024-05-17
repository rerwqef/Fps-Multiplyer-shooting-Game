using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public TextMeshProUGUI msgText;
    public void Start()
    {
        GetComponent<RectTransform>().SetAsFirstSibling();
    }

}
