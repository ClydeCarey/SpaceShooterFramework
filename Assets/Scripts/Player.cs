using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour

    
{
    public GameObject bgMusic1;
    public GameObject bgMusic2;
    
    private bool _secondaryFireIsActive = false;
    public bool isSecondaryFireActive = false;


    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private float _thrustersMultiplier = 1.0f;    
    
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _missile1Prefab;
    [SerializeField]
    private GameObject _missile2Prefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1.0f;
    public int ammoCount = 15;
    public TMP_Text textToScreen;
    string convertAmmoToString;

    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;   
    private bool _isSpeedBoostActive = false;
    private bool _isShieldsActive = false;
    private int _shieldLevel = 3;
    public SpriteRenderer shieldSpriteRenderer;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
     
    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _noAmmoClip;
    private AudioSource _audiosource;
    private AudioSource _emptyChamber;

    // Start is called before the first frame update
    void Start()
    {
        _secondaryFireIsActive = false;
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audiosource = GetComponent<AudioSource>();
        _emptyChamber = GetComponent<AudioSource>();
        //string convertAmmoToString;
        //convertAmmoToString = ammoCount.ToString();
        //textToScreen.text = convertAmmoToString;

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }        
    }

    // Update is called once per frame
    void Update()
    {
        SetThrusterSpeed();
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();                  
        }

        if (Input.GetKeyDown(KeyCode.M) && Time.time > _canFire)
        {
            FireMissile();
            FireMissile2();
        }
    }
    
    void SetThrusterSpeed()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            _thrustersMultiplier = 1.5f;
        }
        else 
        {
            _thrustersMultiplier = 1.0f;
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * _thrustersMultiplier * Time.deltaTime);
      
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0.0f), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }        

    }

    void FireLaser()
    {
        if (ammoCount > 0)
        {
            if (_audiosource == null)
            {
                Debug.LogError("AudioSource on the player is NULL");
            }
            else
            {
                _audiosource.clip = _laserSoundClip;                
            }
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }

            _audiosource.Play();
            ammoCount--;
        }
        else if (ammoCount < 1)
        {
            if (_audiosource == null)
            {
                Debug.LogError("AudioSource on the player is NULL");
            }
            else
            {                
                _emptyChamber.clip = _noAmmoClip;
            }
            _canFire = Time.time + _fireRate;            

            _emptyChamber.Play();
        }
        convertAmmoToString = ammoCount.ToString();
        textToScreen.text = convertAmmoToString;
    }

    void FireMissile()
    {        
        _canFire = Time.time + _fireRate;
        
        Instantiate(_missile1Prefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        //Instantiate(_missile2Prefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);

    }

    void FireMissile2()
    {
        _canFire = Time.time + _fireRate;

        //Instantiate(_missile1Prefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        Instantiate(_missile2Prefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);

    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            ShieldHealthMechanics();            
            return;
        }       
        _lives--;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _shieldLevel = 3;
        shieldSpriteRenderer.GetComponent<SpriteRenderer>().color = Color.white;
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AmmoDrop()
    {
        //Debug.Log("Ammodrop ran");
        ammoCount = 15;
        convertAmmoToString = ammoCount.ToString();
        textToScreen.text = convertAmmoToString;
    }

    public void HealthDrop()
    {
        if (_lives < 3)
        {
            _lives++;

            if (_lives == 2)
            {
                _rightEngine.SetActive(false);
            }
            else
            {
                _leftEngine.SetActive(false);
            }

            _uiManager.UpdateLives(_lives);
        }
        else
        {
            _lives = 3;
        }

    }

    public void ShieldHealthMechanics()
    {
        _shieldLevel--;
        switch (_shieldLevel)
        {            
            
            default:
                 return;
            case 2:
                shieldSpriteRenderer.GetComponent<SpriteRenderer>().color = Color.yellow;
                return;
            case 1:
                shieldSpriteRenderer.GetComponent<SpriteRenderer>().color = Color.red;
                return;
            case 0:
                _shieldLevel = 3;
                _isShieldsActive = false;
                _shieldVisualizer.SetActive(false);
                shieldSpriteRenderer.GetComponent<SpriteRenderer>().color = Color.white;
                return;
        }
        
    }

    public void SecondaryFire()
    {        
        Debug.Log("Secondary is called");

        StartCoroutine(SecondaryTimer());

        _secondaryFireIsActive = true;
        isSecondaryFireActive = true;

        
    }

    IEnumerator SecondaryTimer()
    {
        bgMusic1.SetActive(false);
        bgMusic2.SetActive(true);

        yield return new WaitForSeconds(7.01f);

        bgMusic1.SetActive(true);
        bgMusic2.SetActive(false);

    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    //method to add ten to score
    //communicate with UI to update the score
}
