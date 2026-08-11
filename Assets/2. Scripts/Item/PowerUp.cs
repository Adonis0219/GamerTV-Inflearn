using UnityEngine;

public class PowerUp : ItemController
{
    PlayerController playerController;

    protected override void ItemGain()
    {
        playerController = player.GetComponent<PlayerController>();

        if (playerController.Damage < 3)
        {
            playerController.Damage++;
        }
    }
}
