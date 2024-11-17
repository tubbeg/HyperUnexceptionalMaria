

/*
{|position:PositionComp;
  health:HealthComp;
  velocity:VelocityComp;
  monster:MonsterComp|}*/
export function createMonsterQuery (w) {
  const monsters = w.query((e) => e.all("health", "position", "velocity", "monster"));
  return monsters;
}
/*
{|position:PositionComp;
  health:HealthComp;
  velocity:VelocityComp;
  player:PlayerComp|}
*/
export function createPlayerQuery (w) {
  const players = w.query((e) => e.all("health", "position", "velocity", "player"));
  return players;
}