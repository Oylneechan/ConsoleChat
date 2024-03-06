using System.Text.RegularExpressions;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;

namespace ConsoleChat
{
    public class ConsoleChat : BasePlugin
    {
        public override string ModuleName => "Console Chat";
        public override string ModuleVersion => "1.0";
        public override string ModuleAuthor => "Oylsister";

        public List<string> BlackList = new List<string> { "recharge", "recast", "cooldown", "cool" };

        public override void Load(bool hotReload)
        {
            AddCommandListener("say", OnSayTest, HookMode.Pre);


        }

        public HookResult OnSayTest(CBaseEntity? client, CommandInfo info)
        {
            if (client == null)
            {
                Server.PrintToChatAll($" {ChatColors.Red}CONSOLE:{ChatColors.Green} {info.ArgString}");
                return HookResult.Handled;
            }

            return HookResult.Continue;
        }

        bool CheckString(string str)
        {
            foreach (var blackword in BlackList)
            {
                if (str.Contains(blackword, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public bool IsCountable(string message)
        {
            string FilterText = "";
            int filterPos = 0;
            int ConsoleNumber;

            bool isCountable = false;

            for (int i = 0; i < message.Length; i++)
            {
                if (IsCharAlpha(message[i]) || IsCharNumeric(message[i]) || IsCharSpace(message[i]))
                {
                    FilterText += message[i];
                    filterPos += 1;
                }
            }

            FilterText += '\0';
            FilterText = Regex.Replace(FilterText, @"\s", "");

            if (CheckString(message))
                return false;



            return false;
        }

        public bool IsCharAlpha(char array)
        {
            return array <= sbyte.MaxValue;
        }

        public bool IsCharNumeric(char array)
        {
            return char.IsNumber(array);
        }

        public bool IsCharSpace(char array)
        {
            return char.IsWhiteSpace(array);
        }
    }
}
