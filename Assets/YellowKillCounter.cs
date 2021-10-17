using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YellowKillCounter : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> pips = new List<SpriteRenderer>();
    int count = 0;
    public UnityEvent OnTargetKilled;
    // Start is called before the first frame update
    void Start()
    {
        LoopSystem.instance.OnTargetEnemyKilled.AddListener(TargetKilled);   
    }

    void TargetKilled() {
        
        OnTargetKilled.Invoke();
        pips[count].color =  new Color(255, 206, 0);
        count++;
    }

}
