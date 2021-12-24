using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject _Player;
    public Animation anim;
    public bool _UpDoorGoing = false;
    public bool _DownDoorGoing = false;
    public bool _LeftDoorGoing = false;
    public bool _RightDoorGoing = false;
    public bool _TaskBtnTapped = false;
    public float moveVertical = 0;
    public float moveHorizontal = 0;
    public float _Health = 100;
    public bool _OnLava = false;
    public bool _MinRad = false;
    public bool _MidRad = false;
    public bool _MaxRad = false;
    public bool _GetDamage = false;
    public bool _Damaging = false;
    public bool Sword_Status = false;
    public int _Find=0;
    public int _Rooms=0;
    public bool _Attack;
    public GameObject _WorldCenter;
    public bool _stop = false;
    
    public int _globalHealth;
    Rigidbody _rb;
    public float _speed = 0.1f;
    public float _fixed_speed = 0.1f;
    // Start is called before the first frame update
    public void Hello()
    {

    }

    public int getHealth()
    {
        return ((int)_Health);
    }
    void Start()
    {
        
        _WorldCenter = GameObject.FindWithTag("WorldCenter");
        _Health = _WorldCenter.GetComponent<SpawnScript>().getHealth();
        anim = gameObject.GetComponent<Animation>();
        _rb = GetComponent<Rigidbody>();
        
    }
    void Going(float moveHorizontal, float moveVertical)
    {
        Vector3 movement = new Vector3(-moveVertical, 0.0f, moveHorizontal);

        _rb.AddForce(movement * _speed);
    }
    IEnumerator Damage()
    {
        _Damaging = true;
        _Health -= 10f;
        _GetDamage = false;
        yield return new WaitForSeconds(2);

        _Damaging = false;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        SpawnScript Center = _WorldCenter.GetComponent<SpawnScript>();
        FightButton Tap = _WorldCenter.GetComponent<FightButton>();
        Center.UpdateHealth(((int)_Health));
        _TaskBtnTapped = Tap.Tapped;
        _Attack = Tap.Sword_Status;
        
        Sword_Status = Tap.Sword_Status;
        _Find = Center._FindedNum;
        _Rooms = Center._Roomses;
        if (Sword_Status)
        {
            Fight();
        }
        if (_Health <= 0)
        {
            Tap.Dead();
            
        }
        if (_Find == _Rooms && !GameObject.FindWithTag("Enemy"))
        {

            Tap.Win();
        }
        if (_GetDamage && !_Damaging && !Sword_Status)
        {
            StartCoroutine(Damage());
        }
        if (_MinRad && !Sword_Status)
        {
            if(_MinRad && _MidRad && !Sword_Status)
            {
                
                if (_MidRad && _MidRad && _MaxRad && !Sword_Status)
                {
                    Debug.Log("Min,Mid and Max");
                    _Health -= 0.08f;
                } else
                {
                    Debug.Log("Min and Mid");
                    _Health -= 0.04f;
                }
            } else
            {
                Debug.Log("Min");
                _Health -= 0.02f;
            }
        }
       // Debug.Log(_TaskBtnTapped);
        /* float translation_x = Input.GetAxis("Horizontal") * _speed;
         float translation_y = Input.GetAxis("Vertical") * _speed;
         transform.Translate(-translation_y, 0, translation_x);
         */
        float rotateX = SimpleInput.GetAxis("Horizontal");
        float rotateY = SimpleInput.GetAxis("Vertical");

        if (_OnLava && !Sword_Status)
        {
            _Health -= 0.05f;
        }
       // Debug.Log(transform.eulerAngles);
        
       if(rotateX > 0 && rotateY == 0 && (transform.eulerAngles.y > 100 || transform.eulerAngles.y < 80))
        {
            if(transform.eulerAngles.y < 270 && transform.eulerAngles.y > 90)
            {
                transform.eulerAngles += new Vector3(0, -20, 0);
            } else
            {
                transform.eulerAngles += new Vector3(0, 20, 0);
            }
        } else if (rotateX > 0 && rotateY == 0)
        {
            Going(SimpleInput.GetAxis("Horizontal"), 0);
        }
        if (rotateX < 0 && rotateY == 0 && (transform.eulerAngles.y > 280 || transform.eulerAngles.y < 260))
        {
            if (transform.eulerAngles.y < 90 || transform.eulerAngles.y > 270 || transform.eulerAngles.y == 0)
            {
                transform.eulerAngles += new Vector3(0, -20, 0);
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 20, 0);
            }
        } else if (rotateX < 0 && rotateY == 0)
        {
            Going(SimpleInput.GetAxis("Horizontal"), 0);
        }
        if (rotateX == 0 && rotateY > 0 && (transform.eulerAngles.y > 5 && transform.eulerAngles.y < 355))
        {
            if (transform.eulerAngles.y < 180)
            {
                transform.eulerAngles += new Vector3(0, -20, 0);
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 20, 0);
            }
        } else if (rotateX == 0 && rotateY > 0)
        {
            Going(0, SimpleInput.GetAxis("Vertical"));
        }
        if (rotateX == 0 && rotateY < 0 && (transform.eulerAngles.y > 185 || transform.eulerAngles.y < 175))
        {
            if (transform.eulerAngles.y > 180)
            {
                transform.eulerAngles += new Vector3(0, -20, 0);
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 20, 0);
            }
        } else if (rotateX == 0 && rotateY < 0)
        {
            Going(0, SimpleInput.GetAxis("Vertical"));
        }
        if (rotateX > 0 && rotateY > 0 && (transform.eulerAngles.y > 55 || transform.eulerAngles.y < 35))
        {
            if (transform.eulerAngles.y < 215 && transform.eulerAngles.y > 45)
            {
                transform.eulerAngles += new Vector3(0, -20, 0);
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 20, 0);
            }
        }
        else if (rotateX > 0 && rotateY > 0)
        {
            Going(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        }
        if (rotateX > 0 && rotateY < 0 && (transform.eulerAngles.y > 145 || transform.eulerAngles.y < 125))
        {
            if (transform.eulerAngles.y < 315 && transform.eulerAngles.y > 135)
            {
                transform.eulerAngles += new Vector3(0, -20, 0);
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 20, 0);
            }
        }
        else if (rotateX > 0 && rotateY < 0)
        {
            Going(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        }
        if (rotateX < 0 && rotateY < 0 && (transform.eulerAngles.y > 225 || transform.eulerAngles.y < 205))
        {
            if (transform.eulerAngles.y > 215 || transform.eulerAngles.y < 45)
            {
                transform.eulerAngles += new Vector3(0, -20, 0);
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 20, 0);
            }
        }
        else if (rotateX < 0 && rotateY < 0)
        {
            Going(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        }
        if (rotateX < 0 && rotateY > 0 && (transform.eulerAngles.y > 325 || transform.eulerAngles.y < 305))
        {
            if (transform.eulerAngles.y > 315 || transform.eulerAngles.y < 135)
            {
                transform.eulerAngles += new Vector3(0, -20, 0);
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 20, 0);
            }
        }
        else if (rotateX < 0 && rotateY > 0)
        {
            Going(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        }





    }
    IEnumerator Attack()
    {
        Sword_Status = true;
        Debug.Log("Start");
        yield return new WaitForSeconds(5);
        Sword_Status = false;

    }
   
    public void Fight()
    {
        Debug.Log("FIGHT INI");
        anim.Play();
        //StartCoroutine(Attack());
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RadiationAreaMin" && !Sword_Status)
        {
            _MinRad = true;
        }
        if (other.tag == "RadiationAreaMid" && !Sword_Status)
        {
            _MidRad = true;
        }
        if (other.tag == "RadiationAreaMax" && !Sword_Status)
        {
            _MaxRad = true;
        }
        if (other.tag == "Enemy")
        {
            _GetDamage = true;
        }
        if (other.tag == "UpDoor" && _TaskBtnTapped)
        {
            _WorldCenter.GetComponent<SpawnScript>().UpDoorActivate();
            transform.position += Vector3.up * 1000;
            ////(gameObject,1);
            
        }
        if (other.tag == "Lava" && !Sword_Status)
        {
            _Health -= 2;
            _OnLava = true;
        }
        else if (other.tag == "DownDoor" && _TaskBtnTapped)
        {
            _WorldCenter.GetComponent<SpawnScript>().DownDoorActivate();
            transform.position += Vector3.up * 1000;
            ////(gameObject,1);
        }
        else if (other.tag == "LeftDoor" && _TaskBtnTapped)
        {
            _WorldCenter.GetComponent<SpawnScript>().LeftDoorActivate();
            transform.position += Vector3.up * 1000;
            ////(gameObject,1);
        }
        else if (other.tag == "RightDoor" && _TaskBtnTapped)
        {
            _WorldCenter.GetComponent<SpawnScript>().RightDoorActivate();
            transform.position += Vector3.up * 1000;
            ////(gameObject,1);
        } 
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _GetDamage = true;
        }
        if (other.tag == "RadiationAreaMin" && !Sword_Status)
        {
            _MinRad = true;
        }
        if (other.tag == "RadiationAreaMid" && !Sword_Status)
        {
            _MidRad = true;
        }
        if (other.tag == "RadiationAreaMax" && !Sword_Status)
        {
            _MaxRad = true;
        }
        if (other.tag == "UpDoor" && _TaskBtnTapped)
        {
            _WorldCenter.GetComponent<SpawnScript>().UpDoorActivate();
            Debug.Log("Up_TR");
            transform.position += Vector3.up * 1000;
            //

        }
        else if (other.tag == "DownDoor" && _TaskBtnTapped)
        {
            _WorldCenter.GetComponent<SpawnScript>().DownDoorActivate();
            Debug.Log("Down_TR");
            transform.position += Vector3.up * 1000;
            //
        }
        else if (other.tag == "LeftDoor" && _TaskBtnTapped)
        {
            _WorldCenter.GetComponent<SpawnScript>().LeftDoorActivate();
            transform.position += Vector3.up * 1000;
            //
        }
        else if (other.tag == "RightDoor" && _TaskBtnTapped)
        {
            _WorldCenter.GetComponent<SpawnScript>().RightDoorActivate();
            transform.position += Vector3.up * 1000;
            //
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _GetDamage = false;
        }
        if (other.tag == "RadiationAreaMin")
        {
            _MinRad = false;
        }
        if (other.tag == "RadiationAreaMid")
        {
            _MidRad = false;
        }
        if (other.tag == "RadiationAreaMax")
        {
            _MaxRad = false;
        }
        if (other.tag == "Lava")
        {
            
            _OnLava = false;
        }
        if (other.tag == "UpDoor")
        {
            _UpDoorGoing = false;
        }
        if (other.tag == "DownDoor")
        {
            _DownDoorGoing = false;
        }
        if (other.tag == "LeftDoor")
        {
            _LeftDoorGoing = false;
        }
        if (other.tag == "RightDoor")
        {
            _RightDoorGoing = false;
            
        }
    }
}
