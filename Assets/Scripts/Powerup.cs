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
        
    public Vector3 _playerPosition; 
 
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.C))
        {
            _playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            var pos = transform.position;
            transform.position = Vector3.Lerp(pos, _playerPosition, 0.01f);
                        
        }

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
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
