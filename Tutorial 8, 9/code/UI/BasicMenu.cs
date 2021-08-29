using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;

public partial class BasicMenu : Panel
{
	private bool IsOpen = false;
	private TimeSince LastOpen;
	private List<(Panel, Panel)> Pages = new();
	private int ActivePage = -1;
	private Panel NavigationPanel;

	public BasicMenu()
	{
		StyleSheet.Load( "/ui/BasicMenu.scss" );

		Panel menuPanel = Add.Panel( "menu" );
		NavigationPanel = menuPanel.Add.Panel( "navbar" );

		Panel mainArea = menuPanel.Add.Panel( "mainarea" );

		// Pages
		Panel homePage = mainArea.Add.Panel( "page" );
		homePage.Add.Label( "Home page" );
		AddPage( homePage, "Home", new Color( 1f, 0.3f, 0.3f ) );

		Panel commandsPage = mainArea.Add.Panel( "page" );
		commandsPage.Add.Label( "Commands page" );
		AddPage( commandsPage, "Commands", new Color( 0.3f, 0.3f, 1f ) );
	}

	private void AddPage( Panel panel, string name, Color buttonColor )
	{
		int pageKey = Pages.Count;

		Panel button = NavigationPanel.Add.Label( name, "navbutton" );
		button.Style.BorderBottomColor = buttonColor;
		button.AddEventListener( "onclick", () => {
			SetActivePage( pageKey );
		} );

		Pages.Add( (panel, button) );

		if( Pages.Count <= 1 )
		{
			SetActivePage( pageKey );
		}
	}

	private void SetActivePage( int pageKey )
	{
		if( ActivePage >= 0 )
		{
			(Panel, Panel) activeInfo = Pages[ActivePage];
			activeInfo.Item1.SetClass( "active", false );
			activeInfo.Item2.SetClass( "active", false );
		}

		ActivePage = pageKey;

		(Panel, Panel) pageInfo = Pages[pageKey];
		pageInfo.Item1.SetClass( "active", true );
		pageInfo.Item2.SetClass( "active", true );
	}

	public override void Tick()
	{
		base.Tick();

		if( Input.Pressed( InputButton.Menu ) && LastOpen >= .1f )
		{
			IsOpen = !IsOpen;
			LastOpen = 0;
		}

		// IsOpen = true;

		SetClass( "open", IsOpen );
	}
}
