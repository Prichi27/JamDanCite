using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightUpdater : MonoBehaviour
{
    private SpriteRenderer _sprite;
    [SerializeField] Vector2Variable _playerPosuition;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        TileHeightManager.Instance.ReportPosition(new Vector3(_playerPosuition.RuntimeValue.x, _playerPosuition.RuntimeValue.y), _sprite.sprite.bounds);
    }
}
