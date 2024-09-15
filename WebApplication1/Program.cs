using WebApplication1.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games=[
    new (1, "The Legend of Zelda: Breath of the Wild", "Adventure", 59.99m, new DateOnly(2017, 3, 3)),
            new (2, "Super Mario Odyssey", "Platformer", 49.99m, new DateOnly(2017, 10, 27)),
            new (3, "Hades", "Action Roguelike", 24.99m, new DateOnly(2020, 9, 17)),
            new (4, "Cyberpunk 2077", "RPG", 59.99m, new DateOnly(2020, 12, 10)),
            new (5, "Animal Crossing: New Horizons", "Simulation", 59.99m, new DateOnly(2020, 3, 20))
];
app.MapGet("/games", () => games);
app.MapGet("/games/{id}",(int id)=>games.Find(game=>game.Id==id))
 .WithName("Getgame");
app.MapPost("/gamesi",(CreateGameDto newgame)=>{
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
app.Run();