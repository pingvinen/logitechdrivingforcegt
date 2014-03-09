using System;

namespace Pingvinen.LogitechDrivingForceGTDriver.Internal
{
	internal class PedalInput : Input
	{
		public override object ConvertValue(short value)
		{
			return -value + short.MaxValue; // map from [-max,max] to [0,2*max]
		}
	}
}