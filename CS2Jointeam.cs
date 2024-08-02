using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Logging;
using CSSharpUtils.Extensions;
using CounterStrikeSharp.API.Modules.Utils;

namespace CS2Jointeam;

public class CS2Jointeam : BasePlugin, IPluginConfig<CS2JointeamConfig>
{
    public override string ModuleName => "CS2 Jointeam";
    public override string ModuleDescription => "Basic plugin to join different teams";
    public override string ModuleAuthor => "verneri";
    public override string ModuleVersion => "1.0.0";

    public CS2JointeamConfig Config { get; set; } = new();

    public void OnConfigParsed(CS2JointeamConfig config)
	{
        Config = config;
    }

    public override void Load(bool hotReload)
    {
        Logger.LogInformation($"[{ModuleName}] Loaded (version {ModuleVersion})");
        AddCommand($"{Config.Joinctcommand}", "Join CT team", OnCommandct);
        AddCommand($"{Config.Jointerroristcommand}", "Join T team", OnCommandterrorist);
        AddCommand($"{Config.Joinspectate}", "Join spec", OnCommandspectate);
    }

    public void OnCommandct(CCSPlayerController? playerController, CommandInfo command)
    {

        if (playerController.TeamNum == 3)
        {
            command.ReplyToCommand($"{Localizer["already.inteam", CsTeam.CounterTerrorist]}");
            return;
        }

        if (playerController.PawnIsAlive)
        {
            playerController.CommitSuicide(true, false);
            playerController.MoveToTeam(CsTeam.CounterTerrorist);
            command.ReplyToCommand($"{Localizer["moved.ct", CsTeam.CounterTerrorist]}");
        }else
        {
            playerController.MoveToTeam(CsTeam.CounterTerrorist);
            command.ReplyToCommand($"{Localizer["moved.ct", CsTeam.CounterTerrorist]}");
        }
    }

    public void OnCommandterrorist(CCSPlayerController? playerController, CommandInfo command)
    {
        if (playerController.TeamNum == 2)
        {
            command.ReplyToCommand($"{Localizer["already.inteam", CsTeam.Terrorist]}");
            return;
        }

        if (playerController.PawnIsAlive)
        {
            playerController.CommitSuicide(true, false);
            playerController.MoveToTeam(CsTeam.Terrorist);
            command.ReplyToCommand($"{Localizer["moved.t", CsTeam.Terrorist]}");
        }else
        {
            playerController.MoveToTeam(CsTeam.Terrorist);
            command.ReplyToCommand($"{Localizer["moved.t", CsTeam.Terrorist]}");
        }
    }

    public void OnCommandspectate(CCSPlayerController? playerController, CommandInfo command)
    {
        if (playerController.TeamNum == 1)
        {
            command.ReplyToCommand($"{Localizer["already.inteam", CsTeam.Spectator]}");
            return;
        }

        if (playerController.PawnIsAlive)
        {
            playerController.CommitSuicide(false, true);
            playerController.MoveToTeam(CsTeam.Spectator);
            command.ReplyToCommand($"{Localizer["moved.spec", CsTeam.Spectator]}");
        }else
        {
            playerController.MoveToTeam(CsTeam.Spectator);
            command.ReplyToCommand($"{Localizer["moved.spec", CsTeam.CounterTerrorist]}");
        }
    }
}