using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum TANK
{
    NONE = 0,
    RED = 1,
    BLUE = 2
}
public class GameController : MonoBehaviour
{
    public static TANK choosen = 0;
    public void RedTank()
    {
        choosen = TANK.RED;
        SceneManager.LoadScene("Tanks");
    }

    public void BlueTank()
    {
        choosen = TANK.BLUE;
        SceneManager.LoadScene("Tanks");
    }

    private void Update()
    {
        Debug.Log(choosen);
    }
    public void PlayAgain()
    {
        choosen = TANK.NONE;
        SceneManager.LoadScene("MainScene");
    }
}
