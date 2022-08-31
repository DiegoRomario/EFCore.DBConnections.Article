using EFCore.TrackingVsNoTracking.Console.Context;
using EFCore.TrackingVsNoTracking.Console.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using static System.Console;

InitializeDatabase();
WarmingUpDb();
// ManagedConnection();
// ExplicitOpenCloseConnection();
Console.Read();

static void InitializeDatabase()
{
    using var context = new DemoContext();
    ResetDataBase(context);
    AddTeamAndPlayers(context, "São Paulo Futebol Club");
    context.SaveChanges();
}

static void ResetDataBase(DemoContext context)
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

static void AddTeamAndPlayers(DemoContext context, string team)
{
    context.Teams.Add(new Team
    {
        Name = team,
        Players = Enumerable.Range(1, 2500).Select(p => new Player
        {
            Name = $"{p}º - Player",
        }).ToList()
    });
}


static void ManagedConnection()
{
    int dbConnectionsCounter = 0;

    using DemoContext demoContext = new();

    var time = Stopwatch.StartNew();

    var connection = demoContext.Database.GetDbConnection();

    connection.StateChange += (object sender, StateChangeEventArgs args) =>
    {
        if (args.CurrentState == ConnectionState.Open) ++dbConnectionsCounter;
    };

    for (var i = 0; i < 10000; i++) demoContext.Players.AsNoTracking().Any();

    time.Stop();

    WriteLine($"Connections counter: {dbConnectionsCounter}\nTime: {time.Elapsed}");
}

static void ExplicitOpenCloseConnection()
{
    int dbConnectionsCounter = 0;

    using DemoContext demoContext = new();

    var time = Stopwatch.StartNew();

    var connection = demoContext.Database.GetDbConnection();

    connection.StateChange += (object sender, StateChangeEventArgs args) =>
    {
        if (args.CurrentState == ConnectionState.Open) ++dbConnectionsCounter;
    };

    connection.Open();

    for (var i = 0; i < 10000; i++) demoContext.Players.AsNoTracking().Any();

    connection.Close();

    time.Stop();

    WriteLine($"Connections counter: {dbConnectionsCounter}\nTime: {time.Elapsed}");
}

static void WarmingUpDb()
{
    new DemoContext().Teams.AsNoTracking().Any();
    WriteLine($"DB Warmed Up");
    WriteLine(new string('-', 50));
}