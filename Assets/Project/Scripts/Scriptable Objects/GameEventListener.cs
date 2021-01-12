using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int>
{
}

[System.Serializable]
public class EnemyEvent : UnityEvent<EnemyStats>
{
}

[System.Serializable]
public class AttackEvent : UnityEvent<int, Power, bool>
{
}

[System.Serializable]
public class PlayerAttackEvent : UnityEvent<Power>
{
}

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;

    [SerializeField] private UnityEvent _response;

    [SerializeField] private IntEvent _specificResponse;

    [SerializeField] private EnemyEvent _enemyEventResponse;

    [SerializeField] private AttackEvent _attackEventResponse;

    [SerializeField] private PlayerAttackEvent _playerAttackEventResponse;

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

    public void AddResponse(UnityAction<EnemyStats> call)
    {
        _enemyEventResponse.AddListener(call);
    }

    public void AddResponse(UnityAction<int, Power, bool> call)
    {
        _attackEventResponse.AddListener(call);
    }

    public void AddResponse(UnityAction<Power> call)
    {
        _playerAttackEventResponse.AddListener(call);
    }

    public void OnEventRaised()
    {
        _response.Invoke();
    }

    public void OnEventRaised(int id)
    {
        _specificResponse.Invoke(id);
    }

    public void OnEventRaised(EnemyStats stats)
    {
        _enemyEventResponse.Invoke(stats);
    }

    public void OnEventRaised(int id, Power power, bool isIce)
    {
        _attackEventResponse.Invoke(id, power, isIce);
    }

    public void OnEventRaised(Power power)
    {
        _playerAttackEventResponse.Invoke(power);
    }
}
