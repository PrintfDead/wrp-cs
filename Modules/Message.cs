using SampSharp.GameMode;
using SampSharp.GameMode.SAMP;
using WashingtonRP.Structures;

namespace WashingtonRP.Modules
{
    public static class Message
    {
        public static void PutChatAme(Player player, string text)
        {
            if (player.pCrack == false)
            {
                Color color = new Color(208, 174, 235);

                player.SendClientMessage(color, $"> {text}");
                player.SetChatBubble($"> {text}", color, (float)15.0, 1000);
            }
        }

        public static void SendMessageProx(float radius, Player player, string text, Color col1, Color col2, Color col3, Color col4, Color col5)
        {
            Vector3 position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z);

            foreach (Player i in Player.GetAll<Player>())
            {
                if (player.VirtualWorld != i.VirtualWorld) return;

                if (i.IsInRangeOfPoint(radius / 16, position))
                {
                    SendSplitMessage(i, col1, text);
                }
                else if (i.IsInRangeOfPoint(radius / 8, position))
                {
                    SendSplitMessage(i, col2, text);
                }
                else if (i.IsInRangeOfPoint(radius / 4, position))
                {
                    SendSplitMessage(i, col3, text);
                }
                else if (i.IsInRangeOfPoint(radius / 2, position))
                {
                    SendSplitMessage(i, col4, text);
                }
                else if (i.IsInRangeOfPoint(radius, position))
                {
                    SendSplitMessage(i, col5, text);
                }
            }
        }

        public static void SendSplitMessage(Player player, Color color, string text)
        {
            if (text.Length > 64)
            {
                string splitText1 = text.Substring(0, 64);
                string splitText2 = text.Substring(64 - 1, 64 * 2);

                player.SendClientMessage(color, "%s...", splitText1);
                player.SendClientMessage(color, "...%s", splitText2);
            }
            else
            {
                player.SendClientMessage(color, text);
            }
        }
    }
}
