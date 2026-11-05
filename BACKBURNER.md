# Backburner

Ideas parked until GalaxEngine has caught up with the class. Add to it freely.

## Physics engine (the big one, after catching up)

### Rigid-body physics

- Done: Newton's second law (\(F = ma\)). `AddForce` accumulates, `Integrate` divides by mass (semi-implicit Euler).
- Done: mass, velocity, momentum, and `AddImpulse` on `Rigidbody`.
- Partly done: `PhysicsScene` has `PixelsPerMeter` and `GravityStrength` (9.81), but only floor friction uses them. `Rigidbody.Gravity` is still a per-body pixel value.
- Density: mass = density × area, with the area from the collider's points (the shoelace formula `Polygon` already uses), so bigger shapes are heavier.
- Center of mass and moment of inertia.
- Torque and angular velocity, so things rotate naturally from collisions (tumbling when hit off-center).

### Collision system

- Done: Unreal-style responses. Block resolves, Overlap only detects, Ignore skips. A pair uses the weaker of the two.
- Collision channels/layers with configurable responses (a matrix of which layers can hit which).
- Separate collision detection from collision resolution.
- Collision and overlap events independent of the physical response.
- Done: impulses along the collision normal from mass and velocity, plus a mass-weighted position push. Still missing: contact position.
- Done: restitution (`Bounciness`, the higher of the two bodies).
- `MovementComponent`'s velocity cancel is now redundant with the impulse; remove it.
- Contact points (needed for impulses at a position and for torque).
- More shapes: circle vs circle and circle vs polygon. `Collider` has a radius, but only the point test uses it.
- Tunneling: fast objects skip through thin walls. Substeps, or continuous collision detection (sweeping the shape along its path).
- Broad phase: a spatial grid or tree, so not every pair gets tested (currently n²).
- Queries: raycasts and overlap checks ("is the player on the ground?", "what's under the mouse?").
- Child objects: the push adds a world-space MTV to a local `Position`, which is wrong for children of rotated or scaled parents.

### Friction and resistance

- Done (top-down floor friction): static and kinetic friction (`StaticFriction`, `Friction`) from the normal force \(m \cdot g\). Objects slow down and stop from friction.
- Contact friction between colliding bodies (sliding along walls and platforms), using the impulse as the normal force.
- Collision impulses bypass static friction, so a bump can nudge a body that a push couldn't.
- The overshoot check also zeroes the velocity when the player reverses direction; apply friction's slowdown separately to fix it.
- Aerodynamic drag from air density, velocity, surface area, and drag coefficient.

### Physics demonstrations

Individual demos to test and verify the calculations.

- Bowling ball vs feather: identical acceleration in a vacuum, different with air resistance.
- Ball vs wall: impulses, reflections, and restitution.
- Thrown object: projectile motion, ground collision, then sliding.
- Friction test: an object slides until calculated friction stops it.
- Center-of-mass test: balance an object on a platform edge and see whether it tips.

### Simulation architecture

- Done: a fixed-timestep loop (60 Hz) with a time accumulator and a 5-step safety limit, independent of rendering FPS.
- Interpolation, so movement stays smooth when frames and physics steps don't line up.
- Numerical integration for positions and velocities (semi-implicit Euler to start, maybe Verlet or RK4 later).
- Clear responsibilities for `Collider`, `Rigidbody`, and `PhysicsScene`.
- Debug drawing: forces, velocities, contact points, and centers of mass.

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

### Fog and fluid physics

- Fog emitted from stage fog machines.
- Fog as a 3D density field.
- Fog spreading, dissipating, and responding to airflow.
- Eventually temperature, buoyancy, and simplified fluid dynamics.
- Different fog behaviors: atmospheric haze, low-lying fog.

### Physically based lighting

- Volumetric lighting that interacts with the fog density.
- Light scattering and attenuation through fog.
- The Beer–Lambert law for light extinction.
- Realistic spotlight beams, volumetric shadows, and atmospheric haze.
- The fog simulation and lighting shaders share the same underlying environment.

## Housekeeping

- Rename the repo to GalaxYngine.
- The 4 nullable warnings in the sandbox `HoverHighlight`.
