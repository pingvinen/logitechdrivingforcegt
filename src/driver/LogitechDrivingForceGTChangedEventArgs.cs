using System;

namespace Pingvinen.LogitechDrivingForceGTDriver
{
	public class LogitechDrivingForceGTChangedEventArgs : EventArgs
	{
		private LogitechDrivingForceGT device;

		public LogitechDrivingForceGTChangedEventArgs(LogitechDrivingForceGT device)
		{
			this.device = device;
		}

		public LogitechDrivingForceGT Device
		{
			get
			{
				return this.device;
			}
		}
	}
}