using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Player;

    private void Awake() {
        instance = this;
        Player = GameObject.FindWithTag("Player");
    }



    public void MovePlayer(Vector3 position) {
        Player.transform.position = position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
