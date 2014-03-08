using System;
using System.IO;

//
// Original code taken from
// https://github.com/mpolaczyk/blog-examples/blob/master/jstest-mono/jstest-mono.cs
//

namespace Pingvinen.LogitechDrivingForceGTDriver
{
	public class LogitechDrivingForceGTListener
	{
		public event EventHandler<LogitechDrivingForceGTChangedEventArgs> RaiseOnChangedEvent;

		private const int PedalMaxValue = 32767;

		public void Listen(string deviceFile)
		{
			if (!File.Exists(deviceFile))
			{
				throw new ArgumentException(String.Format("The device file '{0}' does not exist", deviceFile));
			}

			LogitechDrivingForceGT device = new LogitechDrivingForceGT(deviceFile);

			// Read loop.
			using(FileStream fs = new FileStream(device.DeviceFile, FileMode.Open))
			{
				byte[] buff = new byte[8];
				Joystick j = new Joystick();

				while (true)
				{
					// Read 8 bytes from file and analyze.
					fs.Read(buff, 0, 8);
					if (j.DetectChange(buff))
					{
						#region Map axis
						device.RightPedal = 0;
						device.LeftPedal = 0;
						foreach (byte key in j.Axis.Keys)
						{
							switch (key)
							{
								case 0:
									device.SteeringWheelPosition = j.Axis[key];
									break;

								case 1:
									device.RightPedal = -j.Axis[key] + PedalMaxValue; // map from [-max,max] to [0,2*max]
									break;

								case 2:
									device.LeftPedal = -j.Axis[key] + PedalMaxValue; // map from [-max,max] to [0,2*max]
									break;

								case 3:
									device.HorizontalAxisPosition = j.Axis[key];
									break;

								case 4:
									device.VerticalAxisPosition = j.Axis[key];
									break;
							}
						}
						#endregion

						#region Map buttons
						device.GearShiftPosition = GearShiftPosition.Neutral;
						foreach (byte key in j.Buttons.Keys)
						{
							switch (key)
							{
								case 0:
									device.Cross = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 1:
									device.Square = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 2:
									device.Circle = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 3:
									device.Triangle = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 4:
									device.RightFlap = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 5:
									device.LeftFlap = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 6:
									device.R2 = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 7:
									device.L2 = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 8:
									device.Select = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 9:
									device.Start = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 10:
									device.R3 = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 11:
									device.L3 = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 12:
									if (j.Buttons[key])
									{
										device.GearShiftPosition = GearShiftPosition.Minus;
									}
									break;

								case 13:
									if (j.Buttons[key])
									{
										device.GearShiftPosition = GearShiftPosition.Plus;
									}
									break;

								case 14:
									device.Back = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 15:
									device.Plus = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 18:
									device.Minus = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 19:
									device.Horn = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;

								case 20:
									device.PsButton = j.Buttons[key] ? ButtonState.Down : ButtonState.Up;
									break;
							}
						}
						#endregion

						this.RaiseOnChangedEvent(this, new LogitechDrivingForceGTChangedEventArgs(device));
					}
				}
			}
		}
	}
}