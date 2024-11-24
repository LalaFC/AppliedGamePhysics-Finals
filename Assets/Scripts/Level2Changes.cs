using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Level2Changes : MonoBehaviour
{
    private PlayerMovement player;
    //Different Gravity Scale... Lol
    private int level = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(player != other.GetComponent<PlayerMovement>()) player= other.GetComponent<PlayerMovement>();
            if (player.hasLanded)
            {
                GetComponent<MeshRenderer>().enabled = true;
                player.ChangeJumpForce(300);
                level = 2;
            }
        }
    }
    public int GetLevel()
    {
        return level;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().enabled = false;
            player.ChangeJumpForce(400);
        }
    }
}
