using UnityEngine;
using System.Collections;

public class MoveNorthCommand : Command
{

    public override void Execute(Player player)
    {
        player.ChangeDirection(Player.Direction.NORTH);
    }
}
