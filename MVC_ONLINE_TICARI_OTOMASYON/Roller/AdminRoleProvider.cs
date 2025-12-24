using System;
using System.Collections.Generic;
using System.Linq;
using MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar;

using System.Web;
using System.Web.Security;

namespace MVC_ONLINE_TICARI_OTOMASYON.Roller
{
    public class AdminRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            Context c = new Context();
            var k = c.Admins.FirstOrDefault(x => x.KullaniciAd == username); //Yetkilendirme işlemi için
            
            if (k == null || string.IsNullOrEmpty(k.Yetki))
            {
                return new string[] { }; // Kullanıcı bulunamadıysa boş array dön
            }
            
            return new string[] { k.Yetki };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            Context c = new Context();
            var users = c.Admins.Where(x => x.Yetki == roleName).Select(x => x.KullaniciAd).ToArray();
            return users;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            Context c = new Context();
            var k = c.Admins.FirstOrDefault(x => x.KullaniciAd == username);
            
            if (k == null)
            {
                return false;
            }
            
            return k.Yetki == roleName;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}