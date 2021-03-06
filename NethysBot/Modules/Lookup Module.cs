﻿using Discord.Commands;
using NethysBot.Helpers;
using NethysBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NethysBot.Modules
{
	[Name("Lookup")]
	public class LookupModule : NethysBase<SocketCommandContext>
	{

		[Command("Feat"), Alias("F","Feats")] 
		[Summary("Lookup a feat on your active character or companion. Or list all of them if used with no arguments.")]
		public async Task getfeat([Remainder] string Name = null)
		{
			Character c = null;
			if (!Name.NullorEmpty() && Name.ToLower().EndsWith("-c"))
			{
				Name = Name.Replace("-c", "");
				c = GetCompanion();

				if (c == null)
				{
					await ReplyAsync("You have no active companion.");
					return;
				}
			}
			else
			{
				c = GetCharacter();

				if (c == null)
				{
					await ReplyAsync("You have no active character.");
					return;
				}
			}

			if (Name.NullorEmpty())
			{
				var fs = await SheetService.GetAllFeats(c, Context);
				if (fs == null)
				{
					await ReplyAsync(c.Name + " has no feats.");
					return;
				}
				await ReplyAsync("", fs);
				return;
			}

			var f = await SheetService.GetFeat(c,Name, Context);
			if (f == null)
			{
				await ReplyAsync(c.Name + " has no feat that start with that name.");
				return;
			}
			await ReplyAsync("", f);
			return;
		}
		
		[Command("Feature"), Alias("Features")] 
		[Summary("Lookup a feature on your active character or companion. Or list all of them if used with no arguments.")]
		public async Task getfeature([Remainder] string Name = null)
		{
			Character c = null;
			if (!Name.NullorEmpty() && Name.ToLower().EndsWith("-c"))
			{
				Name = Name.Replace("-c", "");
				c = GetCompanion();

				if (c == null)
				{
					await ReplyAsync("You have no active companion.");
					return;
				}
			}
			else
			{
				c = GetCharacter();

				if (c == null)
				{
					await ReplyAsync("You have no active character.");
					return;
				}
			}

			if (Name.NullorEmpty())
			{
				var fs = await SheetService.GetAllFeatures(c, Context);
				if (fs == null)
				{
					await ReplyAsync(c.Name + " has no features.");
					return;
				}
				await ReplyAsync("", fs);
				return;
			}

			var f = await SheetService.GetFeature(c, Name, Context);
			if (f == null)
			{
				await ReplyAsync(c.Name + " has no feature that start with that name.");
				return;
			}
			await ReplyAsync("", f);
			return;
		}

		[Command("Action"),Alias("Actions","Act","Acts","Activities")]
		[Summary("Lookup an action or activity on your active or companion. Or list of all of them if used with no arguments.")]
		public async Task GetAction([Remainder] string Name = null)
		{
			Character c = null;
			if (!Name.NullorEmpty() && Name.ToLower().EndsWith("-c"))
			{
				Name = Name.Replace("-c", "");
				c = GetCompanion();

				if (c == null)
				{
					await ReplyAsync("You have no active companion.");
					return;
				}
			}
			else
			{
				c = GetCharacter();

				if (c == null)
				{
					await ReplyAsync("You have no active character.");
					return;
				}
			}

			if (Name.NullorEmpty())
			{
				var fs = await SheetService.GetAllActions(c, Context);
				if (fs == null)
				{
					await ReplyAsync(c.Name + " has no Activities.");
					return;
				}
				await ReplyAsync("", fs);
				return;
			}

			var f = await SheetService.GetAction(c, Name, Context);
			if (f == null)
			{
				await ReplyAsync(c.Name + " has no activity that start with that name.");
				return;
			}
			await ReplyAsync("", f);
			return;
		}

		[Command("Inventory"), Alias("I","Item","Items")]
		[Summary("Lookup an item in your active character or companion's inventory. Or list all of them if used with no arguments.")]
		public async Task Inventory([Remainder] string Name = null)
		{
			Character c = null;
			if (!Name.NullorEmpty() && Name.ToLower().EndsWith("-c"))
			{
				Name = Name.Replace("-c", "");
				c = GetCompanion();

				if (c == null)
				{
					await ReplyAsync("You have no active companion.");
					return;
				}
			}
			else
			{
				c = GetCharacter();

				if (c == null)
				{
					await ReplyAsync("You have no active character.");
					return;
				}
			}

			if (Name.NullorEmpty())
			{
				var fs = await SheetService.Inventory(c, Context);
				if (fs == null)
				{
					await ReplyAsync(c.Name + " has no Items.");
					return;
				}
				await ReplyAsync("", fs);
				return;
			}

			var f = await SheetService.GetItem(c, Name, Context);
			if (f == null)
			{
				await ReplyAsync(c.Name + " has no Item that start with that name.");
				return;
			}
			await ReplyAsync("", f);
			return;
		}

		[Command("Spellbook"), Alias("Spells", "Spell")]
		[Summary("Lookup a spell your active character knows. Or list all of them if used with no arguments.")]
		public async Task Spellbook([Remainder] string Name = null)
		{
			var c = GetCharacter();

			if (c == null)
			{
				await ReplyAsync("You have no active character.");
				return;
			}

			if (Name.NullorEmpty())
			{
				var fs = await SheetService.GetAllSpells(c, Context);
				if (fs == null)
				{
					await ReplyAsync(c.Name + " has no Spellcasting classes.");
					return;
				}
				await ReplyAsync("", fs);
				return;
			}

			var f = await SheetService.GetSpell(c, Name, Context);
			if (f == null)
			{
				await ReplyAsync(c.Name + " knows no spell that start with that name.");
				return;
			}
			await ReplyAsync("", f);
			return;
		}

		
	}
}
