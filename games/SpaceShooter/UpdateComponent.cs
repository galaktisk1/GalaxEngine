
using GalaxEngine;
using Raylib_cs;
using System.Collections;

namespace SpaceShooter;
public class UpdateComponent : GameplayComponent
{   
    public float speed = 2 * 60; // due to 60 frames per second
    public override void Start()
    {
        Transform.Position = Engine.Center;
        Transform.Scale = new Vector2(0.5f, 0.5f);
        StartCoroutine(Fire());
        // anything i wanna test or debug
        StartCoroutine(Testing());
    }
    public override void Update()
    {
        var direction = Vector2.Zero;
        if (Input.GetKey(KeyboardKey.Up) || Input.GetKey(KeyboardKey.W))
            direction += Vector2.Up;
        if (Input.GetKey(KeyboardKey.Down) || Input.GetKey(KeyboardKey.S))
            direction += Vector2.Down;
        if (Input.GetKey(KeyboardKey.Left) || Input.GetKey(KeyboardKey.A))
            direction += Vector2.Left;
        if (Input.GetKey(KeyboardKey.Right) || Input.GetKey(KeyboardKey.D))
            direction += Vector2.Right;
        // due to Vector2 being a struct, this is a copy of Transform.Position 
        // and not a reference
        Transform.Position += direction.Normalized * speed * Time.DeltaTime;
        if (Input.GetKey(KeyboardKey.Q)) Transform.Rotation -= 2f * Time.DeltaTime;
        if (Input.GetKey(KeyboardKey.E)) Transform.Rotation += 2f * Time.DeltaTime;
    }
    private IEnumerator Fire()
    {
        while (true)
            {
                var laser = Engine.CurrentScene.Instantiate(new LaserGameObject(), Transform.Position);
                laser.Transform.Position = Transform.WorldPosition + Transform.Up * 100;
                laser.Transform.Rotation = Transform.WorldRotation;
                var laserComp = laser.GetComponent<LaserController>()!;
                laserComp.Direction = Transform.Up;

                yield return new WaitForSeconds(0.2f);
            }
    }

    private IEnumerator Testing()
    {
        while (true)
        {
            Console.WriteLine(Engine.CurrentScene.SceneObjects.Count);
            yield return new WaitForSeconds(1f);
        }
    }
}