using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField]
    private int _sortingOrderBase = 5000;
    private Renderer _renderer;
    
    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.sortingOrder = (int)(_sortingOrderBase - (transform.position.y * 100f));
        
    }
}
