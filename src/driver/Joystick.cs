using System;
using System.Collections.Generic;

//
// Original code taken from
// https://github.com/mpolaczyk/blog-examples/blob/master/jstest-mono/jstest-mono.cs
//

namespace Pingvinen.LogitechDrivingForceGTDriver
{
	public class Joystick
	{
		public Joystick()
		{
			this.Buttons = new Dictionary<byte, bool>();
			this.Axis = new Dictionary<byte, short>();
		}

		private enum STATE : byte
		{
			PRESSED = 0x01,
			RELEASED = 0x00
		}

		private enum TYPE : byte
		{
			AXIS = 0x02,
			BUTTON = 0x01
		}

		private enum MODE : byte
		{
			CONFIGURATION = 0x80,
			VALUE = 0x00
		}

		/// <summary>
		/// Buttons collection, key: address, bool: value
		/// </summary>
		public Dictionary<byte, bool> Buttons { get; set; }

		/// <summary>
		/// Axis collection, key: address, short: value
		/// </summary>
		public Dictionary<byte, short> Axis { get; set; }

		/// <summary>
		/// Function recognizes flags in buffer and modifies value of button, axis or configuration.
		/// Every new buffer changes only one value of one button/axis. Joystick object have to remember all previous values.
		/// </summary>
		public bool DetectChange(byte[] buff)
		{
			// If configuration
			if (checkBit(buff[6], (byte)MODE.CONFIGURATION))
			{
				if (checkBit(buff[6], (byte)TYPE.AXIS))
				{
					// Axis configuration, read address and register axis
					byte key = (byte)buff[7];
					if (!this.Axis.ContainsKey(key))
					{
						this.Axis.Add(key, 0);
						return true;
					}
				}
				else if (checkBit(buff[6], (byte)TYPE.BUTTON))
				{
					// Button configuration, read address and register button
					byte key = (byte)buff[7];
					if (!Buttons.ContainsKey(key))
					{
						this.Buttons.Add((byte)buff[7], false);
						return true;
					}
				}
			}

			// If new button/axis value
			if (checkBit(buff[6], (byte)TYPE.AXIS))
			{
				// Axis value, decode U2 and save to Axis dictionary.
				short value = BitConverter.ToInt16(new byte[2] { buff[4], buff[5] }, 0);
				this.Axis[(byte)buff[7]] = value;
				return true;
			}
			else if (checkBit(buff[6], (byte)TYPE.BUTTON))
			{
				// Bytton value, decode value and save to Button dictionary.
				Buttons[(byte)buff[7]] = buff[4] == (byte)STATE.PRESSED;
				return true;
			}

			return false;
		}

		/// <summary>
		/// Checks if bits that are set in flag are set in value.
		/// </summary>
		private bool checkBit(byte value, byte flag)
		{
			byte c = (byte)(value & flag);
			return c == (byte)flag;
		}
	}
}