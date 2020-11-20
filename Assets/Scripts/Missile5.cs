using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile5 : MonoBehaviour
{
    [SerializeField]
    private float _missileSpeed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        Missile5MoveUp();
    }

    void Missile5MoveUp()
    {
        transform.Translate(new Vector3(Random.Range(0f, 4f), Random.Range(0.8f, 2f)) * _missileSpeed * Time.deltaTime);

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
