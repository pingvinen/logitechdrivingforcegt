//
// Original code taken from
// https://github.com/mpolaczyk/blog-examples/blob/master/jstest-mono/jstest-mono.cs
//

using System;

namespace Pingvinen.LogitechDrivingForceGTDriver.Internal
{
	/// <summary>
	/// Handles the low level communication with the "joystick"
	/// </summary>
	internal class JoystickCommunicator
	{
		private enum State : byte
		{
			Pressed = 0x01,
			Released = 0x00
		}

		private enum Type : byte
		{
			Axis = 0x02,
			Button = 0x01
		}

		private enum Mode : byte
		{
			Configuration = 0x80,
			Value = 0x00
		}

		/// <summary>
		/// Recognizes flags in buffer and returns input identification and value
		/// </summary>
		/// <returns>Input identification and value</returns>
		/// <param name="buffer">The input buffer</param>
		public JoystickChange GetChange(byte[] buffer)
		{
			if (this.CheckBit(buffer[6], (byte)Type.Axis))
			{
				return new JoystickChange() {
					KeyType = InputType.Axis,
					Key = buffer[7],
					Value = BitConverter.ToInt16(new byte[] { buffer[4], buffer[5] }, 0)
				};
			}

			if (this.CheckBit(buffer[6], (byte)Type.Button))
			{
				return new JoystickChange() {
					KeyType = InputType.Button,
					Key = buffer[7],
					Value = buffer[4] == (byte)State.Pressed ? (short)1 : (short)0
				};
			}

			return default(JoystickChange);
		}

		/// <summary>
		/// Checks if bits set in flag are set in value.
		/// </summary>
		/// <returns><c>true</c>, if value contains flag. <c>false</c> otherwise.</returns>
		/// <param name="value">The value to check</param>
		/// <param name="flag">The flag to look for</param>
		private bool CheckBit(byte value, byte flag)
		{
			byte c = (byte)(value & flag);
			return c == (byte)flag;
		}
	}
}