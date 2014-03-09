using System;

namespace Pingvinen.LogitechDrivingForceGTDriver
{
	public class LogitechDrivingForceGT
	{
		public LogitechDrivingForceGT (string deviceInputFile)
		{
			this.DeviceFile = deviceInputFile;
		}

		public string DeviceFile { get; set; }

		public int SteeringWheelPosition { get; set; }
		public int VerticalAxisPosition { get; set; }
		public int HorizontalAxisPosition { get; set; }
		public ButtonState L2 { get; set; }
		public ButtonState R2 { get; set; }
		public ButtonState L3 { get; set; }
		public ButtonState R3 { get; set; }
		public ButtonState Plus { get; set; }
		public ButtonState Minus { get; set; }
		public ButtonState Horn { get; set; }
		public ButtonState PsButton { get; set; }
		public ButtonState Back { get; set; }
		public ButtonState Triangle { get; set; }
		public ButtonState Circle { get; set; }
		public ButtonState Cross { get; set; }
		public ButtonState Square { get; set; }
		public ButtonState LeftFlap { get; set; }
		public ButtonState RightFlap { get; set; }
		public GearShiftPosition GearShiftPosition { get; set; }
		public ButtonState Select { get; set; }
		public ButtonState Start { get; set; }
		public int LeftPedal { get; set; }
		public int RightPedal { get; set; }
		public ButtonState MiniWheelLeft { get; set; }
		public ButtonState MiniWheelRight { get; set; }
	}
}