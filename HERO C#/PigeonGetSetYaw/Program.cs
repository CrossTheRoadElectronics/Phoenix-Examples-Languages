/**
 * A quick example on getting and setting the yaw of a pigeon
 *
 * This assumes the pigeon is directly connected over CAN. This example prints to console and requires the use of a
 * gamepad.
 */

using CTRE.Phoenix;
using Microsoft.SPOT;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.Sensors;
using System.Threading;

namespace PigeonGetSetYaw
{
	public class Program
	{
		static RobotApplication _robotApp = new RobotApplication();

		public static void Main()
		{
			_robotApp.init();

			while (true)
			{
				_robotApp.run();

				Thread.Sleep(10);
			}
		}
	}

	public class RobotApplication
	{
		/* any system wide initializations here */
		PigeonIMU _imu = new PigeonIMU(3);
		GameController _joystick = new GameController(UsbHostDevice.GetInstance());
		
		bool _lastButton1 = false;
		bool _lastButton2 = false;
		bool _lastButton3 = false;

		float _lastSetYaw = 0;

		public void init()
		{
			_imu.SetYaw(0, 30);
		}

		
		/* loop forever */
		public void run()
        {
			//Set or Add yaw in degrees based on joystick
			float yaw = 180 * _joystick.GetAxis(0);
			if (_joystick.GetButton(1) && !_lastButton1)
			{
				float[] ypr_deg = { 0, 0, 0 };
				_imu.GetYawPitchRoll(ypr_deg);
				Debug.Print("yaw: " + ypr_deg[0].ToString() + " last set yaw: " + _lastSetYaw.ToString());
			}

			if (_joystick.GetButton(2) && !_lastButton2)
			{
				_imu.SetYaw(yaw, 30);
				_lastSetYaw = yaw;
				Debug.Print("Set yaw to: " + yaw.ToString());
			}

			if (_joystick.GetButton(3) && !_lastButton3)
			{
				float[] ypr_deg = { 0, 0, 0 };

				_imu.GetYawPitchRoll(ypr_deg);

				_imu.AddYaw(yaw, 30);

				_lastSetYaw = ypr_deg[0] + yaw;
				Debug.Print("Added " + yaw.ToString() + " to yaw");
			}
			_lastButton1 = _joystick.GetButton(1);
			_lastButton2 = _joystick.GetButton(2);
			_lastButton3 = _joystick.GetButton(3);

		}
	}

   
}
