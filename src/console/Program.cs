using System;
using Pingvinen.LogitechDrivingForceGTDriver;

namespace console
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.Clear();

			string deviceFile = "/dev/input/js0";

			LogitechDrivingForceGTListener listener = new LogitechDrivingForceGTListener();

			listener.RaiseOnChangedEvent += (object sender, LogitechDrivingForceGTChangedEventArgs e) => {
				var dev = e.Device;
				Console.SetCursorPosition(0, 0);
				Console.WriteLine("SteeringWheelPosition: {0,-50}", dev.SteeringWheelPosition);
				Console.WriteLine("HorizontalAxisPosition: {0,-50}", dev.HorizontalAxisPosition);
				Console.WriteLine("VerticalAxisPosition: {0,-50}", dev.VerticalAxisPosition);
				Console.WriteLine("LeftPedal: {0,-50}", dev.LeftPedal);
				Console.WriteLine("RightPedal: {0,-50}", dev.RightPedal);
				Console.WriteLine("                                                            ");
				Console.WriteLine("GearShiftPosition: {0,-50}", dev.GearShiftPosition);
				Console.WriteLine("Horn: {0,-50}", dev.Horn);

				Console.WriteLine("L2: {0,-50}", dev.L2);
				Console.WriteLine("L3: {0,-50}", dev.L3);
				Console.WriteLine("R2: {0,-50}", dev.R2);
				Console.WriteLine("R3: {0,-50}", dev.R3);

				Console.WriteLine("Triangle: {0,-50}", dev.Triangle);
				Console.WriteLine("Circle: {0,-50}", dev.Circle);
				Console.WriteLine("Cross: {0,-50}", dev.Cross);
				Console.WriteLine("Square: {0,-50}", dev.Square);

				Console.WriteLine("LeftFlap: {0,-50}", dev.LeftFlap);
				Console.WriteLine("RightFlap: {0,-50}", dev.RightFlap);

				Console.WriteLine("Minus: {0,-50}", dev.Minus);
				Console.WriteLine("Plus: {0,-50}", dev.Plus);

				Console.WriteLine("Back: {0,-50}", dev.Back);
				Console.WriteLine("Select: {0,-50}", dev.Select);
				Console.WriteLine("Start: {0,-50}", dev.Start);
				Console.WriteLine("PsButton: {0,-50}", dev.PsButton);
				Console.WriteLine("MiniWheelLeft: {0,-50}", dev.MiniWheelLeft);
				Console.WriteLine("MiniWheelRight: {0,-50}", dev.MiniWheelRight);

				Console.WriteLine("                                                            ");
				Console.WriteLine("{0}: {1} => {2}                                              ", e.ChangedName, e.OldValue, e.NewValue);
			};

			Console.WriteLine("Press enter to start listening");
			Console.ReadLine();

			listener.Listen(deviceFile);
		}
	}
}
