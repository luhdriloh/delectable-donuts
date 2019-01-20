using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public MoveableObjectType _pressurePlateType;
    public GameObject _itemToSpawn;
    public Transform _xMarksTheSpotPosition;

    private GameObject _spawnedItem;

    private void Start()
    {
        _spawnedItem = Instantiate(_itemToSpawn, _xMarksTheSpotPosition.position, Quaternion.identity);
        _spawnedItem.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoveableObject moveableObject = collision.gameObject.GetComponent<MoveableObject>();
        if (moveableObject._movementType == _pressurePlateType)
        {
            _spawnedItem.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MoveableObject moveableObject = collision.gameObject.GetComponent<MoveableObject>();
        if (moveableObject._movementType == _pressurePlateType)
        {
            _spawnedItem.SetActive(false);
        }
    }
}
