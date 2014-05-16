using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace Project
{
	static class Program
	{
		static void Main (string[] args)
		{
			NSApplication.Init ();

			using (var p = new NSAutoreleasePool ()) {
				NSApplication.SharedApplication.Delegate = new AppDelegate ();
				NSApplication.Main (args);
			}
		}
	}

	class AppDelegate : NSApplicationDelegate
	{
		Application game;

		public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
		{
			game = new Application();
			game.Run();
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
	}
}