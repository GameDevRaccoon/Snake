using UnityEngine;
using System.Collections;

public class MoveWestCommand : Command
{

    public override void Execute(Player player)
    {
        player.ChangeDirection(Player.Direction.WEST);
    }
}
