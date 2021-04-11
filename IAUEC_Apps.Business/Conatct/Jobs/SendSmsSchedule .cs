using Quartz;
using Quartz.Impl;


namespace IAUEC_Apps.Business.Conatct.Jobs
{
    //public class SendSmsSchedule: ISchedule
    //{
    //    public void Run()
    //    {

    //        IJobDetail job = JobBuilder.Create<SendSmsJob>()
    //                                   .WithIdentity("job2")
    //                                   .Build();
   
    //        ITrigger trigger = TriggerBuilder.Create()
    //                                         .WithIdentity("trigger1", "group1")
    //                                         .StartAt(DateBuilder.DateOf(8,0,0))
    //                                         .WithSimpleSchedule(x => x.WithIntervalInHours(24).RepeatForever())
    //                                         .Build();

    //        ISchedulerFactory sf = new StdSchedulerFactory();
    //        IScheduler sc = sf.GetScheduler();
    //        sc.ScheduleJob(job, trigger);
    //        sc.Start();
    //    }

    //}
}
