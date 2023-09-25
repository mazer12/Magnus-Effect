using UnityEngine;
using System;
using System.Globalization;
using System.IO;
using System.Text;

public class ODESimulation : MonoBehaviour
{
    //To make a txt file for the data
    public StringBuilder sb = new System.Text.StringBuilder();


    public float Cm = 0.175f;   //0.175 // Coefficient to adjust the strength of the Magnus force

    private Rigidbody rb;

    public float Cd = 0.35f; //0.65f;  //0.205        // Drag coefficient (?units)
    public float m = 0.058f;     //0.425f;    // Mass of projectile (kg)
    public float g = 9.8f;          // Acceleration due to gravity (m/s^2)
    private float ct = 0.05f;       // contact time of ball hitting the racket
    public float dt = 0.2f;          // timestep (seconds)

    public float radius = 0.05f; //0.11f;        // ball radius (m)
    public float rho = 1.21f;      // density of air (kg/m^3)
    public Vector3 omega = new Vector3(0.0f,50.0f,-50.0f);       // spin rate (rads/s)

    int ballCountTemp = 0; //To test the height of ball passing through net



    private float A; // A constant
    private float B; // B constant

    public Vector3 initialForce = new Vector3(30.0f, 10.5f, 1.0f);

    private Vector6 variables;
    private Transform ballTransform;
    private Vector3 initialPosition;

    //public Inputs inputManager;
    private void Start()
    {
        sb.AppendLine("x, y, z");

        ballTransform = GetComponent<Transform>();
        initialPosition = ballTransform.position;

        // Set initial conditions for the variables x1 to x6
        //variables = new Vector6(0.0f, float.Parse(inputManager.F1, CultureInfo.InvariantCulture.NumberFormat) * ct / m, 0.0f, float.Parse(inputManager.F2, CultureInfo.InvariantCulture.NumberFormat) * ct / m, 0.0f, float.Parse(inputManager.F3, CultureInfo.InvariantCulture.NumberFormat) * ct / m); // (pos.x , vel.x, pos.y, vel.y, pos.z, vel.z) (0.0f, 18.0f, 0.0f, 18.0f, 0.0f, 0.0f)
        variables = new Vector6(0.0f, initialForce.x * ct / m, 0.0f, initialForce.y * ct / m, 0.0f, initialForce.z * ct / m); // (pos.x , vel.x, pos.y, vel.y, pos.z, vel.z) (0.0f, 18.0f, 0.0f, 18.0f, 0.0f, 0.0f)
        //variables = new Vector6(0.0f, 28.25f, 0.0f, -2.0f, 0.0f, 0.0f); // (pos.x , vel.x, pos.y, vel.y, pos.z, vel.z) (0.0f, 18.0f, 0.0f, 18.0f, 0.0f, 0.0f)

        
    }

    private void Update()
    {
        float timeStep = Time.deltaTime;
        



        if (ballTransform.position.y < 0.0f)
        {
          
            //Debug.Log(ballTransform.position.y);
            //Debug.Log(ballTransform.position.z);
            
            Destroy(ballTransform.gameObject);
            //Debug.Log("time " + Time.realtimeSinceStartup);

        }
        else
        {
            //// Solve the system using Euler's method
            //variables = SolveEuler(DifferentialEquations, variables, timeStep);

            // Solve the system using the Runge-Kutta method
            variables = SolveRungeKutta(DifferentialEquations, variables, timeStep);



            // Update ball's position based on variables
            Vector3 newPosition = initialPosition + new Vector3(variables.x1, variables.x3, variables.x5);
            ballTransform.position = newPosition;
            

            if (ballTransform.position.x >= -9.05f && ballCountTemp ==0)
            {
                //Debug.Log(ballTransform.position.y);
                ballCountTemp += 1;
            }

            record();
            
          
            
        }




    }

    private Vector6 SolveEuler(Func<float, Vector6, Vector6> equations, Vector6 initialConditions, float timeStep)
    {
        Vector6 result = initialConditions;

        Vector6 derivatives = equations(Time.time, result);
        result += derivatives * timeStep;

        return result;
    }

