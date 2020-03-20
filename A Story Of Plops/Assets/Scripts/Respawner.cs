using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform playerCharacter;

    public void Respawn()
    {
        playerCharacter.position = spawnPoint.position;
    }
}
