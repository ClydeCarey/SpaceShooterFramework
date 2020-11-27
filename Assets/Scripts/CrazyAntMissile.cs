using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyAntMissile : MonoBehaviour
{

    public Transform crazyAntTarget;

    public float crazyAntMissileSpeed = 8.0f;
    public float rotateSpeed = 200.0f;

    private Rigidbody2D crazyAntMissileRB;




    // Start is called before the first frame update
    void Start()
    {
        crazyAntTarget = GameObject.FindGameObjectWithTag("Player").transform;
        crazyAntMissileRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)crazyAntTarget.position - crazyAntMissileRB.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        crazyAntMissileRB.angularVelocity = -rotateAmount * rotateSpeed;



        crazyAntMissileRB.velocity = transform.up * crazyAntMissileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                Destroy(this.gameObject);
            }
        }

        if (other.tag == "Laser" || other.tag == "missile")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }

        //if (other.tag == "Missile")
        //{
        //    Destroy(other.gameObject);
        //}
    }
}