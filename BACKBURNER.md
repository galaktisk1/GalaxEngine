# Backburner

Ideas parked until GalaxEngine has caught up with the class. Add to it freely.

## Physics engine (the big one, after catching up)

- **Fixed timestep:** step physics at a set rate (like 50 Hz) with a time accumulator, independent of frame rate. The real version of the class's `FixedUpdate`.
- **Collision responses:** Block, Overlap, and Ignore instead of `isTrigger`. A pair uses the weaker of the two (`Math.Min` over `{ Ignore, Overlap, Block }`).
- **Collision layers:** a matrix of which layers can hit which.
- **Better resolution:** impulses instead of position pushes, with bounciness (restitution), friction, and mass.
- **Tunneling:** fast objects skip through thin walls. Smaller substeps, or continuous collision detection (sweeping the shape along its path).
- **More shapes:** circle vs circle and circle vs polygon. `Collider` has a radius, but only the point test uses it.
- **Rotation:** angular velocity and torque, so things tumble when hit off-center.
- **Broad phase:** a spatial grid or tree, so not every pair gets tested (currently n²).
- **Queries:** raycasts and overlap checks ("is the player on the ground?", "what's under the mouse?").
- **Child objects:** the push adds a world-space MTV to a local `Position`, which is wrong for children of rotated or scaled parents.

## Components

- **`requires`:** components declare what they need, and it gets added automatically. `MovementComponent` would add its own `Rigidbody`. Also the check that blocks engine components (like `Polygon`) on a `LogicObject`.
- **`enabled`:** with `OnEnable` and `OnDisable`, skipping disabled components in `Start`, `Update`, and `Draw`, and pausing their coroutines. `Transform` can't be disabled.
- **`Awake`:** runs right when a component is added, before `Start`.
- **`ProjectileMovementComponent`:** from the 10/28 space shooter, for the turret's lasers.

## Transform math

- Done: `TransformPoint`, `InverseTransformPoint`, world-aware `LookAt`, `Vector2.MoveTowards`.
- Settable `WorldPosition` and `WorldRotation`.
- `AlignTo`.
- `worldPositionStays` for `SetParent`.
- `Transform.MoveTowards`, and `MovementComponent.MoveTowards` (the velocity version).

## UI

- UI managers in the gameplay tier: messages, popups, tooltips.
- `TextLabel.LineSpacing` is also used as letter spacing; split them.

## Time and music

- Per-object time scale.
- A music conductor: a BPM-based time scale in `Time`, and beat-based waits for coroutines.

## Ray tracers

- GPU reflections.
- An OBJ loader, so someone could bring a Blender model into the engine.

## Ports

- `factory` and `mygame` from simplegaem.
- The 10/28 space shooter additions (turret).

## Big goals

- A 3D concert stage lighting simulator.

## Tied to the concert stage

- I would like to do some actual physics calculations for lighting
- Think about an actual concert hall, the lights arent shining in a cone
- Fog fills the room, so what if we did the same, and then simulated how light would interact with that fog

## Housekeeping

- Rename the repo to GalaxYngine.
- The 4 nullable warnings in the sandbox `HoverHighlight`.
