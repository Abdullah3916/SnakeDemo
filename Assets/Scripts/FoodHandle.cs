using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHandle : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _gridArea;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition() 
    {
        Bounds bounds = this._gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(typeof(SnakeController), out var player ))
        {
            RandomizePosition();
        }
        
    }
}
