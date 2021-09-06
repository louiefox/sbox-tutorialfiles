using Sandbox;
using Sandbox.UI;

public partial class MyHUD : RootPanel
{
	public MyHUD()
	{
		AddChild<Vitals>();
		AddChild<BasicMenu>();
	}
}
