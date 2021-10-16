using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    [System.Serializable]
    public class TriggerEvent : UnityEngine.Events.UnityEvent<GameObject> { }

    public TriggerEvent OnTriggerEnterEvent;
    public TriggerEvent OnTriggerStayEvent;
    public TriggerEvent OnTriggerExitEvent;

    public bool triggerOnlyForTag = false;
    public string triggerTag = "";

    private bool Filter(GameObject checkObj)
    {
        return !triggerOnlyForTag || checkObj.CompareTag(triggerTag);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (Filter(collision.gameObject))
        {
            OnTriggerEnterEvent.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (Filter(collision.gameObject))
        {
            OnTriggerStayEvent.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (Filter(collision.gameObject))
        {
            OnTriggerExitEvent.Invoke(collision.gameObject);
        }
    }
}
