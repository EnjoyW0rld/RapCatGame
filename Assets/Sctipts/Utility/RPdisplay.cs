using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RPdisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        if (textBox != null)
        {
            textBox.text = GameInformation.Instance.reputationPoints.ToString();
        }
    }

}
