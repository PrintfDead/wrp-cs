using MySql.Data.MySqlClient;
using System.Text;
using System.Text.RegularExpressions;
using WashingtonRP.Structures;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WashingtonRP.Modules.Utils
{
    public class Account
    {
        public static bool CheckEmail(string email)
        {
            WashingtonContext context = new WashingtonContext(new DbContextOptions<WashingtonContext>());

            var account = context.Accounts
                .Where(x => x.Email == email)
                .Select(x => x.ID)
                .SingleOrDefault();
            
            if (account == 0) return false;

            return true;
        }

        public static bool CheckUsername(string nick)
        {
            WashingtonContext context = new WashingtonContext(new DbContextOptions<WashingtonContext>());

            var account = context.Accounts
                .Where(x => x.Name == nick)
                .Select(x => x.ID)
                .SingleOrDefault();

            if (account == 0) return false;

            return true;
        }

        public static string HashingPassword(string pass)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;

            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }

            return hashString;
        }

        public static bool CheckPassword(string password, Player player)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }

            if (hashString == player.aPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int LoadAccount(string email, Player player)
        {
            WashingtonContext context = new WashingtonContext(new DbContextOptions<WashingtonContext>());

            var account = context.Accounts
                .Where(x => x.Email == email)
                .Select(x => x)
                .ToList();

            if (account == null) return 0;

            account.ForEach(account =>
            {
                player.aID = account.ID;
                player.aPassword = account.Password;
                player.aEmail = account.Email;
                player.aName = account.Name;
                player.aIP = account.Ip;
            });

            return 1;
        }

        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}
