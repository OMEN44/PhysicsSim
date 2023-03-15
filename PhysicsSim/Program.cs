using SFML.Graphics;
using SFML.System;

namespace PhysicsSim;

public class Program
{
    static void Main()
    {
        GameBase application = new PhysicsGame();

        Vector2f[] vertices =
        {
            new(0, 0), 
            new(1, 0), 
            new(1, 1), 
            new(0, 1)
        };
        
        Vector2f[] vertices1 =
        {
            new(0, 0), 
            new(300, -200), 
            new(400, 0), 
            new(800, 100), 
            new(400, 200), 
            new(200, 300)
        };

        Body c = new Body(Color.Red, Values.PositionToPixel(new(20, 20)), 1, 1, false, vertices1);
        
        
        
        Vector2f[] vertices3 =
        {
            new(0, 0), 
            new(16, 16), 
            new(16, 0), 
            new(0, -16)
        };

        Body b = new Body(Color.Green, Values.PositionToPixel(new(10, 10)), 1, 1, false, vertices3);
        application.AddEntity("newBody", b);
        application.AddEntity("newBodyc", c);
        Console.WriteLine(b._area);

        Vector2f pos = new Vector2f(3, 40);
    
        Entity rect = new Entity(Color.Green, pos, false, 10, new(5,0), vertices);
        
        application.AddEntity("rect", rect);
        
        application.Run();
    }

    public static Vector2f FindCentroid(Vector2f[] vectors)
    {
        int numVertices = vectors.GetLength(0);

        float totalArea = 0;
        float cx = 0;
        float cy = 0;

        for (int i = 0; i < numVertices; i++)
        {
            float xi = vectors[i].X;
            float yi = vectors[i].Y;

            float xj = vectors[(i + 1) % numVertices].X;
            float yj = vectors[(i + 1) % numVertices].Y;

            float area = (xi * yj - xj * yi) / 2;
            totalArea += area;

            cx += (xi + xj) * area;
            cy += (yi + yj) * area;
        }

        cx /= 3 * totalArea;
        cy /= 3 * totalArea;

        Console.WriteLine("The center of gravity is ({0}, {1}) area is {2}", cx, cy, totalArea);
        return new(cx, cy);
    }
}