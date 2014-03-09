using System;

namespace Pingvinen.LogitechDrivingForceGTDriver
{
	public class LogitechDrivingForceGTChangedEventArgs : EventArgs
	{
		public LogitechDrivingForceGTChangedEventArgs(LogitechDrivingForceGT device, InputName changedName, InputType changedType, object oldValue, object newValue)
		{
			this.Device = device;
			this.ChangedName = changedName;
			this.ChangedType = changedType;
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}

		public LogitechDrivingForceGT Device { private set; get; }
		public InputType ChangedType { private set; get; }
		public InputName ChangedName { private set; get; }
		public object OldValue { private set; get; }
		public object NewValue { private set; get; }
	}
}