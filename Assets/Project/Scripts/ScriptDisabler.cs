using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDisabler : MonoBehaviour
{
    [SerializeField] private BoolVariable _isGamePaused;

    private MonoBehaviour[] scripts;
    private bool _isDisabled;

    private void Awake()
    {
        scripts = GetComponents<MonoBehaviour>();
    }

    private void Update()
    {
        if(_isGamePaused.RuntimeValue && !_isDisabled)
        {
            // Disable Scripts
            foreach (MonoBehaviour script in scripts)
            {
                if(script.GetType() != typeof(ScriptDisabler))
                {
                    script.enabled = false;
                }
            }

            _isDisabled = true;
        }

        else if (!_isGamePaused.RuntimeValue && _isDisabled)
        {
            foreach (MonoBehaviour script in scripts)
            {
                if (script.GetType() != typeof(ScriptDisabler))
                {
                    script.enabled = true;
                }
            }

            _isDisabled = false;
        }
    }
}
