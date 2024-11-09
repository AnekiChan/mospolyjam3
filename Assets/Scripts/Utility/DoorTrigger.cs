using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _door;

    void Start()
    {
        _door.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _door.SetActive(true);
            CommonEvents.Instance.OnBattleStart?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
