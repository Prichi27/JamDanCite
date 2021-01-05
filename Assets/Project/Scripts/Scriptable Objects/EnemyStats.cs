using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy/Stats", order = 30)]
public class EnemyStats : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] public string enemyName;
    [SerializeField] public Texture2D texture;
    [SerializeField] public float health;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float waypointDistance;
    [SerializeField] public float score;
    [SerializeField] public bool isLongRanged;

    public string Name { get; set; }
    public Texture2D Texture { get; set; }
    public float Health { get; set; }
    public float Damage { get; set; }
    public float Speed { get; set; }
    public float WaypointDistance { get; set; }
    public float Score { get; set; }
    public bool IsLongRanged { get; set; }

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        Name = enemyName;
        Health = health;
        Damage = damage;
        Speed = speed;
        Score = score;
        Texture = texture;
        WaypointDistance = waypointDistance;
        IsLongRanged = isLongRanged;
    }
}
