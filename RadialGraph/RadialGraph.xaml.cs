/*
 * Copyright (C) 2012-2014 Brian Mwadime
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FKLabs
{
	public partial class RadialGraph : UserControl
	{
		#region Dependency Property Registrations

		public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register("Text", typeof(string), typeof(RadialGraph), new PropertyMetadata(default(string)));

		public static readonly DependencyProperty EndAngleProperty = 
            DependencyProperty.Register("EndAngle", typeof(double), typeof(RadialGraph), new PropertyMetadata(default(double)));

		public static readonly DependencyProperty MinimumValueProperty = 
            DependencyProperty.Register("MinimumValue", typeof(double), typeof(RadialGraph), new PropertyMetadata(default(double)));

		public static readonly DependencyProperty MaximumValueProperty = 
            DependencyProperty.Register("MaximumValue", typeof(double), typeof(RadialGraph), new PropertyMetadata(default(double)));

		public static readonly DependencyProperty FillProperty = 
            DependencyProperty.Register("Fill", typeof(Brush), typeof(RadialGraph), new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty CurrentValueProperty = 
            DependencyProperty.Register("CurrentValue", typeof(double), typeof(RadialGraph), new PropertyMetadata(default(double)));

		public static readonly DependencyProperty OverflowValueToZeroProperty = 
            DependencyProperty.Register("OverflowValueToZero", typeof(bool), typeof(RadialGraph), new PropertyMetadata(false));

		public static readonly DependencyProperty AllowKeyboardInputProperty = 
            DependencyProperty.Register("AllowKeyboardInput", typeof(bool), typeof(RadialGraph), new PropertyMetadata(default(bool)));

		public static readonly DependencyProperty ShowSliderValueProperty = 
            DependencyProperty.Register("ShowSliderValue", typeof(bool), typeof(RadialGraph), new PropertyMetadata(false));

        #endregion Dependency Property Registrations

        #region Properties

        //public string Text
        //{
        //	get
        //	{
        //		return sliderTextBoxText;
        //	}

        //	set
        //	{
        //		sliderTextBoxText = value;

        //		if (!showSliderValue)
        //		{
        //			SliderValueTextBox.Text = sliderTextBoxText;
        //		}
        //	}
        //}

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        //      public double EndAngle
        //{
        //	get
        //	{
        //		return Slider.EndAngle;
        //	}

        //	private set
        //	{
        //		Slider.EndAngle = value;
        //	}
        //}

        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set
            {
                SetValue(EndAngleProperty, value);
            }
        }

        public double MinimumValue
        {
            get { return (double)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }

        public double MaximumValue
        {
            get { return (double)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        //public double MinimumValue
        //{
        //	get { return minimumValue; }
        //	set { minimumValue = value; }
        //}

        //public double MaximumValue
        //{
        //	get { return maximumValue; }
        //	set { maximumValue = value; }
        //}

        //public double CurrentValue
        //{
        //	get { return currentValue; }
        //	set { currentValue = value; SetSliderValue(value, false); }
        //}

        #endregion Properties

        // The brush used to fill the slider track
        [Category("Appearance")]
		public Brush SliderBrush
		{
			get
			{
				return Slider.Fill;
			}

			set
			{
				Slider.Fill = value;
			}
		}

		[Category("Appearance")]
		public double SliderOpacity
		{
			get
			{
				return Slider.Opacity;
			}

			set
			{
				Slider.Opacity = value;
			}
		}

		// Simply toggles the visibility of the text in center of the control
		[Category("Appearance")]
		public Visibility TextVisible
		{
			get
			{
				return SliderValueTextBox.Visibility;
			}

			set
			{
				SliderValueTextBox.Visibility = value;
			}
		}

		/// <summary>
		/// In case someone inserts a value over the maximum by using the textbox input, set the CurrentValue
		/// to zero if this is true. Otherwise the CurrentValue is set to the maximum value
		/// </summary>
		public bool OverflowValueToZero
		{
			get { return (bool)GetValue(OverflowValueToZeroProperty); }
			set { SetValue(OverflowValueToZeroProperty, value); }
		}

		public bool AllowKeyboardInput
		{
			get { return (bool)GetValue(AllowKeyboardInputProperty); }
			set { SetValueCustom(AllowKeyboardInputProperty, value); }
		}

		public bool ShowSliderValue
		{
			get { return (bool)GetValue(ShowSliderValueProperty); }
			set { SetValue(ShowSliderValueProperty, value); }
		}

        public double CurrentValue
        {
            get { return (double)GetValue(CurrentValueProperty); }
            set
            {
                SetValue(CurrentValueProperty, value);
            }
        }

        //      public bool OverflowValueToZero
        //{
        //	get { return overflowValueToMinimum; }
        //	set { overflowValueToMinimum = value; }
        //}

        //[DefaultValue(false)]
        //public bool AllowKeyboardInput
        //{
        //	get
        //	{
        //		return allowKeyboardInput;
        //	}

        //	set
        //	{
        //		allowKeyboardInput = value;

        //		if (this.IsEnabled)
        //		{
        //			SliderValueTextBox.IsEnabled = value;
        //		}
        //	}
        //}

        //public bool ShowSliderValue
        //{
        //	get
        //	{
        //		return showSliderValue;
        //	}

        //	set
        //	{
        //		showSliderValue = value;

        //		if (showSliderValue == true)
        //		{
        //			SetSliderValue(this.CurrentValue, false);
        //			SliderValueTextBox.IsEnabled = true;
        //		}
        //		else
        //		{
        //			this.Text = sliderTextBoxText;
        //			SliderValueTextBox.IsEnabled = false;
        //		}
        //	}
        //}

        #region Private fields

        private double controlWidth;
		private double controlHeight;
		private Point zeroAnglePoint;
		private Point centerPoint;
        private string sliderTextBoxText = "";
        private double minimumValue = 0;
        private double maximumValue = 100;
        private double currentValue;
        private bool showSliderValue = true;
        private bool overflowValueToMinimum;
        private bool allowKeyboardInput = false;

        #endregion Private fields

        public event PropertyChangedEventHandler PropertyChanged;
        void SetValueCustom(DependencyProperty property, object value,[System.Runtime.CompilerServices.CallerMemberName] string p = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }



        #region Constructor

        public RadialGraph()
		{
			InitializeComponent();

			//LayoutRoot.DataContext = this;
			(Content as FrameworkElement).DataContext = this;

			controlWidth = this.RenderSize.Width;
			controlHeight = this.RenderSize.Height;

			SetInputScope();

			zeroAnglePoint = new Point(this.Width / 2, 0);
			centerPoint = new Point(this.Width / 2, this.Height / 2);

			this.Loaded += new RoutedEventHandler(RadialGraph_Loaded);

			this.Unloaded += new RoutedEventHandler(RadialGraph_Unloaded);
		}

		private void RadialGraph_Loaded(object sender, RoutedEventArgs e)
		{
			SliderValueTextBox.Text = Text;
			SetSliderValue(CurrentValue, false);
		}

		#endregion Constructor

		#region Methods

		/// <summary>
		/// Defines the value of the slider with an input of degrees from 0 to 360 (full circle).
		/// </summary>
		/// <param name="newValue">A degree value between 0 and 360</param>
		/// <param name="isDegrees">If true, degrees will be converted to a slider value. If false, the value
		/// will be set as the slider value.</param>
		private void SetSliderValue(double newValue, bool isDegrees)
		{
			double oldValue = CurrentValue;

			if (!isDegrees && (newValue < this.MinimumValue || newValue > this.MaximumValue))
			{
				if (OverflowValueToZero)
					SetSliderValue(this.MinimumValue, false);
				else
					SetSliderValue(this.MaximumValue, false);

				return;
			}

			if (isDegrees)
				// Calculate the slider value according to minimum and maximum values
				CurrentValue = (int)(this.MinimumValue + (this.MaximumValue - this.MinimumValue) * newValue / 360);
			else
			{
				CurrentValue = Convert.ToInt32(newValue);
				newValue = newValue / (this.MaximumValue - this.MinimumValue) * 360;
			}

			// Update the UI thread
			Dispatcher.BeginInvoke(() =>
			{
				Slider.EndAngle = newValue; // Visually update the slider
			});

			// Optionally show the calculated slider value on the control
			if (ShowSliderValue)
				// Update the UI thread
				Dispatcher.BeginInvoke(() =>
				{
					SliderValueTextBox.Text = CurrentValue.ToString();
				});

			// If the SliderValueChanged event is already initialized...
			if (SliderValueChanged != null)
			{
				//... raise a SliderValueChanged event with the corresponding data
				SliderValueChangedEventArgs newData = new SliderValueChangedEventArgs(oldValue, newValue);
				SliderValueChanged(this, newData);
			}
		}

		private void CalculateDraggingPosition(object currentPosition)
		{
			Point position = (Point)currentPosition;

			double rotation = Math.Atan2(position.X - controlWidth / 2, position.Y - controlHeight / 2);

			rotation = 180 - RadianToDegree(rotation);
			rotation = Normalise(rotation);
			SetSliderValue(rotation, true);
		}

		private static double RadianToDegree(double angle)
		{
			return angle * (180.0 / Math.PI);
		}

		public double Normalise(double degrees)
		{
			double retval = degrees % 360;
			if (retval < 0)
				retval += 360;
			return retval;
		}

		private void SetInputScope()
		{
			InputScopeNameValue digitsInputNameValue = InputScopeNameValue.Digits;

			SliderValueTextBox.InputScope = new InputScope()
			{
				Names = { new InputScopeName() { NameValue = digitsInputNameValue } }
			};
		}

		private bool IsPointInsideEllipseShape(Ellipse ellipseToUse, Point point)
		{
			EllipseGeometry ellipse = new EllipseGeometry();
			ellipse.RadiusX = ellipseToUse.RenderSize.Width / 2;
			ellipse.RadiusY = ellipseToUse.RenderSize.Height / 2;

			// Get absolute position of the ellipse inside parent control
			//Point absolutePosition = ellipse.Transform.Transform(new Point(0, 0));
			var transform = ellipseToUse.TransformToVisual(this);
			Point absolutePosition = transform.Transform(new Point(0, 0));

			ellipse.Center = new Point(absolutePosition.X + ellipse.RadiusX,
										absolutePosition.Y + ellipse.RadiusY);

			if (ellipse.Bounds.Contains(point))
			{
				double xPositionInsideBounds = point.X - absolutePosition.X;
				double yPositionInsideBounds = point.Y - absolutePosition.Y;

				if (PointInsideEllipse(xPositionInsideBounds, ellipse.RadiusX, yPositionInsideBounds, ellipse.RadiusY))
				{
					return true;
				}
			}

			return false;
		}

		private bool PointInsideEllipse(double x, double a, double y, double b)
		{
			double xDistance = x - a;
			xDistance = xDistance < 0 ? xDistance * -1 : xDistance;

			double yDistance = y - b;
			yDistance = yDistance < 0 ? yDistance * -1 : yDistance;

			double ellipseCalculation = (Math.Pow(xDistance, 2) / Math.Pow(a, 2)) +
										(Math.Pow(yDistance, 2) / Math.Pow(b, 2));

			if (ellipseCalculation < 1)
			{
				return true;
			}

			return false;
		}

		#endregion Methods

		#region Events

		private void Touch_FrameReported(object sender, TouchFrameEventArgs e)
		{
			TouchPoint touchPoint = e.GetTouchPoints(this)[0];

			// Slider value can only be modified when the control itself is enabled and the touch point
			// is inside the control (which is elliptic). Also check if the touch position is not in the middle
			// of the control where the textbox is.
			if (this.IsEnabled && PointInsideEllipse(touchPoint.Position.X, controlWidth / 2, touchPoint.Position.Y, controlHeight / 2)
				&& !IsPointInsideEllipseShape(InnerEllipse, touchPoint.Position))
			{
				/*
				* Make sure that the touch point is inside the control, otherwise touch events from any
				* point inside the app are processed. If we don't add this check and there are multiple
				* radial sliders on the form, they will all react to all touch events on the parent page.
				*/
				if (Enumerable.Range(0, (int)controlWidth).Contains((int)touchPoint.Position.X) &&
					Enumerable.Range(0, (int)controlHeight).Contains((int)touchPoint.Position.Y))
				{
					//Thread calcThread = new Thread(new ParameterizedThreadStart(CalculateDraggingPosition));
					//calcThread.Start(touchPoint.Position);
				}
			}
		}

		private void Knob_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
		{
			zeroAnglePoint = new Point(this.Width / 2, 0);
			centerPoint = new Point(this.Width / 2, this.Height / 2);

			controlWidth = this.RenderSize.Width;
			controlHeight = this.RenderSize.Height;
		}

		private void SliderValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (ShowSliderValue)
			{
				string sliderTextValue = SliderValueTextBox.Text;

				if (!string.IsNullOrEmpty(sliderTextValue))
				{
					SetSliderValue(Convert.ToInt32(SliderValueTextBox.Text), false);
				}
			}
		}

		private void SliderValueTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			// All the keys but dot is accepted from the digits keyboard
			if (e.Key == Key.Unknown)
			{
				e.Handled = true;
			}
		}

		private void RadialGraph_Unloaded(object sender, RoutedEventArgs e)
		{
			Touch.FrameReported -= new TouchFrameEventHandler(Touch_FrameReported);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			Touch.FrameReported += new TouchFrameEventHandler(Touch_FrameReported);
		}

		private void UserControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (this.IsEnabled)
			{
				VisualStateManager.GoToState(this, "Normal", true);
				if (AllowKeyboardInput)
				{
					SliderValueTextBox.IsEnabled = false;
				}
			}
			else
			{
				VisualStateManager.GoToState(this, "Disabled", true);
				SliderValueTextBox.IsEnabled = false;
			}
		}

		private void UserControl_GotFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			VisualStateManager.GoToState(this, "Focused", true);
		}

		private void UserControl_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			VisualStateManager.GoToState(this, "Unfocused", true);
		}

		private void SliderValueTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			// The user will be able to input new valuse faster if the content is already selected
			//SliderValueTextBox.SelectAll();
		}

		#region Custom events

		public delegate void OnSliderValueChanged(object sender, SliderValueChangedEventArgs e);

		public event OnSliderValueChanged SliderValueChanged;

		#endregion Custom events

		#endregion Events
	}

	public class SliderValueChangedEventArgs : EventArgs
	{
		private Double oldValue;
		private Double newValue;

		public SliderValueChangedEventArgs(Double oldValue, Double newValue)
		{
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		public Double OldValue
		{
			get { return this.oldValue; }
		}

		public Double NewValue
		{
			get { return this.newValue; }
		}
	}
}