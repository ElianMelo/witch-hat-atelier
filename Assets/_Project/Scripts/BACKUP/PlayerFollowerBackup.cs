using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowerBackup : MonoBehaviour
{
    public Transform player;
    private void FixedUpdate()
    {
        transform.position = player.position;
    }
}
