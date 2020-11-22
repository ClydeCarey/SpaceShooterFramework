using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{    
    [SerializeField]
    private float _speed = 3.0f;
    
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;


    //private float _collectSpeed = 2.0f;
    public Vector3 _playerPosition; //= GameObject.FindGameObjectWithTag("Player").transform.position; //see if public transform works better
    //public Transform playerTransform;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if(Input.GetKeyDown(KeyCode.C))
        //{
            
        ////    //var pos = transform.position;
        ////    //transform.position = Vector3.Lerp(pos, playerPosition.position, 1.0f);

        ////    //transform.Translate(Vector3.right * _collectSpeed * Time.deltaTime);
        //    _playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        //    Debug.Log(_playerPosition);
        //}

        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       

        //if (other.tag == "Laser")
        //{
        //    Debug.Log("hit by enemy laser");
        //}
        if (other.tag == "Player" || other.tag == "Laser")
        {            
            Player player = other.transform.GetComponent<Player>();
            
            AudioSource.PlayClipAtPoint(_clip, transform.position, 1.5f);
            if(player != null)
            {
                switch (powerupID)
                {
                    case 0:                        
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    case 3:
                        player.AmmoDrop();
                        break;
                    case 4:
                        player.HealthDrop();
                        break;
                    case 5:
                        player.SecondaryFire();                        
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
