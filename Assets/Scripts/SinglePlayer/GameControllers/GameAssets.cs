using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets I;

    public GameObject playerAssets;
    private void Awake() {
        I = this;
    }
}
