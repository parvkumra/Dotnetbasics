namespace WebApplication1.Endpoints;
using WebApplication1.Dtos;

public static class GamesEndpoints{

private static readonly List<GameDto> games=[
    new (1, "The Legend of Zelda: Breath of the Wild", "Adventure", 59.99m, new DateOnly(2017, 3, 3)),
            new (2, "Super Mario Odyssey", "Platformer", 49.99m, new DateOnly(2017, 10, 27)),
            new (3, "Hades", "Action Roguelike", 24.99m, new DateOnly(2020, 9, 17)),
            new (4, "Cyberpunk 2077", "RPG", 59.99m, new DateOnly(2020, 12, 10)),
            new (5, "Animal Crossing: New Horizons", "Simulation", 59.99m, new DateOnly(2020, 3, 20))
];

public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){


var group=app.MapGroup("games");
group.MapGet("/", () => games);
group.MapGet("/{id}",(int id)=>{

  GameDto? game=games.Find(game=>game.Id==id);

  return game is null?Results.NotFound():Results.Ok(game);

}).WithName("Getgame");
group.MapPost("/",(CreateGameDto newgame)=>{
  GameDto game=new(
    games.Count+1,
    newgame.Name,
    newgame.Genre,
    newgame.Price,
    newgame.ReleaseDate

  );
  games.Add(game);
  return Results.CreatedAtRoute("Getgame",new {id= game.Id},game);
});

group.MapPut("/{id}",(int id,UpdateGameDto uggame)=>{
  var index=games.FindIndex(game=>game.Id==id);
  if(index==-1)return Results.NotFound();
  games[index]=new GameDto(
      id,
      uggame.Name,
      uggame.Genre,
      uggame.Price,
      uggame.ReleaseDate
  );
  return Results.NoContent();
});
group.MapDelete("/{id}",(int id)=>{
  games.RemoveAll(game=>game.Id==id);
});
return group;
}

};