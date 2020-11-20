using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile3 : MonoBehaviour
{
    [SerializeField]
    private float _missileSpeed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        Missile3MoveUp();
    }

    void Missile3MoveUp()
    {
        
        transform.Translate(new Vector3(Random.Range(-1f, 1f) , Random.Range(0.5f, 2f)) * _missileSpeed * Time.deltaTime);

        if (transform.position.y > 18.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

}
