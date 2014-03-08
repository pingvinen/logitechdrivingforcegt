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
				Console.WriteLine("SteeringWheelPosition: {0,-10}", dev.SteeringWheelPosition);
				Console.WriteLine("HorizontalAxisPosition: {0,-10}", dev.HorizontalAxisPosition);
				Console.WriteLine("VerticalAxisPosition: {0,-10}", dev.VerticalAxisPosition);
				Console.WriteLine("LeftPedal: {0,-10}", dev.LeftPedal);
				Console.WriteLine("RightPedal: {0,-10}", dev.RightPedal);
				Console.WriteLine();
				Console.WriteLine("GearShiftPosition: {0,-10}", dev.GearShiftPosition);
				Console.WriteLine("Horn: {0,-10}", dev.Horn);

				Console.WriteLine("L2: {0,-10}", dev.L2);
				Console.WriteLine("L3: {0,-10}", dev.L3);
				Console.WriteLine("R2: {0,-10}", dev.R2);
				Console.WriteLine("R3: {0,-10}", dev.R3);

				Console.WriteLine("Triangle: {0,-10}", dev.Triangle);
				Console.WriteLine("Circle: {0,-10}", dev.Circle);
				Console.WriteLine("Cross: {0,-10}", dev.Cross);
				Console.WriteLine("Square: {0,-10}", dev.Square);

				Console.WriteLine("LeftFlap: {0,-10}", dev.LeftFlap);
				Console.WriteLine("RightFlap: {0,-10}", dev.RightFlap);

				Console.WriteLine("Minus: {0,-10}", dev.Minus);
				Console.WriteLine("Plus: {0,-10}", dev.Plus);

				Console.WriteLine("Back: {0,-10}", dev.Back);
				Console.WriteLine("Select: {0,-10}", dev.Select);
				Console.WriteLine("Start: {0,-10}", dev.Start);
				Console.WriteLine("PsButton: {0,-10}", dev.PsButton);
			};

			Console.WriteLine("Press enter to start listening");
			Console.ReadLine();

			listener.Listen(deviceFile);
		}
	}
}
