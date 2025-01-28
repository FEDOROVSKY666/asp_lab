using lr_fifteen.Models;
using Quartz;

namespace lr_fifteen.DB
{
    public class DBEmailService
    {
        private readonly ApplicationContext _context;
        private readonly IScheduler _scheduler;

        public DBEmailService(ApplicationContext context, IScheduler scheduler)
        {
            _context = context;
            _scheduler = scheduler;
        }

        public async Task AddNewUser(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            IJobDetail job = JobBuilder.Create<DBEmailSender>().Build();

            ITrigger trigger = TriggerBuilder.Create().StartNow().Build();

            await _scheduler.ScheduleJob(job, trigger);
        }
    }
}
