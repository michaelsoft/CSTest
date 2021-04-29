using System.Collections.Concurrent;
using System.Threading.Tasks;
using System;
using System.Linq;

public class BatchRuleManager
{
    private int maxProcessingNum = 3;

    private readonly IBatchRuleRepository repository;

    private ConcurrentDictionary<int, Company> processingCompanies = new ConcurrentDictionary<int, Company>();

    public BatchRuleManager(IBatchRuleRepository repository)
    {
        this.repository = repository;
    }

    public async Task ProcessBatchRules()
    {
        var companies = this.repository.GetCompanies();
        Utilities.OutputMsg($"Got {companies.Count} companies.");
        
        foreach (var company in companies)
        {
            while (this.processingCompanies.Count >= maxProcessingNum)
            {
                Utilities.OutputMsg($"Number of current processing companies excceeds the max value {maxProcessingNum}, wait seconds to process next company.");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            if (!this.processingCompanies.ContainsKey(company.CompanyID))
            {
                this.processingCompanies.TryAdd(company.CompanyID, company);
                Utilities.OutputMsg($"Added company '{company.CompanyName}' to processing list, current number {this.processingCompanies.Count}.");
            }
            else
            {
                Utilities.OutputMsg($"Company '{company.CompanyName}' is still processing, skipped.");
                continue;
            }

            var task = this.repository.ProcessForACompany(company);

            //Remove the finished company from current processing list 
            //Don't use "await" here, because that way it will wait for all the companies within the batch to finish.
            //But we want once a company finishes, than next company can be processed.
            task.GetAwaiter().OnCompleted(() =>
            {
                this.processingCompanies.TryRemove(task.Result.CompanyID, out Company finishedCompany);
                Utilities.OutputMsg($"Removed company '{task.Result.CompanyName}' from processing list, current number {this.processingCompanies.Count}.");
            });

            
        }
    }
}