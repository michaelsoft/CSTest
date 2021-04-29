using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public interface IBatchRuleRepository
{
    List<Company> GetCompanies();

    Task<Company> ProcessForACompany(Company company);
}


public class BatchRuleRepository : IBatchRuleRepository
{
    private Random random = new Random();

     public List<Company> GetCompanies()
     {
         var retVal = new List<Company>();

         for (var i=1; i<7; i++)
         {
              var company = new Company()
              {
                  CompanyID = i,
                  CompanyName = $"Company {i}" 
              };

              retVal.Add(company); 
         }

         return retVal;
     }

     public async Task<Company> ProcessForACompany(Company company)
     {
         var delaySecs = random.Next(1, 6);
         if (company.CompanyID == 1)
            delaySecs = 100;

         Utilities.OutputMsg($"Start processing company {company.CompanyID} - {company.CompanyName}, need {delaySecs} seconds ...");
         await Task.Delay(TimeSpan.FromSeconds(delaySecs));
         Utilities.OutputMsg($"Finished processing company {company.CompanyID} - {company.CompanyName} ...");
         return company;
     }
}