using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelection : MonoBehaviour
{
    public void SelectMap(string worldName){
        GameManager.SetWorld(worldName);
    }
}
