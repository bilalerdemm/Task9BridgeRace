using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> blueList, greenList, purpleList;
    private void Awake() => instance = this;

}
