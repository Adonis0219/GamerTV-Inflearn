using UnityEngine;

public class PowerUp : ItemController
{
    PlayerController playerController;

    protected override void ItemGain()
    {
        base.ItemGain();

        playerController = player.GetComponent<PlayerController>();

        if (playerController.Damage < 3)
        {
            playerController.Damage++;
        }

        if (playerController.Damage >= 3)
        {
            UIManager.instance.Score += base.score;
        }
    }
}
