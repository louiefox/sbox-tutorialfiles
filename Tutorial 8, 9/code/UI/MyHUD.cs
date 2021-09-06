using Sandbox;
using Sandbox.UI;

public partial class MyHUD : RootPanel
{
	public MyHUD()
	{
		AddChild<Vitals>();

		AddChild<Vitals>();
		AddChild<BasicMenu>();
	}
}
