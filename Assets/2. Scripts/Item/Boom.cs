using UnityEngine;

public class Boom : ItemController
{
    PlayerController playerController;

    protected override void ItemGain()
    {
        playerController = player.GetComponent<PlayerController>();

        if (playerController.Boom < 4)
        {
            playerController.Boom++;
        }
    }
}
