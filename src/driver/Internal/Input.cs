using System;

namespace Pingvinen.LogitechDrivingForceGTDriver.Internal
{
	internal class Input
	{
		public InputName Name { get; set; }

		public Action<LogitechDrivingForceGT, object> Setter { get; set; }
		public Func<LogitechDrivingForceGT, object> Getter { get; set; }

		public virtual object ConvertValue(short value)
		{
			return value;
		}
	}
}