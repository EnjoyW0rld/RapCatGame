using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionHolder : MonoBehaviour
{
    [SerializeField] private UnityEvent OnActivate;
    [SerializeField] private KeyCode buttonToActivate = KeyCode.None;
    public bool ToActivate;
    private bool Activated;

    private void Update()
    {
        if(buttonToActivate != KeyCode.None)
        {
            if (Input.GetKeyDown(buttonToActivate))
            {
                ToActivate = true;
            }
        }
        if(ToActivate && !Activated)
        {
            Activate();
            Activated = true;
        }
    }
    public void Activate()
    {
        OnActivate?.Invoke();
    }
}
