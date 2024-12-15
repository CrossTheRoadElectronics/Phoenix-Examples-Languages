/**
 * @brief A quick example on getting and setting the yaw of a pigeon
 * 
 * This assumes the pigeon is directly connected over CAN. This example prints to console and requires the use of a 
 * gamepad.
 */
package org.usfirst.frc.team217.robot;

import com.ctre.phoenix.sensors.PigeonIMU;

import edu.wpi.first.wpilibj.IterativeRobot;
import edu.wpi.first.wpilibj.Joystick;

public class Robot extends IterativeRobot {

	//This can be changed to an pigeon plugged into a talon with a ribbon cable by instantating a talon
	//and passing that talon object into the pigeon constructor. 
	
	PigeonIMU _imu = new PigeonIMU(3);

	Joystick _joystick = new Joystick(0);
	
	boolean _lastButton1 = false;
	boolean _lastButton2 = false;
	boolean _lastButton3 = false;
	
	double _lastSetYaw = 0;
	
	public void teleopInit() {
		_imu.setYaw(0);
	}

	public void teleopPeriodic() {
		
		//Set or Add yaw in degrees based on joystick
		double yaw = 180 * _joystick.getRawAxis(1);
		
		if(_joystick.getRawButton(1) && !_lastButton1) {
			double[] ypr_deg = {0, 0, 0};
			
			_imu.getYawPitchRoll(ypr_deg);
			System.out.printf("yaw: %f last set yaw: %f\n", ypr_deg[0], _lastSetYaw);
		}
		
		if(_joystick.getRawButton(2) && !_lastButton2) {
			_imu.setYaw(yaw, 30);
			_lastSetYaw = yaw;
			System.out.printf("Set yaw to: %f\n", yaw);
		}
		
		if(_joystick.getRawButton(3) && !_lastButton3) {
			double[] ypr_deg = {0, 0, 0};
			
			_imu.getYawPitchRoll(ypr_deg);
			
			_imu.addYaw(yaw, 30);	
			
			_lastSetYaw = ypr_deg[0] +  yaw;
			System.out.printf("Added %f to yaw\n", yaw);
		}
		_lastButton1 = _joystick.getRawButton(1);
		_lastButton2 = _joystick.getRawButton(2);
		_lastButton3 = _joystick.getRawButton(3);
	}
}
