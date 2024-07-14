using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    [SerializeField] Transform _segmentPrefab;

    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments;

    
    

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }
    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        SnakeSegmentController();
        
    }

    private void Move() 
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
    }
    private void SnakeSegmentController() 
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x + _direction.x), Mathf.Round(this.transform.position.y + _direction.y), 0f);
    } 


    private void Grow() 
    {
       Transform segment = Instantiate(this._segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    //TODO change this to scene stuff when you add UI
    private void ResetState() 
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(typeof(FoodHandle), out var player))
        {
            Grow();
        }
        if (collision.gameObject.TryGetComponent(typeof(Obstacle), out var obstacle))
        {
            ResetState();
        }
    }
}
