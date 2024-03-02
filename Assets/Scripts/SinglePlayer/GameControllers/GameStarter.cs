using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    public void LoadScene(){
        SceneLoader.Load(SceneLoader.Scene.offline_test);
    }
}
