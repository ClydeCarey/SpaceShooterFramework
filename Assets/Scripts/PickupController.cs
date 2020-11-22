using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _ammoBoxTransform;
    [SerializeField]    
    private Transform _healthPickupTransform;
    [SerializeField]
    private Transform _secondaryFirePickupTransform;
    [SerializeField]
    private Transform _shieldPickupTransform;
    [SerializeField]
    private Transform _speedPickupTransform;
    [SerializeField]
    private Transform _tripleshotPickupTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        

        if (Input.GetKey(KeyCode.C))
        {
            //Debug.Log("I pressed C");
                        
            _ammoBoxTransform.position = Vector3.Lerp(_ammoBoxTransform.position, _playerTransform.position, 0.25f);

            //Debug.Log($"Player position: {_playerTransform.position}");
            //Debug.Log($"Ammo box position: {_ammoBoxTransform.position}");


        }
    }
}
