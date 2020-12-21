using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Vector2Variable _target;

    [SerializeField]
    private FloatVariable _enemySpeed;

    [SerializeField]
    private FloatVariable _nextWaypointDistace;

    private Path _path;
    private int _currentWaypoint;
    private bool _reachedEndOfPath = false;
    private Seeker _seeker;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void UpdatePath()
    {
        if(_seeker.IsDone()) _seeker.StartPath(_rigidbody.position, _target.RuntimeValue, OnPathComplete);
    }

    private void Update()
    {
        if (_path == null) return;

        _reachedEndOfPath = _currentWaypoint >= _path.vectorPath.Count;

        if (_reachedEndOfPath) return;

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody.position).normalized;
        Vector2 force = direction * _enemySpeed.RuntimeValue * Time.deltaTime;

        _rigidbody.AddForce(force);

        float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWaypoint]);

        if (distance < _nextWaypointDistace.RuntimeValue)
        {
            _currentWaypoint++;
        }
        
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

}
