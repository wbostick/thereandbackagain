using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject Player;
    public GameObject PCPlayerPrefab;
    public GameObject VRPlayerPrefab;
    public Transform playerStartPos;

    private void Awake() {
        instance = this;
        SpawnPlayer();
        
    }


    void SpawnPlayer() {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Player = GameObject.Instantiate(VRPlayerPrefab, playerStartPos.position, playerStartPos.rotation);
        #else
            Player = GameObject.Instantiate(PCPlayerPrefab, playerStartPos.position, playerStartPos.rotation);
        #endif

    }

    public void MovePlayer(Vector3 position, Vector3 rotation, float disableTime) {
        StartCoroutine("DisablePlayerController", disableTime);
        Player.transform.position = position;
        Player.transform.rotation = Quaternion.Euler(0f, rotation.y - 90, 0f);
    }

    
    IEnumerator DisablePlayerController(float time) {
        DisablePlayerMovement();
        yield return new WaitForSeconds(time);
        EnablePlayerMovement();
    }

    public void DisablePlayerMovement() {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Player.GetComponent<OVRPlayerController>().enabled = false;
        #else
            Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        #endif
        
        Rigidbody playerRigid = Player.GetComponent<Rigidbody>();
        if (playerRigid)
        {
            playerRigid.isKinematic = false;
        }
        Player.GetComponent<CharacterController>().enabled = false;
    }

    public void EnablePlayerMovement() {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Player.GetComponent<OVRPlayerController>().enabled = true;
        #else
            Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        #endif
        
        Rigidbody playerRigid = Player.GetComponent<Rigidbody>();
        if (playerRigid)
        {
            playerRigid.isKinematic = true;
        }
        Player.GetComponent<CharacterController>().enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
