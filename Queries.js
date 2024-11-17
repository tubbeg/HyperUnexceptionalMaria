

export function createMonsterQuery (w) {
  const monsters = w.query((e) => e.all("health", "position", "velocity"));
  for (const entity of monsters) {
    console.log(entity);
    const position = entity.position;
    console.log(position);
    const velocity = entity.velocity;
    console.log(velocity);
  }
  return monsters;
}