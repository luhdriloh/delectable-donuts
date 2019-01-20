using System.Collections.Generic;
using UnityEngine;

public enum Direction
{ 
    UP = 0,
    RIGHT,
    DOWN,
    LEFT
}

public enum MoveableObjectType
{ 
    BOX,
    BALL,
}

public class MoveableObject : MonoBehaviour
{
    public MoveableObjectType _movementType;
    private Vector2 _newPosition;
    private Rigidbody2D _rigidbody;

    private readonly Dictionary<Direction, Vector3> _directionToVectorMap = new Dictionary<Direction, Vector3>
    {
        { Direction.UP, Vector3.up },
        { Direction.LEFT, Vector3.left },
        { Direction.DOWN, Vector3.down },
        { Direction.RIGHT, Vector3.right }
    };

    private void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _newPosition = transform.position;
	}

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_newPosition);
    }

    public bool MoveableInDirection(Direction direction)
    {
        Vector3 raycast = transform.position + (Vector3)_directionToVectorMap[direction];
        RaycastHit2D[] hits = new RaycastHit2D[1];
        // create a filter for the raycast
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Bounds"));
        int collidersHit = Physics2D.Raycast(raycast, raycast, filter, hits, .1f);
        if (collidersHit > 0)
        {
            return false;
        }

        // the colliders hit will be stored in hits
        filter.SetLayerMask(LayerMask.GetMask("Moveable"));
        collidersHit = Physics2D.Raycast(raycast, raycast, filter, hits, .1f);


        if (collidersHit > 0)
        {
            MoveableObject moveableObject = hits[0].collider.gameObject.GetComponent<MoveableObject>();
            if (moveableObject.ObjectsInDirection(direction, 1) == false)
            {
                moveableObject.MoveInDirection(direction);
                return true;
            }
        }

        return collidersHit == 0;
    }

    public bool ObjectsInDirection(Direction direction, int distance)
    {
        Vector3 raycast = transform.position + (Vector3)_directionToVectorMap[direction] * distance;
        RaycastHit2D[] hits = new RaycastHit2D[1];

        // create a filter for the raycast
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Moveable", "Bounds", "Donut"));

        // the colliders hit will be stored in hits
        int collidersHit = Physics2D.Raycast(raycast, raycast, filter, hits, .1f);

        return collidersHit > 0;
    }

    public void MoveInDirection(Direction direction)
    {
        int distanceToMove = 1;

        if (_movementType == MoveableObjectType.BALL)
        {
            while (ObjectsInDirection(direction, distanceToMove) == false && distanceToMove < 10)
            {
                distanceToMove++;
            }

            distanceToMove--;
        }


        _newPosition = (Vector3)_newPosition + _directionToVectorMap[direction] * distanceToMove;
    }
}
