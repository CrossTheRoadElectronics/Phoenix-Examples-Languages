#include <stdio.h>
#include "ctre/Phoenix.h"
#include "Joystick.h"
#include <IterativeRobot.h>

class Robot: public frc::IterativeRobot {
public:
	//This can be changed to an pigeon plugged into a talon with a ribbon cable by instantating a talon
	//and passing that talon object into the pigeon constructor.

	PigeonIMU * _imu = new PigeonIMU(3);

	Joystick * _joystick = new Joystick(0);

	bool _lastButton1 = false;
	bool _lastButton2 = false;
	bool _lastButton3 = false;

	double _lastSetYaw = 0;

	void TeleopInit() {
		_imu->SetYaw(0);
	}

	void TeleopPeriodic() {

		//Set or Add yaw in degrees based on joystick
		double yaw = 180 * _joystick->GetRawAxis(1);

		if(_joystick->GetRawButton(1) && !_lastButton1) {
			double ypr_deg[] = {0, 0, 0};

			_imu->GetYawPitchRoll(ypr_deg);
			printf("yaw: %f last set yaw: %f\n", ypr_deg[0], _lastSetYaw);
		}

		if(_joystick->GetRawButton(2) && !_lastButton2) {
			_imu->SetYaw(yaw, 30);
			_lastSetYaw = yaw;
			printf("Set yaw to: %f\n", yaw);
		}

		if(_joystick->GetRawButton(3) && !_lastButton3) {
			double ypr_deg[] = {0, 0, 0};

			_imu->GetYawPitchRoll(ypr_deg);

			_imu->AddYaw(yaw, 30);

			_lastSetYaw = ypr_deg[0] +  yaw;
			printf("Added %f to yaw\n", yaw);
		}
		_lastButton1 = _joystick->GetRawButton(1);
		_lastButton2 = _joystick->GetRawButton(2);
		_lastButton3 = _joystick->GetRawButton(3);
	}
};

START_ROBOT_CLASS(Robot)
