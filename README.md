# Magnus-Effect
A simulation of a tennis ball showing the Magnus Effect and other forces.

Magnus effect Application:

1) You can find the game in the build folder inside MagnusEffect. However, I would suggest using the unity editor to play the game, because of a bug in the game which does not allow the values of parameters to be changed.
2) Press z for the ball to spawn and shoot. 
3) The values of the variables a set and as shown in the screen. You can change these values to test the application. I would suggest using Unity to play the game and change the values in the inspector. To change the values of force velocity in the inspector: Select Prefabs -> Ball. Drag down in the inspector to see the ODE Simulations script, you can change the values there.
4) To change the camera angle: press F1 for the secondary camera and F2 for the main camera. 
5) Reset button to reset to default values in the game. 
6) You can find the data (position coordinates) of the last ball in the StreamingAsset -> export file.

Alignment of axis:
positive x-axis: point inside the screen.
positive y-axis: points upwards.
positive z-axis: point left.

Note: The simulation is not a complete tennis game, the ball does not bounce as in tennis. But it shows the trajectory of the ball under Magnus and Drag force. 
