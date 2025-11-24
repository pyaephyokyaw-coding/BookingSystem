using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.Data;

namespace BCT.BusinessRule.Services.HangfireServices;

public class NotificationJobService(BookingSystemDbContext dbContext, HangfireEmailService hangfireEmailService)
{
    public void SendNotificationsForOldUsers()
    {
        var users = dbContext.Users
            .Where(x => x.CreatedAt <= DateTime.Now.AddMinutes(-5))
            .ToList();

        var validExpiredData = (
            from u in dbContext.Users
            join a in dbContext.AuditLogs
                on u.UserId equals a.UserId into auditGroup
            from a in auditGroup.DefaultIfEmpty()
            where u.CreatedAt <= DateTime.Now.AddMonths(-1)
                  && a == null
            select u
        ).ToList();

        foreach (var user in validExpiredData)
        {
            AuditLogModel log = new AuditLogModel
            {
                UserId = user.UserId,
                Action = $"User : {user.FullName} will be expired soon.",
                LogDate = DateTime.Now
            };

            dbContext.AuditLogs.Add(log);
            dbContext.SaveChanges();

            //hangfireEmailService.SendEmail(
            //    user.Email,
            //    "Reminder",
            //    "Your account is older than 7 days."
            //);
        }
    }
}
