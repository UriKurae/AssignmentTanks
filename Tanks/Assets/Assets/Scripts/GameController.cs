using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public GameObject canvas;
    enum TANK
    {
        RED = 0,
        BLUE = 1
    }

    // Start is called before the first frame update
    TANK choosen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RedTank()
    {
        choosen = TANK.RED;
        canvas.SetActive(false);
    }

    public void BlueTank()
    {
        choosen = TANK.BLUE;
        canvas.SetActive(false);
    }
    
}
