using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public string _nextLevel;

    private MoveableObject _moveableObject;
    private Rigidbody2D _rigidbody;
    private Vector2 _newPosition;

    private bool _flip;
    private bool _rightFacing;

	private void Start ()
    {
        _moveableObject = GetComponent<MoveableObject>();
        _newPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();

        _flip = false;
        _rightFacing = true;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _moveableObject.MoveableInDirection(Direction.LEFT) == true)
        {
            if (_rightFacing)
            {
                _rightFacing = false;
                _flip = true;
            }

            _newPosition += Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.W) && _moveableObject.MoveableInDirection(Direction.UP) == true)
        {
            _newPosition += Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _moveableObject.MoveableInDirection(Direction.RIGHT) == true)
        {
            if (_rightFacing == false)
            {
                _rightFacing = true;
                _flip = true;
            }
            _newPosition += Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _moveableObject.MoveableInDirection(Direction.DOWN) == true)
        {
            _newPosition += Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LevelSelect");
        }

        if (_flip)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0f);
            _flip = false;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Donut"))
        {
            collision.gameObject.SetActive(false);
            SceneManager.LoadScene(_nextLevel);
        }
    }
}
