//
// Original code taken from
// https://github.com/mpolaczyk/blog-examples/blob/master/jstest-mono/jstest-mono.cs
//
using System;
using System.IO;
using Pingvinen.LogitechDrivingForceGTDriver.Internal;

namespace Pingvinen.LogitechDrivingForceGTDriver
{
	public class LogitechDrivingForceGTListener
	{
		public event EventHandler<LogitechDrivingForceGTChangedEventArgs> RaiseOnChangedEvent;

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
				JoystickCommunicator j = new JoystickCommunicator();
				JoystickChange change;

				while (true)
				{
					// Read 8 bytes from file and analyze.
					fs.Read(buff, 0, 8);
					change = j.GetChange(buff);

					if (change != default(JoystickChange))
					{
						if (change.KeyType == InputType.Axis)
						{
							#region Map axis
							device.RightPedal = 0;
							device.LeftPedal = 0;

							switch (change.Key)
							{
								case 0:
									device.SteeringWheelPosition = change.Value;
									break;

								case 1:
									device.RightPedal = -change.Value + short.MaxValue; // map from [-max,max] to [0,2*max]
									break;

								case 2:
									device.LeftPedal = -change.Value + short.MaxValue; // map from [-max,max] to [0,2*max]
									break;

								case 3:
									device.HorizontalAxisPosition = change.Value;
									break;

								case 4:
									device.VerticalAxisPosition = change.Value;
									break;
							}
							#endregion
						}
						else if (change.KeyType == InputType.Button)
						{
							#region Map buttons
							device.GearShiftPosition = GearShiftPosition.Neutral;

							switch (change.Key)
							{
								case 0:
									device.Cross = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 1:
									device.Square = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 2:
									device.Circle = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 3:
									device.Triangle = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 4:
									device.RightFlap = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 5:
									device.LeftFlap = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 6:
									device.R2 = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 7:
									device.L2 = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 8:
									device.Select = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 9:
									device.Start = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 10:
									device.R3 = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 11:
									device.L3 = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 12:
									if (change.Value == 1)
									{
										device.GearShiftPosition = GearShiftPosition.Minus;
									}
									break;

								case 13:
									if (change.Value == 1)
									{
										device.GearShiftPosition = GearShiftPosition.Plus;
									}
									break;

								case 14:
									device.Back = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 15:
									device.Plus = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 18:
									device.Minus = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 19:
									device.Horn = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;

								case 20:
									device.PsButton = change.Value == 1 ? ButtonState.Up : ButtonState.Down;
									break;
							}
							#endregion
						}

						this.RaiseOnChangedEvent(this, new LogitechDrivingForceGTChangedEventArgs(device));
					}
				}
			}
		}
	}
}