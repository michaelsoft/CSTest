using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

public class BatchRuleScheduler
{
    public async Task RunAsync()
    {
        var companies = await GetCompanies();
        var round = 1;
        var processingCompanies = new List<Company>();
        var maxProcessingNum = 5;

        while(true)
        {
            OutputMsg($"Round {round++}");

            foreach (var company in companies)
            {
                while (processingCompanies.Count >= maxProcessingNum)
                {
                    await Task.Delay(1000);
                }

                lock (processingCompanies)
                {
                    processingCompanies.Add(company);
                }

                var task = ProcessAsync(company);

                task.GetAwaiter().OnCompleted(() =>
                {
                    lock (processingCompanies)
                    {
                        processingCompanies.Remove(company);
                    }
                });

                
            }

            await Task.Delay(1000);
        }

    }

    private void OutputMsg(string msg)
    {
        Console.WriteLine(msg);
    }

    private  Task<List<Company>> GetCompanies()
    {
        var retVal = new List<Company>();
        for (var i = 1; i<20; i++)
        {
            var company = new Company {
                CompanyID = i,
                CompanyName = $"Company {i}"

            };
            retVal.Add(company);
        }

        return Task.FromResult<List<Company>>(retVal);
    }

    private async Task ProcessAsync(Company company)
    {
        OutputMsg($"Processing company {company.CompanyName}");
        var delaySecs = new Random().Next(10);
        await Task.Delay(delaySecs * 1000);
        OutputMsg($"Finished processing company {company.CompanyName}");
    }

}

public class Company 
{
    public int CompanyID {get; set;}

    public string CompanyName {get; set;}
}


