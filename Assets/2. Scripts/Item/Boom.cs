using UnityEngine;

public class Boom : ItemController
{
    PlayerController playerController;

    protected override void ItemGain()
    {
        base.ItemGain();

        playerController = player.GetComponent<PlayerController>();

        if (playerController.Boom < 3)
        {
            playerController.Boom++;
        }

        if (playerController.Boom >= 3)
        {
            UIManager.instance.Score += base.score;
        }
    }
}
