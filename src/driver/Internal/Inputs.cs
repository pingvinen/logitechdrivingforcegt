using System;
using System.Collections.Generic;

namespace Pingvinen.LogitechDrivingForceGTDriver.Internal
{
	internal class InputRepository
	{
		private IDictionary<string, Input> inputs = new Dictionary<string, Input>();

		public InputRepository()
		{
			this.inputs.Add(InputType.Axis + "0", new Input() {
				Name = InputName.Wheel,
				Setter = (dev, value) => dev.SteeringWheelPosition = (short)value,
				Getter = (dev) => dev.SteeringWheelPosition
			});
			this.inputs.Add(InputType.Axis + "1", new PedalInput() {
				Name = InputName.RightPedal,
				Setter = (dev, value) => dev.RightPedal = (int)value,
				Getter = (dev) => dev.RightPedal
			});
			this.inputs.Add(InputType.Axis + "2", new PedalInput() {
				Name = InputName.LeftPedal,
				Setter = (dev, value) => dev.LeftPedal = (int)value,
				Getter = (dev) => dev.LeftPedal
			});
			this.inputs.Add(InputType.Axis + "3", new Input() {
				Name = InputName.Horizontal,
				Setter = (dev, value) => dev.HorizontalAxisPosition = (short)value,
				Getter = (dev) => dev.HorizontalAxisPosition
			});
			this.inputs.Add(InputType.Axis + "4", new InvertedInput() {
				Name = InputName.Vertical,
				Setter = (dev, value) => dev.VerticalAxisPosition = (int)value,
				Getter = (dev) => dev.VerticalAxisPosition
			});

			this.inputs.Add(InputType.Button + "0", new ButtonInput() { Setter = (dev, value) => dev.Cross = (ButtonState)value, Getter = (dev) => dev.Cross, Name = InputName.Cross });
			this.inputs.Add(InputType.Button + "1", new ButtonInput() { Setter = (dev, value) => dev.Square = (ButtonState)value, Getter = (dev) => dev.Square, Name = InputName.Square });
			this.inputs.Add(InputType.Button + "2", new ButtonInput() { Setter = (dev, value) => dev.Circle = (ButtonState)value, Getter = (dev) => dev.Circle, Name = InputName.Circle });
			this.inputs.Add(InputType.Button + "3", new ButtonInput() { Setter = (dev, value) => dev.Triangle = (ButtonState)value, Getter = (dev) => dev.Triangle, Name = InputName.Triangle });
			this.inputs.Add(InputType.Button + "4", new ButtonInput() { Setter = (dev, value) => dev.RightFlap = (ButtonState)value, Getter = (dev) => dev.RightFlap, Name = InputName.RightFlap });
			this.inputs.Add(InputType.Button + "5", new ButtonInput() { Setter = (dev, value) => dev.LeftFlap = (ButtonState)value, Getter = (dev) => dev.LeftFlap, Name = InputName.LeftFlap });
			this.inputs.Add(InputType.Button + "6", new ButtonInput() { Setter = (dev, value) => dev.R2 = (ButtonState)value, Getter = (dev) => dev.R2, Name = InputName.R2 });
			this.inputs.Add(InputType.Button + "7", new ButtonInput() { Setter = (dev, value) => dev.L2 = (ButtonState)value, Getter = (dev) => dev.L2, Name = InputName.L2 });
			this.inputs.Add(InputType.Button + "8", new ButtonInput() { Setter = (dev, value) => dev.Select = (ButtonState)value, Getter = (dev) => dev.Select, Name = InputName.Select });
			this.inputs.Add(InputType.Button + "9", new ButtonInput() { Setter = (dev, value) => dev.Start = (ButtonState)value, Getter = (dev) => dev.Start, Name = InputName.Start });
			this.inputs.Add(InputType.Button + "10", new ButtonInput() { Setter = (dev, value) => dev.R3 = (ButtonState)value, Getter = (dev) => dev.R3, Name = InputName.R3 });
			this.inputs.Add(InputType.Button + "11", new ButtonInput() { Setter = (dev, value) => dev.L3 = (ButtonState)value, Getter = (dev) => dev.L3, Name = InputName.L3 });
			this.inputs.Add(InputType.Button + "14", new ButtonInput() { Setter = (dev, value) => dev.Back = (ButtonState)value, Getter = (dev) => dev.Back, Name = InputName.Back });
			this.inputs.Add(InputType.Button + "15", new ButtonInput() { Setter = (dev, value) => dev.Plus = (ButtonState)value, Getter = (dev) => dev.Plus, Name = InputName.Plus });
			this.inputs.Add(InputType.Button + "18", new ButtonInput() { Setter = (dev, value) => dev.Minus = (ButtonState)value, Getter = (dev) => dev.Minus, Name = InputName.Minus });
			this.inputs.Add(InputType.Button + "19", new ButtonInput() { Setter = (dev, value) => dev.Horn = (ButtonState)value, Getter = (dev) => dev.Horn, Name = InputName.Horn });
			this.inputs.Add(InputType.Button + "20", new ButtonInput() { Setter = (dev, value) => dev.PsButton = (ButtonState)value, Getter = (dev) => dev.PsButton, Name = InputName.PS });

			this.inputs.Add(InputType.Button + "12", new ButtonInput() { Setter = (dev, value) => {
					if ((ButtonState)value == ButtonState.Down)
					{
						dev.GearShiftPosition = GearShiftPosition.Minus;
					}
					else
					{
						if (dev.GearShiftPosition == GearShiftPosition.Minus)
						{
							dev.GearShiftPosition = GearShiftPosition.Neutral;
						}
					}
				}, Getter = (dev) => dev.GearShiftPosition, Name = InputName.GearShift });

			this.inputs.Add(InputType.Button + "13", new ButtonInput() { Setter = (dev, value) => {
					if ((ButtonState)value == ButtonState.Down)
					{
						dev.GearShiftPosition = GearShiftPosition.Plus;
					}
					else
					{
						if (dev.GearShiftPosition == GearShiftPosition.Plus)
						{
							dev.GearShiftPosition = GearShiftPosition.Neutral;
						}
					}
				}, Getter = (dev) => dev.GearShiftPosition, Name = InputName.GearShift });
		}

		public Input Get(InputType type, byte key)
		{
			string inputKey = type + key.ToString();

			if (this.inputs.ContainsKey(inputKey))
			{
				return this.inputs[inputKey];
			}

			return default(Input);
		}
	}
}