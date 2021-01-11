using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy/Stats", order = 30)]
public class EnemyStats : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] public string enemyName;
    [SerializeField] public float health;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float waypointDistance;
    [SerializeField] public int score;
    [SerializeField] public bool isLongRanged;
    [SerializeField] public bool canAttackAtPlayerPosition;
    [SerializeField] public float attackSpeed;

    public string Name { get; set; }
    public float Health { get; set; }
    public float Damage { get; set; }
    public float Speed { get; set; }
    public float WaypointDistance { get; set; }
    public int Score { get; set; }
    public bool IsLongRanged { get; set; }
    public float AttackSpeed { get; set; }


    public void OnAfterDeserialize()
    {
        
        Name = enemyName;
        Health = health;
        Damage = damage;
        Speed = speed;
        Score = score;
        WaypointDistance = waypointDistance;
        IsLongRanged = isLongRanged;
        AttackSpeed = attackSpeed;
    }

    public void OnBeforeSerialize()
    {
    }
}
