using System;

namespace Pingvinen.LogitechDrivingForceGTDriver.Internal
{
	internal class InvertedInput : Input
	{
		public override object ConvertValue(short value)
		{
			return -value;
		}
	}
}