using SFML.Graphics;
using SFML.System;

namespace PhysicsSim;

public class Body: Transformable, Drawable
{
    //public new Vector2f Position;
    private float _density;
    private float _restitution;
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

        _vertices = new Vertex[vertices.Length];
        int count = 0;
        for (int i = 0; i < vertices.Length; i++)
            if (i % 2 == 0)
            {
                _vertices[i] = new(vertices[count], color);
                count++;
            }
            else _vertices[i] = new(vertices[^count], color);
    }
        
    public void Draw(RenderTarget target, RenderStates states)
    {
        // Apply the transform of the drawable object
        states.Transform *= Transform;
        
        // Draw the shape using the vertex array
        target.Draw(_vertices, PrimitiveType.TriangleStrip, states);
    }
}