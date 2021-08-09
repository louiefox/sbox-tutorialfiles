using Sandbox;

public partial class MyGame : Sandbox.Game
{
	public MyHUD MyHUD;

	public MyGame()
	{
		if ( IsClient ) MyHUD = new MyHUD();
	}

	[Event.Hotload]
	public void HotloadUpdate()
	{
		if ( !IsClient ) return;
		MyHUD?.Delete();
		MyHUD = new MyHUD();
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
