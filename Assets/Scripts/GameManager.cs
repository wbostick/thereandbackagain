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



    public void MovePlayer(Vector3 position, Vector3 rotation) {
        StartCoroutine("DisablePlayerController");
        Player.transform.position = position;
        Player.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    
    IEnumerator DisablePlayerController() {
        Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
