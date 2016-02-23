using UnityEngine;
using System.Collections;

public class MoveSouthCommand : Command
{

    public override void Execute(Player player)
    {
        player.ChangeDirection(Player.Direction.SOUTH);
    }
}
