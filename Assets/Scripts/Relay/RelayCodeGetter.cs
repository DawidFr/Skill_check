using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RelayCodeGetter : MonoBehaviour
{
    void Start(){
        GetComponent<TextMeshProUGUI>().text = RelayJoinAndHostController.relayCodeTMP;
    }
}
