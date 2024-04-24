using Microsoft.EntityFrameworkCore;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<ApplicationUser> GetUsersByClaim(string claimType, string claimValue)
        {
            var allClaims = db.UserClaims.Where(c => c.ClaimType == claimType && c.ClaimValue == claimValue).ToList();

            List<ApplicationUser> allUsers = new List<ApplicationUser>();

            foreach (var claim in allClaims)
            {
                ApplicationUser? user = db.Users.Include(u => u.Company).Include(u => u.Companies).FirstOrDefault(u => u.Id == claim.UserId && u.EmailConfirmed);

                if (user is not null)
                {
                    allUsers.Add(user);
                }
            }
            return allUsers;
        }

        public List<ApplicationUser> GetEmployeeesByManagerId(string ManagerId)
        {
            return db.Users.Where(e => e.Company.ManagerId == ManagerId).Include(u => u.Company).ToList();
        }

        public ApplicationUser GetUserById(string UserId)
        {
            var user = db.Users.FirstOrDefault(e => e.Id == UserId);

            if (user is not null)
            {
                return user;
            }
            else
            {
                return new ApplicationUser();
            }
        }

        public void DeleteUser(string Id)
        {
            var user = db.Users.Find(Id);
            if (user is not null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
    }
}
