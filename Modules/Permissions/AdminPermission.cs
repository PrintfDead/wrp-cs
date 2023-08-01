using SampSharp.GameMode.SAMP.Commands.PermissionCheckers;
using SampSharp.GameMode.World;
using WashingtonRP.Structures;

namespace WashingtonRP.Modules.Permissions
{
    public class HelperChecker : IPermissionChecker
    {
        public string Message
        {
            get
            {
                return "* No puedes usar este comando si no eres helper";
            }
        }

        public bool Check(BasePlayer playerBase)
        {
            Player player = playerBase as Player;

            if (player.pAdmin == Admin.Moderator || player.pAdmin == Admin.Admin || player.pAdmin == Admin.SeniorAdmin || player.pAdmin == Admin.Manager) return true;

            return player.pAdmin == Admin.Helper ? true : false;
        }
    }

    public class ModeratorChecker : IPermissionChecker
    {
        public string Message
        {
            get
            {
                return "* No puedes usar este comando si no eres moderador";
            }
        }

        public bool Check(BasePlayer playerBase)
        {
            Player player = playerBase as Player;

            if (player.pAdmin == Admin.Admin || player.pAdmin == Admin.SeniorAdmin || player.pAdmin == Admin.Manager) return true;

            return player.pAdmin == Admin.Moderator ? true : false;
        }
    }

    public class AdminChecker : IPermissionChecker
    {
        public string Message
        {
            get
            {
                return "* No puedes usar este comando si no eres admin";
            }
        }

        public bool Check(BasePlayer playerBase)
        {
            Player player = playerBase as Player;

            if (player.pAdmin == Admin.SeniorAdmin || player.pAdmin == Admin.Manager) return true;

            return player.pAdmin == Admin.Admin ? true : false;
        }
    }

    public class SeniorAdminChecker : IPermissionChecker
    {
        public string Message
        {
            get
            {
                return "* No puedes usar este comando si no eres senior admin";
            }
        }

        public bool Check(BasePlayer playerBase)
        {
            Player player = playerBase as Player;

            if (player.pAdmin == Admin.Manager) return true;

            return player.pAdmin == Admin.SeniorAdmin ? true : false;
        }
    }

    public class ManagerChecker : IPermissionChecker
    {
        public string Message
        {
            get
            {
                return "* No puedes usar este comando si no eres manager";
            }
        }

        public bool Check(BasePlayer playerBase)
        {
            Player player = playerBase as Player;

            return player.pAdmin == Admin.Manager ? true : false;
        }
    }
}
