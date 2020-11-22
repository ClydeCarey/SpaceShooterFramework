using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField]
    private Transform _playerPosition;
    [SerializeField]
    private Transform _ammoBoxPosition;
    [SerializeField]    
    private Transform _healthPickupPosition;
    [SerializeField]
    private Transform _secondaryFirePickupPosition;
    [SerializeField]
    private Transform _shieldPosition;
    [SerializeField]
    private Transform _speedPickupPosition;
    [SerializeField]
    private Transform _tripleshotPickupPosition;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            Debug.Log("I pressed C");
            //    _playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            //    var pos = transform.position;
            //    transform.position = Vector3.Lerp(pos, _playerPosition, 0.01f);
        }
    }
}
