using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int>
{
}

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;

    [SerializeField] private UnityEvent _response;

    [SerializeField] private IntEvent _specificResponse;

    private void OnEnable()
    {
        _gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        _gameEvent.UnegisterListener(this);
    }

    public void AddResponse(UnityAction<int> call)
    {
        _specificResponse.AddListener(call);
    }

    public void OnEventRaised()
    {
        _response.Invoke();
    }

    public void OnEventRaised(int id)
    {
        _specificResponse.Invoke(id);
    }
}
