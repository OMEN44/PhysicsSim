using SFML.Graphics;
using SFML.System;

namespace PhysicsSim;

public class Body: Transformable, Drawable
{
    //public new Vector2f Position;
    private float _density;
    private float _restitution;
    public float _area;
    private readonly bool _isStatic;
    private readonly Vector2f[] _points;
    private readonly Vertex[] _vertices;
    
    public Body
        (Color color, Vector2f centerPoint, float density, float restitution, bool isStatic, params Vector2f[] vertices)
    {
        Position = centerPoint;
        _density = density;
        _restitution = restitution;
        _isStatic = isStatic;
        _points = vertices;
        
        // convert points to render-able list of vertices 
        _vertices = new Vertex[vertices.Length];
        int count = 0;
        for (int i = 0; i < vertices.Length; i++)
            if (i % 2 == 0)
            {
                _vertices[i] = new(vertices[count], color);
                count++;
            }
            else _vertices[i] = new(vertices[^count], color);
        
        //calculate the area of all the triangles
        for (int i = 0; i < vertices.Length - 2; i++)
        {
            float d = VMath.Distance(_vertices[i+1].Position, _vertices[i+2].Position);
            float t = VMath.Angle(_vertices[i].Position, _vertices[i+1].Position, _vertices[i+2].Position);
            float h = d * (float)Math.Sin(t);
            Console.WriteLine($"\n1: {_vertices[i].Position}\n2: {_vertices[i+1].Position}\n3: {_vertices[i+2].Position}\na: {VMath.Distance(_vertices[i].Position, _vertices[i+1].Position) * h * 0.5f}");
            Console.WriteLine($"d: {d}\nt: {t}\nh: {h}");

            _area += VMath.Distance(_vertices[i].Position, _vertices[i+1].Position) * h * 0.5f;
        }
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        // Apply the transform of the drawable object
        states.Transform *= Transform;
        // Draw the shape using the vertex array
        target.Draw(_vertices, PrimitiveType.TriangleStrip, states);
    }
}