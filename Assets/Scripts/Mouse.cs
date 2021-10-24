using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    private void Update()
    {
        UnlockMouseOfScreen();
    }

    public void LockMouseInScreen()
    {
        print("locked");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockMouseOfScreen()
    {
        print("unlocked");
        //Cursor.lockState = Cur;
    }
}
