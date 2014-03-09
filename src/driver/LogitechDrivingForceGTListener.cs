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

		private InputRepository inputRepository = new InputRepository();

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
				object oldValue;
				object newValue;
				Input input;

				while (true)
				{
					// Read 8 bytes from file and analyze.
					fs.Read(buff, 0, 8);
					change = j.GetChange(buff);

					if (change != default(JoystickChange))
					{
						input = inputRepository.Get(change.KeyType, change.Key);

						if (input == default(Input))
						{
							Console.SetCursorPosition(0, Console.BufferHeight);
							Console.WriteLine("No input for {0} {1} = {2}", change.KeyType, change.Key, change.Value);
							continue;
						}

						oldValue = input.Getter(device);
						newValue = input.ConvertValue(change.Value);
						input.Setter(device, newValue);

						this.RaiseOnChangedEvent(this, new LogitechDrivingForceGTChangedEventArgs(
							device,
							input.Name,
							change.KeyType,
							oldValue,
							newValue
						));
					}
				}
			}
		}


	}
}