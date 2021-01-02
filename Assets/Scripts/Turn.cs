using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {

	public void TurnWheel(string direction)
    {
        PlayerShip.direction = direction;
    }

    public void Stop()
    {
        if(PlayerShip.go)
        {
            PlayerShip.go = false;
        }
        else
        {
            PlayerShip.go = true;
        }
    }
}
