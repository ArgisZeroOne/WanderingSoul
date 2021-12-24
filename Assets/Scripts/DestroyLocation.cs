using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLocation : MonoBehaviour
{
    public bool _UpDoor = false;
    public bool _DownDoor = false;
    public bool _LeftDoor = false;
    public bool _RightDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            PlayerController plc = GameObject.Find("Player").GetComponent<PlayerController>();
            _UpDoor = plc._UpDoorGoing;
            _DownDoor = plc._DownDoorGoing;
            _LeftDoor = plc._LeftDoorGoing;
            _RightDoor = plc._RightDoorGoing;
        } catch
        {

        }
        
        if(_DownDoor || _LeftDoor || _RightDoor || _UpDoor)
        {
            Destroy(gameObject);
        }
    }
}