    private Vector6 SolveRungeKutta(System.Func<float, Vector6, Vector6> equations, Vector6 initialConditions, float timeStep)
    {
        Vector6 k1, k2, k3, k4;
        Vector6 result = initialConditions;

        k1 = equations(Time.time, result) * timeStep;
        k2 = equations(Time.time + timeStep / 2f, result + k1 / 2f) * timeStep;
        k3 = equations(Time.time + timeStep / 2f, result + k2 / 2f) * timeStep;
        k4 = equations(Time.time + timeStep, result + k3) * timeStep;

        result += (k1 + k2 * 2f + k3 * 2f + k4) / 6f;

        return result;
    }


    private Vector6 DifferentialEquations(float t, Vector6 variables)
    {
        float x1 = variables.x1;
        float x2 = variables.x2;
        float x3 = variables.x3;
        float x4 = variables.x4;
        float x5 = variables.x5;
        float x6 = variables.x6;

        //calculate the new velocity
        float v = (float)Math.Sqrt(x2 * x2 + x4 * x4 + x6 * x6);

        //angular velocity decrease due to drag 
        omega.x -= 0.001225f * x4 * x4 * dt * Math.Sign(omega.x);
        omega.y -= 0.001225f * x6 * x6 * dt * Math.Sign(omega.y);
        omega.z -= 0.001225f * x2 * x2 * dt * Math.Sign(omega.z);
        //Debug.Log(omega.z);

        //float newOmega = (float)Math.Sqrt(omega.x * omega.x + omega.y * omega.y + omega.z * omega.z);

        //change in magnus coefficient with velocity and angular velocity
        //float temp = 2 + v / radius / newOmega;
        //Cm = 1 / temp;

        //A = Cm * rho * 3.14f * radius * radius / (2 * m);

        Vector3 tempVelocity = new Vector3(x2,x4,x6);
        float temp = Vector3.Cross(omega, tempVelocity).magnitude;

        A = Cm * rho * 3.14f * radius * radius * v * v/ (2 * m * temp);
        if(omega == Vector3.zero)
        {
            A = 0;
        }
        B = Cd * rho * 3.14f * radius * radius / (2 * m);

        //Debug.Log(omega.y);

        float dx1 = x2;
        float dx2 = A * (omega.y * x6 - omega.z * x4)  - B * v * x2;   // A * (omega.y * x6 - omega.z * x4)  - B * v * x2;
        float dx3 = x4;
        float dx4 = - g - A * (omega.x * x6 - omega.z * x2) - B * v * x4;    //- g - A * (omega.x * x6 - omega.z * x2) - B * v * x4;
        float dx5 = x6;
        float dx6 = A * (omega.x * x4 - omega.y * x2) - B * v * x6;   //A * (omega.x * x4 - omega.y * x2) - B * v * x6;

        return new Vector6(dx1, dx2, dx3, dx4, dx5, dx6);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //when ball hit ground
        if (collision.transform.CompareTag("Ground"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void record()
    {
        //decimal time = Decimal.Round((decimal)Time.time, 2);
        sb.AppendLine(ballTransform.position.x.ToString() + ',' + ballTransform.position.y.ToString() + "," + ballTransform.position.z.ToString());
        SaveToFile(sb.ToString());
    }
    public void SaveToFile(string content)
    {
        // Use the CSV generation from before
        //var content = ToCSV();

        // The target file path e.g.
        var folder = Application.streamingAssetsPath;

        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);


        var filePath = Path.Combine(folder, "export.csv");

        using (var writer = new StreamWriter(filePath, false))
        {
            writer.Write(content);
        }
    }


}

[Serializable]
public struct Vector6
{
    public float x1, x2, x3, x4, x5, x6;

    public Vector6(float x1, float x2, float x3, float x4, float x5, float x6)
    {
        this.x1 = x1;
        this.x2 = x2;
        this.x3 = x3;
        this.x4 = x4;
        this.x5 = x5;
        this.x6 = x6;
    }

    public static Vector6 operator *(Vector6 v, float scalar)
    {
        return new Vector6(v.x1 * scalar, v.x2 * scalar, v.x3 * scalar, v.x4 * scalar, v.x5 * scalar, v.x6 * scalar);
    }

    public static Vector6 operator +(Vector6 a, Vector6 b)
    {
        return new Vector6(a.x1 + b.x1, a.x2 + b.x2, a.x3 + b.x3, a.x4 + b.x4, a.x5 + b.x5, a.x6 + b.x6);
    }

    public static Vector6 operator /(Vector6 a, float scalar)
    {
        return new Vector6(a.x1 / scalar, a.x2 / scalar, a.x3 / scalar, a.x4 / scalar, a.x5 / scalar, a.x6 / scalar);
    }
}


