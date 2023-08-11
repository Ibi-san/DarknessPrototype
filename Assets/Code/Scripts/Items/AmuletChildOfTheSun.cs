using UnityEngine;

public class AmuletChildOfTheSun : MonoBehaviour, IItem
{
    public GameObject ButtonAmuletUI;
    public GameObject FollowPoint;

    [SerializeField] private KeyCode _pickupKey = KeyCode.E;
    private bool _canPickup = false;
    private bool _isEquipped = false;
    private float _followSpeed = 20.0f;

    public void EquipItem()
    {
        _isEquipped = true;
    }

    public void UnequipItem()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canPickup = true;
            ButtonAmuletUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canPickup = false;
            ButtonAmuletUI.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(_pickupKey) && _canPickup)
        {
            EquipItem();
            
        }

        if (_isEquipped)
        {
            FollowPlayer();
        }

    }
    
    private void FollowPlayer()
    {
        if (FollowPoint != null)
        {
            Vector3 direction = FollowPoint.transform.position - transform.position;
            float distance = direction.magnitude;

            Vector3 moveDirection = direction.normalized;
            float moveDistance = _followSpeed * Time.deltaTime;

            if (moveDistance < distance)
            {
                transform.position += moveDirection * moveDistance;
            }
            else
            {
                transform.position = FollowPoint.transform.position;
            }
        }
    }

}
