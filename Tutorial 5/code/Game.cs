using Sandbox;
using System.Linq;

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

	[ServerCmd( "killeveryone" )]
	public static void KillEveryone()
	{
		foreach( Player player in All.OfType<Player>() )
		{
			player.TakeDamage( DamageInfo.Generic( 100f ) );
		}
	}	
	
	[ServerCmd( "sethealth" )]
	public static void SetHealth( int health )
	{
		var caller = ConsoleSystem.Caller.Pawn;
		if ( caller == null ) return;

		caller.Health = health;
	}	
	
	[ServerCmd( "damagetarget" )]
	public static void DamageTarget( int damage )
	{
		var caller = ConsoleSystem.Caller.Pawn;
		if ( caller == null ) return;

		var tr = Trace.Ray( caller.EyePos, caller.EyePos + caller.EyeRot.Forward * 5000 )
			.UseHitboxes()
			.Ignore( caller )
			.Run();

		if( tr.Entity is Player victim && victim.IsValid() )
		{
			victim.TakeDamage( DamageInfo.Generic( damage ) );
		}

		foreach ( Player player in All.OfType<Player>() )
		{
			Log.Info( $"{player.GetClientOwner().Name} - {player.Health}" );
		}
	}
}
