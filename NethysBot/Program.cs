﻿using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NethysBot
{
	class Program
	{
		public static LiteDatabase Database = new LiteDatabase(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Database.db"));
		static void Main(string[] args)
			=> new Program().MainAsync().GetAwaiter().GetResult();

		private DiscordSocketClient _client;
		private IConfiguration _config;

		public async Task MainAsync()
		{
			Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Data"));
			_client = new DiscordSocketClient();
			_config = BuildConfig();

			var services = ConfigureServices();
			services.GetRequiredService<LogService>();
			await services.GetRequiredService<CommandHandlingService>().InitializeAsync(services);

			await _client.LoginAsync(TokenType.Bot, _config["token"]);
			await _client.StartAsync();

			await Task.Delay(-1);
		}

		private IServiceProvider ConfigureServices()
		{
			return new ServiceCollection()
				// Base
				.AddSingleton(_client)
				.AddSingleton(new CommandService(new CommandServiceConfig()
				{
					DefaultRunMode = RunMode.Async,
					CaseSensitiveCommands = false
				})
				)
				.AddSingleton<CommandHandlingService>()
				// Logging
				.AddLogging()
				.AddSingleton<LogService>()
				// Extra
				.AddSingleton(new HelpService(_client))
				.AddSingleton(_config)
				.AddSingleton(new InteractiveService(_client))
				// Add additional services here...
				.BuildServiceProvider();
		}

		private IConfiguration BuildConfig()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Data", "config.json"))
				.Build();
		}
	}
}