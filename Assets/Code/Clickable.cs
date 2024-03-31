using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public int number;
    public void ToggleInfo()
    {
        Helper.ToggleInfoScene(number);
    }
}
