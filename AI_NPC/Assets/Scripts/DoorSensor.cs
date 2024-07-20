using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    public Animator leftDoorAnimator;
    public Animator rightDoorAnimator;
    public AudioSource pnuematicSound;

    private int playerCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Contact!");
            playerCount++;
            if(playerCount == 1)
            {
                OpenDoors();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--;
            if(playerCount == 0)
            {
                CloseDoors();
            }
        }
    }


    private void OpenDoors()
    {
        leftDoorAnimator.SetBool("isOpen", true);
        rightDoorAnimator.SetBool("isOpen", true);
        //pnuematicSound.Play();
    }

    private void CloseDoors()
    {
        leftDoorAnimator.SetBool("isOpen", false);
        rightDoorAnimator.SetBool("isOpen", false);
        //pnuematicSound.Play();
    }
}
