using Quartz;
using Quartz.Impl;

namespace lr_fifteen.Services
{
    public class EmailScheduler
    {
        public async Task Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailSender>().Build();

            ITrigger trigger = TriggerBuilder.Create().StartNow().WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever()).Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
