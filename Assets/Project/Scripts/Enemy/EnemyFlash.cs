using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlash : MonoBehaviour
{
    [SerializeField] private Material _flashMat;

    private Renderer _renderer;
    private Material _defaultMat;
    private GameEventListener _damageEvent;
    private int _id;

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _damageEvent = GetComponent<GameEventListener>();
        _id = transform.gameObject.GetInstanceID();
        _damageEvent.AddResponse(Flash);
        _defaultMat = _renderer.material;
    }

    public void Flash(int id)
    {
        if(_flashMat && id == _id)
        {
            _renderer.material = _flashMat;
            Invoke("ResetFlash", 0.125f);
        }
    }

    private void ResetFlash()
    {
        _renderer.material = _defaultMat;
    }
}
