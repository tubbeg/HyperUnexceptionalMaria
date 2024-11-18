

/*
    {|
    health:HealthComp;
    sprite:SpriteComp;
    monster:MonsterComp|}
  */
export function createMonsterQuery (w) {
  const monsters = w.query((e) => e.all("health", "sprite","monster"));
  return monsters;
}
/*
{    {|health:HealthComp;
    player:PlayerComp;
    sprite:SpriteComp|}
*/
export function createPlayerQuery (w) {
  const players = w.query((e) => e.all("health", "sprite", "player"));
  return players;
}

/*
    {|ground:GroundComp;
    sprite:SpriteComp|}

*/ 
export function createGroundQuery (w) {
  const g = w.query((e) => e.all("ground", "sprite"));
  return g;
}