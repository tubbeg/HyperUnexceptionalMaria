

export function createMonsterQuery (w) {
  const monsters = w.query((e) => e.all("health", "position", "velocity"));
  return monsters;
}