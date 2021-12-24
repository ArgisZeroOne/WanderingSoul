using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnScript : MonoBehaviour
{
    public GameObject _Player;
    public Text _Progress;
    public GameObject[] _Spawn_Positions;
    public GameObject[] _Rooms_Insides_Prefabs;
    public int Lenght = 7;
    public int Weight = 7;
    public GameObject[,] _Rooms_Insides = new GameObject[7, 7];
    public int _Spawn_Pos_Int = 0;
    public bool _UpDoor = false;
    public bool _DownDoor = false;
    public bool _LeftDoor = false;
    public bool _RightDoor = false;
    public int _start_coordinate_x = 3;
    public int _start_coordinate_y = 3;
    public int _x_coordinate = 3;
    public int _y_coordinate = 3;
    public int _max_coordinate_x = 6;
    public int _min_coordinate_x = 0;
    public int _max_coordinate_y = 6;
    public int _min_coordinate_y = 0;
    public GameObject _Central_Room;
    public GameObject _Down_Room;
    public GameObject _Down_Left_Room;
    public GameObject _Down_Right_Room;
    public GameObject _Left_Room;
    public GameObject _Left_Right_Room;
    public GameObject _Right_Room;
    public GameObject _Up_Room;
    public GameObject _Up_Down_Room;
    public GameObject _Up_Left_Room;
    public GameObject _Up_Right_Room;
    public Text _HealthText;
    public int _GlobalHealth = 100;
    public GameObject[,] Rooms = new GameObject[7, 7];
    public GameObject[,] _Enemes = new GameObject[7, 7];
    public GameObject[] _Enemes_Prefab;
    private int[,] _Finded = new int[7, 7];
    public int _FindedNum = 1;
    public int _Roomses = 0;

    public GameObject Left()
    {
        int _rand = Random.Range(1, 5);
        _Roomses++;
        switch (_rand)
        {
            case 1:
                return _Central_Room;

            case 2:
                return _Down_Right_Room;

            case 3:
                return _Right_Room;

            case 4:
                return _Left_Right_Room;

            case 5:
                return _Up_Right_Room;

            default:
                return _Central_Room;

        }
    }
    public GameObject Right()
    {
        _Roomses++;
        int _rand = Random.Range(1, 5);
        switch (_rand)
        {
            case 1:
                return _Central_Room;

            case 2:
                return _Down_Left_Room;

            case 3:
                return _Left_Room;

            case 4:
                return _Left_Right_Room;

            case 5:
                return _Up_Left_Room;

            default:
                return _Central_Room;

        }
    }
    public GameObject Up()
    {
        _Roomses++;
        int _rand = Random.Range(1, 5);
        switch (_rand)
        {
            case 1:
                return _Central_Room;

            case 2:
                return _Down_Room;

            case 3:
                return _Up_Down_Room;

            case 4:
                return _Down_Left_Room;

            case 5:
                return _Down_Right_Room;

            default:
                return _Central_Room;

        }
    }
    public GameObject Down()
    {
        _Roomses++;
        int _rand = Random.Range(1, 5);
        switch (_rand)
        {
            case 1:
                return _Central_Room;

            case 2:
                return _Up_Room;

            case 3:
                return _Up_Down_Room;

            case 4:
                return _Up_Left_Room;

            case 5:
                return _Up_Right_Room;

            default:
                return _Central_Room;

        }
    }
    public void EnemyOff()
    {
        _Enemes[_y_coordinate, _x_coordinate] = null;
    }
    public void UpdateHealth(int HealthValue) => _HealthText.text = HealthValue + "%";
    public int getHealth()
    {
        return _GlobalHealth;
    }
    public void Spawn()
    {


        GameObject.FindWithTag("WorldCenter").GetComponent<FightButton>().TappedUpdate();

        if (_x_coordinate > _max_coordinate_x || _x_coordinate < _min_coordinate_x)
        {
            _x_coordinate = _start_coordinate_x;
            _y_coordinate = _start_coordinate_y;
        }
        if (_y_coordinate > _max_coordinate_y || _y_coordinate < _min_coordinate_y)
        {
            _x_coordinate = _start_coordinate_x;
            _y_coordinate = _start_coordinate_y;
        }
        if (Rooms[_x_coordinate, _y_coordinate] == null)
        {
            _x_coordinate = _start_coordinate_x;
            _y_coordinate = _start_coordinate_y;

        }
        if (_Finded[_y_coordinate, _x_coordinate] == 1)
        {
            _FindedNum++;
            _Finded[_y_coordinate, _x_coordinate] = 0;
        }
        try
        {
            PlayerController plc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _GlobalHealth = plc.getHealth();
        }
        catch
        {
            // Debug.Log("NoPlayer");
        }
        Debug.Log(_Enemes[_y_coordinate, _x_coordinate]);
        if (_y_coordinate != _start_coordinate_y || _x_coordinate != _start_coordinate_x)
        {
            try
            {
                int _rand = Random.Range(0, 4);
                _Spawn_Positions[_rand].GetComponent<PlayerSpawner>().SpawnEnemy(_Enemes[_y_coordinate, _x_coordinate]);
                Debug.Log(_Enemes[_y_coordinate, _x_coordinate]);
                _rand = Random.Range(0, 4);
                _Spawn_Positions[_rand].GetComponent<PlayerSpawner>().SpawnEnemy(_Enemes[_y_coordinate, _x_coordinate]);
                _rand = Random.Range(0, 4);
                _Spawn_Positions[_rand].GetComponent<PlayerSpawner>().SpawnEnemy(_Enemes[_y_coordinate, _x_coordinate]);

            }
            catch
            {

            }



        }
        Destroy(GameObject.FindWithTag("Location"));

        Destroy(GameObject.FindWithTag("Insides"));


        _Spawn_Positions[_Spawn_Pos_Int].GetComponent<PlayerSpawner>().Spawn();
        Instantiate(Rooms[_y_coordinate, _x_coordinate]);
        try { Instantiate(_Rooms_Insides[_y_coordinate, _x_coordinate]); }
        catch
        { //Debug.Log("NoInsides");
        }

        _Progress.text = "Rooms Detected: " + _FindedNum + "/" + _Roomses;
        //Debug.Log(_x_coordinate);
        //Debug.Log(_y_coordinate);

    }

    void Start()
    {
        _Enemes = new GameObject[Lenght, Weight];
        _Rooms_Insides = new GameObject[Lenght, Weight];
        Rooms = new GameObject[Lenght, Weight];
        _Finded = new int[Lenght, Weight];
        _x_coordinate = (int)(Weight / 2);
        _y_coordinate = (int)(Lenght / 2);
        _max_coordinate_x = Weight - 1;
        _max_coordinate_y = Lenght - 1;
        _start_coordinate_x = (int)(Weight / 2);
        _start_coordinate_y = (int)(Lenght / 2);
        for (int i = 0; i < Lenght; i++)
        {
            for (int j = 0; j < Weight; j++)
            {
                _Finded[i, j] = 0;
            }
        }
        Rooms[_x_coordinate, _y_coordinate] = _Central_Room;
        for (int n = 0; n < 50; n++)
        {
            for (int i = 1; i < Lenght - 1; i++)
            {
                for (int j = 0; j < Weight; j++)
                {
                    if (Rooms[i + 1, j] != null && Rooms[i, j] == null)
                    {
                        if (Rooms[i + 1, j] == _Central_Room || Rooms[i + 1, j] == _Down_Left_Room || Rooms[i + 1, j] == _Down_Right_Room || Rooms[i + 1, j] == _Up_Down_Room)
                        {
                            Rooms[i, j] = Down();
                            _Finded[i, j] = 1;
                        }
                    }
                    if (Rooms[i - 1, j] != null && Rooms[i, j] == null)
                    {
                        if (Rooms[i - 1, j] == _Central_Room || Rooms[i - 1, j] == _Up_Left_Room || Rooms[i - 1, j] == _Up_Right_Room || Rooms[i - 1, j] == _Up_Down_Room)
                        {
                            Rooms[i, j] = Up();
                            _Finded[i, j] = 1;
                        }
                    }
                }
            }
            for (int i = 0; i < Lenght; i++)
            {
                for (int j = 1; j < Weight - 1; j++)
                {
                    if (Rooms[i, j - 1] != null && Rooms[i, j] == null)
                    {
                        if (Rooms[i, j - 1] == _Central_Room || Rooms[i, j - 1] == _Down_Left_Room || Rooms[i, j - 1] == _Left_Room || Rooms[i, j - 1] == _Left_Right_Room || Rooms[i, j - 1] == _Up_Left_Room)
                        {
                            Rooms[i, j] = Left();
                            _Finded[i, j] = 1;
                        }
                    }
                    if (Rooms[i, j + 1] != null && Rooms[i, j] == null)
                    {
                        if (Rooms[i, j + 1] == _Central_Room || Rooms[i, j + 1] == _Down_Right_Room || Rooms[i, j + 1] == _Left_Right_Room || Rooms[i, j + 1] == _Right_Room || Rooms[i, j + 1] == _Up_Right_Room)
                        {
                            Rooms[i, j] = Right();
                            _Finded[i, j] = 1;
                        }
                    }
                }
            }

        }
        for (int i = 0; i < Lenght; i++)
        {
            for (int j = 0; j < Weight; j++)
            {
                if (Rooms[i, j] != null)
                {

                    int _rand = Random.Range(0, _Rooms_Insides_Prefabs.Length);
                    //Debug.Log(_rand);
                    _Rooms_Insides[i, j] = _Rooms_Insides_Prefabs[_rand];
                }
            }
        }
        for (int i = 0; i < Lenght; i++)
        {
            for (int j = 0; j < Weight; j++)
            {
                if (Rooms[i, j] != null)
                {
                    int _enem = Random.Range(0, 2);
                    Debug.Log(_enem);
                    if (_enem == 0)
                    {
                        int _rand = Random.Range(0, _Enemes_Prefab.Length);
                        //Debug.Log(_rand);
                        _Enemes[i, j] = _Enemes_Prefab[_rand];
                    }

                }
            }
        }
        _Rooms_Insides[_y_coordinate, _x_coordinate] = null;
        _Enemes[_y_coordinate, _x_coordinate] = null;
        /*for (int n = 0; n < 7; n++)
        {

            for (int m = 0; m < 7; m++)
            {
                if (Rooms[n, m] != null)
                {
                    Debug.Log(Rooms[n, m]);

                }
            }

        }*/
        Spawn();
    }
    // Update is called once per frame
    public void UpDoorActivate()
    {
        _UpDoor = true;
    }
    public void DownDoorActivate()
    {
        _DownDoor = true;
    }
    public void LeftDoorActivate()
    {
        _LeftDoor = true;
    }
    public void RightDoorActivate()
    {
        _RightDoor = true;
    }
    void FixedUpdate()
    {
        
     



        if (_UpDoor)
        {
            Debug.Log("Up");
            _UpDoor = false;
            _y_coordinate -= 1;
            _Spawn_Pos_Int = 1;
            Spawn();


        }
        if (_DownDoor)
        {
            Debug.Log("Down");
            _DownDoor = false;
            _y_coordinate += 1;
            _Spawn_Pos_Int = 2;
            Spawn();

        }
        if (_LeftDoor)
        {
            _LeftDoor = false;
            _x_coordinate -= 1;
            _Spawn_Pos_Int = 3;
            Spawn();

        }
        if (_RightDoor)
        {
            _RightDoor = false;
            _x_coordinate += 1;
            _Spawn_Pos_Int = 4;
            Spawn();

        }
    }
}


