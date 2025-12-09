using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Room")]
public class Room : ScriptableObject
{
    public int roomNum;
    public string roomName;
    public int roomReward;

    public float roomWeight;
}
