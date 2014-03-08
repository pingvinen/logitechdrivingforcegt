using System;

namespace Pingvinen.LogitechDrivingForceGTDriver.Internal
{
	internal class JoystickChange
	{
		/// <summary>
		/// Gets or sets what type of input changed
		/// </summary>
		public InputType KeyType { get; set; }

		/// <summary>
		/// Gets or sets the key of the input
		/// </summary>
		public byte Key { get; set; }

		/// <summary>
		/// Gets or sets the new value of the input
		/// </summary>
		public short Value { get; set; }
	}
}