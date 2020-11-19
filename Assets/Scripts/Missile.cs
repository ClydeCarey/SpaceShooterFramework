using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField]
    private float _missileSpeed = 8.0f;
    [SerializeField]
    private GameObject _smoke; 
   
    // Update is called once per frame
    void Update()
    {
        MissileMoveUp();
    }

    void MissileMoveUp()
    {
        transform.Translate(Vector3.up * _missileSpeed * Time.deltaTime);

        if (transform.position.y > 8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
   
}
