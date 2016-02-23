using UnityEngine;
using System.Collections;

public class MoveEastCommand : Command
{

    public override void Execute(Player player)
    {
        player.ChangeDirection(Player.Direction.EAST);
    }
}
