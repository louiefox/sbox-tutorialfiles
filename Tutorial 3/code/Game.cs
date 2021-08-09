using Sandbox;

public partial class MyGame : Sandbox.Game
{
	public MyGame()
	{
	}

	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );

		// Create a pawn and assign it to the client.
		var player = new MyPlayer();
		client.Pawn = player;

		player.Respawn();
	}
}
