using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

public class BatchRuleScheduler
{
    public async Task RunAsync()
    {
        var round = 1;
        var repository = new BatchRuleRepository();
        var batchRuleManager = new BatchRuleManager(repository);

        while(true)
        {
            Utilities.OutputMsg($"Round {round}");
           
            await batchRuleManager.ProcessBatchRules();

            Utilities.OutputMsg($"Round {round} finished");

            round++;

            await Task.Delay(1000);

            //break;
        }

    }

}



