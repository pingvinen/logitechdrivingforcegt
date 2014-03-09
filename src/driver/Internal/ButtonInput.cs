using System;

namespace Pingvinen.LogitechDrivingForceGTDriver.Internal
{
	internal class ButtonInput : Input
	{
		public override object ConvertValue(short value)
		{
			return value == (short)1 ? ButtonState.Down : ButtonState.Up;
		}
	}
}

