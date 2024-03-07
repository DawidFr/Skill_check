using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 spawnPointPos;
    private void Start(){
        Instantiate(GameAssets.I.playerAssets, spawnPointPos, Quaternion.identity);
    }
}
