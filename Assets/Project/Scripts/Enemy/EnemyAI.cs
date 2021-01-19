using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Vector2Variable _target;

    [SerializeField]
    private EnemyStats _enemy;

    [SerializeField] 
    private GameEventListener _freezeEvent;
    [SerializeField] 
    private GameEvent OnEnemyUnfreeze;

    [SerializeField]
    private BoolVariable _isDead;

    private Path _path;
    private int _currentWaypoint;
    private bool _reachedEndOfPath = false;
    private Seeker _seeker;
    private Rigidbody2D _rigidbody;
    private bool _canAttack;

    private float _currentSpeed;
    private int _id;

    private void OnEnable() 
    {
        _currentSpeed = _enemy.Speed;
    }

    private void Start()
    {
        _id = transform.gameObject.GetInstanceID();
        _freezeEvent.AddResponse(FreezeEnemy);
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
        if (_isDead.RuntimeValue) return;

        if (!CanEnemyAttack()) FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (_path == null) return;

        _reachedEndOfPath = _currentWaypoint >= _path.vectorPath.Count;

        if (_reachedEndOfPath) return;

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody.position).normalized;
        Vector2 force = direction * _currentSpeed * Time.deltaTime;

        _rigidbody.AddForce(force);

        float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWaypoint]);

        if (distance < _enemy.WaypointDistance)
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

    public bool CanEnemyAttack()
    {
        return Vector2.Distance(_target.RuntimeValue, _rigidbody.position) <= _enemy.DistanceFromPlayer;
    }

    public void FreezeEnemy(int id)
    {
        if (_id == id)
        {
            _currentSpeed = 1;
            _rigidbody.bodyType = RigidbodyType2D.Static;
            transform.Find("FreezeLayer").GetComponent<SpriteRenderer>().enabled = true;
            Invoke("UnfreezeEnemy", 6.0f);
        }
    }

    void UnfreezeEnemy()
    {
        _currentSpeed = _enemy.Speed;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        transform.Find("FreezeLayer").GetComponent<SpriteRenderer>().enabled = false;
        OnEnemyUnfreeze.Raise(transform.gameObject.GetInstanceID());
    }

}
